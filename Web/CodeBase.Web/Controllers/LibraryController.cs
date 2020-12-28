namespace CodeBase.Web.Controllers
{
    using System.Threading.Tasks;

    using CodeBase.Data.Models;
    using CodeBase.Services.Data.Contracts;
    using CodeBase.Web.ViewModels.Courses;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class LibraryController : BaseController
    {
        private const int ItemsPerPage = 1;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILibraryService libraryService;

        public LibraryController(UserManager<ApplicationUser> userManager, ILibraryService libraryService)
        {
            this.userManager = userManager;
            this.libraryService = libraryService;
        }

        [Authorize]
        [HttpGet("/Library")]
        public IActionResult Library(int pageNumber = 1)
        {
            if (pageNumber <= 0)
            {
                return this.NotFound();
            }

            var userId = this.userManager.GetUserId(this.User);
            var courses = this.libraryService.GetUserCourses<CoursesViewModel>(userId, pageNumber, ItemsPerPage);

            foreach (var course in courses)
            {
                course.UserId = userId;
            }

            var model = new CoursesListViewModel
            {
                Courses = courses,
                ItemsPerPage = ItemsPerPage,
                PageNumber = pageNumber,
                EntitiesCount = this.libraryService.GetCount(userId),
            };

            return this.View(model);
        }
    }
}
