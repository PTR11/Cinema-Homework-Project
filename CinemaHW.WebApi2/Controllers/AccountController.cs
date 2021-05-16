using CinemaHW.Persistence;
using CinemaHW.Persistence.DTO;
using CinemaHW.Persistence.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaHW.WebApi2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private SignInManager<Users> _signInManager;
        private readonly ICinemaHWService _service;

        public AccountController(SignInManager<Users> signInManager, ICinemaHWService service)
        {
            _signInManager = signInManager;
            _service = service;
        }
        //api/Account/Login
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password,false, false);
            if (result.Succeeded)
            {
                return Ok();
            }
            return Unauthorized("Login failed");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }


    }

}
