using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CinemaHW.Persistence
{
    public class Rent
    {
        [Key]
        public Int32 Id { get; set; }
        public String UserId { get; set; }

        [Required]
        public virtual Users User { get; set; }

        public Int32 ProgramId { get; set; }
        [Required]
        public virtual Programs Program { get; set; }
        public virtual ICollection<Places> RentPlaces { get; set; }

        
    }
}
