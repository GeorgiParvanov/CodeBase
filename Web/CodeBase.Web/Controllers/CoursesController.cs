namespace CodeBase.Web.Controllers
{
    using System.Threading.Tasks;

    using CodeBase.Data.Models;
    using CodeBase.Services.Data.Contracts;
    using CodeBase.Web.ViewModels.Courses;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CoursesController : BaseController
    {
        private const int ItemsPerPage = 1;
        private readonly ICoursesService coursesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IBalanceService balanceService;

        public CoursesController(
            ICoursesService coursesService,
            UserManager<ApplicationUser> userManager,
            IBalanceService balanceService)
        {
            this.coursesService = coursesService;
            this.userManager = userManager;
            this.balanceService = balanceService;
        }

        public IActionResult Index(int pageNumber)
        {
            if (pageNumber <= 0)
            {
                return this.NotFound();
            }

            var userId = this.userManager.GetUserId(this.User);
            var courses = this.coursesService.GetAll<CoursesViewModel>(pageNumber, ItemsPerPage);

            foreach (var course in courses)
            {
                course.UserId = userId;
            }

            var model = new CoursesListViewModel
            {
                Courses = courses,
                ItemsPerPage = ItemsPerPage,
                PageNumber = pageNumber,
                EntitiesCount = this.coursesService.GetCount(),
            };

            return this.View(model);
        }

        public IActionResult ByTag(string name, int pageNumber)
        {
            if (pageNumber <= 0)
            {
                return this.NotFound();
            }

            var userId = this.userManager.GetUserId(this.User);
            var courses = this.coursesService.GetAllByTagName<CoursesViewModel>(name, pageNumber, ItemsPerPage);

            foreach (var course in courses)
            {
                course.UserId = userId;
            }

            var model = new CoursesListViewModel
            {
                Courses = courses,
                ItemsPerPage = ItemsPerPage,
                PageNumber = pageNumber,
                EntitiesCount = this.coursesService.GetCount(),
            };

            return this.View(model);
        }

        public IActionResult Course(int id)
        {
            var model = this.coursesService.GetById<CoursesViewModel>(id);
            var userId = this.userManager.GetUserId(this.User);

            model.UserId = userId;

            return this.View(model);
        }

        [Authorize]
        public async Task<IActionResult> PurchaseCourse(int id)
        {
            var userId = this.userManager.GetUserId(this.User);
            var balanceAmount = this.coursesService.GetBalanceAmount(id);

            var user = await this.userManager.GetUserAsync(this.User);

            if (user.Balance < balanceAmount)
            {
                this.TempData["Message"] = "Your Balance amount is too low. Please add funds to your Balance in order to purchase a course.";
                return this.Redirect("/Balance");
            }

            await this.balanceService.RemoveBalanceAmountAsync(userId, balanceAmount);
            await this.coursesService.AddUserToCourse(id, userId);

            this.TempData["Message"] = "The team at CodeBase thanks you for purchasing this course. We wish you happy learning. :)";

            return this.RedirectToAction($"Course", new { id });
        }
    }
}
