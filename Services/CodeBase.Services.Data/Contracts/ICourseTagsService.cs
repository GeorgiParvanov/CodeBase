namespace CodeBase.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CodeBase.Web.ViewModels.Administration.CourseTags;

    public interface ICourseTagsService
    {
        IEnumerable<T> GetAllWithDeleted<T>();

        T GetByIdWithDeleted<T>(int id);

        Task Create(CourseTagInputModel model);

        Task UpdateAsync(int id, CourseTagInputModel input);

        Task DeleteAsync(int id);

        bool CourseTagExists(int id);
    }
}
