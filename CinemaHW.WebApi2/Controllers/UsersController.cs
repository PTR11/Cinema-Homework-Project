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
    public class UsersController : ControllerBase
    {

        private readonly ICinemaHWService _service;

        public UsersController(ICinemaHWService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public ActionResult<UserDto> GetUserById(String id)
        {
            Users result = _service.GetUserById(id);
            return (UserDto)result;
        }
       
    }
}
