namespace CodeBase.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class CoursesController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
