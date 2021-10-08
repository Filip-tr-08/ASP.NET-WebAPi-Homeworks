using DTOs.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface IUsersServices
    {
        void Register(RegisterUserDTO registerUserDTO);
        string Login(LoginDTO loginDTO);

    }
}
