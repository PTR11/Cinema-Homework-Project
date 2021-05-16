using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaHW.Persistence.DTO
{
    public class ActorDto
    {
        public Int32 Id { get; set; }

        public String Name { get; set; }

        public Int32 MovieId { get; set; }

        public static explicit operator Actors(ActorDto dto) => new Actors
        {
            Id = dto.Id,
            Name = dto.Name,
            MovieId = dto.MovieId
        };

        public static explicit operator ActorDto(Actors a) => new ActorDto
        {
            Id = a.Id,
            Name = a.Name,
            MovieId = a.MovieId
        };
    }
}
