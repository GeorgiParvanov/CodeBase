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
        private readonly IDeletableEntityRepository<Course> coursesRepository;

        public CoursesService(IDeletableEntityRepository<Course> courcesRepository)
        {
            this.coursesRepository = courcesRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.coursesRepository.All().To<T>().ToList();
        }

        public IEnumerable<T> GetAll<T>(int pageNumber, int itemsPerPage)
        {
            var courses = this.coursesRepository.AllAsNoTracking()

                // .OrderByDescending(x => x.Id)
                .Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToList();

            return courses;
        }

        public IEnumerable<T> GetAllByTagName<T>(string tagName)
        {
            return this.coursesRepository.All()
                .Where(c => c.Tags.Any(t => t.Tag.Name == tagName))
                .To<T>()
                .ToList();
        }

        public T GetById<T>(int id)
        {
            return this.coursesRepository.All()
                .Where(c => c.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public int GetCount()
        {
            return this.coursesRepository.AllAsNoTracking().Count();
        }
    }
}
