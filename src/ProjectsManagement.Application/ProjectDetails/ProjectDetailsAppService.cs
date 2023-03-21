using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using ProjectsManagement.Authorization;
using ProjectsManagement.ProjectDatabase.Enums;
using ProjectsManagement.ProjectDetails.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.ProjectDetails
{
    public class ProjectDetailsAppService : AsyncCrudAppService<ProjectsManagement.ProjectDatabase.ProjectDetails.ProjectDetails, ProjectDetailsDto, long, PagedProjectDetailsResultRequestDto, CreateProjectDetailsDto, UpdateInputDto>, IProjectDetailsAppService
    {
        private readonly IRepository<ProjectDatabase.ProjectDetails.ProjectDetails, long> _repository;
        public ProjectDetailsAppService(IRepository<ProjectDatabase.ProjectDetails.ProjectDetails, long> repository) : base(repository)
        {
            _repository = repository;
        }
        [AbpAuthorize(PermissionNames.Pages_ProjectDetails)]

        public override async Task<PagedResultDto<ProjectDetailsDto>> GetAllAsync(PagedProjectDetailsResultRequestDto input)
        {
            try
            {
                var listNotes = _repository.GetAll().Include(x=>x.Project).Where(x => x.ProjectId == input.ProjectId)
                    .OrderByDescending(s => s.CreationTime)
                .Where(x => !string.IsNullOrWhiteSpace(input.Keyword) ? x.Project.Name.Contains(input.Keyword) : true);
                return new PagedResultDto<ProjectDetailsDto>()
                {
                    Items = ObjectMapper.Map<List<ProjectDetailsDto>>(listNotes
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount)),
                    TotalCount = listNotes.Count()
                };
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("error");
            }

        }
        public async Task<EditProjectDetailsDto> GetProjectDetailsForEdit(EntityDto<long> input)
        {
            var ProjectDetails = _repository.Get(input.Id);
            var model = ObjectMapper.Map<EditProjectDetailsDto>(ProjectDetails);
            return model;
        }
        [AbpAuthorize(PermissionNames.Pages_ProjectDetails_CreateProjectDetails)]

        public override async Task<ProjectDetailsDto> CreateAsync(CreateProjectDetailsDto input)
        {
            var projectClosed = await _repository.GetAll().Where(x => x.ProjectId == input.ProjectId).Select(x => x.Project.Status).FirstOrDefaultAsync();
            if (projectClosed == ProjectStatus.Closed)
            {
                throw new UserFriendlyException("The Project Was Cloesd");
            }
            return await base.CreateAsync(input);
        }
        [AbpAuthorize(PermissionNames.Pages_ProjectDetails_EditProjectDetails)]

        public override async Task<ProjectDetailsDto> UpdateAsync(UpdateInputDto input)
        {
            var projectClosed = await _repository.GetAll().Where(x => x.Id == input.Id).Select(x => x.Project.Status).FirstOrDefaultAsync();
            if (projectClosed == ProjectStatus.Closed)
            {
                throw new UserFriendlyException("The Project Was Cloesd");
            }
            return await base.UpdateAsync(input);
        }
        [AbpAuthorize(PermissionNames.Pages_ProjectDetails_DeleteProjectDetails)]

        public override async Task DeleteAsync(EntityDto<long> input)
        {
            var projectClosed = await _repository.GetAll().Where(x => x.Id == input.Id).Select(x => x.Project.Status).FirstOrDefaultAsync();
            if (projectClosed == ProjectStatus.Closed)
            {
                throw new UserFriendlyException("The Project Was Cloesd");
            }
            await base.DeleteAsync(input);
        }
    }
}
