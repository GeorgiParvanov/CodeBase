namespace CodeBase.Web.Controllers
{
    using CodeBase.Services.Data.Contracts;
    using CodeBase.Web.ViewModels.Courses;
    using Microsoft.AspNetCore.Mvc;

    public class CoursesController : BaseController
    {
        private readonly ICoursesService coursesService;

        public CoursesController(ICoursesService coursesService)
        {
            this.coursesService = coursesService;
        }

        public IActionResult Index()
        {
            var courses = this.coursesService.GetAll<CoursesViewModel>();
            var model = new CoursesListViewModel { Courses = courses };

            return this.View(model);
        }
    }
}
