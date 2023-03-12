using Microsoft.AspNetCore.Mvc;
using ProjectsManagement.Controllers;

namespace ProjectsManagement.Web.Controllers
{
    public class WorkersHistoryController : ProjectsManagementControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
