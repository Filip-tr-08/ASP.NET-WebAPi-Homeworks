using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.Directors
{
   public class AddUpdateDirectorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
    }
}
