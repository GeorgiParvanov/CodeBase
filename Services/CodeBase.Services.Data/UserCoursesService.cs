namespace CodeBase.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CodeBase.Data.Common.Repositories;
    using CodeBase.Data.Models;
    using CodeBase.Services.Data.Contracts;
    using CodeBase.Services.Mapping;
    using CodeBase.Web.ViewModels.Administration.UserCourses;
    using Microsoft.EntityFrameworkCore;

    public class UserCoursesService : IUserCoursesService
    {
        private readonly IDeletableEntityRepository<UserCourse> userCourseRepository;
        private readonly IDeletableEntityRepository<Course> courseRepository;

        public UserCoursesService(IDeletableEntityRepository<UserCourse> userCourseRepository, IDeletableEntityRepository<Course> courseRepository)
        {
            this.userCourseRepository = userCourseRepository;
            this.courseRepository = courseRepository;
        }

        public async Task AddAsync(UserCourseInputModel input)
        {
            var userCourse = new UserCourse
            {
                CourseId = input.CourseId,
                UserId = input.UserId,
            };

            await this.userCourseRepository.AddAsync(userCourse);
            await this.userCourseRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var userCourse = this.GetById<UserCourse>(id);
            this.userCourseRepository.Delete(userCourse);
            await this.userCourseRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.userCourseRepository.AllWithDeleted()
                .To<T>()
                .ToList();
        }

        public T GetById<T>(int id)
        {
            return this.userCourseRepository.AllWithDeleted()
                .Include(u => u.Course)
                .Where(m => m.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public async Task Update(UserCourseInputModel input)
        {
            var userCourse = this.userCourseRepository.All().FirstOrDefault(uc => uc.Id == input.Id);

            userCourse.UserId = input.UserId;
            userCourse.CourseId = input.CourseId;
            userCourse.IsDeleted = input.IsDeleted;

            this.userCourseRepository.Update(userCourse);
            await this.userCourseRepository.SaveChangesAsync();
        }

        public bool UserCourseExists(int id)
        {
            return this.userCourseRepository.AllWithDeleted().Any(e => e.Id == id);
        }
    }
}
