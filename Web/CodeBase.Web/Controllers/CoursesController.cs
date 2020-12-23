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

        public IActionResult ByTag(string name)
        {
            var courses = this.coursesService.GetAllByTagName<CoursesViewModel>(name);
            var model = new CoursesListViewModel { Courses = courses };

            return this.View(model);
        }

        public IActionResult Course(int id)
        {
            var model = this.coursesService.GetById<CoursesViewModel>(id);

            return this.View(model);
        }
    }
}
