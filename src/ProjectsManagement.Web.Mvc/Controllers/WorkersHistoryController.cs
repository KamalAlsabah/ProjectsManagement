using Microsoft.AspNetCore.Mvc;
using ProjectsManagement.Controllers;
using ProjectsManagement.Web.Models.WorkersHistory;

namespace ProjectsManagement.Web.Controllers
{
    public class WorkersHistoryController : ProjectsManagementControllerBase
    {
        public IActionResult Index(long WorkerId)
        {
            IndexWorkersHistoryModalViewModel model = new IndexWorkersHistoryModalViewModel() {WorkerId=WorkerId };
            return View(model);
        }
    }
}
