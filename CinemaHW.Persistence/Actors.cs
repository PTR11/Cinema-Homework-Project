using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaHW.Persistence
{
    public class Actors
    {
        [Key]
        public Int32 Id { get; set; }

        public String Name { get; set; }

        public Int32 MovieId { get; set; }
        [Required]
        public virtual Movie Movie { get; set; }
    }
}
