namespace CodeBase.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CodeBase.Data.Common.Repositories;
    using CodeBase.Data.Models;
    using CodeBase.Services.Data.Contracts;
    using CodeBase.Services.Mapping;

    public class CoursesService : ICoursesService
    {
        private readonly IDeletableEntityRepository<Course> courcesRepository;

        public CoursesService(IDeletableEntityRepository<Course> courcesRepository)
        {
            this.courcesRepository = courcesRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.courcesRepository.All().To<T>().ToList();
        }

        public IEnumerable<T> GetAllByTagName<T>(string tagName)
        {
            return this.courcesRepository.All()
                .Where(c => c.Tags.Any(t => t.Tag.Name == tagName))
                .To<T>()
                .ToList();
        }

        public T GetById<T>(int id)
        {
            return this.courcesRepository.All()
                .Where(c => c.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public int GetCount()
        {
            return this.courcesRepository.AllAsNoTracking().Count();
        }
    }
}
