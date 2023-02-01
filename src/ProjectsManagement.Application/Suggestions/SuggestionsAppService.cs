using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using ProjectsManagement.Authorization;
using ProjectsManagement.ProjectDatabase.Enums;
using ProjectsManagement.Suggestions.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectsManagement.Suggestions
{

    public class SuggestionsAppService : AsyncCrudAppService<ProjectsManagement.ProjectDatabase.Suggestion.Suggestions, SuggestionsDto, long, PagedSuggestionsResultRequestDto, CreateSuggestionsDto, UpDateInputSuggestionsDto>, ISuggestionsAppService
    {
        private readonly IRepository<ProjectsManagement.ProjectDatabase.Suggestion.Suggestions, long> _suggestionsrepository;
        private readonly IRepository<ProjectsManagement.ProjectDatabase.Project.Projects, long> projectRepo;
        public SuggestionsAppService(IRepository<ProjectsManagement.ProjectDatabase.Suggestion.Suggestions, long> repository) : base(repository)
        {
            _suggestionsrepository = repository;
        }
        [AbpAuthorize(PermissionNames.Pages_Suggestions)]
        public override async Task<PagedResultDto<SuggestionsDto>> GetAllAsync(PagedSuggestionsResultRequestDto input)
        {
            try
            {
                var listJobs = _suggestionsrepository.GetAll()
                    .OrderByDescending(s => s.CreationTime)
                    .Where(x => x.ProjectId == input.ProjectId)
                .Include(x => x.Project)
                .Include(x => x.Supervisor);


                return new PagedResultDto<SuggestionsDto>()
                {
                    Items = ObjectMapper.Map<List<SuggestionsDto>>(listJobs
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount)),
                    TotalCount = listJobs.Count()
                };
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("error");
            }

        }

        [AbpAuthorize(PermissionNames.Pages_Suggestions_CreateSuggestions)]

        public override async Task<SuggestionsDto> CreateAsync(CreateSuggestionsDto input)
        {
            var projectClosed = await projectRepo.GetAll().Where(x => x.Id == input.ProjectId).Select(x => x.Status).FirstOrDefaultAsync();

            if (projectClosed==ProjectStatus.Closed)
            {
                throw new UserFriendlyException("The Project Was Cloesd");
            }
            return await base.CreateAsync(input);
        }

        public async Task<EditSuggestionsDto> GetSuggestionsForEdit(EntityDto<long> input)
        {
            var study = _suggestionsrepository.Get(input.Id);
            var model = ObjectMapper.Map<EditSuggestionsDto>(study);

            return model;
        }
        [AbpAuthorize(PermissionNames.Pages_Suggestions_EditSuggestions)]

        public override async Task<SuggestionsDto> UpdateAsync(UpDateInputSuggestionsDto input)
        {
            var projectClosed = await _suggestionsrepository.GetAll().Where(x => x.Id == input.Id).Select(x => x.Project.Status).FirstOrDefaultAsync();
            if (projectClosed==ProjectStatus.Closed)
            {
                throw new UserFriendlyException("The Project Was Cloesd");
            }
            return await base.UpdateAsync(input);
        }
        [AbpAuthorize(PermissionNames.Pages_Suggestions_DeleteSuggestions)]

        public override async Task DeleteAsync(EntityDto<long> input)
        {
            var projectClosed = await _suggestionsrepository.GetAll().Where(x => x.Id == input.Id).Select(x => x.Project.Status).FirstOrDefaultAsync();
            if (projectClosed == ProjectStatus.Closed)
            {
                throw new UserFriendlyException("The Project Was Cloesd");
            }
            await base.DeleteAsync(input);
        }
    }
}
