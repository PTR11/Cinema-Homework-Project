using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CinemaHW.Persistence;
using CinemaHW.Persistence.Services;
using Microsoft.AspNetCore.Identity;

namespace CinemaHW.Web.Controllers
{
    public class RentsController : Controller
    {
        private readonly ICinemaHWService _service;
        private readonly UserManager<Users> _userManager;

        public RentsController(ICinemaHWService service, UserManager<Users> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        // GET: Rents
        [HttpGet]
        public async Task<ActionResult> Index(int pId)
        {
            RentViewModel rvm = _service.NewRent(pId);
            if (rvm == null) // ha nem sikerül (nem volt azonosító)
                return RedirectToAction("Index", "Home"); // visszairányítjuk a főoldalra
            if (User.Identity.IsAuthenticated)
            {
                Users user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user != null)
                {
                    rvm.UserFullName = user.FullName;
                    rvm.UserPhoneNumber = user.PhoneNumber;
                }
            }
            return View(rvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(int id,RentViewModel rvm)
        {
            rvm.Row = _service.GetLineSizeOfRoom(id);

            rvm.Col = _service.GetColSizeOfRoom(id);

            int count = 0;
            rvm.Program = _service.FindProgramById(id);

            if (!ModelState.IsValid)
            {
                rvm = _service.NewRent(id);
                return View("Index", rvm);
            }
            Users user;
            if (User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(User.Identity.Name);
            }
            else
            {
                if(rvm.UserFullName == "" || rvm.UserPhoneNumber == "")
                {
                    ModelState.AddModelError("", "Nem lehetnek üresek az input mezők");
                    rvm = _service.NewRent(id);
                    return View("Index", rvm);
                }
                user = new Users
                {
                    UserName = "user" + Guid.NewGuid(),
                    FullName = rvm.UserFullName,
                    PhoneNumber = rvm.UserPhoneNumber
                };
                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Sikertelen foglalás");
                    rvm = _service.NewRent(id);
                    return View("Index", rvm);
                }
            }
            List<Places> newReservation = new List<Places>();
            for (int i = 0; i < rvm.Row; i++)
            {
                for (int j = 0; j < rvm.Col; j++)
                {
                    String tmp = "ulesek[" + i + j + "]";
                    if (Request.Form[tmp].Count == 2)
                    {
                        newReservation.Add(new Places
                        {
                            Line = i,
                            Column = j
                        });
                        count++;
                    }
                }
            }
            if(newReservation.Count == 0)
            {
                ModelState.AddModelError("", "Nem kertül ülőhely kiválasztásra");
                rvm = _service.NewRent(id);
                return View("Index", rvm);
            }
            if (!await _service.SaveRentAsync(id, user.UserName, newReservation, rvm))
            {
                ModelState.AddModelError("", "A foglalás rögzítése sikertelen, kérem próbálja újra!");
                rvm = _service.NewRent(id);
                return View("Index", rvm);
            }
            rvm = _service.NewRent(id);
            return View("Result",rvm);
        }
    }
}
