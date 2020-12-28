namespace CodeBase.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CodeBase.Web.ViewModels.Administration.UserCourses;

    public interface IUserCoursesService
    {
        IEnumerable<T> GetAll<T>();

        T GetById<T>(int id);

        Task AddAsync(UserCourseInputModel input);

        Task Update(UserCourseInputModel input);

        Task DeleteAsync(int id);

        bool UserCourseExists(int id);
    }
}
