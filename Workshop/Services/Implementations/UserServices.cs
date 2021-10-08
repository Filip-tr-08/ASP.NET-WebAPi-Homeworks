using DataAccess.Interfaces;
using Domain.Models;
using DTOs.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;
using Shared;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Services.Implementations
{
    public class UserServices : IUsersServices
    {
        private IUserRepository _userRepository;
        IOptions<AppSettings> _options;
        public UserServices(IUserRepository userRepository, IOptions<AppSettings> options)
        {
            _userRepository = userRepository;
            _options = options;

        }
        public string Login(LoginDTO loginDto)
        {
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] hashedBytes = mD5CryptoServiceProvider.ComputeHash(Encoding.ASCII.GetBytes(loginDto.Password));
            string hashedPassword = Encoding.ASCII.GetString(hashedBytes);

            User userDb = _userRepository.LoginUser(loginDto.Username, hashedPassword);
            if (userDb == null)
            {
                throw new ResourceNotFoundException($"Could not login user {loginDto.Username}");
            }

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            byte[] secretKeyBytes = Encoding.ASCII.GetBytes(_options.Value.SecretKey);

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(1), 

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),
                    SecurityAlgorithms.HmacSha256Signature),

                Subject = new ClaimsIdentity(
                    new[]
                   {
                        new Claim(ClaimTypes.Name, userDb.UserName),
                        new Claim(ClaimTypes.NameIdentifier, userDb.Id.ToString()),
                        new Claim(ClaimTypes.Role, userDb.Role.ToString())
                    }

                )

            };

            SecurityToken token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

            return jwtSecurityTokenHandler.WriteToken(token);
        }

        public void Register(RegisterUserDTO registerUserDTO)
        {
            ValidateUser(registerUserDTO);
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] passwordBytes = Encoding.ASCII.GetBytes(registerUserDTO.Password);
         
            byte[] passwordHash = mD5CryptoServiceProvider.ComputeHash(passwordBytes);
      
            string hashedPasword = Encoding.ASCII.GetString(passwordHash);


            User newUser = new User
            {
                FirstName = registerUserDTO.FirstName,
                LastName = registerUserDTO.LastName,
                UserName = registerUserDTO.Username,
                Role = registerUserDTO.Role,
                Password = hashedPasword 
            };
            _userRepository.Insert(newUser);
        }
        private void ValidateUser(RegisterUserDTO registerUserDto)
        {
            if (string.IsNullOrEmpty(registerUserDto.Username) || string.IsNullOrEmpty(registerUserDto.Password))
            {
                throw new UserException("Username and password are required fields!");
            }
            if (string.IsNullOrEmpty(registerUserDto.Role.ToString()))
            {
                throw new UserException("Role is a required field!");
            }
            if (registerUserDto.Username.Length > 50)
            {
                throw new UserException("Username can contain maximum 50 characters!");
            }
            if (registerUserDto.FirstName.Length > 100 || registerUserDto.LastName.Length > 100)
            {
                throw new UserException("Firstname and Lastname can contain maximum 100 characters!");
            }
            if (!IsUserNameUnique(registerUserDto.Username))
            {
                throw new UserException("A user with this username already exists!");
            }
            if (registerUserDto.Password != registerUserDto.ConfirmedPassword)
            {
                throw new UserException("The passwords do not match!");
            }
            if (!IsPasswordValid(registerUserDto.Password))
            {
                throw new UserException("The password is not complex enough!");
            }
        }
        private bool IsUserNameUnique(string username)
        {
            return _userRepository.GetUserByUserName(username) == null;
        }

        private bool IsPasswordValid(string password)
        {
            Regex passwordRegex = new Regex("^(?=.*[0-9])(?=.*[a-z]).{6,20}$");
            return passwordRegex.Match(password).Success;
        }
    }
}
