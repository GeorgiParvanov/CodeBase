namespace CodeBase.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using CodeBase.Data;
    using CodeBase.Data.Common.Repositories;
    using CodeBase.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class CoursesController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Course> courseRepository;

        public CoursesController(IDeletableEntityRepository<Course> courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        // GET: Administration/Courses
        public async Task<IActionResult> Index()
        {
            return this.View(await this.courseRepository.AllWithDeleted().ToListAsync());
        }

        // GET: Administration/Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var course = await this.courseRepository.AllWithDeleted()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return this.NotFound();
            }

            return this.View(course);
        }

        // GET: Administration/Courses/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Price,Difficulty,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Course course)
        {
            if (this.ModelState.IsValid)
            {
                await this.courseRepository.AddAsync(course);
                await this.courseRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(course);
        }

        // GET: Administration/Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var course = this.courseRepository.AllWithDeleted().FirstOrDefault(c => c.Id == id);
            if (course == null)
            {
                return this.NotFound();
            }

            return this.View(course);
        }

        // POST: Administration/Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,Price,Difficulty,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Course course)
        {
            if (id != course.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.courseRepository.Update(course);
                    await this.courseRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.CourseExists(course.Id))
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

            return this.View(course);
        }

        // GET: Administration/Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var course = await this.courseRepository.AllWithDeleted()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return this.NotFound();
            }

            return this.View(course);
        }

        // POST: Administration/Courses/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = this.courseRepository.AllWithDeleted().FirstOrDefault(c => c.Id == id);
            this.courseRepository.Delete(course);
            await this.courseRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool CourseExists(int id)
        {
            return this.courseRepository.All().Any(e => e.Id == id);
        }
    }
}
