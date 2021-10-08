using DTOs.Movies;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
   public interface IMoviesServices
    {
        List<MovieDto> GetAllMovies(string userId);
        List<MovieDto> GetAllMovies();
        MovieDto GetMovieById(int id);
        void AddMovie(AddUpdateMovieDto addMovieDto);
        void UpdateMovie(AddUpdateMovieDto updateMovieDto);
        void DeleteMovie(int id);
        List<MovieDto> GetMoviesByCountry(string country);
    }
}
