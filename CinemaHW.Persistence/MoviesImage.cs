using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaHW.Persistence
{
    public class MoviesImage
    {
        [Key]
        public Int32 Id { get; set; }
        public Int32 MovieId { get; set; }
        public virtual Movie Movie { get; set; }

        public byte[] Image { get; set; }

    }
}
