namespace CodeBase.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CodeBase.Data.Common.Repositories;
    using CodeBase.Data.Models;
    using CodeBase.Services.Data.Contracts;
    using CodeBase.Services.Mapping;

    public class CheatsheetService : ICheatsheetService
    {
        private readonly IDeletableEntityRepository<Cheatsheet> cheatsheetRepository;

        public CheatsheetService(IDeletableEntityRepository<Cheatsheet> cheatsheetRepository)
        {
            this.cheatsheetRepository = cheatsheetRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.cheatsheetRepository.All().To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            return this.cheatsheetRepository.All()
                .Where(c => c.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public int GetCount()
        {
            return this.cheatsheetRepository.AllAsNoTracking().Count();
        }
    }
}
