using Abp.Application.Services.Dto;
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

        public async Task<IActionResult> Index()
        {
            ViewData["UserId"] = 0;
            return View();
        }

        public async Task<ActionResult> EditModal(long projectsId)
        {
            var output = await _projectsAppService.GetProjectsForEdit(new EntityDto<long> { Id = projectsId });
            var model = ObjectMapper.Map<EditProjectsModalViewModel>(output);

            return PartialView("_EditModal", model);
        }
    }
}
