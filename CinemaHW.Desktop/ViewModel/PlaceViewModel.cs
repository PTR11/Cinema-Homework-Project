using CinemaHW.Persistence.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaHW.Desktop.ViewModel
{
    public class PlaceViewModel
    {
        private Int32 _id;
        private Int32 _line;
        private Int32 _column;
        private Int32 _status;
        private String _statusName;
        private Int32 _rentId;
        public Int32 Id { get; set; }
        public Int32 Line { get; set; }
        public Int32 Column { get; set; }
        public Int32 Status { get; set; }
        public String StatusName { get; set; }


        public DelegateCommand AddSellingListCommand { get; set; }

        public void CopyFrom(PlaceViewModel rhs)
        {
            Id = rhs.Id;
            Line = rhs.Line;
            Column = rhs.Column;
            Status = rhs.Status;
        }

        public static explicit operator PlaceViewModel(PlaceDto dto) => new PlaceViewModel
        {
            Id = dto.Id,
            Line = dto.Line,
            Column = dto.Column,
            Status = dto.Status
        };

        public static explicit operator PlaceDto(PlaceViewModel mvm) => new PlaceDto
        {
            Id = mvm.Id,
            Line = mvm.Line,
            Column = mvm.Column,
            Status = mvm.Status
        };
    }
}
