using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using ProjectsManagement.ProjectWorkers.Dto;
using ProjectsManagement.ProjectDatabase.Sprint;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectsManagement.Authorization.Roles;
using Abp;
using ProjectsManagement.Authorization.Users;
using ProjectsManagement.ProjectDatabase.Enums;
using Abp.UI;
using Abp.Authorization;
using ProjectsManagement.Authorization;

namespace ProjectWorkerManagement.ProjectWorkers
{

    public class ProjectWorkersAppService : AsyncCrudAppService<ProjectsManagement.ProjectDatabase.ProjectWorker.ProjectWorkers, ProjectWorkersDto, long, PagedProjectWorkerResultRequestDto, CreateProjectWorkerDto, UpdateInputDto>, IProjectWorkersAppService
    {
        private readonly IRepository<ProjectsManagement.ProjectDatabase.ProjectWorker.ProjectWorkers, long> _ProjectWorkerrepository;
        private readonly IRepository<ProjectsManagement.ProjectDatabase.Project.Projects, long> projectRepo;
        private readonly UserManager userManager;


        public ProjectWorkersAppService(IRepository<ProjectsManagement.ProjectDatabase.ProjectWorker.ProjectWorkers, long> repository, UserManager userManager, IRepository<ProjectsManagement.ProjectDatabase.Project.Projects, long> projectRepo) : base(repository)
        {
            _ProjectWorkerrepository = repository;
            this.userManager = userManager;
            this.projectRepo = projectRepo;
        }
        [AbpAuthorize(PermissionNames.Pages_ProjectsWorkers)]
        public override async Task<PagedResultDto<ProjectWorkersDto>> GetAllAsync(PagedProjectWorkerResultRequestDto input)
        {

            var listProjectWorkers = _ProjectWorkerrepository.GetAll().Include(x=>x.Project).Include(x=>x.Worker)
                .WhereIf(input.ProjectId>0,x=>x.ProjectId == input.ProjectId)
                .OrderBy(x=>x.CreationTime)
                .WhereIf(!string.IsNullOrWhiteSpace(input.Keyword) ,x =>x.Worker.FullName.Contains(input.Keyword)
                || x.Project.NameL.Contains(input.Keyword) 
                || x.Project.NameF.Contains(input.Keyword));
            return new PagedResultDto<ProjectWorkersDto>()
            {
                Items = ObjectMapper.Map<List<ProjectWorkersDto>>(listProjectWorkers
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)),
                TotalCount = listProjectWorkers.Count()
            };
        }


        public override async Task<ProjectWorkersDto> CreateAsync(CreateProjectWorkerDto input)
        {
            var projectClosed = await projectRepo.GetAll().Where(x => x.Id == input.ProjectId).Select(x => x.Status).FirstOrDefaultAsync();
            if(projectClosed==ProjectStatus.Closed)
            {
                throw new UserFriendlyException("The Project Was Cloesd");
            }
            return await base.CreateAsync(input);
        }

       

        public async Task<EditProjectWorkerDto> GetProjectWorkerForEdit(EntityDto<long> input)
        {
            var projectWorker =await _ProjectWorkerrepository.GetAll().Where(x=>x.Id== input.Id).FirstOrDefaultAsync();
            var model = ObjectMapper.Map<EditProjectWorkerDto>(projectWorker);

            return model;
        }

        public async Task<List<NameValue<long>>> GetWorkers(EntityDto<long> input)
        {
            var projectWorkers =await Repository.GetAll().Where(x => x.ProjectId == input.Id).Select(x=>x.WorkerId).ToListAsync();
           var Users = (await userManager.GetUsersInRoleAsync(StaticRoleNames.Host.Worker)).Where(x => !projectWorkers.Contains(x.Id))
                .Select(x => new NameValue<long> { Name = x.FullName, Value = x.Id }).ToList();
            return Users;
        }
        public override async Task<ProjectWorkersDto> UpdateAsync(UpdateInputDto input)
        {
            var projectClosed = await _ProjectWorkerrepository.GetAll().Where(x => x.ProjectId == input.ProjectId).Select(x => x.Project.Status).FirstOrDefaultAsync();
            if (projectClosed==ProjectStatus.Closed)
            {
                throw new UserFriendlyException("The Project Was Cloesd");
            }
            return await base.UpdateAsync(input);
        }
        public override async Task DeleteAsync(EntityDto<long> input)
        {
            var projectClosed = await _ProjectWorkerrepository.GetAll().Where(x => x.Id == input.Id).Select(x => x.Project.Status).FirstOrDefaultAsync();
            if (projectClosed == ProjectStatus.Closed)
            {
                throw new UserFriendlyException("The Project Was Cloesd");
            }
            await base.DeleteAsync(input);
        }
    }
}
