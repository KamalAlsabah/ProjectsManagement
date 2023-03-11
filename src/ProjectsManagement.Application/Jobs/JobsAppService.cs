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
using ProjectsManagement.WorkersDashboards;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace JobManagement.Jobs
{

    public class JobsAppService : AsyncCrudAppService<ProjectsManagement.ProjectDatabase.Job.Jobs, JobsDto, long, PagedJobResultRequestDto, CreateJobDto, UpdateInputDto>, IJobsAppService
    {
        private readonly IRepository<ProjectsManagement.ProjectDatabase.Job.Jobs, long> _Jobrepository;
        private readonly IRepository<ProjectsManagement.ProjectDatabase.WorkersJobs.WorkersJobs, long> _WorkersJobsrepository;
        private readonly IRepository<ProjectsManagement.ProjectDatabase.Sprint.Sprints, long> _Sprintrepository;
        private readonly IRepository<ProjectsManagement.ProjectDatabase.Project.Projects, long> projectRepo;
        private readonly IRepository<ProjectsManagement.ProjectDatabase.ProjectWorker.ProjectWorkers, long> _projectWorkersRepo;
        private readonly IWorkersDashboardAppService _workersDashboardAppService;

        public JobsAppService(IRepository<ProjectsManagement.ProjectDatabase.Job.Jobs, long> repository,
            IRepository<ProjectsManagement.ProjectDatabase.Sprint.Sprints, long> Sprintrepository,
            IRepository<ProjectsManagement.ProjectDatabase.Project.Projects, long> projectRepo,
            IRepository<ProjectsManagement.ProjectDatabase.WorkersJobs.WorkersJobs, long> WorkersJobsrepository,
            IRepository<ProjectsManagement.ProjectDatabase.ProjectWorker.ProjectWorkers, long> projectWorkersRepo,
            IWorkersDashboardAppService workersDashboardAppService) : base(repository)
        {
            _Jobrepository = repository;
            this.projectRepo = projectRepo;
            _Sprintrepository = Sprintrepository;
            _WorkersJobsrepository = WorkersJobsrepository;
            _workersDashboardAppService = workersDashboardAppService;
            _projectWorkersRepo = projectWorkersRepo;
        }
        [AbpAuthorize(PermissionNames.Pages_Jobs)]

        public override async Task<PagedResultDto<JobsDto>> GetAllAsync(PagedJobResultRequestDto input)
        {

            var listJobs = _Jobrepository.GetAll().Where(x=>x.ProjectId== input.ProjectId)
                .OrderBy(x=>x.Sprint.EndDate)
                    .ThenBy(x=>x.EndDate)
                    .ThenBy(x=>x.Status)
                .Include(x => x.Sprint)
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
            if (input.SprintId == 0) input.SprintId = null;

            var Sprint = _Sprintrepository.GetAll().Where(x => x.Id == input.SprintId && x.ProjectId==input.ProjectId ).FirstOrDefault();
            var jobsList = _Jobrepository.GetAll().Where(x => x.SprintId == input.SprintId);
            var total = 0;
             foreach(var job in jobsList)
             {
                total += job.WieghtOfHours;
             }
            total += input.WieghtOfHours;

            //if (total>Sprint.WieghtOfHours)
            //{
            //    throw new UserFriendlyException($"the total hours of jobs in the sprint {Sprint.Name}  should be less than {Sprint.WieghtOfHours}");
            //}
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
            if (input.SprintId == 0) input.SprintId = null;

            var job = _Jobrepository.GetAll().Where(x => x.Id == input.Id).FirstOrDefault();
            var Sprint = _Sprintrepository.GetAll().Where(x => x.Id == input.SprintId && x.ProjectId==job.ProjectId).FirstOrDefault();
            var jobsList = _Jobrepository.GetAll().Where(x => x.SprintId == input.SprintId);
            var total = 0;
            foreach (var item in jobsList)
            {
                total += item.ExpectedNoOfHours;
            }
            total += input.WieghtOfHours;
            total-=job.WieghtOfHours;
            if (total > Sprint.WieghtOfHours)
                throw new UserFriendlyException($"the total hours of jobs in the sprint {Sprint.Name}  should be less than {Sprint.WieghtOfHours}");
            if (input.Status == ProjectsManagement.ProjectDatabase.Enums.JobStatus.InProgress)
                input.StartDate = System.DateTime.Now;
            else if(input.Status == ProjectsManagement.ProjectDatabase.Enums.JobStatus.Done)
            {
                input.EndDate = System.DateTime.Now;
                input.ActualNumberOfHours = (int)(input.EndDate.Date.Subtract(input.StartDate.Date)).TotalHours;


            }
                

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

        public async Task<List<JobWorkersOptionsDto>> GetJobWorkersOptions(long JobsId)
        {
            var ProjectjobId = await _Jobrepository.GetAll().Where(x=>x.Id==JobsId).Select(x=>x.ProjectId).FirstOrDefaultAsync();
            var workers =  _projectWorkersRepo.GetAll().Include(x => x.Worker).Where(x=>x.ProjectId==ProjectjobId).ToList();
            var jobsWrokersIds = _WorkersJobsrepository.GetAll().Include(x => x.Worker).Include(x => x.Job).Where(x => x.JobId == JobsId).Select(x=>x.WorkerId).ToList();
           
            var JobWorkersOptionsList = new List<JobWorkersOptionsDto>();
            foreach (var item in workers)
            {
                var JobWorkersOptions = new JobWorkersOptionsDto
                {
                    Id = item.Worker.Id,
                    Name = item.Worker.Name,
                };
                foreach (var id in jobsWrokersIds)
                {
                    if (item.WorkerId ==id)
                    {
                        JobWorkersOptions.IsSelected = true;
                    }
                }
                JobWorkersOptionsList.Add(JobWorkersOptions);
            }
            return JobWorkersOptionsList;
        }
    }
}
