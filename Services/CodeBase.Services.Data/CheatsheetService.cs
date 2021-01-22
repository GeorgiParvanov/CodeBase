namespace CodeBase.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CodeBase.Data.Common.Repositories;
    using CodeBase.Data.Models;
    using CodeBase.Services.Data.Contracts;
    using CodeBase.Services.Mapping;
    using CodeBase.Web.ViewModels.Administration.Cheatsheets;

    public class CheatsheetService : ICheatsheetService
    {
        private readonly IDeletableEntityRepository<Cheatsheet> cheatsheetRepository;

        public CheatsheetService(IDeletableEntityRepository<Cheatsheet> cheatsheetRepository)
        {
            this.cheatsheetRepository = cheatsheetRepository;
        }

        public async Task Create(CheatsheetInputModel model)
        {
            var cheatsheet = new Cheatsheet()
            {
                Content = model.Content,
                CourseId = model.CourseId,
            };

            await this.cheatsheetRepository.AddAsync(cheatsheet);
            await this.cheatsheetRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.cheatsheetRepository.All().To<T>().ToList();
        }

        public IEnumerable<T> GetAllWithDeleted<T>()
        {
            return this.cheatsheetRepository.AllWithDeleted().To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            return this.cheatsheetRepository.All()
                .Where(c => c.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public T GetByIdWithDeleted<T>(int id)
        {
            return this.cheatsheetRepository.AllWithDeleted()
                .Where(c => c.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public int GetCount()
        {
            return this.cheatsheetRepository.AllAsNoTracking().Count();
        }

        public async Task UpdateAsync(int id, CheatsheetInputModel input)
        {
            var cheatsheet = this.cheatsheetRepository.AllWithDeleted().FirstOrDefault(x => x.Id == id);
            cheatsheet.Content = input.Content;
            cheatsheet.CourseId = input.CourseId;
            cheatsheet.IsDeleted = input.IsDeleted;
            await this.cheatsheetRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var cheatsheet = this.cheatsheetRepository.AllWithDeleted().FirstOrDefault(c => c.Id == id);
            this.cheatsheetRepository.Delete(cheatsheet);
            await this.cheatsheetRepository.SaveChangesAsync();
        }

        public bool CheatsheetExists(int id)
        {
            return this.cheatsheetRepository.AllWithDeleted().Any(e => e.Id == id);
        }
    }
}
