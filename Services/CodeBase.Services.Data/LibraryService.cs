namespace CodeBase.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using CodeBase.Data.Common.Repositories;
    using CodeBase.Data.Models;
    using CodeBase.Services.Data.Contracts;
    using CodeBase.Services.Mapping;

    public class LibraryService : ILibraryService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepositoty;
        private readonly IDeletableEntityRepository<Course> courcesRepository;

        public LibraryService(IDeletableEntityRepository<ApplicationUser> userRepositoty, IDeletableEntityRepository<Course> courcesRepository)
        {
            this.userRepositoty = userRepositoty;
            this.courcesRepository = courcesRepository;
        }

        public int GetCount(string userId)
        {
            return this.courcesRepository.All()
                .SelectMany(c => c.Users)
                .Where(uc => uc.UserId == userId)
                .Select(uc => uc.Course)
                .Count();
        }

        public IEnumerable<T> GetUserCourses<T>(string userId, int pageNumber, int itemsPerPage)
        {
            var courses = this.courcesRepository.All()
                .SelectMany(c => c.Users)
                .Where(uc => uc.UserId == userId)
                .Select(uc => uc.Course)
                .Skip((pageNumber - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToList();

            // var courses = this.userRepositoty.All()
            //    .Where(u => u.Id == userId)
            //    .Select(u => u.Courses.Select(uc => uc.Course))
            //    .Skip((pageNumber - 1) * itemsPerPage)
            //    .Take(itemsPerPage)
            //    .To<T>()
            //    .ToList();
            return courses;
        }
    }
}
