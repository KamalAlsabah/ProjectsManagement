using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using ProjectsManagement.Sprints.Dto;
using ProjectsManagement.ProjectDatabase.Sprint;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using Abp.UI;
using ProjectsManagement.ProjectDatabase.Enums;
using Abp.Authorization;
using ProjectsManagement.Authorization;

namespace SprintManagement.Sprints
{

    public class SprintsAppService : AsyncCrudAppService<ProjectsManagement.ProjectDatabase.Sprint.Sprints, SprintsDto, long, PagedSprintResultRequestDto, CreateSprintDto, UpdateInputDto>, ISprintsAppService
    {
        private readonly IRepository<ProjectsManagement.ProjectDatabase.Sprint.Sprints, long> _Sprintrepository;
        private readonly IRepository<ProjectsManagement.ProjectDatabase.Project.Projects, long> projectRepo;

        public SprintsAppService(IRepository<ProjectsManagement.ProjectDatabase.Sprint.Sprints, long> repository, IRepository<ProjectsManagement.ProjectDatabase.Project.Projects, long> projectRepo) : base(repository)
        {
            _Sprintrepository = repository;
            this.projectRepo = projectRepo;
        }
        [AbpAuthorize(PermissionNames.Pages_Sprints)]

        public override async Task<PagedResultDto<SprintsDto>> GetAllAsync(PagedSprintResultRequestDto input)
        {

            var listSprints = _Sprintrepository.GetAll().Where(x=>x.ProjectId== input.ProjectId)
                .OrderBy(x=>x.EndDate)
                    .ThenBy(x=>x.Status)
                .Include(x => x.Project)
                .WhereIf(!string.IsNullOrWhiteSpace(input.Keyword) ,x =>x.Name.Contains(input.Keyword));
            var items = ObjectMapper.Map<List<SprintsDto>>(listSprints
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount));
            return new PagedResultDto<SprintsDto>()
            {
                Items = items,
                TotalCount = listSprints.Count()
            };
        }
        [AbpAuthorize(PermissionNames.Pages_Sprints_CreateSprints)]
        public override async Task<SprintsDto> CreateAsync(CreateSprintDto input)
        {
            var projectClosed = await projectRepo.GetAll().Where(x => x.Id == input.ProjectId).Select(x => x.Status).FirstOrDefaultAsync();
            if (projectClosed==ProjectStatus.Closed)
            {
                throw new UserFriendlyException("The Project Was Cloesd");
            }
            return await base.CreateAsync(input);
        }
        public async Task<EditSprintDto> GetSprintForEdit(EntityDto input)
        {
            var sprint =await _Sprintrepository.GetAll().Where(x=>x.Id== input.Id).FirstOrDefaultAsync();
            var model = ObjectMapper.Map<EditSprintDto>(sprint);

            return model;
        }
        [AbpAuthorize(PermissionNames.Pages_Sprints_EditSprints)]

        public override async Task<SprintsDto> UpdateAsync(UpdateInputDto input)
        {
            var projectClosed = await _Sprintrepository.GetAll().Where(x => x.Id == input.Id).Select(x => x.Project.Status).FirstOrDefaultAsync();
            if (projectClosed==ProjectStatus.Closed)
            {
                throw new UserFriendlyException("The Project Was Cloesd");
            }
            return await base.UpdateAsync(input);
        }
        [AbpAuthorize(PermissionNames.Pages_Sprints_DeleteSprints)]
        public override async Task DeleteAsync(EntityDto<long> input)
        {
            var projectClosed = await _Sprintrepository.GetAll().Where(x => x.Id == input.Id).Select(x => x.Project.Status).FirstOrDefaultAsync();
            if (projectClosed == ProjectStatus.Closed)
            {
                throw new UserFriendlyException("The Project Was Cloesd");
            }
            await base.DeleteAsync(input);
        }
    }
}
