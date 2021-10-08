using DTOs.Movies;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mappers.Movies
{
    public static class MoviesMapper
    {
        public static MovieDto ToMovieDto(this Movie movie)
        {
            return new MovieDto()
            {
                Title=movie.Title,
                Description=movie.Description,
                Year=movie.Year,
                Genre=movie.Genre,
                DirectorFullName=$"{movie.Director.FirstName} {movie.Director.LastName}"
            };
        }
        public static Movie AddUpdateMovieDtoToMovie (this AddUpdateMovieDto addUpdateMovieDto)
        {
            return new Movie()
            {
                Title=addUpdateMovieDto.Title,
                Description=addUpdateMovieDto.Description,
                Year=addUpdateMovieDto.Year,
                Genre=addUpdateMovieDto.Genre,
                DirectorId=addUpdateMovieDto.DirectorId
            };
        }
    }
}
