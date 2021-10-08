using DataAccess.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Implementations
{
    public class UserRepository : IUserRepository
    {
        private CinemaAppDbContext _cinemaAppDbContext;
        public UserRepository(CinemaAppDbContext cinemaAppDbContext)
        {
            _cinemaAppDbContext = cinemaAppDbContext;
        }
        public void Delete(User entity)
        {
            _cinemaAppDbContext.Remove(entity);
            _cinemaAppDbContext.SaveChanges();
        }

        public List<User> GetAll()
        {
            return _cinemaAppDbContext.Users.Include(x => x.MovieReservations).ToList();
        }

        public User GetById(int id)
        {
            return _cinemaAppDbContext.Users.Include(x => x.MovieReservations).FirstOrDefault(x => x.Id == id);
        }

        public User GetUserByUserName(string username)
        {
            return _cinemaAppDbContext.Users.Include(x => x.MovieReservations).FirstOrDefault(x => x.UserName.ToLower() == username.ToLower());
        }

        public void Insert(User entity)
        {
            _cinemaAppDbContext.Users.Add(entity);
            _cinemaAppDbContext.SaveChanges();
        }

        public User LoginUser(string username, string password)
        {
            return _cinemaAppDbContext.Users.Include(x => x.MovieReservations).FirstOrDefault(x => x.UserName.ToLower() == username.ToLower() && x.Password == password);
        }

        public void Update(User entity)
        {
            _cinemaAppDbContext.Users.Update(entity);
            _cinemaAppDbContext.SaveChanges();
        }
    }
}
