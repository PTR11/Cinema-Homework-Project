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
    public class ProgramsController : ControllerBase
    {
        private readonly ICinemaHWService _service;

        public ProgramsController(ICinemaHWService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProgramDto>> GetPrograms()
        {
            return _service.GetPrograms().Select(list => (ProgramDto)list).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<ProgramDto> GetProgram(int id)
        {
            try
            {
                return (ProgramDto)_service.GetProgramById(id);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "administrator")]
        [HttpPut("{id}")]
        public IActionResult PutProgram(int id, ProgramDto program)
        {

            if (id != program.Id)
            {
                return BadRequest();
            }
            try
            {
                _service.IsRoomFree((Programs)program);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status405MethodNotAllowed);
            }


            if (_service.UpdateProgram((Programs)program))
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
        public ActionResult<Programs> PostProgram(ProgramDto program)
        {
            


            var m = _service.CreateProgram((Programs)program);


            if (m == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            else
            {
                return CreatedAtAction(nameof(GetProgram), new { id = m.Id }, m);
            }
        }
        [Authorize(Roles = "administrator")]
        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            if (_service.DeleteProgram(id))
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
