using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectsManagement.Authorization;
using ProjectsManagement.Authorization.Users;
using ProjectsManagement.Project.Dto;
using ProjectsManagement.ProjectDatabase.Project;
using ProjectsManagement.Projects.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectsManagement.Project
{

    public class ProjectsAppService : AsyncCrudAppService<ProjectsManagement.ProjectDatabase.Project.Projects, ProjectsDto, long, PagedProjectsResultRequestDto, CreateProjectsDto, UpdateInputDto>, IProjectsAppService
    {
        private readonly IRepository<ProjectsManagement.ProjectDatabase.Project.Projects, long> _projectsrepository;
        private readonly IRepository<ProjectDatabase.ProjectWorker.ProjectWorkers, long> _projectWorkersrepository;
        private readonly IRepository<ProjectDatabase.ProjectSupervisor.ProjectSupervisors, long> _projectSupervisorsrepository;
        private readonly IRepository<ProjectDatabase.Sprint.Sprints, long> _sprintsRepositry;
        private readonly IRepository<ProjectDatabase.Job.Jobs, long> _jobsRepositry;
        private readonly UserManager _userManager;
        public ProjectsAppService(
            IRepository<ProjectsManagement.ProjectDatabase.Project.Projects, long> repository,
            IRepository<ProjectDatabase.ProjectWorker.ProjectWorkers, long> projectWorkersrepository,
            IRepository<ProjectDatabase.ProjectSupervisor.ProjectSupervisors, long> projectSupervisorsrepository,
            IRepository<ProjectDatabase.Sprint.Sprints, long> sprintsRepositry,
            IRepository<ProjectDatabase.Job.Jobs, long> jobsRepositry,
        UserManager userManager) : base(repository)
        {
            _projectsrepository = repository;
            _projectWorkersrepository = projectWorkersrepository;
            _projectSupervisorsrepository = projectSupervisorsrepository;
            _sprintsRepositry = sprintsRepositry;
            _jobsRepositry = jobsRepositry; 
            _userManager = userManager;
        }
        [AbpAuthorize(PermissionNames.Pages_Projects)]
        public override async Task<PagedResultDto<ProjectsDto>> GetAllAsync(PagedProjectsResultRequestDto input)
        {
            var listStudy = _projectsrepository.GetAll();
            var user = await _userManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Count != 0 && roles.Count == 1)
            {
                if (roles.FirstOrDefault() == "Admin")
                {
                    return new PagedResultDto<ProjectsDto>()
                    {
                        Items = ObjectMapper.Map<List<ProjectsDto>>(listStudy
                          .Skip(input.SkipCount)
                           .Take(input.MaxResultCount).ToList()),
                            TotalCount = listStudy.Count()


                    };
                }
                else if (roles.FirstOrDefault() == "Worker")
                {
                    List<ProjectsManagement.ProjectDatabase.Project.Projects> WorkerProjectList = new List<ProjectsManagement.ProjectDatabase.Project.Projects>();
                    var WorkersProject = await _projectWorkersrepository.GetAll().Where(x => x.WorkerId == user.Id).Select(x => x.ProjectId).ToListAsync();

                    foreach (var projectId in WorkersProject)
                    {
                        foreach (var project in listStudy)
                        {
                            if (projectId == project.Id)
                            {
                                WorkerProjectList.Add(project);
                            }
                        }
                    }

                    return new PagedResultDto<ProjectsDto>()
                    {
                        Items = ObjectMapper.Map<List<ProjectsDto>>(WorkerProjectList
                         .Skip(input.SkipCount)
                          .Take(input.MaxResultCount).ToList()),
                        TotalCount = WorkerProjectList.Count()


                    };
                }
                else
                {
                    List<ProjectsManagement.ProjectDatabase.Project.Projects> SupervisorProjectList = new List<ProjectsManagement.ProjectDatabase.Project.Projects>();
                    var WorkersProject =await _projectSupervisorsrepository.GetAll().Where(x => x.SupervisorId == user.Id).Select(x => x.ProjectId).ToListAsync();

                    foreach (var projectId in WorkersProject)
                    {
                        foreach (var project in listStudy)
                        {
                            if (projectId == project.Id)
                            {
                                SupervisorProjectList.Add(project);
                            }
                        }
                    }

                    return new PagedResultDto<ProjectsDto>()
                    {
                        Items = ObjectMapper.Map<List<ProjectsDto>>(SupervisorProjectList
                         .Skip(input.SkipCount)
                          .Take(input.MaxResultCount).ToList()),
                        TotalCount = SupervisorProjectList.Count()


                    };
                }

            }

            if (roles.Count>1)
            {
                foreach (var role in roles)
                {
                    if(role=="Admin")
                    {
                        return new PagedResultDto<ProjectsDto>()
                        {
                            Items = ObjectMapper.Map<List<ProjectsDto>>(listStudy
                         .Skip(input.SkipCount)
                          .Take(input.MaxResultCount).ToList()),
                            TotalCount = listStudy.Count()


                        };
                    }
                }
                if((roles[1]=="Worker" && roles[0]== "Supervisor") || (roles[0] == "Worker" && roles[1] == "Supervisor"))
                {
                    List<ProjectsManagement.ProjectDatabase.Project.Projects> SupervisorProjectList = new List<ProjectsManagement.ProjectDatabase.Project.Projects>();
                    var SupervisorsProject = await _projectSupervisorsrepository.GetAll().Where(x => x.SupervisorId == user.Id).Select(x => x.ProjectId).ToListAsync();
                    var WorkersProject = await _projectWorkersrepository.GetAll().Where(x => x.WorkerId == user.Id).Select(x => x.ProjectId).ToListAsync();

                    foreach (var projectId in SupervisorsProject)
                    {
                        foreach (var project in listStudy)
                        {
                            if (projectId == project.Id)
                            {
                                SupervisorProjectList.Add(project);
                            }
                        }
                    }
                    foreach (var projectId in WorkersProject)
                    {
                        foreach (var project in listStudy)
                        {
                            if (projectId == project.Id)
                            {
                                SupervisorProjectList.Add(project);
                            }
                        }
                    }

                    return new PagedResultDto<ProjectsDto>()
                    {
                        Items = ObjectMapper.Map<List<ProjectsDto>>(SupervisorProjectList
                         .Skip(input.SkipCount)
                          .Take(input.MaxResultCount).ToList()),
                        TotalCount = SupervisorProjectList.Count()


                    };
                }

            }

            return new PagedResultDto<ProjectsDto>()
            {
                Items = ObjectMapper.Map<List<ProjectsDto>>(listStudy
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount).ToList()),

                TotalCount = listStudy.Count()


            };
        }
        [AbpAuthorize(PermissionNames.Pages_Projects_CreateProject)]
        public override Task<ProjectsDto> CreateAsync(CreateProjectsDto input)
        {
            return base.CreateAsync(input);
        }

        public async Task<EditProjectsDto> GetProjectsForEdit(EntityDto<long> input)
        {
            var study = _projectsrepository.Get(input.Id);
            var model = ObjectMapper.Map<EditProjectsDto>(study);

            return model;
        }

        [AbpAuthorize(PermissionNames.Pages_Projects_EditProject)]
        public override async Task<ProjectsDto> UpdateAsync(UpdateInputDto input)
        {
            return  await base.UpdateAsync(input);
        }

        public override async Task DeleteAsync(EntityDto<long> input)
        {
           var jobs=  _jobsRepositry.GetAll().Where(x=>x.ProjectId==input.Id);
            foreach(var job in jobs)
                await _jobsRepositry.DeleteAsync(job); 
            var sprints=  _sprintsRepositry.GetAll().Where(x=>x.ProjectId==input.Id);
            foreach(var sprint in sprints)
                await _sprintsRepositry.DeleteAsync(sprint); 
            var workers=  _projectWorkersrepository.GetAll().Where(x=>x.ProjectId==input.Id);
            foreach(var worker in workers)
                await _projectWorkersrepository.DeleteAsync(worker); 
            var supervisors=  _projectSupervisorsrepository.GetAll().Where(x=>x.ProjectId==input.Id);
            foreach(var supervisor in supervisors)
                await _projectSupervisorsrepository.DeleteAsync(supervisor);

              await  base.DeleteAsync(input);
          
        }
    }
}
