using SEDC.NotesApp.DataAccess.Interfaces;
using SEDC.NotesApp.Domain.Models;
using SEDC.NotesApp.Dtos.Users;
using SEDC.NotesApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using SEDC.NotesApp.Mappers.Users;
using System.Text;
using System.Linq;
using SEDC.NotesApp.Shared.CustomExceptions;

namespace SEDC.NotesApp.Services.Implementations
{
    public class UserService : IUserService
    {
        private IRepository<User> _userRepository;
        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public void AddUser(AddUpdateUserDto addUpdateUserDto)
        {
            ValidateUserInput(addUpdateUserDto);
            User newUser = addUpdateUserDto.AddUpdateUserDtoToUser();
            _userRepository.Insert(newUser);
        }

        public void Delete(int id)
        {
            User userDb = _userRepository.GetById(id);
            if (userDb == null)
            {
                throw new ResourceNotFoundException(id,$"User with id {id} is not found");
            }
            _userRepository.Delete(userDb);
        }

        public List<UserDto> GetAllUsers()
        {
            List<User> usersDb = _userRepository.GetAll();
            return usersDb.Select(x => x.UserToUserDto()).ToList();
        }

        public UserDto GetById(int id)
        {
            User userDb = _userRepository.GetById(id);
            if (userDb == null)
            {
                throw new ResourceNotFoundException(id,$"User with id {id} is not found");
            }
            return userDb.UserToUserDto();
        }

        public void UpdateUser(AddUpdateUserDto addUpdateUserDto)
        {
            User userDb = _userRepository.GetById(addUpdateUserDto.Id);
            if (userDb == null)
            {
                throw new ResourceNotFoundException(addUpdateUserDto.Id,$"User with id {addUpdateUserDto.Id} is not found");
            }
            ValidateUserInput(addUpdateUserDto);
            userDb.FirstName = addUpdateUserDto.FirstName;
            userDb.LastName = addUpdateUserDto.LastName;
            userDb.Username = addUpdateUserDto.UserName;
            _userRepository.Update(userDb);
        }

        #region private method
        private void ValidateUserInput(AddUpdateUserDto addUpdateUserDto)
        {
            if (string.IsNullOrEmpty(addUpdateUserDto.FirstName))
            {
                throw new UserException("First name must not be empty");
            }
            if (string.IsNullOrEmpty(addUpdateUserDto.LastName))
            {
                throw new UserException("Last name must not be empty");
            }
            if (string.IsNullOrEmpty(addUpdateUserDto.UserName))
            {
                throw new UserException("Username must not be empty");
            }
        }
        #endregion
    }
}
