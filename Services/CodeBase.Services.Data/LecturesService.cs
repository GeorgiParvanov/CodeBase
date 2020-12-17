namespace CodeBase.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using CodeBase.Data.Common.Repositories;
    using CodeBase.Data.Models;
    using CodeBase.Services.Data.Contracts;
    using CodeBase.Services.Mapping;

    public class LecturesService : ILecturesService
    {
        private readonly IDeletableEntityRepository<Lecture> lecturesRepository;

        public LecturesService(IDeletableEntityRepository<Lecture> lecturesRepository)
        {
            this.lecturesRepository = lecturesRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.lecturesRepository.All().To<T>().ToList();
        }

        public int GetCount()
        {
            return this.lecturesRepository.AllAsNoTracking().Count();
        }

        public T GetById<T>(int id)
        {
            return this.lecturesRepository.All()
                .Where(l => l.Id == id)
                .To<T>()
                .FirstOrDefault();
        }
    }
}
