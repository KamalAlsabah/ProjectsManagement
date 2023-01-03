using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using ProjectsManagement.JobTasks.Dto;
using ProjectsManagement.ProjectDatabase.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectsManagement.JobTasks
{

    public class JobTasksAppService : AsyncCrudAppService<ProjectsManagement.ProjectDatabase.JobTask.JobTasks, JobTasksDto, long, PagedJobTasksResultRequestDto, CreateJobTasksDto, UpDateInputJobTasksDto>, IJobTasksAppService
    {
        private readonly IRepository<ProjectsManagement.ProjectDatabase.JobTask.JobTasks, long> _jobTasksrepository;
        private readonly IRepository<ProjectsManagement.ProjectDatabase.Job.Jobs, long> _jobrepository;
        public JobTasksAppService(IRepository<ProjectsManagement.ProjectDatabase.JobTask.JobTasks, long> repository, IRepository<ProjectDatabase.Job.Jobs, long> jobrepository) : base(repository)
        {
            _jobTasksrepository = repository;
            _jobrepository = jobrepository;
        }

        public override async Task<PagedResultDto<JobTasksDto>> GetAllAsync(PagedJobTasksResultRequestDto input)
        {
            try
            {
                var listJobs = _jobTasksrepository.GetAll().Where(x => x.JobId == input.JobId)
                    .OrderByDescending(s => s.CreationTime)
                .Include(x => x.Job)
                .WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Name.Contains(input.Keyword));
                return new PagedResultDto<JobTasksDto>()
                {
                    Items = ObjectMapper.Map<List<JobTasksDto>>(listJobs
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


        public override async Task<JobTasksDto> CreateAsync(CreateJobTasksDto input)
        {
            var projectClosed = await _jobrepository.GetAll().Where(x => x.Id == input.JobId).Select(x => x.Project.Status).FirstOrDefaultAsync();
            if (projectClosed==ProjectStatus.Closed)
            {
                throw new UserFriendlyException("The Project Was Cloesd");
            }
            return await base.CreateAsync(input);
        }

        public async Task<EditJobTasksDto> GetJobTasksForEdit(EntityDto<long> input)
        {
            var study = _jobTasksrepository.Get(input.Id);
            var model = ObjectMapper.Map<EditJobTasksDto>(study);

            return model;
        }

        public override async Task<JobTasksDto> UpdateAsync(UpDateInputJobTasksDto input)
        {
            var projectClosed = await _jobTasksrepository.GetAll().Where(x => x.Id == input.Id).Select(x => x.Job.Project.Status).FirstOrDefaultAsync();
            if (projectClosed==ProjectStatus.Closed)
            {
                throw new UserFriendlyException("The Project Was Cloesd");
            }
            return await base.UpdateAsync(input);
        }
        public override async Task DeleteAsync(EntityDto<long> input)
        {
            var projectClosed = await _jobTasksrepository.GetAll().Where(x => x.Id == input.Id).Select(x => x.Job.Project.Status).FirstOrDefaultAsync();
            if (projectClosed == ProjectStatus.Closed)
            {
                throw new UserFriendlyException("The Project Was Cloesd");
            }
            await base.DeleteAsync(input);
        }
    }
}
