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
    using CodeBase.Web.ViewModels.Administration.Lectures;
    using Microsoft.EntityFrameworkCore;

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

        public IEnumerable<T> GetAllWithDeleted<T>(int pageNumber, int itemsPerPage)
        {
            return this.lecturesRepository.AllWithDeleted()
                .Include(l => l.Course)
                .Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToList();
        }

        public int GetCountWithDeleted()
        {
            return this.lecturesRepository.AllWithDeleted().Count();
        }

        public T GetByIdWithDeleted<T>(int id)
        {
            return this.lecturesRepository.AllWithDeleted()
               .Where(l => l.Id == id)
               .To<T>()
               .FirstOrDefault();
        }

        public async Task CreateAsync(LectureInputModel input)
        {
            var lecture = new Lecture
            {
                Name = input.Name,
                Content = input.Content,
                Difficulty = input.Difficulty,
                ReadTime = TimeSpan.FromMinutes(input.ReadTime),
                CourseId = input.CourseId,
            };

            await this.lecturesRepository.AddAsync(lecture);
            await this.lecturesRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, LectureInputModel input)
        {
            var lecture = this.lecturesRepository.AllWithDeleted().FirstOrDefault(x => x.Id == id);
            lecture.Name = input.Name;
            lecture.Content = input.Content;
            lecture.Difficulty = input.Difficulty;
            lecture.ReadTime = TimeSpan.FromMinutes(input.ReadTime);
            lecture.CourseId = input.CourseId;
            lecture.IsDeleted = input.IsDeleted;

            this.lecturesRepository.Update(lecture);
            await this.lecturesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var lecture = this.lecturesRepository.AllWithDeleted().FirstOrDefault(l => l.Id == id);
            this.lecturesRepository.Delete(lecture);
            await this.lecturesRepository.SaveChangesAsync();
        }

        public bool LectureExist(int id)
        {
            return this.lecturesRepository.AllWithDeleted().Any(e => e.Id == id);
        }
    }
}
