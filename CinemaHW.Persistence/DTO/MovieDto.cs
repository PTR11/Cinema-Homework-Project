using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaHW.Persistence.DTO
{
    public class MovieDto
    {
        public Int32 Id { get; set; }

        public String Title { get; set; }

        public String Director { get; set; }

        //public virtual ICollection<Actors> Actors { get; set; }

        public Int32 Length { get; set; }

        public String Description { get; set; }

        public DateTime UploadTime { get; set; }

        public static explicit operator Movie(MovieDto dto) => new Movie
        {
            Id = dto.Id,
            Title = dto.Title,
            Director = dto.Director,
            Length = dto.Length,
            UploadTime = dto.UploadTime,
            Description = dto.Description
        };

        public static explicit operator MovieDto(Movie m) => new MovieDto
        {
            Id = m.Id,
            Title = m.Title,
            Director = m.Director,
            Length = m.Length,
            UploadTime = m.UploadTime,
            Description = m.Description
        };
    }
}
