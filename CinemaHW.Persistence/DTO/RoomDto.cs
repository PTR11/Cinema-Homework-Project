using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaHW.Persistence.DTO
{
    public class RoomDto
    {
        public Int32 Id { get; set; }
        public Int32 Line { get; set; }
        public Int32 Column { get; set; }

        public static explicit operator Room(RoomDto dto) => new Room
        {
            Id = dto.Id,
            Line = dto.Line,
            Column = dto.Column
        };

        public static explicit operator RoomDto(Room a) => new RoomDto
        {
            Id = a.Id,
            Line = a.Line,
            Column = a.Column
        };
    }
}
