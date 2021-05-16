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
    public class ActorsController : ControllerBase
    {
        private readonly ICinemaHWService _service;

        public ActorsController(ICinemaHWService service)
        {
            _service = service;
        }

        // GET: api/Actors/5
        [HttpGet("{movieId}")]
        public ActionResult<IEnumerable<ActorDto>> GetActors(int movieId)
        {
            return _service.GetActorsByMovie(movieId).Select(list => (ActorDto)list).ToList();
        }

        [HttpGet("ActorId/{id}")]
        public ActionResult<ActorDto> GetActor(int id)
        {
            try
            {
                return (ActorDto)_service.GetActor(id);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "administrator")]
        [HttpPut("{id}")]
        public IActionResult PutActor(int id, ActorDto actor)
        {
            if (id != actor.Id)
            {
                return BadRequest();
            }

            if (_service.UpdateActor((Actors)actor))
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [Authorize(Roles = "administrator")]
        [HttpPost]
        public ActionResult<ActorDto> PostActor(ActorDto actor)
        {
            var m = _service.CreateActor((Actors)actor);
            if (m == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            else
            {
                return CreatedAtAction(nameof(GetActor), new { id = m.Id }, (ActorDto) m);
            }
        }
        [Authorize(Roles = "administrator")]
        [HttpDelete("{id}")]
        public IActionResult DeleteActor(int id)
        {
            if (_service.DeleteActor(id))
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
