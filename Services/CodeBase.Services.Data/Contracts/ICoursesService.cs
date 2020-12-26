namespace CodeBase.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICoursesService
    {
        int GetCount();

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAll<T>(int pageNumber, int itemsPerPage);

        IEnumerable<T> GetAllByTagName<T>(string tagName);

        T GetById<T>(int id);

        decimal GetBalanceAmount(int courseId);

        Task AddUserToCourse(int courseId, string userId);
    }
}
