using Abp;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectsManagement.Controllers;
using ProjectsManagement.JobTasks;
using ProjectsManagement.Web.Models.JobTasks;
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

        public async Task<IActionResult> Index(long JobsId)
        {
            ViewData["JobsId"] = JobsId;
            return View();
        }

        public async Task<ActionResult> CreateModal(int JobsId)
        {
            var model = new CreateJobTasksModalViewModel();
            model.CreateJobTasksDto = new() { JobId = JobsId };
            //model.Jobs = await jobsRepository.GetAll().Where(x => x.Id == JobsId)
            //    .Select(x => new NameValue<long> { Name = x.Name, Value = x.Id }).ToListAsync();
            return PartialView("_CreateModal", model);
        }
        public async Task<ActionResult> EditModal(int jobTasksId)
        {
            var output = await _jobTasksAppService.GetJobTasksForEdit(new EntityDto<long>(jobTasksId));
            var model = new EditJobTasksModalViewModel();
            model.EditJobTasksDto = output;
            //model.Jobs = await jobsRepository.GetAll().Where(x => x.Id == output.JobId)
            //    .Include(x => x.Worker).Select(x => new NameValue<long> { Name = x.Name, Value = x.Id }).ToListAsync();
            return PartialView("_EditModal", model);
        }
    }
}