using DataAccess.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Implementations
{
    public class MoviesRepository : IMoviesRepositorycs
    {
        private CinemaAppDbContext _cinemaAppDbContext;
        public MoviesRepository(CinemaAppDbContext cinemaAppDbContext)
        {
            _cinemaAppDbContext = cinemaAppDbContext;
        }
        public void Delete(Movie entity)
        {
            _cinemaAppDbContext.Movies.Remove(entity);
            _cinemaAppDbContext.SaveChanges();
        }

        public List<Movie> FindMovieByContry(string country)
        {
            return _cinemaAppDbContext.Movies.Include(x => x.Director).Where(x => x.Director.Country.ToLower().Contains(country.ToLower())).Include(x=>x.MovieReservations).ToList();
        }

        public List<Movie> GetAll(string userId)
        {
            bool success = int.TryParse(userId, out int userID);
            if (success) {
                User userDb = _cinemaAppDbContext.Users.FirstOrDefault(x => x.Id == userID);
                List<MovieReservation> movieReservations = userDb.MovieReservations;
                List<Movie> Movies = new List<Movie>();
                foreach(MovieReservation movieReservation in movieReservations)
                {
                    Movies.Add(movieReservation.Movie);
                }
                return Movies;
            }
            return null;
        }

        public List<Movie> GetAll()
        {
            return _cinemaAppDbContext.Movies.Include(x => x.Director).Include(x => x.MovieReservations).ToList();
        }

        public Movie GetById(int id)
        {
            return _cinemaAppDbContext.Movies.Include(x => x.Director).FirstOrDefault(x=>x.Id==id);
        }

        public void Insert(Movie entity)
        {
            _cinemaAppDbContext.Movies.Add(entity);
            _cinemaAppDbContext.SaveChanges();
        }

        public void Update(Movie entity)
        {
            _cinemaAppDbContext.Movies.Update(entity);
            _cinemaAppDbContext.SaveChanges();
        }
    }
}
