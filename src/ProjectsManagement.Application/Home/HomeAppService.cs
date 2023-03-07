using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using ProjectsManagement.Authorization.Users;
using ProjectsManagement.Home.Dto;
using ProjectsManagement.ProjectDatabase.Project;
using ProjectsManagement.ProjectDatabase.ProjectSupervisor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.Home
{
    public class HomeAppService : IHomeAppService
    {
        private readonly UserManager _userManager;
        private readonly IRepository<ProjectDatabase.Project.Projects,long> _projectsRepositry;
        private readonly IRepository<ProjectDatabase.ProjectWorker.ProjectWorkers,long> _projectsWorkerRepositry;
        private readonly IRepository<ProjectDatabase.ProjectSupervisor.ProjectSupervisors,long> _projectsSupervisorRepositry;
        private readonly IRepository<ProjectDatabase.Sprint.Sprints,long> _sprintsRepositry;
        private readonly IRepository<ProjectDatabase.Job.Jobs,long> _jobsRepositry;
        private readonly IRepository<ProjectDatabase.JobTask.JobTasks,long> _jobTasksRepositry;
        private readonly IRepository<ProjectDatabase.WorkersHistory.WorkersHistory,long> _workersHistoryRepositry;

        public HomeAppService(UserManager userManager,IRepository<Projects, long> projectsRepositry,IRepository<ProjectDatabase.ProjectWorker.ProjectWorkers, long> projectsWorkerRepositry,IRepository<ProjectSupervisors, long> projectsSupervisorRepositry,IRepository<ProjectDatabase.Sprint.Sprints, long> sprintsRepositry,IRepository<ProjectDatabase.Job.Jobs, long> jobsRepositry,IRepository<ProjectDatabase.JobTask.JobTasks, long> jobTasksRepositry,IRepository<ProjectDatabase.WorkersHistory.WorkersHistory, long> workersHistoryRepositry)
        {
            _userManager = userManager;
            _projectsRepositry = projectsRepositry;
            _projectsWorkerRepositry = projectsWorkerRepositry;
            _projectsSupervisorRepositry = projectsSupervisorRepositry;
            _sprintsRepositry = sprintsRepositry;
            _jobsRepositry = jobsRepositry;
            _jobTasksRepositry = jobTasksRepositry;
            _workersHistoryRepositry = workersHistoryRepositry; 
        }

        public async Task<HomeDto> GetDetatilsForHome ()
        {
            var User =await _userManager.FindByIdAsync(_userManager.AbpSession.UserId.ToString());
            var RolesForUser = await _userManager.GetRolesAsync(User);
            HomeDto model = new HomeDto();
            var listprojects = _projectsRepositry.GetAll();
            var listsprints = _sprintsRepositry.GetAll();
            var listjobs = _jobsRepositry.GetAll();
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
                var  sprintsProjectIds = _sprintsRepositry.GetAll().Select(x=>x.ProjectId).Distinct().ToList();
                var sprints = listsprints.Include(x => x.Project).ToList();
                long Wieght = 0;
                foreach (var projectId in sprintsProjectIds)
                {
                     foreach(var sprint in sprints)
                     {
                          if (sprint.ProjectId == projectId)
                          {
                             Wieght += sprint.WieghtOfHours;
                          }
                     }
                    var projectsprint = new ProjectSprintDto() { WightOfSprints = Wieght, ProjectName= sprints.Where(x=>x.ProjectId==projectId).Select(x=>x.Project.Name).FirstOrDefault()};
                    model.ProjectSprintDto.Add(projectsprint);
                }
            }
            else if (RolesForUser.FirstOrDefault()== "Worker")
            {
                List<Projects> WorkerProjectList = new List<Projects>();
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
                foreach(var project in WorkerProjectList)
                {
                    foreach (var sprint in listsprints)
                    {
                        if (project.Id== sprint.ProjectId)
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
    }
}
