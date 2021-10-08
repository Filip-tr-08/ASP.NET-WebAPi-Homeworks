using DTOs.Directors;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mappers.Directors
{
    public static class DirectorsMapper
    {
       public static DirectorDto ToDirectorDto(this Director director)
        {
            return new DirectorDto()
            {
                FirstName=director.FirstName,
                LastName=director.LastName,
                Country=director.Country
            };
        }
        public static Director ToDirector(this AddUpdateDirectorDto addUpdateDirectorDto)
        {
            return new Director()
            {
                FirstName = addUpdateDirectorDto.FirstName,
                LastName = addUpdateDirectorDto.LastName,
                Country = addUpdateDirectorDto.Country
            };
        }
    }
}
