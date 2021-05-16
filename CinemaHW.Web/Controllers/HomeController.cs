using CinemaHW.Persistence;
using CinemaHW.Persistence.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaHW.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICinemaHWService _service;

        public HomeController(ILogger<HomeController> logger, ICinemaHWService service)
        {
            _logger = logger;
            _service = service;
        }

        public IActionResult Index()
        {
            ViewModel vm = new ViewModel();
            vm.lastUploadedMovies = _service.GetLastUploadedMovies();
            vm.todaysProgram = _service.GetMoviesByDate(DateTime.Now);
            return View("Index",vm);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
