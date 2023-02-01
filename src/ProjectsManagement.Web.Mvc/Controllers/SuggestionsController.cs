using Abp;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectsManagement.Controllers;
using ProjectsManagement.Suggestions;
using ProjectsManagement.Web.Models.Suggestions;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectsManagement.Web.Controllers
{
    public class SuggestionsController : ProjectsManagementControllerBase
    {
        private readonly ISuggestionsAppService _suggestionsAppService;
        private readonly IRepository<ProjectsManagement.ProjectDatabase.Project.Projects, long> projectRepository;
        public SuggestionsController(ISuggestionsAppService suggestionsAppService, IRepository<ProjectsManagement.ProjectDatabase.Project.Projects, long> suggestionsRepository)
        {
            _suggestionsAppService = suggestionsAppService;
            this.projectRepository = suggestionsRepository;
        }

        public async Task<IActionResult> Index(long ProjectId)
        {
            IndexSuggestionsModalViewModel model = new IndexSuggestionsModalViewModel() { ProjectId = ProjectId ,
            SupervisorId= User.FindFirstValue(ClaimTypes.NameIdentifier)
        };
            
            return View(model);
        }

        public async Task<ActionResult> CreateModal(int ProjectId)
        {
            var model = new CreateSuggestionsModalViewModel();
            model.CreateSuggestionsDto = new() { ProjectId = ProjectId, SupervisorId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)) };
            model.Projects = await projectRepository.GetAll().Where(x => x.Id == ProjectId)
                .Select(x => new NameValue<long> { Name = x.Name, Value = x.Id }).ToListAsync();
            return PartialView("_CreateModal", model);
        }
        public async Task<ActionResult> EditModal(int suggestionsId)
        {
            var output = await _suggestionsAppService.GetSuggestionsForEdit(new EntityDto<long>(suggestionsId));
            var model = new EditSuggestionsModalViewModel();
            model.EditSuggestionsDto = output;
            model.Projects = await projectRepository.GetAll().Where(x => x.Id == output.ProjectId)
              .Select(x => new NameValue<long> { Name = x.Name, Value = x.Id }).ToListAsync();
            return PartialView("_EditModal", model);
        }
    }
}