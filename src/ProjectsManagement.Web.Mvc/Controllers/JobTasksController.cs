using Abp;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectsManagement.Controllers;
using ProjectsManagement.JobTasks;
using ProjectsManagement.Web.Models.JobTasks;
using ProjectsManagement.Web.Models.SupervisorNotes;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectsManagement.Web.Controllers
{
    public class JobTasksController : ProjectsManagementControllerBase
    {
        private readonly IJobTasksAppService _jobTasksAppService;
        private readonly IRepository<ProjectsManagement.ProjectDatabase.Job.Jobs, long> jobsRepository;
        public JobTasksController(IJobTasksAppService jobTasksAppService, IRepository<ProjectsManagement.ProjectDatabase.Job.Jobs, long> jobsRepository)
        {
            _jobTasksAppService = jobTasksAppService;
            this.jobsRepository = jobsRepository;
        }
        [AbpMvcAuthorize]
        public async Task<IActionResult> Index(long JobsId,long ProjectId)
        {
            if (!PermissionChecker.IsGranted("Pages.JobTasks"))
                throw new AbpAuthorizationException("You are not authorized !");
            IndexJobTaskModalViewModel model =new IndexJobTaskModalViewModel() { JobId=JobsId,ProjectId=ProjectId};
            return View(model);
        }
        [AbpMvcAuthorize]

        public async Task<ActionResult> CreateModal(int JobsId)
        {
            if (!PermissionChecker.IsGranted("Pages.JobTasks.CreateJobTasks"))
                throw new AbpAuthorizationException("You are not authorized to Create Tasks !");
            var model = new CreateJobTasksModalViewModel();
            model.CreateJobTasksDto = new() { JobId = JobsId };
            return PartialView("_CreateModal", model);
        }
        [AbpMvcAuthorize]

        public async Task<ActionResult> EditModal(int jobTasksId)
        {
            if (!PermissionChecker.IsGranted("Pages.JobTasks.EditJobTasks"))
                throw new AbpAuthorizationException("You are not authorized to Edit Tasks !");
            var output = await _jobTasksAppService.GetJobTasksForEdit(new EntityDto<long>(jobTasksId));
            var model = new EditJobTasksModalViewModel();
            model.EditJobTasksDto = output;
            return PartialView("_EditModal", model);
        }
        [AbpMvcAuthorize]

        public async Task<ActionResult> SupervisorNotesCreateModal(long JobTaskId,long JobId)
        {
            if (!PermissionChecker.IsGranted("Pages.SupervisorNotes.CreateSupervisorNotes"))
                throw new AbpAuthorizationException("You are not authorized to Create Notes !");
            var model = new CreateSupervisorNotesModalViewModel();
            model.CreateSupervisorNotesDto = new() { JobTasksId = JobTaskId,JobId=JobId };
            return PartialView("_SupervisorNotesCreateModal", model);
        }
    }
}