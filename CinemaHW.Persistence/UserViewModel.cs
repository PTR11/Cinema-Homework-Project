using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaHW.Persistence
{
    public class LoginViewModel
    {
        [DisplayName("Név")]
        [Required(ErrorMessage = "Adja meg a felhasználónevét!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Adja meg a jelszavát!")]
        [DisplayName("Jelszó")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
    public class RegisterViewModel
    {
        [DisplayName("Felhasználó név")]
        [Required(ErrorMessage = "Adja meg a felhasználónevét!")]
        public String UserName { get; set; }

        [DisplayName("Jelszó")]
        [Required(ErrorMessage = "Adja meg a jelszavát!")]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [DisplayName("Jelszó megerősítése")]
        [Required(ErrorMessage = "Adja meg a jelszavát újra!")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "A két jelszó nem egyezik.")]
        public String PasswordRepeat { get; set; }

        [DisplayName("Teljes név")]
        [Required(ErrorMessage = "Adja meg a teljes nevét!")]
        public String UserFullName { get; set; }

        [DisplayName("Telefonszám")]
        [Required(ErrorMessage = "Adja meg a telefonszámát!")]
        [RegularExpression("06[0-9]{2}[0-9]{7}$", ErrorMessage = "A telefonszám nem megfelelő.")]
        public String UserPhoneNumber { get; set; }



    }

    public class UserViewModel
    {
        [DisplayName("Teljes Név")]
        [Required(ErrorMessage ="Adja meg a nevét!")]
        public string UserFullName { get; set; }

        [DisplayName("Telefon")]
        [Required(ErrorMessage = "Adja meg a telefonszámát!")]
        [RegularExpression("06[0-9]{2}[0-9]{7}$", ErrorMessage = "A telefonszám nem megfelelő.")]
        public String UserPhoneNumber { get; set; }

    }
}
