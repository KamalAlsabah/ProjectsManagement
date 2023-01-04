using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using ProjectsManagement.ProjectSupervisor.Dto;
using ProjectsManagement.ProjectDatabase.ProjectSupervisor;
using ProjectsManagement.ProjectDatabase.Project;
using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using ProjectsManagement.Authorization.Users;
using Microsoft.EntityFrameworkCore;
using ProjectsManagement.Authorization.Roles;
using Abp.UI;
using ProjectsManagement.ProjectDatabase.Enums;

namespace ProjectsManagement.ProjectSupervisor
{
    public class ProjectSupervisorAppService:AsyncCrudAppService<ProjectSupervisors, ProjectSupervisorListDto, long, ProjectSupervisorPagedDto, ProjectSupervisorCreateDto, UpDateInputProjectSupervisorDto>, IProjectSupervisorAppService
    {
        private readonly IRepository<ProjectSupervisors,long> repository;
        private readonly IRepository<Projects,long> repositoryProjects;
        private readonly IRepository<Projects, long> prepository;
        private readonly UserManager user;

        public ProjectSupervisorAppService(IRepository<ProjectSupervisors, long> repository, IRepository<Projects, long> Prepository, UserManager user, IRepository<Projects, long> repositoryProjects) : base(repository)
        {
            this.repository = repository;
            prepository = Prepository;
            this.user = user;
            this.repositoryProjects = repositoryProjects;
        }
        public async Task<ListResultDto<ComboboxItemDto>> GetSupervisors(EntityDto<long> input)
        {
            var projectSupers =await repository.GetAll().Where(x => x.ProjectId == input.Id).Select(x => x.SupervisorId).ToListAsync();
            var Users = (await user.GetUsersInRoleAsync(StaticRoleNames.Host.Supervisor)).Where(x=> !projectSupers.Contains(x.Id)).ToList();
            
            return new ListResultDto<ComboboxItemDto>(
                Users.Select(p => new ComboboxItemDto(p.Id.ToString("D"), p.UserName)).ToList()
            );
        }
        public async Task<ListResultDto<ComboboxItemDto>> GetProjects()
        {
            var Projects = await prepository.GetAllListAsync();
            return new ListResultDto<ComboboxItemDto>(
                Projects.Select(p => new ComboboxItemDto(p.Id.ToString("D"), p.Name)).ToList()
            );
        }
        public override async Task<ProjectSupervisorListDto> CreateAsync(ProjectSupervisorCreateDto input)
        {
            var projectClosed = await repositoryProjects.GetAll().Where(x => x.Id == input.ProjectId).Select(x => x.Status).FirstOrDefaultAsync();
            if (projectClosed == ProjectStatus.Closed)
            {
                throw new UserFriendlyException("The Project Was Cloesd");
            }
            return await base.CreateAsync(input);
        }
        public override async Task<ProjectSupervisorListDto> UpdateAsync(UpDateInputProjectSupervisorDto input)
        {
            var projectClosed = await repository.GetAll().Where(x => x.Id == input.Id).Select(x => x.Project.Status).FirstOrDefaultAsync();
            if (projectClosed == ProjectStatus.Closed)
            {
                throw new UserFriendlyException("The Project Was Cloesd");
            }
            return await base.UpdateAsync(input);
        }
        public override async Task DeleteAsync(EntityDto<long> input)
        {
            var projectClosed = await repository.GetAll().Where(x => x.Id == input.Id).Select(x => x.Project.Status).FirstOrDefaultAsync();
            if (projectClosed == ProjectStatus.Closed)
            {
                throw new UserFriendlyException("The Project Was Cloesd");
            }
             await base.DeleteAsync(input);
        }
        public async Task<ProjectSupervisorEditDto> GetProjectSupervisorForEdit(EntityDto<long> input)
        {
            var ProjectSupervisor = await repository.GetAsync(input.Id);
            var ProjectSupervisors = ObjectMapper.Map<ProjectSupervisorEditDto>(ProjectSupervisor);

            return ProjectSupervisors;
        }
        public override async Task<PagedResultDto<ProjectSupervisorListDto>> GetAllAsync(ProjectSupervisorPagedDto input)
        {
             List<ProjectSupervisors> listProjectSupervisors = await repository.GetAll()
               .Include(x => x.Project)
               .Include(x => x.Supervisor)
               .Where(x=>x.ProjectId == input.ProjectId)
               .Where(x =>
               x.Project.Name.Contains(input.KeyWord) ||
               x.Supervisor.UserName.Contains(input.KeyWord) ||
               input.KeyWord == null ? true : false)
               .Skip(input.SkipCount)
               .Take(input.MaxResultCount)
               .ToListAsync();

            return new PagedResultDto<ProjectSupervisorListDto>()
            {
                Items = ObjectMapper.Map<List<ProjectSupervisorListDto>>(listProjectSupervisors),
                TotalCount = listProjectSupervisors.Count
            };
        }
    }
}
