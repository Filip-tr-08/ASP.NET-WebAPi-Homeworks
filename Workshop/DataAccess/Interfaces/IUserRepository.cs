using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Interfaces
{
   public interface IUserRepository:IRepository<User>
    {
        User GetUserByUserName(string username);
        User LoginUser(string username, string password);
    }
}
