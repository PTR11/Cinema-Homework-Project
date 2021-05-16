using CinemaHW.Persistence.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaHW.Desktop.ViewModel
{
    public class ProgramViewModel : ViewModelBase
    {
        private Int32 _id;
        private DateTime _date;
        private Int32 _roomId;
        private Int32 _movieId;
        private String _moveiTitle;
        private String _interval;

        public Int32 Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }
        public String MovieTitle
        {
            get { return _moveiTitle; }
            set { _moveiTitle = value; OnPropertyChanged(); }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; OnPropertyChanged(); }
        }
        public Int32 RoomId
        {
            get { return _roomId; }
            set { _roomId = value; OnPropertyChanged(); }
        }

        public Int32 MovieId
        {
            get { return _movieId; }
            set { _movieId = value; OnPropertyChanged(); }
        }

        public String Interval
        {
            get { return _interval; }
            set { _interval = value; OnPropertyChanged(); }
        }

        public DelegateCommand TicketSellCommand { get; set; }

        public ProgramViewModel ShallowClone()
        {
            return (ProgramViewModel)this.MemberwiseClone();
        }

        public void CopyFrom(ProgramViewModel rhs)
        {
            Id = rhs.Id;
            Date = rhs.Date;
            RoomId = rhs.RoomId;
            MovieId = rhs.MovieId;
        }

        public static explicit operator ProgramViewModel(ProgramDto dto) => new ProgramViewModel
        {
            Id = dto.Id,
            Date = dto.Date,
            RoomId = dto.RoomId,
            MovieId = dto.MovieId,
        };

        public static explicit operator ProgramDto(ProgramViewModel mvm) => new ProgramDto
        {
            Id = mvm.Id,
            Date = mvm.Date,
            RoomId = mvm.RoomId,
            MovieId = mvm.MovieId,
        };
    }
}
