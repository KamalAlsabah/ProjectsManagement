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
    public class WorkersDashboardAppService : AsyncCrudAppService<ProjectsManagement.ProjectDatabase.WorkersDashboard.WorkersDashboard, WorkersDashboardDto, long, PagedWorkersDashboardResultRequestDto, CreateWorkersDashboardDto, UpdateInputDto>, IWorkersDashboardAppService
    {
        private readonly IRepository<ProjectsManagement.ProjectDatabase.WorkersDashboard.WorkersDashboard, long> _WorkersDashboardrepository;
        private readonly IRepository<ProjectsManagement.ProjectDatabase.Project.Projects, long> projectRepo;

        public WorkersDashboardAppService(IRepository<ProjectsManagement.ProjectDatabase.WorkersDashboard.WorkersDashboard, long> repository, IRepository<ProjectsManagement.ProjectDatabase.Project.Projects, long> projectRepo) : base(repository)
        {
            _WorkersDashboardrepository = repository;
            this.projectRepo = projectRepo;
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
            var projectClosed = await projectRepo.GetAll().Where(x => x.Id == input.ProjectId).Select(x => x.Status).FirstOrDefaultAsync();
            if (projectClosed == ProjectStatus.Closed)
            {
                throw new UserFriendlyException("The Project Was Cloesd");
            }
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
    }
}
