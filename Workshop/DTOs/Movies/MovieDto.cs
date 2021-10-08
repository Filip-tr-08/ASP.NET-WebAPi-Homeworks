using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.Movies
{
    public class MovieDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Year { get; set; }
        public Genre? Genre { get; set; }
        public string DirectorFullName { get; set; }
    }
}
