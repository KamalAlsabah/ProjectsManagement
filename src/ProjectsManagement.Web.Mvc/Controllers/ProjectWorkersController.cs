using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using ProjectWorkerManagement.ProjectWorkers;
using Microsoft.AspNetCore.Mvc;
using ProjectsManagement.Web.Models.ProjectWorkers;
using System.Threading.Tasks;
using ProjectsManagement.Controllers;
using ProjectsManagement.Authorization.Users;
using System.Linq;
using Abp;
using Microsoft.EntityFrameworkCore;
using ProjectsManagement.ProjectDatabase.Project;
using Pipelines.Sockets.Unofficial.Arenas;
using Abp.Extensions;
using System.Security.Cryptography.X509Certificates;
using ProjectsManagement.Authorization.Roles;

namespace ProjectsManagement.Web.Controllers
{
    public class ProjectWorkersController : ProjectsManagementControllerBase
    {
        private readonly IProjectWorkersAppService _projectWorkersAppService;
        private readonly UserManager userManager;
        private readonly IRepository<Projects, long> projectsRepository;

        public ProjectWorkersController(IProjectWorkersAppService projectWorkersAppService,UserManager userManager,IRepository<Projects,long> projectsRepository)
        {
            _projectWorkersAppService = projectWorkersAppService;
            this.userManager = userManager;
            this.projectsRepository = projectsRepository;
        }

        public async Task<IActionResult> Index(long ProjectId)
        {
            IndexProjectWorkerModalViewModel model=new IndexProjectWorkerModalViewModel() { ProjectId=ProjectId};
            return View(model);
        }

        public async Task<ActionResult> CreateModal(long projectId)
        {
            var model = new CreateProjectWorkerModalViewModel();
           
            model.CreateProjectWorkerDto = new() { ProjectId = projectId };
            model.Users =await _projectWorkersAppService.GetWorkers(new EntityDto<long> { Id=projectId});
            return PartialView("_CreateModal", model);
        }
        //public async Task<ActionResult> EditModal(int projectWorkersId)
        //{
        //    var output = await _projectWorkersAppService.GetProjectWorkerForEdit(new EntityDto(projectWorkersId));
        //    var model = new EditProjectWorkerModalViewModel();
        //    model.Users = await userManager.Users.Select(x => new NameValue<long> { Name = x.FullName, Value = x.Id }).ToListAsync();
        //    model.Projects = await projectsRepository.GetAll().Select(x => new NameValue<long> { Name = x.Name, Value = x.Id }).ToListAsync();
        //    model.EditProjectWorkerDto = output;
        //    return PartialView("_EditModal", model);
        //}
    }
}
