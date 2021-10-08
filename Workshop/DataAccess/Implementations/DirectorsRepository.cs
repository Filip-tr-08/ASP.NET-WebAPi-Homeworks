using DataAccess.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Implementations
{
   public class DirectorsRepository:IRepository<Director>
    {
        private CinemaAppDbContext _cinemaAppDbContext;
        public DirectorsRepository(CinemaAppDbContext cinemaAppDbContext)
        {
            _cinemaAppDbContext = cinemaAppDbContext;
        }
        public void Delete(Director entity)
        {
            _cinemaAppDbContext.Directors.Remove(entity);
            _cinemaAppDbContext.SaveChanges();
        }

        public List<Director> GetAll()
        {
            return _cinemaAppDbContext.Directors.Include(x => x.Movies).ToList();
        }

        public Director GetById(int id)
        {
            return _cinemaAppDbContext.Directors.Include(x => x.Movies).FirstOrDefault(x => x.Id == id);
        }

        public void Insert(Director entity)
        {
            _cinemaAppDbContext.Directors.Add(entity);
            _cinemaAppDbContext.SaveChanges();
        }

        public void Update(Director entity)
        {
            _cinemaAppDbContext.Directors.Update(entity);
            _cinemaAppDbContext.SaveChanges();
        }
    }
}
