using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaHW.Persistence
{
    public class Room
    {
        [Key]
        public Int32 Id { get; set; }
        public Int32 Line { get; set; }
        public Int32 Column { get; set; }

        public virtual ICollection<Programs> RoomPrograms { get; set; }
    }
}
