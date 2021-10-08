using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Movie : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Year { get; set; }
        public Genre? Genre { get; set; }
        public int DirectorId { get; set; }
        public Director Director { get; set; }
        public List<MovieReservation> MovieReservations { get; set; }

    }
}
