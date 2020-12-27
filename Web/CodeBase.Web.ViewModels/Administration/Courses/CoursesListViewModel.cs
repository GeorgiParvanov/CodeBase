namespace CodeBase.Web.ViewModels.Administration.Courses
{
    using System.Collections.Generic;

    public class CoursesListViewModel : PagingViewModel
    {
        public IEnumerable<CourseViewModel> Courses { get; set; }
    }
}
