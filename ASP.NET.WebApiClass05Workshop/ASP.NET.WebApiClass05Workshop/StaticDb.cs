using ASP.NET.WebApiClass05Workshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET.WebApiClass05Workshop
{
    public class StaticDb
    {
        public static List<Director> Directors = new List<Director>()
        {
            new Director
            {
                Id=1,
                FirstName="Director1",
                LastName="NumberOne",
                Country="Macedonia",                            
            },
            new Director
            {
                Id=2,
                FirstName="Director2",
                LastName="NumberTwo",
                Country="USA",
            }
        };
        public static List<Movie> Movies = new List<Movie>
        {
            new Movie
            {
                Id=1,
                Title="Movie1",
                Description="Story about the first movie",
                Year=2012,
                Genre=Genre.Comedy,
                Director=Directors.First()
            },
            new Movie
            {
                Id=2,
                Title="Movie2",
                Description="Story about the second movie",
                Year=2015,
                Genre=Genre.Thriller,
                Director=Directors.Last()
            }
        };
    }
}
