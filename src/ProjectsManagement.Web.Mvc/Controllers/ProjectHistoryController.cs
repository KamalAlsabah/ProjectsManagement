using Microsoft.AspNetCore.Mvc;
using ProjectsManagement.Controllers;
using ProjectsManagement.ProjectHistory;
using ProjectsManagement.Web.Models.ProjectHistory;

namespace ProjectsManagement.Web.Controllers
{
    public class ProjectHistoryController : ProjectsManagementControllerBase
    {
        private readonly IProjectHistoryAppService _ProjectHistoryService;
        public ProjectHistoryController(IProjectHistoryAppService ProjectHistoryService)
        {
            _ProjectHistoryService = ProjectHistoryService;
        }
        public IActionResult Index(long projrctId)
        {
            IndexProjectHistoryModalViewModel model = new IndexProjectHistoryModalViewModel() { ProjectId = projrctId };
            return View(model);
        }
    }
}
