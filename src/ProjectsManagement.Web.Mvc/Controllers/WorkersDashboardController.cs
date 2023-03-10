using Microsoft.AspNetCore.Mvc;
using ProjectsManagement.Controllers;
using ProjectsManagement.Web.Models.WorkersDashboards;

namespace ProjectsManagement.Web.Controllers
{
    public class WorkersDashboardController : ProjectsManagementControllerBase
    {
        public IActionResult Index(long projrctId)
        {
            IndexWorkersDashboardModalViewModel model = new IndexWorkersDashboardModalViewModel() { ProjectId = projrctId };
            return View(model);
        }
    }
}
