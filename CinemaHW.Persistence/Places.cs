using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CinemaHW.Persistence
{
    public enum Status
    {
        Reserved, Sold
    }

    public class Places
    {
        [Key]
        public Int32 Id { get; set; }
        public Int32 Line { get; set; }
        public Int32 Column { get; set; }

        public Int32 Status { get; set; }

        public Int32 RentId { get; set; }
        [Required]
        public virtual Rent Rent { get; set; }

        
        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Places p = (Places)obj;
                return (Line == p.Line) && (Column == p.Column);
            }
        }
    }
}
