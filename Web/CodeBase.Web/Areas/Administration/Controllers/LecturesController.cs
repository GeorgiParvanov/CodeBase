namespace CodeBase.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using CodeBase.Data.Common.Repositories;
    using CodeBase.Data.Models;
    using CodeBase.Services.Data.Contracts;
    using CodeBase.Web.ViewModels.Administration.Courses;
    using CodeBase.Web.ViewModels.Administration.Lectures;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class LecturesController : AdministrationController
    {
        private const int ItemsPerPage = 2;
        private readonly ILecturesService lecturesService;
        private readonly ICoursesService coursesService;

        public LecturesController(
            ILecturesService lecturesService,
            ICoursesService coursesService)
        {
            this.lecturesService = lecturesService;
            this.coursesService = coursesService;
        }

        public IActionResult Index(int pageNumber)
        {
            if (pageNumber <= 0)
            {
                return this.NotFound();
            }

            var lectures = this.lecturesService.GetAllWithDeleted<LectureViewModel>(pageNumber, ItemsPerPage);

            var model = new LectureListViewModel
            {
                Lectures = lectures,
                ItemsPerPage = ItemsPerPage,
                PageNumber = pageNumber,
                EntitiesCount = this.lecturesService.GetCountWithDeleted(),
            };

            return this.View(model);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var model = this.lecturesService.GetByIdWithDeleted<LectureViewModel>((int)id);
            if (model == null)
            {
                return this.NotFound();
            }

            return this.View(model);
        }

        public IActionResult Create()
        {
            this.ViewData["SeeSharpCourses"] = new SelectList(this.coursesService.GetAll<CourseViewModel>(), "Id", "Name");
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LectureInputModel input)
        {
            if (this.ModelState.IsValid)
            {
                await this.lecturesService.CreateAsync(input);
                return this.RedirectToAction(nameof(this.Index), new { pageNumber = 1 });
            }

            this.ViewData["SeeSharpCourses"] = new SelectList(this.coursesService.GetAll<CourseViewModel>(), "Id", "Name", input.CourseId);
            return this.View(input);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var model = this.lecturesService.GetByIdWithDeleted<LectureInputModel>((int)id);
            if (model == null)
            {
                return this.NotFound();
            }

            this.ViewData["SeeSharpCourses"] = new SelectList(this.coursesService.GetAll<CourseViewModel>(), "Id", "Name", model.CourseName);
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LectureInputModel input)
        {
            if (id != input.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                await this.lecturesService.UpdateAsync(id, input);

                return this.RedirectToAction(nameof(this.Index), new { pageNumber = 1 });
            }

            this.ViewData["SeeSharpCourses"] = new SelectList(this.coursesService.GetAll<CourseViewModel>(), "Id", "Name", input.CourseName);
            return this.View(input);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var model = this.lecturesService.GetByIdWithDeleted<LectureViewModel>((int)id);
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
            await this.lecturesService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.Index), new { pageNumber = 1 });
        }

        private bool LectureExists(int id)
        {
            return this.lecturesService.LectureExist(id);
        }
    }
}
