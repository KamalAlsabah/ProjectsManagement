using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectsManagement.Controllers;
using ProjectsManagement.Project;
using ProjectsManagement.Web.Models.Projects;
using System.Threading.Tasks;

namespace ProjectsManagement.Web.Controllers
{
    public class ProjectsController : ProjectsManagementControllerBase
    {
        private readonly IProjectsAppService _projectsAppService;

        public ProjectsController(IProjectsAppService projectsAppService)
        {
            _projectsAppService = projectsAppService;
        }
        [AbpMvcAuthorize]
        public async Task<IActionResult> Index()
        {
            if (!PermissionChecker.IsGranted("Pages.Projects"))
                throw new AbpAuthorizationException("You are not authorized !");
            return View();
        }
        [AbpMvcAuthorize]
        public async Task<ActionResult> EditModal(long projectsId)
        {
            if (!PermissionChecker.IsGranted("Pages.Projects.EditProject"))
                throw new AbpAuthorizationException("You are not authorized to Edit Project!");
            var output = await _projectsAppService.GetProjectsForEdit(new EntityDto<long> { Id = projectsId });
            var model = ObjectMapper.Map<EditProjectsModalViewModel>(output);

            return PartialView("_EditModal", model);
        }
    }
}
