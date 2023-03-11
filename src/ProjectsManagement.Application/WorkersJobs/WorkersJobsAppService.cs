using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using ProjectsManagement.WorkersJobs.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.WorkersJobs
{
    public class WorkersJobsAppService : AsyncCrudAppService<ProjectsManagement.ProjectDatabase.WorkersJobs.WorkersJobs, WorkersJobsDto, long, PagedWorkersJobsResultRequestDto, CreateWorkersJobsDto, UpdateInputDto>, IWorkersJobsAppService
    {
        private readonly IRepository<ProjectsManagement.ProjectDatabase.WorkersJobs.WorkersJobs, long> _WorkersJobsrepository;
        private readonly IRepository<ProjectsManagement.ProjectDatabase.Project.Projects, long> projectRepo;
        private readonly IRepository<ProjectsManagement.ProjectDatabase.Job.Jobs, long> _jobsRepo;

        public WorkersJobsAppService(IRepository<ProjectsManagement.ProjectDatabase.WorkersJobs.WorkersJobs, long> repository,
            IRepository<ProjectsManagement.ProjectDatabase.Project.Projects, long> projectRepo,
            IRepository<ProjectsManagement.ProjectDatabase.Job.Jobs, long> jobsRepo) : base(repository)
        {
            _WorkersJobsrepository = repository;
            this.projectRepo = projectRepo;
            _jobsRepo = jobsRepo;

        }
        public override async Task<PagedResultDto<WorkersJobsDto>> GetAllAsync(PagedWorkersJobsResultRequestDto input)
        {

            var listWorkersJobs = _WorkersJobsrepository.GetAll()
                .Include(x => x.Job)
                .Include(x => x.Worker)
                .WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Worker.Name.Contains(input.Keyword));
            var items = ObjectMapper.Map<List<WorkersJobsDto>>(listWorkersJobs
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount));
            return new PagedResultDto<WorkersJobsDto>()
            {
                Items = items,
                TotalCount = listWorkersJobs.Count()
            };
        }
        public override async Task<WorkersJobsDto> CreateAsync(CreateWorkersJobsDto input)
        {
                    return await base.CreateAsync(input);
        }
        public async Task<EditWorkersJobsDto> GetWorkersJobsForEdit(EntityDto input)
        {
            var WorkersJobs = await _WorkersJobsrepository.GetAll().Where(x => x.Id == input.Id).FirstOrDefaultAsync();
            var model = ObjectMapper.Map<EditWorkersJobsDto>(WorkersJobs);

            return model;
        }
        public override async Task<WorkersJobsDto> UpdateAsync(UpdateInputDto input)
        {
           
            return await base.UpdateAsync(input);
        }
        public override async Task DeleteAsync(EntityDto<long> input)
        {
           
            await base.DeleteAsync(input);
        }

        public async Task CreateJobWorkers(long JobId, int[] Arr)
        {
            var OldJobWorkers = _WorkersJobsrepository.GetAll().Include(x => x.Job).Include(x => x.Worker).Where(x => x.JobId == JobId).ToList();
             if(OldJobWorkers.Count()>0)
            {
                foreach(var OldJobWorker in OldJobWorkers)
                {
                    foreach (var item in Arr)
                    {
                        UpdateInputDto workersJobs = new UpdateInputDto();
                        workersJobs.Id =OldJobWorker.Id;
                        workersJobs.JobId = JobId;
                        workersJobs.WorkerId = item;
                        await UpdateAsync(workersJobs);
                    }
                }
               
            }
            else
            {
                CreateWorkersJobsDto workersJobsDto = new CreateWorkersJobsDto();
                foreach (var item in Arr)
                {
                    workersJobsDto.JobId = JobId;
                    workersJobsDto.WorkerId = item;
                    CreateAsync(workersJobsDto);
                }
            }
           
        }
    }
}
