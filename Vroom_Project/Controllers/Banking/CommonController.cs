using Microsoft.AspNetCore.Mvc;

namespace Vroom_Project.Controllers.Banking
{
    public class CommonController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return PartialView("_ToastrNotifications");
        }
    }
}
