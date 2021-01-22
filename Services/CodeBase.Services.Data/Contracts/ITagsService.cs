namespace CodeBase.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CodeBase.Web.ViewModels.Administration.Tags;

    public interface ITagsService
    {
        IEnumerable<T> GetAll<T>();

        T GetById<T>(int id);

        Task Create(TagInputModel model);

        Task UpdateAsync(int id, TagInputModel input);

        Task DeleteAsync(int id);

        bool TagExists(int id);
    }
}
