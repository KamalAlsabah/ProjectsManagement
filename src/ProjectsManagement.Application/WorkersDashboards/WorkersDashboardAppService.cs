using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using ProjectsManagement.ProjectDatabase.Enums;
using ProjectsManagement.WorkersDashboards.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.WorkersDashboards
{
    public class WorkersDashboardAppService : AsyncCrudAppService<ProjectDatabase.WorkersDashboard.WorkersDashboard, WorkersDashboardDto, long, PagedWorkersDashboardResultRequestDto, CreateWorkersDashboardDto, UpdateInputDto>, IWorkersDashboardAppService
    {
        private readonly IRepository<ProjectDatabase.WorkersDashboard.WorkersDashboard, long> _WorkersDashboardrepository;
        private readonly IRepository<ProjectDatabase.Job.Jobs, long> _jobsRepo;
        private readonly IRepository<ProjectDatabase.WorkersJobs.WorkersJobs, long> _WorkersJobsrepository;

        public WorkersDashboardAppService(IRepository<ProjectDatabase.WorkersDashboard.WorkersDashboard, long> repository,
            IRepository<ProjectDatabase.Job.Jobs, long> jobsRepo,
            IRepository<ProjectDatabase.WorkersJobs.WorkersJobs, long> WorkersJobsrepository) : base(repository)
        {
            _WorkersDashboardrepository = repository;
            _jobsRepo = jobsRepo;   
            _WorkersJobsrepository = WorkersJobsrepository;

        }
        public override async Task<PagedResultDto<WorkersDashboardDto>> GetAllAsync(PagedWorkersDashboardResultRequestDto input)
        {

            var listWorkersDashboard = _WorkersDashboardrepository.GetAll().Where(x => x.ProjectId == input.ProjectId)
                .OrderBy(x => x.WorkerJobsCount)
                .Include(x => x.Project)
                .Include(x=>x.Worker)
                .WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Worker.Name.Contains(input.Keyword));
            var items = ObjectMapper.Map<List<WorkersDashboardDto>>(listWorkersDashboard
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount));
            return new PagedResultDto<WorkersDashboardDto>()
            {
                Items = items,
                TotalCount = listWorkersDashboard.Count()
            };
        }
        public override async Task<WorkersDashboardDto> CreateAsync(CreateWorkersDashboardDto input)
        {
            return await base.CreateAsync(input);
        }
        public async Task<EditWorkersDashboardDto> GetWorkersDashboardForEdit(EntityDto input)
        {
            var WorkersDashboard = await _WorkersDashboardrepository.GetAll().Where(x => x.Id == input.Id).FirstOrDefaultAsync();
            var model = ObjectMapper.Map<EditWorkersDashboardDto>(WorkersDashboard);

            return model;
        }
        public override async Task<WorkersDashboardDto> UpdateAsync(UpdateInputDto input)
        {
            var projectClosed = await _WorkersDashboardrepository.GetAll().Where(x => x.Id == input.Id).Select(x => x.Project.Status).FirstOrDefaultAsync();
            if (projectClosed == ProjectStatus.Closed)
            {
                throw new UserFriendlyException("The Project Was Cloesd");
            }
            return await base.UpdateAsync(input);
        }
        public override async Task DeleteAsync(EntityDto<long> input)
        {
            var projectClosed = await _WorkersDashboardrepository.GetAll().Where(x => x.Id == input.Id).Select(x => x.Project.Status).FirstOrDefaultAsync();
            if (projectClosed == ProjectStatus.Closed)
            {
                throw new UserFriendlyException("The Project Was Cloesd");
            }
            await base.DeleteAsync(input);
        }
        public async Task CreateWorkerDashboard(long JobId)
        {
            var job =await _jobsRepo.GetAll().Where(x => x.Id == JobId).FirstOrDefaultAsync();
            if (job.Status ==JobStatus.Done)
            {
                var jobWorkers = _WorkersJobsrepository.GetAll().Where(x => x.JobId == JobId).ToList();
                var workersDashboards = Repository.GetAll().Where(x => x.ProjectId == job.ProjectId).ToList();

                foreach (var worker in jobWorkers)
                {
                    var listProjectWorkers = workersDashboards.Where(x => x.WorkerId == worker.WorkerId).FirstOrDefault();
                    if (listProjectWorkers==null)
                    {
                        CreateWorkersDashboardDto workersDashboard = new CreateWorkersDashboardDto() { ProjectId = job.ProjectId, WorkerId = (long)worker.WorkerId, WorkerJobsCount = job.WieghtOfHours/jobWorkers.Count };
                        await CreateAsync(workersDashboard);
                    }
                    else
                    {
                        UpdateInputDto updatedWorkerDash = new UpdateInputDto() {Id=listProjectWorkers.Id, ProjectId = job.ProjectId, WorkerId = (long)worker.WorkerId };
                        updatedWorkerDash.WorkerJobsCount =listProjectWorkers.WorkerJobsCount+job.WieghtOfHours / jobWorkers.Count();
                        await UpdateAsync(updatedWorkerDash);
                    }
                   
                 }
               
            }
           


        }
    }
}
