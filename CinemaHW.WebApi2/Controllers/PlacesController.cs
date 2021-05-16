using CinemaHW.Persistence;
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
    public class PlacesController : ControllerBase
    {
        private readonly ICinemaHWService _service;

        public PlacesController(ICinemaHWService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<PlaceDto>> GetPlacesByRentId(int id)
        {
            var tmp =_service.GetReservedPlacesByRentId(id).Select(list => (PlaceDto)list).ToList();
            if(tmp.Count == 0)
            {
                return NotFound();
            }
            return tmp; 
        }
    }
}
