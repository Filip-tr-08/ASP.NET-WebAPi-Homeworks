using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET.WebApiClass05Workshop.Models
{
    public class Director
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public List<Movie>Movies { get; set; }
        public Director()
        {
            Movies = new List<Movie>();
        }
    }
}
