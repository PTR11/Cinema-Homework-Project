using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CinemaHW.Persistence;
using CinemaHW.Persistence.Services;
using CinemaHW.Persistence.DTO;
using Microsoft.AspNetCore.Authorization;

namespace CinemaHW.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ICinemaHWService _service;

        public MoviesController(ICinemaHWService service)
        {
            _service = service;
        }

        // GET: api/Movies
        [HttpGet]
        public ActionResult<IEnumerable<MovieDto>> GetMovies()
        {
            return _service.GetMovies().Select(list => (MovieDto)list).ToList();
        }


        [HttpGet("{id}")]
        public ActionResult<MovieDto> GetMovie(int id)
        {
            try
            {
                return (MovieDto)_service.GetMovieById(id);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }


        // PUT: api/Movies/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize(Roles = "administrator")]
        [HttpPut("{id}")]
        public IActionResult PutMovie(int id, MovieDto movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            if (_service.UpdateMovie((Movie)movie))
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }

        // POST: api/Movies
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize(Roles = "administrator")]
        [HttpPost]
        public ActionResult<Movie> PostMovie(MovieDto movie)
        {
            var m = _service.CreateMovie((Movie)movie);
            if(m == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            else
            {
                return CreatedAtAction(nameof(GetMovie), new { id = m.Id }, m);
            }
        }

        // DELETE: api/Movies/5
        [Authorize(Roles = "administrator")]
        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            if (_service.DeleteMovie(id))
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
