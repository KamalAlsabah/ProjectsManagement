using Abp;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Authorization;
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
        [AbpMvcAuthorize]

        public async Task<IActionResult> Index(long JobsId, long ProjectId)
        {
            if (!PermissionChecker.IsGranted("Pages.SupervisorNotes"))
                throw new AbpAuthorizationException("You are not authorized !");
            IndexSupervisorNotesModalViewModel model = new IndexSupervisorNotesModalViewModel() { JobId = JobsId,ProjectId=ProjectId };
            return View(model);
        }
        [AbpMvcAuthorize]

        public async Task<ActionResult> CreateModal(int JobsId)
        {
            if (!PermissionChecker.IsGranted("Pages.SupervisorNotes.CreateSupervisorNotes"))
                throw new AbpAuthorizationException("You are not authorized to Create Notes !");
            var model = new CreateSupervisorNotesModalViewModel();
            model.CreateSupervisorNotesDto = new() { JobId = JobsId };
            model.Jobs = await jobsRepository.GetAll().Where(x => x.Id == JobsId)
                .Select(x => new NameValue<long> { Name = x.Name, Value = x.Id }).ToListAsync();
            return PartialView("_CreateModal", model);
        }
        [AbpMvcAuthorize]

        public async Task<ActionResult> EditModal(int supervisorNotesId)
        {
            if (!PermissionChecker.IsGranted("Pages.SupervisorNotes.EditSupervisorNotes"))
                throw new AbpAuthorizationException("You are not authorized to Edit Notes !");
            var output = await _supervisorNotesAppService.GetSupervisorNotesForEdit(new EntityDto<long>(supervisorNotesId));
            var model = new EditSupervisorNotesModalViewModel();
            model.EditSupervisorNotesDto = output;
            model.Jobs = await jobsRepository.GetAll().Where(x => x.Id == output.JobId)
                .Select(x => new NameValue<long> { Name = x.Name, Value = x.Id }).ToListAsync();
            return PartialView("_EditModal", model);
        }
    }
}