using Abp.AspNetCore.Mvc.Authorization;
using Abp.Authorization;
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
        [AbpMvcAuthorize]
        public IActionResult Index(long projrctId)
        {
            if (!PermissionChecker.IsGranted("Pages.ProjectHistory"))
                throw new AbpAuthorizationException("You are not authorized !");
            IndexProjectHistoryModalViewModel model = new IndexProjectHistoryModalViewModel() { ProjectId = projrctId };
            return View(model);
        }
    }
}
