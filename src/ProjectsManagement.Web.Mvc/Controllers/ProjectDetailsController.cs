using Abp.Application.Services.Dto;
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
        public async Task<IActionResult> Index(long ProjectId)
        {
            IndexProjectDetailsModalViewModel model = new IndexProjectDetailsModalViewModel() { ProjectId = ProjectId };
            return View(model);
        }
        public async Task<ActionResult> CreateModal(int projectId)
        {
            var model = new CreateProjectDetailsModalViewModel();
            model.createProjectDetailsDto = new() { ProjectId = projectId };
            return PartialView("_CreateModal", model);
        }
        public async Task<ActionResult> EditModal(int projectDetailsId)
        {
            var output = await _projectDetailsAppService.GetProjectDetailsForEdit(new EntityDto<long>(projectDetailsId));
            var model = new EditProjectDetailsModalViewModel();
            model.editProjectDetailsDto = output;
            return PartialView("_EditModal", model);
        }

    }
}
