namespace CodeBase.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CodeBase.Data.Common.Repositories;
    using CodeBase.Data.Models;
    using CodeBase.Services.Data.Contracts;
    using CodeBase.Services.Mapping;
    using CodeBase.Web.ViewModels.Administration.CourseTags;

    public class CourseTagsService : ICourseTagsService
    {
        private readonly IDeletableEntityRepository<CourseTag> courseTagsRepository;

        public CourseTagsService(IDeletableEntityRepository<CourseTag> courseTagsRepository)
        {
            this.courseTagsRepository = courseTagsRepository;
        }

        public IEnumerable<T> GetAllWithDeleted<T>()
        {
            return this.courseTagsRepository.AllWithDeleted().To<T>().ToList();
        }

        public T GetByIdWithDeleted<T>(int id)
        {
            return this.courseTagsRepository.AllWithDeleted()
                .Where(c => c.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public async Task Create(CourseTagInputModel model)
        {
            var courseTag = new CourseTag()
            {
                CourseId = model.CourseId,
                TagId = model.TagId,
            };

            await this.courseTagsRepository.AddAsync(courseTag);
            await this.courseTagsRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, CourseTagInputModel input)
        {
            var courseTag = this.courseTagsRepository.AllWithDeleted().FirstOrDefault(x => x.Id == id);
            courseTag.CourseId = input.CourseId;
            courseTag.TagId = input.TagId;
            await this.courseTagsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var courseTag = this.courseTagsRepository.AllWithDeleted().FirstOrDefault(c => c.Id == id);
            this.courseTagsRepository.Delete(courseTag);
            await this.courseTagsRepository.SaveChangesAsync();
        }

        public bool CheatsheetExists(int id)
        {
            return this.courseTagsRepository.AllWithDeleted().Any(e => e.Id == id);
        }

        public bool CourseTagExists(int id)
        {
            return this.courseTagsRepository.AllWithDeleted().Any(ct => ct.Id == id);
        }
    }
}
