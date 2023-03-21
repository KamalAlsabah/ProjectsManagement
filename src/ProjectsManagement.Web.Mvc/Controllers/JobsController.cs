


using Abp;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Authorization;
using Abp.Domain.Repositories;
using JobManagement.Jobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectsManagement.Controllers;
using ProjectsManagement.ProjectDatabase.ProjectWorker;
using ProjectsManagement.Web.Models.Jobs;
using ProjectsManagement.Web.Models.SupervisorNotes;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectsManagement.Web.Controllers
{
    public class JobsController : ProjectsManagementControllerBase
    {
        private readonly IJobsAppService _jobsAppService;
        private readonly IRepository<ProjectsManagement.ProjectDatabase.ProjectWorker.ProjectWorkers, long> projectWorkersRepository;
        private readonly IRepository<ProjectsManagement.ProjectDatabase.Sprint.Sprints,long> repositorySprints;
        public JobsController(IJobsAppService jobsAppService, IRepository<ProjectsManagement.ProjectDatabase.ProjectWorker.ProjectWorkers, long> projectWorkersRepository, IRepository<ProjectsManagement.ProjectDatabase.Sprint.Sprints, long> repositorySprints)
        {
            _jobsAppService = jobsAppService;
            this.projectWorkersRepository = projectWorkersRepository;
            this.repositorySprints = repositorySprints;
        }
        [AbpMvcAuthorize]
        public async Task<IActionResult> Index(long ProjectId)
        {
            if (!PermissionChecker.IsGranted("Pages.Jobs"))
                throw new AbpAuthorizationException("You are not authorized !");

            IndexJobModalViewModel model =new IndexJobModalViewModel() { ProjectId=ProjectId};
            return View(model);
        }
        [AbpMvcAuthorize]
        public async Task<ActionResult> CreateModal(int ProjectId)
        {
            if (!PermissionChecker.IsGranted("Pages.Jobs.CreateJob"))
                throw new AbpAuthorizationException("You are not authorized to create Job !");

            var model = new CreateJobModalViewModel();
            model.CreateJobDto = new () { ProjectId = ProjectId }; 
            model.Users =await projectWorkersRepository.GetAll()
                .Where(x => x.ProjectId == ProjectId)
                .Select(x => new NameValue<long> { Name = x.Worker.FullName, Value = x.WorkerId })
                .ToListAsync();
            model.Sprints = await repositorySprints.GetAll()
                .Where(x => x.ProjectId == ProjectId)
                .Select(x => new NameValue<long> { Name = x.Name, Value = x.Id })
                .ToListAsync();
           
            return PartialView("_CreateModal", model);
        }
        [AbpMvcAuthorize]
        public async Task<ActionResult> EditModal(int jobsId)
        {
            if (!PermissionChecker.IsGranted("Pages.Jobs.EditJob"))
                throw new AbpAuthorizationException("You are not authorized to Edit Job !");

            var output = await _jobsAppService.GetJobForEdit(new EntityDto(jobsId));
            var model = new EditJobModalViewModel();
            model.EditJobDto = output;
            model.Users = await projectWorkersRepository.GetAll().Where(x => x.ProjectId == output.ProjectId).Include(x => x.Worker)
                .Select(x => new NameValue<long> { Name = x.Worker.FullName, Value = x.WorkerId }).ToListAsync();
            model.Sprints = await repositorySprints.GetAll().Where(x => x.ProjectId == output.ProjectId)
                .Select(x => new NameValue<long> { Name = x.Name, Value = x.Id }).ToListAsync();
            model.Workers =await _jobsAppService.GetJobWorkersOptions(jobsId);
            return PartialView("_EditModal", model);
        }
        [AbpMvcAuthorize]
        public async Task<ActionResult> SupervisorNotesCreateModal(int JobId)
        {
            if (!PermissionChecker.IsGranted("Pages.SupervisorNotes.CreateSupervisorNotes"))
                throw new AbpAuthorizationException("You are not authorized to Create Notes !");

            var model = new CreateSupervisorNotesModalViewModel();
            model.CreateSupervisorNotesDto = new() { JobId = JobId };
            return PartialView("_SupervisorNotesCreateModal", model);
        }
    }
}