namespace CodeBase.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CodeBase.Web.ViewModels.Administration.Courses;

    public interface ICoursesService
    {
        int GetCount();

        int GetCountByTagName(string name);

        int GetCountWithDeleted();

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAll<T>(int pageNumber, int itemsPerPage);

        IEnumerable<T> GetAllWithDeleted<T>(int pageNumber, int itemsPerPage);

        IEnumerable<T> GetAllWithDeleted<T>();

        IEnumerable<T> GetAllByTagName<T>(string tagName);

        IEnumerable<T> GetAllByTagName<T>(string tagName, int pageNumber, int itemsPerPage);

        T GetById<T>(int id);

        T GetByIdWithDeleted<T>(int id);

        decimal GetBalanceAmount(int courseId);

        Task AddUserToCourse(int courseId, string userId);

        Task Create(CourseInputModel model);

        Task UpdateAsync(int id, CourseInputModel input);

        Task DeleteAsync(int id);

        bool CourseExist(int id);
    }
}
