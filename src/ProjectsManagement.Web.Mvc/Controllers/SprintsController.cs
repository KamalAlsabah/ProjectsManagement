using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using SprintManagement.Sprints;
using Microsoft.AspNetCore.Mvc;
using ProjectsManagement.Web.Models.Sprints;
using System.Threading.Tasks;
using ProjectsManagement.Controllers;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Authorization;

namespace ProjectsManagement.Web.Controllers
{
    public class SprintsController : ProjectsManagementControllerBase
    {
        private readonly ISprintsAppService _sprintsAppService;
        public SprintsController(ISprintsAppService sprintsAppService)
        {
            _sprintsAppService = sprintsAppService;
        }
        [AbpMvcAuthorize]
        public async Task<IActionResult> Index(long ProjectId)
        {
            if (!PermissionChecker.IsGranted("Pages.Sprints"))
                throw new AbpAuthorizationException("You are not authorized !");
            IndexSprintsModalViewModel model = new IndexSprintsModalViewModel() {ProjectId=ProjectId };
             return View(model);
        }
        [AbpMvcAuthorize]
        public async Task<ActionResult> CreateModal(int ProjectId)
        {
            if (!PermissionChecker.IsGranted("Pages.Sprints.CreateSprints"))
                throw new AbpAuthorizationException("You are not authorized to Create Sprints !");
            var model = new CreateSprintModalViewModel();
            model.CreateSprintDto = new() { ProjectId = ProjectId };
            return PartialView("_CreateModal", model);
        }
        [AbpMvcAuthorize]
        public async Task<ActionResult> EditModal(int sprintsId)
        {
            if (!PermissionChecker.IsGranted("Pages.Sprints.EditSprints"))
                throw new AbpAuthorizationException("You are not authorized to Edit Sprints !");
            var output = await _sprintsAppService.GetSprintForEdit(new EntityDto(sprintsId));
            var model = new EditSprintModalViewModel();
            model.EditSprintDto = output;
            return PartialView("_EditModal", model);
        }
    }
}
