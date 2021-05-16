using CinemaHW.Persistence;
using CinemaHW.Persistence.DTO;
using CinemaHW.Persistence.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaHW.WebApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentsController : ControllerBase
    {
        private readonly ICinemaHWService _service;
        private readonly UserManager<Users> _userManager;

        public RentsController(ICinemaHWService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RentDto>> GetRents()
        {
            return _service.GetRents().Select(list => (RentDto)list).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<RentDto>> GetRentsByProgramId(int id)
        {
            return _service.GetRentsById(id).Select(list => (RentDto)list).ToList();
        }

        [HttpGet("GetRent/{id}")]
        public ActionResult<RentDto> GetRentById(int id)
        {
            try
            {
                return (RentDto)_service.GetRentById(id);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "administrator")]
        [HttpPost]
        public async Task<ActionResult<Rent>> PostRentAsync(RentDto rent)
        {
            if (User.Identity.IsAuthenticated)
            {

                Users user = _service.GetUserByName(User.Identity.Name);
                rent.UserId = user.Id;
                Rent m = await _service.CreateRent((Rent)rent);

                if (m == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                else
                {
                    _service.RefreshRentsList();
                    return CreatedAtAction(nameof(GetRentById), new { id = m.Id }, (RentDto)m);
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError);

        }

    }
}
