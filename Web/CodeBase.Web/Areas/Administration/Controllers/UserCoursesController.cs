namespace CodeBase.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using CodeBase.Data;
    using CodeBase.Data.Common.Repositories;
    using CodeBase.Data.Models;
    using CodeBase.Services.Data.Contracts;
    using CodeBase.Web.ViewModels.Administration.Courses;
    using CodeBase.Web.ViewModels.Administration.UserCourses;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class UserCoursesController : AdministrationController
    {
        private readonly IUserCoursesService userCoursesService;
        private readonly ICoursesService coursesService;

        public UserCoursesController(
            IUserCoursesService userCoursesService,
            ICoursesService coursesService)
        {
            this.userCoursesService = userCoursesService;
            this.coursesService = coursesService;
        }

        public IActionResult Index()
        {
            var userCourses = this.userCoursesService.GetAll<UserCourseViewModel>();

            var model = new UserCoursesListViewModel
            {
                UserCourses = userCourses,
            };

            return this.View(model);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var model = this.userCoursesService.GetById<UserCourseViewModel>((int)id);

            if (model == null)
            {
                return this.NotFound();
            }

            return this.View(model);
        }

        public IActionResult Create()
        {
            this.ViewData["CourseId"] = new SelectList(this.coursesService.GetAll<CourseViewModel>(), "Id", "Name");
            return this.View();
        }

        // POST: Administration/UserCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCourseInputModel input)
        {
            if (this.ModelState.IsValid)
            {
                await this.userCoursesService.AddAsync(input);
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["CourseId"] = new SelectList(this.coursesService.GetAll<CourseViewModel>(), "Id", "Name", input.CourseName);
            return this.View(input);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var userCourse = this.userCoursesService.GetById<UserCourseInputModel>((int)id);
            if (userCourse == null)
            {
                return this.NotFound();
            }

            this.ViewData["CourseId"] = new SelectList(this.coursesService.GetAll<CourseViewModel>(), "Id", "Name", userCourse.CourseName);
            return this.View(userCourse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserCourseInputModel input)
        {
            if (id != input.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    await this.userCoursesService.Update(input);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.UserCourseExists(input.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["CourseId"] = new SelectList(this.coursesService.GetAll<CourseViewModel>(), "Id", "Name", input.CourseName);
            return this.View(input);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var userCourse = this.userCoursesService.GetById<UserCourseViewModel>((int)id);
            if (userCourse == null)
            {
                return this.NotFound();
            }

            return this.View(userCourse);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.userCoursesService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool UserCourseExists(int id)
        {
            return this.userCoursesService.UserCourseExists(id);
        }
    }
}
