using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Interfaces
{
   public interface IMoviesRepositorycs:IRepository<Movie>
    {
        List<Movie> FindMovieByContry(string country);
        public List<Movie> GetAll(string userId);
    }
}
