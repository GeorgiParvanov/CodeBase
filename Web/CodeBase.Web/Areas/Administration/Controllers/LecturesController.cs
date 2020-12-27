namespace CodeBase.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using CodeBase.Data.Common.Repositories;
    using CodeBase.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class LecturesController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Lecture> lectureRepository;
        private readonly IDeletableEntityRepository<Course> courseRepository;

        public LecturesController(IDeletableEntityRepository<Lecture> lectureRepository, IDeletableEntityRepository<Course> courseRepository)
        {
            this.lectureRepository = lectureRepository;
            this.courseRepository = courseRepository;
        }

        // GET: Administration/Lectures
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this.lectureRepository.AllWithDeleted().Include(l => l.Course);
            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/Lectures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var lecture = await this.lectureRepository.AllWithDeleted()
                .Include(l => l.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lecture == null)
            {
                return this.NotFound();
            }

            return this.View(lecture);
        }

        // GET: Administration/Lectures/Create
        public IActionResult Create()
        {
            this.ViewData["CourseId"] = new SelectList(this.courseRepository.All(), "Id", "Id");
            return this.View();
        }

        // POST: Administration/Lectures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Content,Difficulty,ReadTime,CourseId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Lecture lecture)
        {
            if (this.ModelState.IsValid)
            {
                await this.lectureRepository.AddAsync(lecture);
                await this.lectureRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["CourseId"] = new SelectList(this.courseRepository.All(), "Id", "Id", lecture.CourseId);
            return this.View(lecture);
        }

        // GET: Administration/Lectures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var lecture = this.lectureRepository.AllWithDeleted().FirstOrDefault(l => l.Id == id);
            if (lecture == null)
            {
                return this.NotFound();
            }

            this.ViewData["CourseId"] = new SelectList(this.courseRepository.All(), "Id", "Id", lecture.CourseId);
            return this.View(lecture);
        }

        // POST: Administration/Lectures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Content,Difficulty,ReadTime,CourseId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Lecture lecture)
        {
            if (id != lecture.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.lectureRepository.Update(lecture);
                    await this.lectureRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.LectureExists(lecture.Id))
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

            this.ViewData["CourseId"] = new SelectList(this.courseRepository.All(), "Id", "Id", lecture.CourseId);
            return this.View(lecture);
        }

        // GET: Administration/Lectures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var lecture = await this.lectureRepository.AllWithDeleted()
                .Include(l => l.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lecture == null)
            {
                return this.NotFound();
            }

            return this.View(lecture);
        }

        // POST: Administration/Lectures/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lecture = this.lectureRepository.AllWithDeleted().FirstOrDefault(l => l.Id == id);
            this.lectureRepository.Delete(lecture);
            await this.lectureRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool LectureExists(int id)
        {
            return this.lectureRepository.AllWithDeleted().Any(e => e.Id == id);
        }
    }
}
