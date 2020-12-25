namespace CodeBase.Web.Controllers
{
    using System.Threading.Tasks;

    using CodeBase.Data.Models;
    using CodeBase.Services.Data.Contracts;
    using CodeBase.Web.ViewModels.Balance;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class BalanceController : BaseController
    {
        private readonly IBalanceService balanceService;
        private readonly UserManager<ApplicationUser> userManager;

        public BalanceController(IBalanceService balanceService, UserManager<ApplicationUser> userManager)
        {
            this.balanceService = balanceService;
            this.userManager = userManager;
        }

        [HttpGet("/Balance")]
        public async Task<IActionResult> Balance()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = user.Id;

            var model = this.balanceService.GetBalance<BalanceViewModel>(userId);

            return this.View(model);
        }

        [HttpPost("/Balance")]
        public async Task<IActionResult> Balance(BalanceInputModel model)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = user.Id;

            await this.balanceService.AddBalanceAsync(userId, model.Balance);

            this.TempData["Message"] = "Funds added successfully.";

            return this.RedirectToAction("Balance");
        }
    }
}
