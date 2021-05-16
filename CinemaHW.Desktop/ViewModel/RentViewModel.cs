using CinemaHW.Persistence;
using CinemaHW.Persistence.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaHW.Desktop.ViewModel
{
    public class RentViewModel : ViewModelBase
    {
        private Int32 _id;
        private String _userId;
        private Int32 _programId;
        public Int32 Id { get; set; }
        public String UserId { get; set; }
        public Int32 ProgramId { get; set; }

        public ICollection<PlaceDto> Places { get; set; }

        public RentViewModel ShallowClone()
        {
            return (RentViewModel)this.MemberwiseClone();
        }
        public void CopyFrom(RentViewModel rhs)
        {
            Id = rhs.Id;
            UserId = rhs.UserId;
            ProgramId = rhs.ProgramId;
            Places = rhs.Places;
        }

        public static explicit operator RentViewModel(RentDto dto) => new RentViewModel
        {
            Id = dto.Id,
            UserId = dto.UserId,
            ProgramId = dto.ProgramId,
            Places = dto.Places
        };

        public static explicit operator RentDto(RentViewModel m) => new RentDto
        {
            Id = m.Id,
            UserId = m.UserId,
            ProgramId = m.ProgramId,
            Places = m.Places,
        };
    }
}
