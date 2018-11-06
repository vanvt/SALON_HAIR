using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SALON_HAIR_AUTHENTICATION.Models
{
    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string GrantType { get; set; } = "password";
        public string RefreshToken { get; set; }
    }
}
