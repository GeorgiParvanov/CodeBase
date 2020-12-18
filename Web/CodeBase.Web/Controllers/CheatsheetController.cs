namespace CodeBase.Web.Controllers
{
    using CodeBase.Services.Data.Contracts;
    using CodeBase.Web.ViewModels.Cheatsheet;
    using Microsoft.AspNetCore.Mvc;

    public class CheatsheetController : BaseController
    {
        private readonly ICheatsheetService cheatsheetService;

        public CheatsheetController(ICheatsheetService cheatsheetService)
        {
            this.cheatsheetService = cheatsheetService;
        }

        [Route("Cheatsheet/{id:int}")]
        public IActionResult Index(int id)
        {
            var model = this.cheatsheetService.GetById<CheatsheetViewModel>(id);

            return this.View(model);
        }
    }
}
