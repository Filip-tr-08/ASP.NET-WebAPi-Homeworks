using ASP.NET.WebApiClass05Workshop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET.WebApiClass05Workshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        [HttpPost("createMovie")]
        public IActionResult PostNewMovie([FromBody] Movie movie)
        {
            try
            {
                if (movie.Id <= 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Id can't be negative");
                }
                if (string.IsNullOrEmpty(movie.Title))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "You have to add a title for the movie ");
                }
                if (movie.Year == null || movie.Year<1980 || movie.Year>2021)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "You have to add a valid year for the movie ");
                }
                if (movie.Genre == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "You have to add a genre for the movie ");
                }
                StaticDb.Movies.Add(movie);
                return StatusCode(StatusCodes.Status201Created, "Movie is created");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }
        [HttpPut]
        public IActionResult UpdateMovie([FromBody] Movie movie)
        {
            try
            {
                if (movie.Id <= 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Id can't be negative");
                }
                if (string.IsNullOrEmpty(movie.Title))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "You have to add a title for the movie ");
                }
                if (movie.Year == null || movie.Year<1980 || movie.Year>2021)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "You have to add a valid year for the movie ");
                }
                if (movie.Genre == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "You have to add a genre for the movie ");
                }
                Movie movieDb = StaticDb.Movies.FirstOrDefault(x => x.Id == movie.Id);
                if (movieDb == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"Resource with id {movie.Id} was not found!");
                }
                int index = StaticDb.Movies.IndexOf(movieDb);
                StaticDb.Movies[index] = movie;
                return StatusCode(StatusCodes.Status204NoContent, "The movie is updated");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Id can't be negative or zero");
                }
                Movie movieDb = StaticDb.Movies.FirstOrDefault(x => x.Id == id);
                if (movieDb == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "The movie is not found");
                    
                }
                StaticDb.Movies.Remove(movieDb);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }
        [HttpGet("{id}")]
        public ActionResult<Movie> Get(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Id can't be negative or zero");
                }
                Movie movieDb = StaticDb.Movies.FirstOrDefault(x => x.Id == id);
                if (movieDb == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "The movie is not found");

                }
                return Ok(movieDb);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }
        [HttpGet] 
        public ActionResult<List<Movie>> GetMovies()
        {
            try
            {
                return Ok(StaticDb.Movies);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }
        [HttpGet("filter")]  
        public ActionResult<List<Movie>> FilterMoviesFromQuery(int? year, Genre? genre)
        {
            try
            {
                if (year==null && genre==null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "You have to send at least one filter parameter!");
                }
                if (year==null)
                {
                    List<Movie> moviesDb = StaticDb.Movies.Where(x => x.Genre == genre).ToList();
                    return Ok(moviesDb);
                }
                if (genre==null)
                {
                    List<Movie> moviesDb = StaticDb.Movies.Where(x => x.Year==year).ToList();
                    return Ok(moviesDb);
                }
                List<Movie> filteredMovies = StaticDb.Movies.Where(x => x.Year==year  && x.Genre == genre).ToList();
                return Ok(filteredMovies);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }
        [HttpGet("director/{country}")]
        public ActionResult<List<Movie>> FilterMoviesByDirectors(string country)
        {
            try
            {
                if (string.IsNullOrEmpty(country))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Director has to be from somewhere");
                }
                
                Director directorDb = StaticDb.Directors.FirstOrDefault(x => x.Country == country);
                if (directorDb == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"Director from {country} was not found!");
                }
                return Ok(StaticDb.Movies.Where(x => x.Director.Country.ToLower().Contains(country.ToLower())));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error");
            }
        }
    }
}
