using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProjectsManagement.Authorization;
using ProjectsManagement.ProjectHistory.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.ProjectHistory
{
    public class ProjectHistoryAppService : AsyncCrudAppService<ProjectsManagement.ProjectDatabase.ProjectHistory.ProjectHistory, ProjectHistoryDto, long, PagedProjectHistoryResultRequestDto, CreateProjectHistoryDto, UpdateInputDto>, IProjectHistoryAppService
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IRepository<ProjectsManagement.ProjectDatabase.ProjectHistory.ProjectHistory, long> _ProjectHistoryrepository;
        private readonly IRepository<ProjectsManagement.ProjectDatabase.Project.Projects, long> _Projectrepository;
        private readonly IRepository<ProjectsManagement.ProjectDatabase.ProjectWorker.ProjectWorkers, long> _ProjectWorkersrepository;
        private readonly IRepository<ProjectsManagement.ProjectDatabase.ProjectSupervisor.ProjectSupervisors, long> _ProjectSupervisorsrepository;
        private readonly IRepository<ProjectsManagement.ProjectDatabase.Sprint.Sprints, long> _Sprintsrepository;
        private readonly IRepository<ProjectsManagement.ProjectDatabase.Job.Jobs, long> _Jobsrepository;
        private readonly IRepository<ProjectsManagement.ProjectDatabase.JobTask.JobTasks, long> _JobTasksrepository;
        private readonly IRepository<ProjectsManagement.ProjectDatabase.SupervisorNotes.SupervisorNotes, long> _SupervisorNotesrepository;

        public ProjectHistoryAppService(
            IRepository<ProjectsManagement.ProjectDatabase.ProjectHistory.ProjectHistory, long> repository,
            IHttpContextAccessor accessor,
            IRepository<ProjectsManagement.ProjectDatabase.Project.Projects, long> Projectrepository,
            IRepository<ProjectsManagement.ProjectDatabase.ProjectWorker.ProjectWorkers, long> ProjectWorkersrepository,
            IRepository<ProjectsManagement.ProjectDatabase.ProjectSupervisor.ProjectSupervisors, long> ProjectSupervisorsrepository,
            IRepository<ProjectsManagement.ProjectDatabase.Sprint.Sprints, long> Sprintsrepository,
            IRepository<ProjectsManagement.ProjectDatabase.Job.Jobs, long> Jobsrepository,
            IRepository<ProjectsManagement.ProjectDatabase.JobTask.JobTasks, long> JobTasksrepository,
            IRepository<ProjectsManagement.ProjectDatabase.SupervisorNotes.SupervisorNotes, long> SupervisorNotesrepository
            ) : base(repository)
        {
            _accessor = accessor;
            _ProjectHistoryrepository = repository;
            _Jobsrepository = Jobsrepository;
            _SupervisorNotesrepository = SupervisorNotesrepository;
            _JobTasksrepository= JobTasksrepository;
            _ProjectWorkersrepository= ProjectWorkersrepository;
            _ProjectSupervisorsrepository= ProjectSupervisorsrepository;
            _Sprintsrepository= Sprintsrepository;
            _Projectrepository= Projectrepository;
        }
        [AbpAuthorize(PermissionNames.Pages_ProjectHistory)]

        public override async Task<PagedResultDto<ProjectHistoryDto>> GetAllAsync(PagedProjectHistoryResultRequestDto input)
        {

            var listProjectHistorys = _ProjectHistoryrepository.GetAll()
                .Where(X=>X.ProjectId==input.ProjectId)
                .Include(x=>x.User)
                .Include(x => x.Project).Include(x=>x.Job).Include(x=>x.JobTasks)
                .Include(x=>x.ProjectSupervisors).Include(x=>x.ProjectWorkers)
                .Include(x=>x.SupervisorNotes).Include(x=>x.Sprint)
                .WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Description.Contains(input.Keyword));
            var items = ObjectMapper.Map<List<ProjectHistoryDto>>(listProjectHistorys
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount));
            return new PagedResultDto<ProjectHistoryDto>()
            {
                Items = items,
                TotalCount = listProjectHistorys.Count()
            };
        }
         public override async Task<ProjectHistoryDto> CreateAsync(CreateProjectHistoryDto input)
        {
            input.UserId= long.Parse(_accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var ProjectName = _Projectrepository.GetAll().Where(x => x.Id == input.ProjectId).Select(x=>x.Name).FirstOrDefault();
            if (input.ProjectId > 0 )
            {
                input.Description = $"The {input.ProjectHistoryColumns} {ProjectName} has been {input.ProjectHistoryActions}";
            }
            if (input.ProjectSupervisorsId > 0)
            {
                var Name = _ProjectSupervisorsrepository.GetAll().Include(x=>x.Supervisor).Where(x => x.Id == input.ProjectSupervisorsId).Select(x => x.Supervisor.Name).FirstOrDefault();
                input.Description = $"The {input.ProjectHistoryColumns} {Name} has been {input.ProjectHistoryActions}  ";
            }
           else if (input.ProjectWorkersId > 0)
            {
                var Name = _ProjectWorkersrepository.GetAll().Include(x => x.Worker).Where(x => x.Id == input.ProjectWorkersId).Select(x => x.Worker.Name).FirstOrDefault();
                input.Description = $"The {input.ProjectHistoryColumns} {Name} has been {input.ProjectHistoryActions}  ";
            }
           else if (input.SprintId>0)
            {
                var Name = _Sprintsrepository.GetAll().Where(x => x.Id == input.SprintId).Select(x => x.Name).FirstOrDefault();
                input.Description = $"The {input.ProjectHistoryColumns} {Name} has been {input.ProjectHistoryActions}  ";
            }
           else if (input.JobId > 0)
            {
                var Name = _Jobsrepository.GetAll().Where(x => x.Id == input.JobId).Select(x => x.Name).FirstOrDefault();
                input.Description = $"The {input.ProjectHistoryColumns} {Name} has been {input.ProjectHistoryActions}  ";
            }
           else if (input.JobTasksId > 0)
            {
                var JobTask = _JobTasksrepository.GetAll().Include(x=>x.Job).Where(x => x.Id == input.JobTasksId).FirstOrDefault();
                input.Description = $"The {input.ProjectHistoryColumns} {JobTask.Name} has been {input.ProjectHistoryActions} in The job {JobTask.Job.Name}  ";
            }
           else if(input.SupervisorNotesId>0)
            {
                var Name = _SupervisorNotesrepository.GetAll().Where(x => x.Id == input.SupervisorNotesId).Include(x=>x.Supervisor).Select(x => x.Supervisor.Name).FirstOrDefault();
                input.Description = $"The {input.ProjectHistoryColumns} {Name} has been {input.ProjectHistoryActions} ";
            }
            


            return await base.CreateAsync(input);
        }
        public async Task<EditProjectHistoryDto> GetProjectHistoryForEdit(EntityDto input)
        {
            var ProjectHistory = await _ProjectHistoryrepository.GetAll().Where(x => x.Id == input.Id).FirstOrDefaultAsync();
            var model = ObjectMapper.Map<EditProjectHistoryDto>(ProjectHistory);
            return model;
        }

        public override async Task<ProjectHistoryDto> UpdateAsync(UpdateInputDto input)
        {
            var projectClosed = await _ProjectHistoryrepository.GetAll().Where(x => x.Id == input.Id).Select(x => x.Project.Status).FirstOrDefaultAsync();
            return await base.UpdateAsync(input);
        }
        

        public override async Task DeleteAsync(EntityDto<long> input)
        {
            var projectClosed = await _ProjectHistoryrepository.GetAll().Where(x => x.Id == input.Id).Select(x => x.Project.Status).FirstOrDefaultAsync();
            await base.DeleteAsync(input);
        }

    }
}
