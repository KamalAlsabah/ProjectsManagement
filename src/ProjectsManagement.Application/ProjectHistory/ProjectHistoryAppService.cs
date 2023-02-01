using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using ProjectsManagement.ProjectHistory.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.ProjectHistory
{
    public class ProjectHistoryAppService : AsyncCrudAppService<ProjectsManagement.ProjectDatabase.ProjectHistory.ProjectHistory, ProjectHistoryDto, long, PagedProjectHistoryResultRequestDto, CreateProjectHistoryDto, UpdateInputDto>, IProjectHistoryAppService
    {
        private readonly IRepository<ProjectsManagement.ProjectDatabase.ProjectHistory.ProjectHistory, long> _ProjectHistoryrepository;

        public ProjectHistoryAppService(IRepository<ProjectsManagement.ProjectDatabase.ProjectHistory.ProjectHistory, long> repository) : base(repository)
        {
            _ProjectHistoryrepository = repository;
        }

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
