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

    public class UserCoursesController : AdministrationController
    {
        private readonly IDeletableEntityRepository<UserCourse> userCourseRepository;
        private readonly IDeletableEntityRepository<Course> courseRepository;

        public UserCoursesController(IDeletableEntityRepository<UserCourse> userCourseRepository, IDeletableEntityRepository<Course> courseRepository)
        {
            this.userCourseRepository = userCourseRepository;
            this.courseRepository = courseRepository;
        }

        // GET: Administration/UserCourses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this.userCourseRepository.AllWithDeleted().Include(u => u.Course);
            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/UserCourses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var userCourse = await this.userCourseRepository.AllWithDeleted()
                .Include(u => u.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userCourse == null)
            {
                return this.NotFound();
            }

            return this.View(userCourse);
        }

        // GET: Administration/UserCourses/Create
        public IActionResult Create()
        {
            this.ViewData["CourseId"] = new SelectList(this.courseRepository.AllWithDeleted(), "Id", "Id");
            return this.View();
        }

        // POST: Administration/UserCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseId,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] UserCourse userCourse)
        {
            if (this.ModelState.IsValid)
            {
                await this.userCourseRepository.AddAsync(userCourse);
                await this.userCourseRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["CourseId"] = new SelectList(this.courseRepository.AllWithDeleted(), "Id", "Id", userCourse.CourseId);
            return this.View(userCourse);
        }

        // GET: Administration/UserCourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var userCourse = this.userCourseRepository.AllWithDeleted().FirstOrDefault(uc => uc.Id == id);
            if (userCourse == null)
            {
                return this.NotFound();
            }

            this.ViewData["CourseId"] = new SelectList(this.courseRepository.AllWithDeleted(), "Id", "Id", userCourse.CourseId);
            return this.View(userCourse);
        }

        // POST: Administration/UserCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseId,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] UserCourse userCourse)
        {
            if (id != userCourse.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.userCourseRepository.Update(userCourse);
                    await this.userCourseRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.UserCourseExists(userCourse.Id))
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

            this.ViewData["CourseId"] = new SelectList(this.courseRepository.AllWithDeleted(), "Id", "Id", userCourse.CourseId);
            return this.View(userCourse);
        }

        // GET: Administration/UserCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var userCourse = await this.userCourseRepository.AllWithDeleted()
                .Include(u => u.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userCourse == null)
            {
                return this.NotFound();
            }

            return this.View(userCourse);
        }

        // POST: Administration/UserCourses/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userCourse = this.userCourseRepository.AllWithDeleted().FirstOrDefault(uc => uc.Id == id);
            this.userCourseRepository.Delete(userCourse);
            await this.userCourseRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool UserCourseExists(int id)
        {
            return this.userCourseRepository.AllWithDeleted().Any(e => e.Id == id);
        }
    }
}
