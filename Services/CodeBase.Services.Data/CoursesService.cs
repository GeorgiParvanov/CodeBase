﻿namespace CodeBase.Services.Data
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

        public int GetCount()
        {
            return this.courcesRepository.AllAsNoTracking().Count();
        }
    }
}