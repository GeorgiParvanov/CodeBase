namespace CodeBase.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CodeBase.Web.ViewModels.Administration.Lectures;

    public interface ILecturesService
    {
        int GetCount();

        IEnumerable<T> GetAll<T>();

        T GetById<T>(int id);

        IEnumerable<T> GetAllWithDeleted<T>(int pageNumber, int itemsPerPage);

        int GetCountWithDeleted();

        T GetByIdWithDeleted<T>(int id);

        Task CreateAsync(LectureInputModel input);

        Task UpdateAsync(int id, LectureInputModel input);

        Task DeleteAsync(int id);

        bool LectureExist(int id);
    }
}
