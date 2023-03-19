using Abp.Application.Services.Dto;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.UI;
using AutoMapper;
using ProjectsManagement.ProjectDatabase.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectsManagement.Home.HomeStatistics.Dto;
using Abp.Collections.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ProjectsManagement.Authorization.Roles;
using ProjectsManagement.Authorization.Users;
using ProjectsManagement.ProjectDatabase.ProjectSupervisor;
using ProjectsManagement.ProjectDatabase.Project;
using ProjectsManagement.Home.Dto;

namespace ProjectsManagement.Home.HomeStatistics
{
    public class HomeStatisticsAppService : AsyncCrudAppService<ProjectDatabase.Home.HomeStatistics, HomeStatisticsDto, long, PagedHomeStatisticsResultRequestDto, CreateHomeStatisticsDto, UpdateInputDto>, IHomeStatisticsAppService
    {
        private readonly IRepository<ProjectDatabase.Home.HomeStatistics, long> _HomeStatisticsrepository;
        private readonly IRepository<ProjectDatabase.Home.HomeStatisticsUserTypes, long> _HomeStatisticsUserTypesrepository;
        private readonly RoleManager _roleManager;
        private readonly UserManager _userManager;
        private readonly IRepository<ProjectDatabase.Project.Projects, long> _projectsRepositry;
        private readonly IRepository<ProjectDatabase.ProjectWorker.ProjectWorkers, long> _projectsWorkerRepositry;
        private readonly IRepository<ProjectDatabase.ProjectSupervisor.ProjectSupervisors, long> _projectsSupervisorRepositry;
        private readonly IRepository<ProjectDatabase.Sprint.Sprints, long> _sprintsRepositry;
        private readonly IRepository<ProjectDatabase.Job.Jobs, long> _jobsRepositry;
        private readonly IRepository<ProjectDatabase.JobTask.JobTasks, long> _jobTasksRepositry;
        private readonly IRepository<ProjectDatabase.WorkersHistory.WorkersHistory, long> _workersHistoryRepositry;


        public HomeStatisticsAppService(IRepository<ProjectDatabase.Home.HomeStatistics, long> repository, 
            RoleManager roleManager, IRepository<ProjectDatabase.Home.HomeStatisticsUserTypes, long> homeStatisticsUserTypesrepository,
            UserManager userManager, IRepository<ProjectsManagement.ProjectDatabase.Project.Projects, long> projectsRepositry, IRepository<ProjectDatabase.ProjectWorker.ProjectWorkers, long> projectsWorkerRepositry, IRepository<ProjectSupervisors, long> projectsSupervisorRepositry, IRepository<ProjectDatabase.Sprint.Sprints, long> sprintsRepositry, IRepository<ProjectDatabase.Job.Jobs, long> jobsRepositry, IRepository<ProjectDatabase.JobTask.JobTasks, long> jobTasksRepositry, IRepository<ProjectDatabase.WorkersHistory.WorkersHistory, long> workersHistoryRepositry) : base(repository)
        {
            _HomeStatisticsrepository = repository;
            _roleManager = roleManager;
            _HomeStatisticsUserTypesrepository = homeStatisticsUserTypesrepository;
            _userManager = userManager;
            _projectsRepositry = projectsRepositry;
            _projectsWorkerRepositry = projectsWorkerRepositry;
            _projectsSupervisorRepositry = projectsSupervisorRepositry;
            _sprintsRepositry = sprintsRepositry;
            _jobsRepositry = jobsRepositry;
            _jobTasksRepositry = jobTasksRepositry;
            _workersHistoryRepositry = workersHistoryRepositry;
        }
        public async Task<HomeDto> GetDetatilsForHome()
        {
            var User = await _userManager.FindByIdAsync(_userManager.AbpSession.UserId.ToString());
            var RolesForUser = await _userManager.GetRolesAsync(User);
            HomeDto model = new HomeDto();
            var listprojects = _projectsRepositry.GetAll();
            var listsprints = _sprintsRepositry.GetAll().Include(x=>x.Project);
            var listjobs = _jobsRepositry.GetAll().Include(x => x.Project);
            var listjobtasks = _jobTasksRepositry.GetAll();
            var listWorkersHistory = _workersHistoryRepositry.GetAll().Include(x => x.Worker);
            if (RolesForUser.FirstOrDefault() == "Admin")
            {
                model.Projects = listprojects.Count();
                model.ProjectsWorkers = _projectsWorkerRepositry.GetAll().Select(x => x.Id).Count();
                model.ProjectsSupervisors = _projectsSupervisorRepositry.GetAll().Select(x => x.Id).Count();
                model.Sprints = listsprints.Count();
                model.Jobs = listjobs.Count();
                model.JobTasks = listjobtasks.Count();
                model.ProjectSprintDto = new List<ProjectSprintDto>();
                model.ProjectWorkersHoursWieghtDto = new List<ProjectWorkersHoursWieghtDto>();
                var jobsProjectIds = _jobsRepositry.GetAll().Include(x=>x.Project).Where(x=>x.Project.Status ==ProjectDatabase.Enums.ProjectStatus.Open).Where(x=>x.Status==ProjectDatabase.Enums.JobStatus.Done).Select(x => x.ProjectId).ToList();
                var DoneJobs = _jobsRepositry.GetAll().Include(x => x.Project).Where(x => x.Project.Status == ProjectDatabase.Enums.ProjectStatus.Open).Where(x => x.Status == ProjectDatabase.Enums.JobStatus.Done).ToList();

            }
            else if (RolesForUser.FirstOrDefault() == "Worker")
            {
                List<ProjectsManagement.ProjectDatabase.Project.Projects> WorkerProjectList = new List<ProjectsManagement.ProjectDatabase.Project.Projects>();
                List<ProjectDatabase.Sprint.Sprints> WorkerSprintsList = new List<ProjectDatabase.Sprint.Sprints>();
                List<ProjectDatabase.Job.Jobs> WorkerJobsList = new List<ProjectDatabase.Job.Jobs>();
                List<ProjectDatabase.JobTask.JobTasks> WorkerJobTasksList = new List<ProjectDatabase.JobTask.JobTasks>();

                var WorkersProject = await _projectsWorkerRepositry.GetAll().Where(x => x.WorkerId == User.Id).Select(x => x.ProjectId).ToListAsync();
                foreach (var projectId in WorkersProject)
                {
                    foreach (var project in listprojects)
                    {
                        if (projectId == project.Id)
                        {
                            WorkerProjectList.Add(project);
                        }
                    }
                }
                model.Projects = WorkerProjectList.Count();
                foreach (var project in WorkerProjectList)
                {
                    foreach (var sprint in listsprints)
                    {
                        if (project.Id == sprint.ProjectId)
                        {
                            WorkerSprintsList.Add(sprint);
                        }
                    }
                }
                model.Sprints = WorkerSprintsList.Count();
                foreach (var project in WorkerProjectList)
                {
                    foreach (var job in listjobs)
                    {
                        if (project.Id == job.ProjectId)
                        {
                            WorkerJobsList.Add(job);
                        }
                    }
                }
                model.Jobs = WorkerJobsList.Count();
                foreach (var job in WorkerJobsList)
                {
                    foreach (var jobtask in listjobtasks)
                    {
                        if (job.Id == jobtask.JobId)
                        {
                            WorkerJobTasksList.Add(jobtask);
                        }
                    }
                }
                model.JobTasks = WorkerJobTasksList.Count();
            }

            return model;
        }
        public override async Task<PagedResultDto<HomeStatisticsDto>> GetAllAsync(PagedHomeStatisticsResultRequestDto input)
        {
            var listHomeStatistics = _HomeStatisticsrepository.GetAll()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Name.Contains(input.Keyword));
            return new PagedResultDto<HomeStatisticsDto>()
            {
                Items = ObjectMapper.Map<List<HomeStatisticsDto>>(listHomeStatistics
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)),
                TotalCount = listHomeStatistics.Count()
            };
        }
        public override async Task<HomeStatisticsDto> CreateAsync(CreateHomeStatisticsDto input)
        {
            var model = await base.CreateAsync(input);

            foreach (var item in input.UserTypesId)
            {
                await _HomeStatisticsUserTypesrepository.InsertAsync(new ProjectDatabase.Home.HomeStatisticsUserTypes()
                {
                 UserTypeId=item,
                 HomeStatisticsId=model.Id
                });
            }
            return model;
        }
        public override async Task<HomeStatisticsDto> UpdateAsync(UpdateInputDto input)
        {
            var model = await base.UpdateAsync(input);
            await _HomeStatisticsUserTypesrepository
                .DeleteAsync(x => x.HomeStatisticsId == model.Id);
            foreach (var item in input.UserTypes)
            {
                await _HomeStatisticsUserTypesrepository.InsertAsync(new ProjectDatabase.Home.HomeStatisticsUserTypes()
                {
                    UserTypeId = item,
                    HomeStatisticsId = model.Id
                });
            }
            return model;
        }
        public async Task<List<NameValueDto<int>>> GetUserTypes()
        {
            return await _roleManager.Roles.Select(x => new NameValueDto<int> { Name = x.Name, Value = x.Id }).ToListAsync();
        }
        public async Task<EditHomeStatisticsDto> GetHomeStatisticsForEdit(EntityDto input)
        {
            var HomeStatistics = await _HomeStatisticsrepository.GetAll().Where(x => x.Id == input.Id).Include(x=>x.UserTypes).FirstOrDefaultAsync();
            var model = ObjectMapper.Map<EditHomeStatisticsDto>(HomeStatistics);
            model.UserTypes =await _roleManager.Roles.Select(x=>new NameValueDto<int> {Name=x.Name,Value=x.Id }).ToListAsync();
            model.GrantedUserTypes =HomeStatistics.UserTypes.Select(x=>x.UserTypeId).ToList();
            return model;
        }
        public override async Task DeleteAsync(EntityDto<long> input)
        {
            await base.DeleteAsync(input);
        }
    }
}
