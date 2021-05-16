using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaHW.Persistence
{
    public class ViewModel
    {
        public List<Movie> lastUploadedMovies { get; set; }

        public List<Movie> todaysProgram { get; set; }
    }
}
