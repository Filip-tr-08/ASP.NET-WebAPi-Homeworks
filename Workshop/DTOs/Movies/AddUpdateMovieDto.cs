using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.Movies
{
   public class AddUpdateMovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Year { get; set; }
        public Genre? Genre { get; set; }
        public int DirectorId { get; set; }
    }
}
