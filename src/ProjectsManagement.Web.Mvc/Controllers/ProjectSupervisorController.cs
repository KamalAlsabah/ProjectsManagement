


using Abp;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using JobManagement.Jobs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectsManagement.Authorization.Users;
using ProjectsManagement.Controllers;
using ProjectsManagement.ProjectDatabase.ProjectWorker;
using ProjectsManagement.ProjectSupervisor;
using ProjectsManagement.Web.Models.Jobs;
using ProjectsManagement.Web.Models.ProjectSuprevisor;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectsManagement.Web.Controllers
{
    public class ProjectSupervisorController : ProjectsManagementControllerBase
    {
        private readonly IProjectSupervisorAppService _AppService;
        private readonly UserManager repository;


        public ProjectSupervisorController(IProjectSupervisorAppService appService, UserManager repository)
        {
            _AppService = appService;
            this.repository = repository;


        }
        [AbpMvcAuthorize]
        public async Task<IActionResult> Index(long ProjectId)
        {
            if (!PermissionChecker.IsGranted("Pages.ProjectsSupervisors"))
                throw new AbpAuthorizationException("You are not authorized !");
            IndexProjectSupervisorModalViewModel model= new IndexProjectSupervisorModalViewModel() { ProjectId = ProjectId };
             return View(model);
        }
        [AbpMvcAuthorize]

        public async Task<ActionResult> CreateModal(long ProjectId)
        {
            if (!PermissionChecker.IsGranted("Pages.ProjectsSupervisors.CreateProjectSupervisors"))
                throw new AbpAuthorizationException("You are not authorized to Create Project Supervisors!");
            var model = new CreateProjectSupervisorModalViewModel();
            model.projectSupervisorCreateDto = new () { ProjectId = ProjectId }; 
            model.Supervisors = (await _AppService.GetSupervisors(new EntityDto<long> { Id=ProjectId })).Items.Select(p => p.ToSelectListItem()).ToList();
            return PartialView("_CreateModal", model);
        }
        [AbpMvcAuthorize]

        public async Task<ActionResult> EditModal(long ProjectSupervisorid,long ProjectId)
        {
            if (!PermissionChecker.IsGranted("Pages.ProjectsSupervisors.EditProjectSupervisors"))
                throw new AbpAuthorizationException("You are not authorized to Edit Project Supervisors!");
            var output = await _AppService.GetProjectSupervisorForEdit(new EntityDto<long>(ProjectSupervisorid));
            var model = new EditProjectSupervisorModalViewModel();
            model.projectSupervisorEditDto = output;
            model.Supervisors = (await _AppService.GetSupervisors(new EntityDto<long> { Id = ProjectId })).Items.Select(p => p.ToSelectListItem()).ToList();
            return PartialView("_EditModal", model);
        }
    }
}