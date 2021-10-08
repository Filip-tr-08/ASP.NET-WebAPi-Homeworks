using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.Users
{
    public class RegisterUserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmedPassword { get; set; }
        public Role Role { get; set; }
    }
}
