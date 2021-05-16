using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace CinemaHW.Persistence.DTO
{
    public class RentDto
    {
        public Int32 Id { get; set; }
        public String UserId { get; set; }
        public Int32 ProgramId { get; set; }
        
        public ICollection<PlaceDto> Places { get; set; }

        public static explicit operator Rent(RentDto dto) => new Rent
        {
            Id = dto.Id,
            UserId = dto.UserId,
            ProgramId = dto.ProgramId,
            RentPlaces = dto.Places.Select(list => (Places)list).ToList()
        };

        public static explicit operator RentDto(Rent m) => new RentDto
        {
            Id = m.Id,
            UserId = m.UserId,
            ProgramId = m.ProgramId,
            Places = m.RentPlaces.Select(list => (PlaceDto)list).ToList()
        };

    }
}
