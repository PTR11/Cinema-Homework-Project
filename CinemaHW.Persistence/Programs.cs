using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaHW.Persistence
{
    public class Programs
    {
        public Int32 Id { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        public Int32 RoomId { get; set; }
        [Required]
        public virtual Room Room { get; set; }
        public Int32 MovieId { get; set; }
        [Required]
        public virtual Movie Movie { get; set; }
    }
}
