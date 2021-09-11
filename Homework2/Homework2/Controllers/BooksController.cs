using Homework2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Book>> GetBooks()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, StaticDb.Books);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occurred");
            }
        }
        [HttpGet("queryString")]
        public ActionResult<Book> GetBook(int index)
        {
            try
            {
                if (index < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "The index can't be negative");
                }
                if (index >= StaticDb.Books.Count)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "The book does not exist");
                }
                return StatusCode(StatusCodes.Status200OK, StaticDb.Books[index]);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occurred");
            }
        }

        [HttpGet("filter")]
        public ActionResult<Book> GetBook(string author, string title)
        {
            try
            {
                if (string.IsNullOrEmpty(author) && string.IsNullOrEmpty(title))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Bad Request you must enter at least one parameter");
                }
                if (string.IsNullOrEmpty(author))
                {
                    List<Book> booksDb = StaticDb.Books.Where(x => x.Title.ToLower().Contains(title.ToLower())).ToList();
                    return StatusCode(StatusCodes.Status200OK, booksDb);
                }
                if (string.IsNullOrEmpty(title))
                {
                    List<Book> booksDb = StaticDb.Books.Where(x => x.Author.ToLower().Contains(author.ToLower())).ToList();
                    return StatusCode(StatusCodes.Status200OK, booksDb);
                }
                List<Book> filteredBooks = StaticDb.Books.Where(x => x.Author.ToLower().Contains(author.ToLower()) && x.Title.ToLower().Contains(title.ToLower())).ToList();
                return StatusCode(StatusCodes.Status200OK, filteredBooks);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occurred");
            }
        }
        [HttpGet("contentType")]
        public ActionResult<Dictionary<string,string>> GetHeaderContentType()
        {
            try
            {
                Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
                foreach (var header in Request.Headers)
                {
                    requestHeaders.Add(header.Key, header.Value);
                }
                return StatusCode(StatusCodes.Status200OK,requestHeaders);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }
        [HttpPost("postbook")]
        public IActionResult postBook([FromBody] Book book)
        {
            try
            {
                StaticDb.Books.Add(book);
                return StatusCode(StatusCodes.Status201Created, "Book is created");
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }
        [HttpPost("listOfTitles")]
        public ActionResult<List<string>> postBooks([FromBody] List<Book> books)
        {
            try
            {
                List<string> titles = books.Select(x => x.Title).ToList();
                return StatusCode(StatusCodes.Status200OK, titles);

            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }
    }
}
