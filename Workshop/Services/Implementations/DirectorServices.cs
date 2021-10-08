using DataAccess.Interfaces;
using Domain.Models;
using DTOs.Directors;
using Services.Interfaces;
using Shared;
using System;
using System.Collections.Generic;
using Mappers.Directors;
using System.Text;
using System.Linq;

namespace Services.Implementations
{
    public class DirectorServices : IDirectorsServices
    {
        private IRepository<Director> _directorsRepository;

        public DirectorServices(IRepository<Director> directorsRepository)
        {
            _directorsRepository = directorsRepository;
        }

        public void AddDirector(AddUpdateDirectorDto addDirectorDto)
        {
            ValidateDirectorInput(addDirectorDto);
            Director newDirector = addDirectorDto.ToDirector();
            _directorsRepository.Insert(newDirector);
        }

        public void DeleteDirector(int id)
        {
            Director directorDb = _directorsRepository.GetById(id);
            if (directorDb == null)
            {
                throw new ResourceNotFoundException($"Director with {id} was not found!");
            }
            _directorsRepository.Delete(directorDb);
        }

        public List<DirectorDto> GetAllDirectors()
        {
            List<Director> directorsDb = _directorsRepository.GetAll();
            return directorsDb.Select(x => x.ToDirectorDto()).ToList();
        }

        public DirectorDto GetDirectorById(int id)
        {
            Director directorDb = _directorsRepository.GetById(id);
            if (directorDb == null)
            {
                throw new ResourceNotFoundException($"Director with id {id} was not found");
            }
            return directorDb.ToDirectorDto();
        }

        public void UpdateDirector(AddUpdateDirectorDto updateDirectorDto)
        {
            Director directorDb = _directorsRepository.GetById(updateDirectorDto.Id);
            if (directorDb == null)
            {
                throw new ResourceNotFoundException($"Director with id {updateDirectorDto.Id} was not found");
            }
            ValidateDirectorInput(updateDirectorDto);
            directorDb.FirstName = updateDirectorDto.FirstName;
            directorDb.LastName = updateDirectorDto.LastName;
            directorDb.Country = updateDirectorDto.Country;
            

            _directorsRepository.Update(directorDb);
        }

        #region private methods
        private void ValidateDirectorInput(AddUpdateDirectorDto addDirectorDto)
        {
            
            if (string.IsNullOrEmpty(addDirectorDto.FirstName))
            {
                throw new DirectorException("The first name must not be empty!");
            }
            if (addDirectorDto.FirstName.Length > 100)
            {
                throw new MovieException("The first name must not have more than 100 characters!");
            }
            if (string.IsNullOrEmpty(addDirectorDto.LastName))
            {
                throw new DirectorException("The last name must not be empty!");
            }
            if (addDirectorDto.LastName.Length > 100)
            {
                throw new MovieException("The last name must not have more than 100 characters!");
            }
            if (string.IsNullOrEmpty(addDirectorDto.Country))
            {
                throw new DirectorException("The country must not be empty!");
            }
            if (addDirectorDto.Country.Length > 100)
            {
                throw new MovieException("The country must not have more than 100 characters!");
            }

        }
        #endregion
}
}