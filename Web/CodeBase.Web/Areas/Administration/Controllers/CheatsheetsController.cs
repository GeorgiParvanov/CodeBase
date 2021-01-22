namespace CodeBase.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using CodeBase.Data;
    using CodeBase.Data.Common.Repositories;
    using CodeBase.Data.Models;
    using CodeBase.Services.Data.Contracts;
    using CodeBase.Web.ViewModels.Administration.Cheatsheets;
    using CodeBase.Web.ViewModels.Administration.Courses;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class CheatsheetsController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Cheatsheet> cheatsheetRepository;
        private readonly IDeletableEntityRepository<Course> courseRepository;
        private readonly ICheatsheetService cheatsheetService;
        private readonly ICoursesService coursesService;

        public CheatsheetsController(
            IDeletableEntityRepository<Cheatsheet> cheatsheetRepository,
            IDeletableEntityRepository<Course> courseRepository,
            ICheatsheetService cheatsheetService,
            ICoursesService coursesService)
        {
            this.cheatsheetRepository = cheatsheetRepository;
            this.courseRepository = courseRepository;
            this.cheatsheetService = cheatsheetService;
            this.coursesService = coursesService;
        }

        public IActionResult Index()
        {
            var cheatsheets = this.cheatsheetService.GetAllWithDeleted<CheatsheetViewModel>();

            var model = new CheatsheetListViewModel()
            {
                Cheatsheets = cheatsheets,
            };

            return this.View(model);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var model = this.cheatsheetService.GetByIdWithDeleted<CheatsheetViewModel>((int)id);

            if (model == null)
            {
                return this.NotFound();
            }

            return this.View(model);
        }

        public IActionResult Create()
        {
            this.ViewData["CourseNames"] = new SelectList(this.coursesService.GetAllWithDeleted<CourseViewModel>(), "Id", "Name");
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CheatsheetInputModel input)
        {
            if (this.ModelState.IsValid)
            {
                await this.cheatsheetService.Create(input);
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["CourseNames"] = new SelectList(this.coursesService.GetAllWithDeleted<CourseViewModel>(), "Id", "Name", input.CourseId);
            return this.View(input);
        }

        // GET: Administration/Cheatsheets/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var model = this.cheatsheetService.GetByIdWithDeleted<CheatsheetInputModel>((int)id);
            if (model == null)
            {
                return this.NotFound();
            }

            this.ViewData["CourseNames"] = new SelectList(this.coursesService.GetAllWithDeleted<CourseViewModel>(), "Id", "Name", model.CourseId);
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CheatsheetInputModel model)
        {
            if (id != model.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    await this.cheatsheetService.UpdateAsync(id, model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.CheatsheetExists(model.Id))
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

            this.ViewData["CourseNames"] = new SelectList(this.coursesService.GetAllWithDeleted<CourseViewModel>(), "Id", "Name", model.CourseId);
            return this.View(model);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var model = this.cheatsheetService.GetByIdWithDeleted<CheatsheetViewModel>((int)id);
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
            await this.cheatsheetService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool CheatsheetExists(int id)
        {
            return this.cheatsheetService.CheatsheetExists(id);
        }
    }
}
