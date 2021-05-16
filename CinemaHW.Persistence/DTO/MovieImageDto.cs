using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaHW.Persistence.DTO
{
    public class MovieImageDto
    {
        public Int32 Id { get; set; }
        public Int32 MovieId { get; set; }
        public byte[] Image { get; set; }

        public static explicit operator MoviesImage(MovieImageDto dto) => new MoviesImage
        {
            Id = dto.Id,
            MovieId = dto.MovieId,
            Image = dto.Image
        };

        public static explicit operator MovieImageDto(MoviesImage m) => new MovieImageDto
        {
            Id = m.Id,
            MovieId = m.MovieId,
            Image = m.Image
        };
    }
}
