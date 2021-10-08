using DTOs.Directors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
   public interface IDirectorsServices
    {
        List<DirectorDto> GetAllDirectors();
        DirectorDto GetDirectorById(int id);
        void AddDirector(AddUpdateDirectorDto addDirectorDto);
        void UpdateDirector(AddUpdateDirectorDto addDirectorDto);
        void DeleteDirector(int id);
    }
}
