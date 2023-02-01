using Microsoft.AspNetCore.Mvc;
using ProjectsManagement.Controllers;
using ProjectsManagement.Web.Models.ProjectHistory;

namespace ProjectsManagement.Web.Controllers
{
    public class ProjectHistoryController : ProjectsManagementControllerBase
    {
        public IActionResult Index(long projrctId)
        {
            IndexProjectHistoryModalViewModel model = new IndexProjectHistoryModalViewModel() { ProjectId = projrctId };
            return View(model);
        }
    }
}
