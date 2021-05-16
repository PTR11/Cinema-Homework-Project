using CinemaHW.Persistence.DTO;
using CinemaHW.Persistence.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaHW.WebApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly ICinemaHWService _service;

        public RoomController(ICinemaHWService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RoomDto>> GetMovies()
        {
            return _service.GetRooms().Select(list => (RoomDto)list).ToList();
        }
    }
}
