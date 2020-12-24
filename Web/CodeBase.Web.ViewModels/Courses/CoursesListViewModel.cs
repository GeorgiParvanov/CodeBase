namespace CodeBase.Web.ViewModels.Courses
{
    using System.Collections.Generic;

    public class CoursesListViewModel : PagingViewModel
    {
        public IEnumerable<CoursesViewModel> Courses { get; set; }
    }
}
