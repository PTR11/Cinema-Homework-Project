using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaHW.Persistence.DTO
{
    public class UserDto
    {
        public String Id { get; set; }
        public String FullName { get; set; }

        public String PhoneNumber { get; set; }

        public static explicit operator Users(UserDto dto) => new Users
        {
            Id = dto.Id,
            FullName = dto.FullName,
            PhoneNumber = dto.PhoneNumber
        };

        public static explicit operator UserDto(Users a) => new UserDto
        {
            Id = a.Id,
            FullName = a.FullName,
            PhoneNumber = a.PhoneNumber
        };

    }
}
