using CinemaHW.Persistence.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaHW.Desktop.ViewModel
{
    public class UserViewModel
    {
        private String _id;
        private String _fullname;
        private String _phoneNumber;
        public String Id { get; set; }
        public String FullName { get; set; }

        public String PhoneNumber { get; set; }

        public UserViewModel ShallowClone()
        {
            return (UserViewModel)this.MemberwiseClone();
        }

        public void CopyFrom(UserViewModel rhs)
        {
            Id = rhs.Id;
            FullName = rhs.FullName;
            PhoneNumber = rhs.PhoneNumber;
        }

        public static explicit operator UserViewModel(UserDto dto) => new UserViewModel
        {
            Id = dto.Id,
            FullName = dto.FullName,
            PhoneNumber = dto.PhoneNumber
        };

        public static explicit operator UserDto(UserViewModel vm) => new UserDto
        {
            Id = vm.Id,
            FullName = vm.FullName,
            PhoneNumber = vm.PhoneNumber
        };

    }
}
