using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Enums;
using Services.Interfaces;
using Shared;
using DTOs.Movies;
using DTOs.Directors;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ASP.NET.WebApiClass05Workshop.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private IMoviesServices _moviesServices;
        private IDirectorsServices _directorServices;
        public MoviesController(IMoviesServices moviesServices, IDirectorsServices directorServices)
        {
            _moviesServices = moviesServices;
            _directorServices = directorServices;
        }
        [HttpPost("createMovie")]
        [Authorize(Roles = "Admin")]
        public IActionResult PostNewMovie([FromBody] AddUpdateMovieDto addUpdateMovieDto)
        {
            try
            {
                if (addUpdateMovieDto.Id <= 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Id can't be negative");
                }
                
                _moviesServices.AddMovie(addUpdateMovieDto);
                return StatusCode(StatusCodes.Status201Created, "Movie is created");
            }
            catch (MovieException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateMovie([FromBody] AddUpdateMovieDto addUpdateMovieDto)
        {
            try
            {
                if (addUpdateMovieDto.Id <= 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Id can't be negative");
                }
               
                _moviesServices.UpdateMovie(addUpdateMovieDto);
                return StatusCode(StatusCodes.Status204NoContent, "The movie is updated");
            }
            catch (ResourceNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (MovieException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }
        [HttpDelete("{id}")]
        [Authorize(Roles ="User")]
        public IActionResult DeleteMovie(int id)
        {
            try
            {
               if (id <= 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Id can't be negative or zero");
                }
                var claims = User.Claims;
                string userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
                string username = User.Claims.First(x => x.Type == ClaimTypes.Name).Value;
                if (username != "superadmin")
                {
                    return StatusCode(StatusCodes.Status403Forbidden);
                }
                _moviesServices.DeleteMovie(id);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (ResourceNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "User")]
        public ActionResult<MovieDto> Get(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Id can't be negative or zero");
                }
                
                return _moviesServices.GetMovieById(id);
            }
            catch (ResourceNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }
        [HttpGet] 
        [Authorize]
        public ActionResult<List<MovieDto>> GetMovies()
        {
            try
            {
                var claims = User.Claims;
                string userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
                return _moviesServices.GetAllMovies(userId);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }
        [HttpGet("filter")]  
        [AllowAnonymous]
        public ActionResult<List<MovieDto>> FilterMoviesFromQuery(int? year, Genre? genre)
        {
            try
            {
                if (year==null && genre==null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "You have to send at least one filter parameter!");
                }
                if (year==null)
                {
                    List<MovieDto> moviesDb = _moviesServices.GetAllMovies().Where(x => x.Genre == genre).ToList();
                    
                    return Ok(moviesDb);
                }
                if (genre==null)
                {
                    List<MovieDto> moviesDb = _moviesServices.GetAllMovies().Where(x => x.Year==year).ToList();
                    return Ok(moviesDb);
                }
                List<MovieDto> filteredMovies = _moviesServices.GetAllMovies().Where(x => x.Year==year  && x.Genre == genre).ToList();
                return Ok(filteredMovies);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }
        [HttpGet("director/{country}")]
        public ActionResult<List<MovieDto>> FilterMoviesByDirectors(string country)
        {
            try
            {
                return Ok(_moviesServices.GetMoviesByCountry(country));
            }
            catch(DirectorException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (ResourceNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error");
            }
        }
    }
}
