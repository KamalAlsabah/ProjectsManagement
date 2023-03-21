using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectsManagement.Controllers;
using ProjectsManagement.ProjectDetails;
using ProjectsManagement.Web.Models.ProjectDetails;
using System.Threading.Tasks;

namespace ProjectsManagement.Web.Controllers
{
    public class ProjectDetailsController : ProjectsManagementControllerBase
    {
        private readonly IProjectDetailsAppService _projectDetailsAppService;
        public ProjectDetailsController(IProjectDetailsAppService projectDetailsAppService)
        {
            _projectDetailsAppService = projectDetailsAppService;
        }
        [AbpMvcAuthorize]

        public async Task<IActionResult> Index(long ProjectId)
        {
            if (!PermissionChecker.IsGranted("Pages.ProjectDetails"))
                throw new AbpAuthorizationException("You are not authorized !");
            IndexProjectDetailsModalViewModel model = new IndexProjectDetailsModalViewModel() { ProjectId = ProjectId };
            return View(model);
        }
        [AbpMvcAuthorize]

        public async Task<ActionResult> CreateModal(int projectId)
        {
            if (!PermissionChecker.IsGranted("Pages.ProjectDetails.CreateProjectDetails"))
                throw new AbpAuthorizationException("You are not authorized to Create Project Details !");
            var model = new CreateProjectDetailsModalViewModel();
            model.createProjectDetailsDto = new() { ProjectId = projectId };
            return PartialView("_CreateModal", model);
        }
        [AbpMvcAuthorize]

        public async Task<ActionResult> EditModal(int projectDetailsId)
        {
            if (!PermissionChecker.IsGranted("Pages.ProjectDetails.EditProjectDetails"))
                throw new AbpAuthorizationException("You are not authorized to Edit Project Details !");
            var output = await _projectDetailsAppService.GetProjectDetailsForEdit(new EntityDto<long>(projectDetailsId));
            var model = new EditProjectDetailsModalViewModel();
            model.editProjectDetailsDto = output;
            return PartialView("_EditModal", model);
        }

    }
}
