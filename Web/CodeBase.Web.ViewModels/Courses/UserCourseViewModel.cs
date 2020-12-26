namespace CodeBase.Web.ViewModels.Courses
{
    using CodeBase.Data.Models;
    using CodeBase.Services.Mapping;

    public class UserCourseViewModel : IMapFrom<UserCourse>
    {
        public string UserId { get; set; }

        public int CourseId { get; set; }
    }
}
