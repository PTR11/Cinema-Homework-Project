using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaHW.Persistence.DTO
{
    public class ProgramDto
    {
        public Int32 Id { get; set; }
        public DateTime Date { get; set; }
        public Int32 RoomId { get; set; }
        public Int32 MovieId { get; set; }

        public static explicit operator Programs(ProgramDto dto) => new Programs
        {
            Id = dto.Id,
            Date = dto.Date,
            RoomId = dto.RoomId,
            MovieId = dto.MovieId
        };

        public static explicit operator ProgramDto(Programs p) => new ProgramDto
        {
            Id = p.Id,
            Date = p.Date,
            RoomId = p.RoomId,
            MovieId = p.MovieId
        };
    }
}
