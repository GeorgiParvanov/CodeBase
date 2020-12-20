namespace CodeBase.Web.Controllers
{
    using CodeBase.Services.Data.Contracts;
    using CodeBase.Web.ViewModels.Lectures;
    using Microsoft.AspNetCore.Mvc;

    public class LecturesController : BaseController
    {
        private readonly ILecturesService lecturesService;

        public LecturesController(ILecturesService lecturesService)
        {
            this.lecturesService = lecturesService;
        }

        public IActionResult Index()
        {
            var lectures = this.lecturesService.GetAll<LectureViewModel>();
            var model = new LectureListViewModel { Lectures = lectures };

            return this.View(model);
        }

        public IActionResult Lecture(int id)
        {
            var model = this.lecturesService.GetById<LectureViewModel>(id);

            if (model == null)
            {
                return this.NotFound();
            }

            return this.View(model);
        }
    }
}
