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
    using CodeBase.Web.ViewModels.Administration.Courses;
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

        public async Task Create(CourseInputModel model)
        {
            var course = new Course
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Difficulty = model.Difficulty,
            };

            await this.coursesRepository.AddAsync(course);
            await this.coursesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.coursesRepository.All().To<T>().ToList();
        }

        public IEnumerable<T> GetAll<T>(int pageNumber, int itemsPerPage)
        {
            var courses = this.coursesRepository.AllAsNoTracking()
                .Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToList();

            return courses;
        }

        public IEnumerable<T> GetAllWithDeleted<T>(int pageNumber, int itemsPerPage)
        {
            var courses = this.coursesRepository.AllWithDeleted()
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

        public int GetCountWithDeleted()
        {
            return this.coursesRepository.AllWithDeleted().Count();
        }

        public T GetByIdWithDeleted<T>(int id)
        {
            return this.coursesRepository.AllWithDeleted()
               .Where(c => c.Id == id)
               .To<T>()
               .FirstOrDefault();
        }

        public async Task UpdateAsync(int id, CourseInputModel input)
        {
            var course = this.coursesRepository.AllWithDeleted().FirstOrDefault(x => x.Id == id);
            course.Name = input.Name;
            course.Description = input.Description;
            course.Price = input.Price;
            course.Difficulty = input.Difficulty;
            course.IsDeleted = input.IsDeleted;
            await this.coursesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var course = this.coursesRepository.AllWithDeleted().FirstOrDefault(c => c.Id == id);
            this.coursesRepository.Delete(course);
            await this.coursesRepository.SaveChangesAsync();
        }

        public bool CourseExist(int id)
        {
            return this.coursesRepository.AllWithDeleted().Any(e => e.Id == id);
        }

        public IEnumerable<T> GetAllByTagName<T>(string tagName, int pageNumber, int itemsPerPage)
        {
            return this.coursesRepository.All()
                .Where(c => c.Tags.Any(t => t.Tag.Name == tagName))
                .Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>()
                .ToList();
        }
    }
}
