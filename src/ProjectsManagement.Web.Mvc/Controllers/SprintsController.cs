using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using SprintManagement.Sprints;
using Microsoft.AspNetCore.Mvc;
using ProjectsManagement.Web.Models.Sprints;
using System.Threading.Tasks;
using ProjectsManagement.Controllers;

namespace ProjectsManagement.Web.Controllers
{
    public class SprintsController : ProjectsManagementControllerBase
    {
        private readonly ISprintsAppService _sprintsAppService;
        public SprintsController(ISprintsAppService sprintsAppService)
        {
            _sprintsAppService = sprintsAppService;
        }

        public async Task<IActionResult> Index(long ProjectId)
        {
            IndexSprintsModalViewModel model = new IndexSprintsModalViewModel() {ProjectId=ProjectId };
             return View(model);
        }

        public async Task<ActionResult> CreateModal(int ProjectId)
        {
            var model = new CreateSprintModalViewModel();
            model.CreateSprintDto = new() { ProjectId = ProjectId };
            return PartialView("_CreateModal", model);
        }
        public async Task<ActionResult> EditModal(int sprintsId)
        {
            var output = await _sprintsAppService.GetSprintForEdit(new EntityDto(sprintsId));
            var model = new EditSprintModalViewModel();
            model.EditSprintDto = output;
            return PartialView("_EditModal", model);
        }
    }
}
