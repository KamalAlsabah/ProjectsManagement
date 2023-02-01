using Abp;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectsManagement.Controllers;
using ProjectsManagement.SupervisorNotes;
using ProjectsManagement.Web.Models.SupervisorNotes;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectsManagement.Web.Controllers
{
    public class SupervisorNotesController : ProjectsManagementControllerBase
    {
        private readonly ISupervisorNotesAppService _supervisorNotesAppService;
        private readonly IRepository<ProjectsManagement.ProjectDatabase.Job.Jobs, long> jobsRepository;
        public SupervisorNotesController(ISupervisorNotesAppService supervisorNotesAppService, IRepository<ProjectsManagement.ProjectDatabase.Job.Jobs, long> jobsRepository)
        {
            _supervisorNotesAppService = supervisorNotesAppService;
            this.jobsRepository = jobsRepository;
        }

        public async Task<IActionResult> Index(long JobsId)
        {
            IndexSupervisorNotesModalViewModel model = new IndexSupervisorNotesModalViewModel() { JobId = JobsId };
            return View(model);
        }

        public async Task<ActionResult> CreateModal(int JobsId)
        {
            var model = new CreateSupervisorNotesModalViewModel();
            model.CreateSupervisorNotesDto = new() { JobId = JobsId };
            model.Jobs = await jobsRepository.GetAll().Where(x => x.Id == JobsId)
                .Select(x => new NameValue<long> { Name = x.Name, Value = x.Id }).ToListAsync();
            return PartialView("_CreateModal", model);
        }
        public async Task<ActionResult> EditModal(int supervisorNotesId)
        {
            var output = await _supervisorNotesAppService.GetSupervisorNotesForEdit(new EntityDto<long>(supervisorNotesId));
            var model = new EditSupervisorNotesModalViewModel();
            model.EditSupervisorNotesDto = output;
            model.Jobs = await jobsRepository.GetAll().Where(x => x.Id == output.JobId)
                .Include(x => x.Worker).Select(x => new NameValue<long> { Name = x.Name, Value = x.Id }).ToListAsync();
            return PartialView("_EditModal", model);
        }
    }
}