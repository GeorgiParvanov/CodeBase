namespace CodeBase.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class LectureController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
