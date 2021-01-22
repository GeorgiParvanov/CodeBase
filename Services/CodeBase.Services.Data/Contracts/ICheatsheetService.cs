namespace CodeBase.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CodeBase.Web.ViewModels.Administration.Cheatsheets;

    public interface ICheatsheetService
    {
        int GetCount();

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAllWithDeleted<T>();

        T GetById<T>(int id);

        T GetByIdWithDeleted<T>(int id);

        Task Create(CheatsheetInputModel model);

        Task UpdateAsync(int id, CheatsheetInputModel input);

        Task DeleteAsync(int id);

        bool CheatsheetExists(int id);
    }
}
