using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NETWebApiHomework1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<string>> Get()
        {
            return StatusCode(StatusCodes.Status200OK, StaticDb.UserNames);
        }
        [HttpGet("{index}")]
        public ActionResult<string>Get(int index)
        {
            try
            {
                if (index < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "The index has a negative value.");
                }
                if (index >= StaticDb.UserNames.Count)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"There is no user with index {index}");
                }
                return StatusCode(StatusCodes.Status200OK, StaticDb.UserNames[index]);
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }
        [HttpPost]
        public IActionResult Post()
        {
            try
            {
                using(StreamReader reader=new StreamReader(Request.Body))
                {
                    string userName = reader.ReadToEnd();
                    StaticDb.UserNames.Add(userName);
                    return StatusCode(StatusCodes.Status201Created, "The user was created");
                }
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occurred");
            }
        }
        public IActionResult Delete()
        {
            try
            {
                using(StreamReader reader=new StreamReader(Request.Body))
                {
                    string requestBody = reader.ReadToEnd();
                    int index = int.Parse(requestBody);
                    if (index < 0)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, "The index has negative value");
                    }
                    if (index >= StaticDb.UserNames.Count)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, $"There is no user wiht index {index}");
                    }
                    StaticDb.UserNames.RemoveAt(index);
                    return StatusCode(StatusCodes.Status204NoContent, "The user was deleted");
                }
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }
    }
}
