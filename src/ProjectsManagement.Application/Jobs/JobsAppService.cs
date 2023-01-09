using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using ProjectsManagement.Authorization;
using ProjectsManagement.Jobs.Dto;
using ProjectsManagement.ProjectDatabase.Enums;
using ProjectsManagement.ProjectDatabase.Sprint;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace JobManagement.Jobs
{

    public class JobsAppService : AsyncCrudAppService<ProjectsManagement.ProjectDatabase.Job.Jobs, JobsDto, long, PagedJobResultRequestDto, CreateJobDto, UpdateInputDto>, IJobsAppService
    {
        private readonly IRepository<ProjectsManagement.ProjectDatabase.Job.Jobs, long> _Jobrepository;
        private readonly IRepository<ProjectsManagement.ProjectDatabase.Project.Projects, long> projectRepo;

        public JobsAppService(IRepository<ProjectsManagement.ProjectDatabase.Job.Jobs, long> repository, IRepository<ProjectsManagement.ProjectDatabase.Project.Projects, long> projectRepo) : base(repository)
        {
            _Jobrepository = repository;
            this.projectRepo = projectRepo;
        }
        [AbpAuthorize(PermissionNames.Pages_Jobs)]

        public override async Task<PagedResultDto<JobsDto>> GetAllAsync(PagedJobResultRequestDto input)
        {

            var listJobs = _Jobrepository.GetAll().Where(x=>x.ProjectId== input.ProjectId)
                .OrderBy(x=>x.Sprint.EndDate)
                    .ThenBy(x=>x.EndDate)
                    .ThenBy(x=>x.Status)
                .Include(x => x.Sprint).Include(X=>X.Worker)
                .WhereIf(!string.IsNullOrWhiteSpace(input.Keyword) ,x =>x.Name.Contains(input.Keyword));
            return new PagedResultDto<JobsDto>()
            {
                Items = ObjectMapper.Map<List<JobsDto>>(listJobs
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)),
                TotalCount = listJobs.Count()
            };
        }

        [AbpAuthorize(PermissionNames.Pages_Jobs_CreateJob)]

        public override async Task<JobsDto> CreateAsync(CreateJobDto input)
        {
            var projectClosed = await projectRepo.GetAll().Where(x => x.Id == input.ProjectId).Select(x => x.Status).FirstOrDefaultAsync();
            if (projectClosed==ProjectStatus.Closed)
            {
                throw new UserFriendlyException("The Project Was Cloesd");
            }
            if (input.WorkerId == 0) input.WorkerId = null;
            if (input.SprintId == 0) input.SprintId = null;
            return await base.CreateAsync(input);
        }

       

        public async Task<EditJobDto> GetJobForEdit(EntityDto input)
        {
            var job =await _Jobrepository.GetAll().Where(x=>x.Id== input.Id).Include(x=>x.Sprint).FirstOrDefaultAsync();
            var model = ObjectMapper.Map<EditJobDto>(job);

            return model;
        }

        [AbpAuthorize(PermissionNames.Pages_Jobs_EditJob)]

        public override async Task<JobsDto> UpdateAsync(UpdateInputDto input)
        {
            var projectClosed = await _Jobrepository.GetAll().Where(x => x.Id == input.Id).Select(x => x.Project.Status).FirstOrDefaultAsync();
            if (projectClosed==ProjectStatus.Closed)
            {
                throw new UserFriendlyException("The Project Was Cloesd");
            }
            if (input.WorkerId == 0) input.WorkerId = null;
            if (input.SprintId == 0) input.SprintId = null;
            return await base.UpdateAsync(input);
        }
        [AbpAuthorize(PermissionNames.Pages_Jobs_DeleteJob)]
        public override async Task DeleteAsync(EntityDto<long> input)
        {
            var projectClosed = await _Jobrepository.GetAll().Where(x => x.Id == input.Id).Select(x => x.Project.Status).FirstOrDefaultAsync();
            if (projectClosed == ProjectStatus.Closed)
            {
                throw new UserFriendlyException("The Project Was Cloesd");
            }
            await base.DeleteAsync(input);
        }
    }
}
