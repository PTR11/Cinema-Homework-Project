using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CinemaHW.Persistence;
using CinemaHW.Persistence.Services;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace CinemaHW.Web.Controllers
{
    //[Authorize(Roles ="Admin")] Bejelentkezést igényel
    //AllowAnonymous nem kell bejelentkezés hozzá
    public class MoviesController : Controller
    {
        private readonly ICinemaHWService _service;

        public MoviesController(ICinemaHWService service)
        {
            _service = service;
        }


       // GET: Movies/Details/5
        public ActionResult Details(int id)
        {

            MovieViewModel mvm = new MovieViewModel();
            mvm.movie = _service.GetMovieById(id);
            mvm.program = _service.GetProgramByMovieId(id, DateTime.Now);
            

            return View(mvm);
        }

        public FileResult ImageForMovies(int Id)
        {
            // lekérjük az épület első tárolt képét (kicsiben)
            
            Byte[] imageContent = _service.GetMovieImage(Id);
            if (imageContent == null)
            {
                return File("~/images/NoImage.jpg", "image/jpg");
            }
            return File(imageContent, "image/jpg");
        }
    }
}
