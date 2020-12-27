namespace CodeBase.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using CodeBase.Data;
    using CodeBase.Data.Common.Repositories;
    using CodeBase.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class TagsController : AdministrationController
    {
        private readonly IRepository<Tag> tagRepository;

        public TagsController(IRepository<Tag> tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        // GET: Administration/Tags
        public async Task<IActionResult> Index()
        {
            return this.View(await this.tagRepository.All().ToListAsync());
        }

        // GET: Administration/Tags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var tag = await this.tagRepository.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tag == null)
            {
                return this.NotFound();
            }

            return this.View(tag);
        }

        // GET: Administration/Tags/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Tags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id,CreatedOn,ModifiedOn")] Tag tag)
        {
            if (this.ModelState.IsValid)
            {
                await this.tagRepository.AddAsync(tag);
                await this.tagRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(tag);
        }

        // GET: Administration/Tags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var tag = this.tagRepository.All().FirstOrDefault(t => t.Id == id);
            if (tag == null)
            {
                return this.NotFound();
            }

            return this.View(tag);
        }

        // POST: Administration/Tags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Id,CreatedOn,ModifiedOn")] Tag tag)
        {
            if (id != tag.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.tagRepository.Update(tag);
                    await this.tagRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.TagExists(tag.Id))
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

            return this.View(tag);
        }

        // GET: Administration/Tags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var tag = await this.tagRepository.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tag == null)
            {
                return this.NotFound();
            }

            return this.View(tag);
        }

        // POST: Administration/Tags/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tag = this.tagRepository.All().FirstOrDefault(t => t.Id == id);
            this.tagRepository.Delete(tag);
            await this.tagRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool TagExists(int id)
        {
            return this.tagRepository.All().Any(e => e.Id == id);
        }
    }
}
