using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CinemaHW.Persistence
{
    public class Users : IdentityUser<string>
    {
        
        public string FullName { get; set; }

    }

}
