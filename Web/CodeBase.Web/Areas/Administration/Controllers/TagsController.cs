namespace CodeBase.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using CodeBase.Data;
    using CodeBase.Data.Common.Repositories;
    using CodeBase.Data.Models;
    using CodeBase.Services.Data.Contracts;
    using CodeBase.Web.ViewModels.Administration.Tags;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class TagsController : AdministrationController
    {
        private readonly IRepository<Tag> tagRepository;
        private readonly ITagsService tagsService;

        public TagsController(IRepository<Tag> tagRepository, ITagsService tagsService)
        {
            this.tagRepository = tagRepository;
            this.tagsService = tagsService;
        }

        public IActionResult Index()
        {
            var tags = this.tagsService.GetAll<TagViewModel>();
            var model = new TagListViewModel() { Tags = tags };

            return this.View(model);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var model = this.tagsService.GetById<TagViewModel>((int)id);
            if (model == null)
            {
                return this.NotFound();
            }

            return this.View(model);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TagInputModel input)
        {
            if (this.ModelState.IsValid)
            {
                await this.tagsService.Create(input);
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(input);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var model = this.tagsService.GetById<TagInputModel>((int)id);
            if (model == null)
            {
                return this.NotFound();
            }

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TagInputModel input)
        {
            if (id != input.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    await this.tagsService.UpdateAsync(id, input);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.TagExists(input.Id))
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

            return this.View(input);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var tag = this.tagsService.GetById<TagViewModel>((int)id);
            if (tag == null)
            {
                return this.NotFound();
            }

            return this.View(tag);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.tagsService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool TagExists(int id)
        {
            return this.tagsService.TagExists(id);
        }
    }
}
