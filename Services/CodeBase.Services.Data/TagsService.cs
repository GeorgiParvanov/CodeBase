namespace CodeBase.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CodeBase.Data.Common.Repositories;
    using CodeBase.Data.Models;
    using CodeBase.Services.Data.Contracts;
    using CodeBase.Services.Mapping;
    using CodeBase.Web.ViewModels.Administration.Tags;

    public class TagsService : ITagsService
    {
        private readonly IRepository<Tag> tagsRepository;

        public TagsService(IRepository<Tag> tagsRepository)
        {
            this.tagsRepository = tagsRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.tagsRepository.All()
                .To<T>()
                .ToList();
        }

        public T GetById<T>(int id)
        {
            return this.tagsRepository.All()
                .Where(c => c.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public async Task Create(TagInputModel model)
        {
            var tag = new Tag()
            {
                Name = model.Name,
            };

            await this.tagsRepository.AddAsync(tag);
            await this.tagsRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, TagInputModel input)
        {
            var tag = this.tagsRepository.All().FirstOrDefault(x => x.Id == id);
            tag.Name = input.Name;

            await this.tagsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var tag = this.tagsRepository.All().FirstOrDefault(c => c.Id == id);
            this.tagsRepository.Delete(tag);
            await this.tagsRepository.SaveChangesAsync();
        }

        public bool TagExists(int id)
        {
            return this.tagsRepository.All().Any(e => e.Id == id);
        }
    }
}
