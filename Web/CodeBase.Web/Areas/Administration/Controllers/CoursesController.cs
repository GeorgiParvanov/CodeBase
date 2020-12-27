namespace CodeBase.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using CodeBase.Data;
    using CodeBase.Data.Common.Repositories;
    using CodeBase.Data.Models;
    using CodeBase.Services.Data.Contracts;
    using CodeBase.Web.ViewModels.Administration.Courses;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class CoursesController : AdministrationController
    {
        private const int ItemsPerPage = 2;
        private readonly IDeletableEntityRepository<Course> courseRepository;
        private readonly ICoursesService coursesService;
        private readonly UserManager<ApplicationUser> userManager;

        public CoursesController(
            IDeletableEntityRepository<Course> courseRepository,
            ICoursesService coursesService,
            UserManager<ApplicationUser> userManager)
        {
            this.courseRepository = courseRepository;
            this.coursesService = coursesService;
            this.userManager = userManager;
        }

        public IActionResult Index(int pageNumber)
        {
            if (pageNumber <= 0)
            {
                return this.NotFound();
            }

            var courses = this.coursesService.GetAllWithDeleted<CourseViewModel>(pageNumber, ItemsPerPage);

            var model = new CoursesListViewModel
            {
                Courses = courses,
                ItemsPerPage = ItemsPerPage,
                PageNumber = pageNumber,
                EntitiesCount = this.coursesService.GetCountWithDeleted(),
            };

            return this.View(model);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var model = this.coursesService.GetByIdWithDeleted<CourseViewModel>((int)id);

            if (model == null)
            {
                return this.NotFound();
            }

            return this.View(model);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseInputModel input)
        {
            if (this.ModelState.IsValid)
            {
                await this.coursesService.Create(input);

                return this.RedirectToAction(nameof(this.Index), new { pageNumber = 1 });
            }

            return this.View(input);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var course = this.coursesService.GetByIdWithDeleted<CourseViewModel>((int)id);

            if (course == null)
            {
                return this.NotFound();
            }

            return this.View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseInputModel input)
        {
            if (id != input.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                await this.coursesService.UpdateAsync(id, input);

                return this.RedirectToAction(nameof(this.Index), new { pageNumber = 1 });
            }

            return this.View(input);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var model = this.coursesService.GetByIdWithDeleted<CourseViewModel>((int)id);

            if (model == null)
            {
                return this.NotFound();
            }

            return this.View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.coursesService.DeleteAsync(id);

            return this.RedirectToAction(nameof(this.Index), new { pageNumber = 1 });
        }

        private bool CourseExists(int id)
        {
            return this.coursesService.CourseExist(id);
        }
    }
}
