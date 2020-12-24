namespace CodeBase.Web.Controllers
{
    using CodeBase.Services.Data.Contracts;
    using CodeBase.Web.ViewModels.Courses;
    using Microsoft.AspNetCore.Mvc;

    public class CoursesController : BaseController
    {
        private const int ItemsPerPage = 1;
        private readonly ICoursesService coursesService;

        public CoursesController(ICoursesService coursesService)
        {
            this.coursesService = coursesService;
        }

        public IActionResult Index(int pageNumber)
        {
            if (pageNumber <= 0)
            {
                return this.NotFound();
            }

            var courses = this.coursesService.GetAll<CoursesViewModel>(pageNumber, ItemsPerPage);
            var model = new CoursesListViewModel
            {
                Courses = courses,
                ItemsPerPage = ItemsPerPage,
                PageNumber = pageNumber,
                EntitiesCount = this.coursesService.GetCount(),
            };

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
