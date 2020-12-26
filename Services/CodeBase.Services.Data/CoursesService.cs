namespace CodeBase.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CodeBase.Data.Common.Repositories;
    using CodeBase.Data.Models;
    using CodeBase.Services.Data.Contracts;
    using CodeBase.Services.Mapping;
    using Microsoft.AspNetCore.Identity;

    public class CoursesService : ICoursesService
    {
        private readonly IDeletableEntityRepository<Course> coursesRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public CoursesService(IDeletableEntityRepository<Course> courcesRepository, IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.coursesRepository = courcesRepository;
            this.userRepository = userRepository;
        }

        public async Task AddUserToCourse(int courseId, string userId)
        {
            var course = this.coursesRepository.All().FirstOrDefault(c => c.Id == courseId);
            course.Users.Add(new UserCourse
            {
                CourseId = courseId,
                UserId = userId,
            });

            await this.coursesRepository.SaveChangesAsync();
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

        public decimal GetBalanceAmount(int courseId)
        {
            return this.coursesRepository.All()
                .FirstOrDefault(c => c.Id == courseId)
                .Price;
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
