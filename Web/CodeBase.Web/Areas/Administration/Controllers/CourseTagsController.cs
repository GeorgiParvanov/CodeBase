namespace CodeBase.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using CodeBase.Data;
    using CodeBase.Data.Common.Repositories;
    using CodeBase.Data.Models;
    using CodeBase.Services.Data.Contracts;
    using CodeBase.Web.ViewModels.Administration.Courses;
    using CodeBase.Web.ViewModels.Administration.CourseTags;
    using CodeBase.Web.ViewModels.Administration.Tags;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class CourseTagsController : AdministrationController
    {
        private readonly IDeletableEntityRepository<CourseTag> courseTagRepository;
        private readonly IDeletableEntityRepository<Course> courseRepository;
        private readonly IRepository<Tag> tagRepository;
        private readonly ICourseTagsService courseTagsService;
        private readonly ICoursesService coursesService;
        private readonly ITagsService tagsService;

        public CourseTagsController(
            IDeletableEntityRepository<CourseTag> courseTagRepository,
            IDeletableEntityRepository<Course> courseRepository,
            IRepository<Tag> tagRepository,
            ICourseTagsService courseTagsService,
            ICoursesService coursesService,
            ITagsService tagsService)
        {
            this.courseTagRepository = courseTagRepository;
            this.courseRepository = courseRepository;
            this.tagRepository = tagRepository;
            this.courseTagsService = courseTagsService;
            this.coursesService = coursesService;
            this.tagsService = tagsService;
        }

        public IActionResult Index()
        {
            var courseTags = this.courseTagsService.GetAllWithDeleted<CourseTagViewModel>();
            var model = new CourseTagListViewModel() { CourseTags = courseTags };

            return this.View(model);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var courseTag = this.courseTagsService.GetByIdWithDeleted<CourseTagViewModel>((int)id);
            if (courseTag == null)
            {
                return this.NotFound();
            }

            return this.View(courseTag);
        }

        public IActionResult Create()
        {
            this.ViewData["CourseNames"] = new SelectList(this.coursesService.GetAllWithDeleted<CourseViewModel>(), "Id", "Name");
            this.ViewData["TagNames"] = new SelectList(this.tagsService.GetAll<TagViewModel>(), "Id", "Name");
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseTagInputModel input)
        {
            if (this.ModelState.IsValid)
            {
                await this.courseTagsService.Create(input);
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["CourseNames"] = new SelectList(this.coursesService.GetAllWithDeleted<CourseViewModel>(), "Id", "Name", input.CourseName);
            return this.View(input);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var model = this.courseTagsService.GetByIdWithDeleted<CourseTagInputModel>((int)id);
            if (model == null)
            {
                return this.NotFound();
            }

            this.ViewData["CourseNames"] = new SelectList(this.coursesService.GetAll<CourseViewModel>(), "Id", "Name", model.CourseName);
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseTagInputModel input)
        {
            if (id != input.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    await this.courseTagsService.UpdateAsync(id, input);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.CourseTagExists(input.Id))
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

            this.ViewData["CourseNames"] = new SelectList(this.coursesService.GetAll<CourseViewModel>(), "Id", "Name", input.CourseName);
            return this.View(input);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var model = this.courseTagsService.GetByIdWithDeleted<CourseTagInputModel>((int)id);
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
            await this.courseTagsService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool CourseTagExists(int id)
        {
            return this.courseTagsService.CourseTagExists(id);
        }
    }
}
