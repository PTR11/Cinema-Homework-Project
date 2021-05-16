using CinemaHW.Persistence.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaHW.Desktop.ViewModel
{
    public class ActorViewModel : ViewModelBase
    {
        private Int32 _id;
        private String _name;
        private Int32 _movieId;
        public Int32 Id { get; set; }

        public String Name { get; set; }

        public Int32 MovieId { get; set; }

        public ActorViewModel ShallowClone()
        {
            return (ActorViewModel)this.MemberwiseClone();
        }

        public void CopyFrom(ActorViewModel rhs)
        {
            Id = rhs.Id;
            Name = rhs.Name;
            MovieId = rhs.MovieId;
        }

        public static explicit operator ActorViewModel(ActorDto dto) => new ActorViewModel
        {
            Id = dto.Id,
            Name = dto.Name,
            MovieId = dto.MovieId
        };

        public static explicit operator ActorDto(ActorViewModel vm) => new ActorDto
        {
            Id = vm.Id,
            Name = vm.Name,
            MovieId = vm.MovieId
        };
    }
}
