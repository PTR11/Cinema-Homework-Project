using CinemaHW.Persistence.DTO;
using System;
using System.Collections.Generic;

namespace CinemaHW.Desktop.ViewModel
{
    public class MovieViewModel : ViewModelBase
    {
        private int _id;
        private string _title;
        private string _director;
        private int _length;
        private string _description;
        private DateTime _uploadTime;
        private int _imageId;
        private byte[] _image;

        public Int32 Id {
            get { return _id; } 
            set { _id = value; OnPropertyChanged(); }
        }
        public Int32 ImageId
        {
            get { return _imageId; }
            set { _imageId = value; OnPropertyChanged(); }
        }
        public String Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }

        public String Director
        {
            get { return _director; }
            set { _director = value; OnPropertyChanged(); }
        }

        public Int32 Length
        {
            get { return _length; }
            set { _length = value; OnPropertyChanged(); }
        }

        public String Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(); }
        }

        public DateTime UploadTime
        {
            get { return _uploadTime; }
            set { _uploadTime = value; OnPropertyChanged(); }
        }

        public Byte[] Image
        {
            get { return _image; }
            set { _image = value; OnPropertyChanged(); }
        }

        public MovieViewModel ShallowClone()
        {
            return (MovieViewModel)this.MemberwiseClone();
        }

        public void CopyFrom(MovieViewModel rhs)
        {
            Id = rhs.Id;
            Title = rhs.Title;
            Director = rhs.Director;
            Length = rhs.Length;
            Description = rhs.Description;
            UploadTime = rhs.UploadTime;
            Image = rhs.Image;
            ImageId = rhs.ImageId;
        }

        public static explicit operator MovieViewModel(MovieDto dto) => new MovieViewModel
        {
            Id = dto.Id,
            Title = dto.Title,
            Director = dto.Director,
            Length = dto.Length,
            Description = dto.Description,
            UploadTime = dto.UploadTime
        };

        public static explicit operator MovieDto(MovieViewModel mvm) => new MovieDto
        {
            Id = mvm.Id,
            Title = mvm.Title,
            Director = mvm.Director,
            Length = mvm.Length,
            Description = mvm.Description,
            UploadTime = mvm.UploadTime
        };






    }
}