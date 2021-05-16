using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaHW.Persistence.DTO
{
    public class PlaceDto
    {
        public Int32 Id { get; set; }
        public Int32 Line { get; set; }
        public Int32 Column { get; set; }
        public Int32 Status { get; set; }
        public Int32 RentId { get; set; }


        public static explicit operator Places(PlaceDto dto) => new Places
        {
            Id = dto.Id,
            Line = dto.Line,
            Column = dto.Column,
            Status = dto.Status,
            RentId = dto.RentId,
            Rent = new Rent()
            {
                Id = 0,
                UserId = "",
                ProgramId = 0,
            }
        };

        public static explicit operator PlaceDto(Places m)
        {
            return new PlaceDto
            {
                Id = m.Id,
                Line = m.Line,
                Column = m.Column,
                Status = m.Status,
                RentId = m.RentId
            };
        }

        public override bool Equals(object obj)
        {
            return this.Line == ((PlaceDto)obj).Line && this.Column == ((PlaceDto)obj).Column && this.Status == ((PlaceDto)obj).Status;
        }
    }
}
