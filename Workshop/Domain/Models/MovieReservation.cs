using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class MovieReservation:BaseEntity
    {
        public Movie Movie { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }

    }
}
