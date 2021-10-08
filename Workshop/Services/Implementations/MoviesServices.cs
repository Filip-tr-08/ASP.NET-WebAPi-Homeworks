using DataAccess.Interfaces;
using Domain.Models;
using DTOs.Movies;
using Services.Interfaces;
using Shared;
using System;
using System.Collections.Generic;
using Mappers.Movies;
using System.Text;
using System.Linq;
using DTOs.Directors;

namespace Services.Implementations
{
    public class MoviesServices:IMoviesServices
    {
        private IMoviesRepositorycs _moviesRepository;
        private IRepository<Director> _directorsRepository;

        public MoviesServices(IMoviesRepositorycs moviesRepository, IRepository<Director> directorsRepository)
        {
            _moviesRepository = moviesRepository;
            _directorsRepository = directorsRepository;
        }

        public void AddMovie(AddUpdateMovieDto addMovieDto)
        {
            ValidateMovieInput(addMovieDto);
            Movie newMovie = addMovieDto.AddUpdateMovieDtoToMovie();
            _moviesRepository.Insert(newMovie);
        }

        public void DeleteMovie(int id)
        {
            Movie movieDb = _moviesRepository.GetById(id);
            if (movieDb == null)
            {
                throw new ResourceNotFoundException($"Movie with {id} was not found!");
            }
            _moviesRepository.Delete(movieDb);
        }

        public List<MovieDto> GetAllMovies(string userId)
        {
            List<Movie> moviesDb = _moviesRepository.GetAll(userId);
            return moviesDb.Select(x => x.ToMovieDto()).ToList();
        }
        public List<MovieDto> GetAllMovies()
        {
            List<Movie> moviesDb = _moviesRepository.GetAll();
            return moviesDb.Select(x => x.ToMovieDto()).ToList();
        }

        public MovieDto GetMovieById(int id)
        {
            Movie movieDb = _moviesRepository.GetById(id);
            if (movieDb == null)
            {
                throw new ResourceNotFoundException($"Movie with id {id} was not found");
            }
            return movieDb.ToMovieDto();
        }

        public void UpdateMovie(AddUpdateMovieDto updateMovieDto)
        {
            Movie movieDb = _moviesRepository.GetById(updateMovieDto.Id);
            if (movieDb == null)
            {
                throw new ResourceNotFoundException($"Movie with id {updateMovieDto.Id} was not found");
            }
            ValidateMovieInput(updateMovieDto);
            movieDb.Title = updateMovieDto.Title;
            movieDb.Description = updateMovieDto.Description;
            movieDb.Genre = updateMovieDto.Genre;
            movieDb.Year = updateMovieDto.Year;
            movieDb.DirectorId = updateMovieDto.DirectorId;

            _moviesRepository.Update(movieDb);
        }

        public List<MovieDto> GetMoviesByCountry(string country)
        {
            if (string.IsNullOrEmpty(country))
            {
               throw new DirectorException("Director has to be from somewhere");
            }

            Director directorDb = _directorsRepository.GetAll().FirstOrDefault(x => x.Country.ToLower().Contains(country.ToLower()));
            if (directorDb == null)
            {
               throw new ResourceNotFoundException($"Director from {country} was not found!");
            }
            return _moviesRepository.FindMovieByContry(country).Select(x=>x.ToMovieDto()).ToList();   
        }
        #region private methods
        private void ValidateMovieInput(AddUpdateMovieDto addMovieDto)
        {
            Director directorDb = _directorsRepository.GetById(addMovieDto.DirectorId);
            if (directorDb == null)
            {
                throw new MovieException("The director does not exist!");
            }
            if (string.IsNullOrEmpty(addMovieDto.Title))
            {
                throw new MovieException("The title must not be empty!");
            }
            if (addMovieDto.Title.Length>100)
            {
                throw new MovieException("The title must not have more than 100 characters!");
            }
         
            if (string.IsNullOrEmpty(addMovieDto.Description))
            {
                throw new MovieException("The description must not be empty!");
            }
            if (addMovieDto.Description.Length >250)
            {
                throw new MovieException("The description must not have more than 250 characters!");
            }
        }
        #endregion
    }
}
