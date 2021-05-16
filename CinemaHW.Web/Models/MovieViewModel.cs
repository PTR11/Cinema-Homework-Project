using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaHW.Persistence
{
    public class MovieViewModel
    {
        public Movie movie { get; set; }

        public List<Programs> program { get; set; }
    }
}
