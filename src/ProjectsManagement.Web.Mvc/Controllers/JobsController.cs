


using Abp;
using Abp.Application.Services.Dto;
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

        public async Task<IActionResult> Index(long ProjectId)
        {
            IndexJobModalViewModel model =new IndexJobModalViewModel() { ProjectId=ProjectId};
            return View(model);
        }

        public async Task<ActionResult> CreateModal(int ProjectId)
        {
            var model = new CreateJobModalViewModel();

            if (PermissionChecker.IsGranted("Pages.Jobs.CreateJob"))
            {

                
            }
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
        public async Task<ActionResult> EditModal(int jobsId)
        {
            var output = await _jobsAppService.GetJobForEdit(new EntityDto(jobsId));
            var model = new EditJobModalViewModel();
            model.EditJobDto = output;
            model.Users = await projectWorkersRepository.GetAll()
                .Where(x => x.ProjectId == output.ProjectId)
                .Include(x => x.Worker)
                .Select(x => new NameValue<long> { Name = x.Worker.FullName, Value = x.WorkerId })
                .ToListAsync();
            model.Sprints = await repositorySprints.GetAll()
                .Where(x => x.ProjectId == output.ProjectId)
                .Select(x => new NameValue<long> { Name = x.Name, Value = x.Id }).ToListAsync();
            return PartialView("_EditModal", model);
        }

        public async Task<ActionResult> SupervisorNotesCreateModal(int JobId)
        {
            var model = new CreateSupervisorNotesModalViewModel();
            model.CreateSupervisorNotesDto = new() { JobId = JobId };
            return PartialView("_SupervisorNotesCreateModal", model);
        }
    }
}