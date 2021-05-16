using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaHW.Persistence
{
    public class Movie
    {
        [Key]
        public Int32 Id { get; set; }

        public String Title { get; set; }

        public String Director { get; set; }

        public virtual ICollection<Actors> Actors { get; set; }

        public Int32 Length { get; set; }
        
        [DataType(DataType.MultilineText)]
        public String Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime UploadTime { get; set; }

    }
}
