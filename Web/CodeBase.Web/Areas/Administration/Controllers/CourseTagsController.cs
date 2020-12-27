namespace CodeBase.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using CodeBase.Data;
    using CodeBase.Data.Common.Repositories;
    using CodeBase.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class CourseTagsController : AdministrationController
    {
        private readonly IDeletableEntityRepository<CourseTag> courseTagRepository;
        private readonly IDeletableEntityRepository<Course> courseRepository;

        public CourseTagsController(IDeletableEntityRepository<CourseTag> courseTagRepository, IDeletableEntityRepository<Course> courseRepository)
        {
            this.courseTagRepository = courseTagRepository;
            this.courseRepository = courseRepository;
        }

        // GET: Administration/CourseTags
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this.courseTagRepository.AllWithDeleted().Include(c => c.Course);
            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/CourseTags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var courseTag = await this.courseTagRepository.AllWithDeleted()
                .Include(c => c.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseTag == null)
            {
                return this.NotFound();
            }

            return this.View(courseTag);
        }

        // GET: Administration/CourseTags/Create
        public IActionResult Create()
        {
            this.ViewData["CourseId"] = new SelectList(this.courseRepository.AllWithDeleted(), "Id", "Id");
            return this.View();
        }

        // POST: Administration/CourseTags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseId,TagId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] CourseTag courseTag)
        {
            if (this.ModelState.IsValid)
            {
                await this.courseTagRepository.AddAsync(courseTag);
                await this.courseTagRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["CourseId"] = new SelectList(this.courseRepository.AllWithDeleted(), "Id", "Id", courseTag.CourseId);
            return this.View(courseTag);
        }

        // GET: Administration/CourseTags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var courseTag = this.courseTagRepository.AllWithDeleted().FirstOrDefault(ct => ct.Id == id);
            if (courseTag == null)
            {
                return this.NotFound();
            }

            this.ViewData["CourseId"] = new SelectList(this.courseRepository.AllWithDeleted(), "Id", "Id", courseTag.CourseId);
            return this.View(courseTag);
        }

        // POST: Administration/CourseTags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseId,TagId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] CourseTag courseTag)
        {
            if (id != courseTag.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.courseTagRepository.Update(courseTag);
                    await this.courseTagRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.CourseTagExists(courseTag.Id))
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

            this.ViewData["CourseId"] = new SelectList(this.courseRepository.AllWithDeleted(), "Id", "Id", courseTag.CourseId);
            return this.View(courseTag);
        }

        // GET: Administration/CourseTags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var courseTag = await this.courseTagRepository.AllWithDeleted()
                .Include(c => c.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseTag == null)
            {
                return this.NotFound();
            }

            return this.View(courseTag);
        }

        // POST: Administration/CourseTags/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseTag = this.courseTagRepository.AllWithDeleted().FirstOrDefault(ct => ct.Id == id);
            this.courseTagRepository.Delete(courseTag);
            await this.courseTagRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool CourseTagExists(int id)
        {
            return this.courseTagRepository.AllWithDeleted().Any(e => e.Id == id);
        }
    }
}
