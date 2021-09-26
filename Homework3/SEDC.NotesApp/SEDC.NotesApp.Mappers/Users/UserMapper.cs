using SEDC.NotesApp.Domain.Models;
using SEDC.NotesApp.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.NotesApp.Mappers.Users
{
   public static class UserMapper
    {
        public static UserDto UserToUserDto(this User user)
        {
            return new UserDto()
            {
                FirstName=user.FirstName,
                LastName=user.LastName,
                UserName=user.Username
            };
        }
        public static User AddUpdateUserDtoToUser(this AddUpdateUserDto addUpdateUserDto)
        {
            return new User()
            {
                Id=addUpdateUserDto.Id,
                FirstName=addUpdateUserDto.FirstName,
                LastName=addUpdateUserDto.LastName,
                Username=addUpdateUserDto.UserName
            };
        }
    }
}
