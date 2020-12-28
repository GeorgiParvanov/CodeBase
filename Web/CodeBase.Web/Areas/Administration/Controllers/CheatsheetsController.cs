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

    public class CheatsheetsController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Cheatsheet> cheatsheetRepository;
        private readonly IDeletableEntityRepository<Course> courseRepository;

        public CheatsheetsController(IDeletableEntityRepository<Cheatsheet> cheatsheetRepository, IDeletableEntityRepository<Course> courseRepository)
        {
            this.cheatsheetRepository = cheatsheetRepository;
            this.courseRepository = courseRepository;
        }

        // GET: Administration/Cheatsheets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this.cheatsheetRepository.AllWithDeleted().Include(c => c.Course);
            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/Cheatsheets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var cheatsheet = await this.cheatsheetRepository.AllWithDeleted()
                .Include(c => c.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cheatsheet == null)
            {
                return this.NotFound();
            }

            return this.View(cheatsheet);
        }

        // GET: Administration/Cheatsheets/Create
        public IActionResult Create()
        {
            this.ViewData["CourseId"] = new SelectList(this.courseRepository.AllWithDeleted(), "Id", "Id");
            return this.View();
        }

        // POST: Administration/Cheatsheets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Content,CourseId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Cheatsheet cheatsheet)
        {
            if (this.ModelState.IsValid)
            {
                await this.cheatsheetRepository.AddAsync(cheatsheet);
                await this.cheatsheetRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["CourseId"] = new SelectList(this.courseRepository.AllWithDeleted(), "Id", "Id", cheatsheet.CourseId);
            return this.View(cheatsheet);
        }

        // GET: Administration/Cheatsheets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var cheatsheet = this.cheatsheetRepository.AllWithDeleted().FirstOrDefault(c => c.Id == id);
            if (cheatsheet == null)
            {
                return this.NotFound();
            }

            this.ViewData["CourseId"] = new SelectList(this.courseRepository.AllWithDeleted(), "Id", "Id", cheatsheet.CourseId);
            return this.View(cheatsheet);
        }

        // POST: Administration/Cheatsheets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Content,CourseId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Cheatsheet cheatsheet)
        {
            if (id != cheatsheet.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.cheatsheetRepository.Update(cheatsheet);
                    await this.cheatsheetRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.CheatsheetExists(cheatsheet.Id))
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

            this.ViewData["CourseId"] = new SelectList(this.courseRepository.AllWithDeleted(), "Id", "Id", cheatsheet.CourseId);
            return this.View(cheatsheet);
        }

        // GET: Administration/Cheatsheets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var cheatsheet = await this.cheatsheetRepository.AllWithDeleted()
                .Include(c => c.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cheatsheet == null)
            {
                return this.NotFound();
            }

            return this.View(cheatsheet);
        }

        // POST: Administration/Cheatsheets/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cheatsheet = this.cheatsheetRepository.AllWithDeleted().FirstOrDefault(c => c.Id == id);
            this.cheatsheetRepository.Delete(cheatsheet);
            await this.cheatsheetRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool CheatsheetExists(int id)
        {
            return this.cheatsheetRepository.AllWithDeleted().Any(e => e.Id == id);
        }
    }
}
