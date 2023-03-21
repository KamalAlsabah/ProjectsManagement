using Abp.AspNetCore.Mvc.Authorization;
using Abp.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectsManagement.Controllers;
using ProjectsManagement.Web.Models.WorkersDashboards;

namespace ProjectsManagement.Web.Controllers
{
    public class WorkersDashboardController : ProjectsManagementControllerBase
    {
   
     
        [AbpMvcAuthorize]

        public IActionResult Index(long projrctId)
        {
            if (!PermissionChecker.IsGranted("Pages.WorkersDashboard"))
                throw new AbpAuthorizationException("You are not authorized !");
            IndexWorkersDashboardModalViewModel model = new IndexWorkersDashboardModalViewModel() { ProjectId = projrctId };
            return View(model);
        }
    }
}
