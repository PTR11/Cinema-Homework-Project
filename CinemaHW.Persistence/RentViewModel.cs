using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaHW.Persistence
{
    public class RentViewModel : UserViewModel
    {
        public Int32 Row { get; set; }
        public Int32 Col { get; set; }
        public Programs Program { get; set; }
        public List<Places> ReservedPlaces { get; set; }

    }
}
