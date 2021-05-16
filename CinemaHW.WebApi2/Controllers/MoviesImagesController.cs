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

namespace CinemaHW.WebApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesImagesController : ControllerBase
    {
        private readonly ICinemaHWService _service;

        public MoviesImagesController(ICinemaHWService service)
        {
            _service = service;
        }

        // GET: api/MoviesImages
        [HttpGet]
        public  ActionResult<IEnumerable<MovieImageDto>> GetMoviesImages()
        {
            return _service.GetImages().Select(list => (MovieImageDto)list).ToList();
        }

        // GET: api/MoviesImages/5
        [HttpGet("{id}")]
        public ActionResult<MovieImageDto> GetMovie(int movieId)
        {
            try
            {
                return (MovieImageDto)_service.GetImageByMovieId(movieId);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        // PUT: api/MoviesImages/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize(Roles = "administrator")]
        [HttpPut("{id}")]
        public IActionResult PutImage(int id, MovieImageDto image)
        {
            if (id != image.Id)
            {
                return BadRequest();
            }

            if (_service.UpdateImage((MoviesImage)image))
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        // POST: api/MoviesImages
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize(Roles = "administrator")]
        [HttpPost]
        public ActionResult<MoviesImage> PostImage(MovieImageDto movie)
        {
            var m = _service.CreateImage((MoviesImage)movie);
            if (m == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            else
            {
                return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
            }
        }

        // DELETE: api/MoviesImages/5
        [Authorize(Roles = "administrator")]
        [HttpDelete("{id}")]
        public IActionResult DeleteImage(int id)
        {
            if (_service.DeleteImage(id))
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
