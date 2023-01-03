using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Identity;
using ProjectsManagement.Authorization;
using ProjectsManagement.Authorization.Users;
using ProjectsManagement.Project.Dto;
using ProjectsManagement.ProjectDatabase.Project;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectsManagement.Project
{

    public class ProjectsAppService : AsyncCrudAppService<Projects, ProjectsDto, long, PagedProjectsResultRequestDto, CreateProjectsDto, ListProjectsDto>, IProjectsAppService
    {
        private readonly IRepository<Projects, long> _projectsrepository;
         private readonly  UserManager _userManager;
        public ProjectsAppService(
            IRepository<Projects, long> repository,
              UserManager userManager) : base(repository)
        {
            _projectsrepository = repository;
            _userManager = userManager;
        }

        public override async Task<PagedResultDto<ProjectsDto>> GetAllAsync(PagedProjectsResultRequestDto input)
        {
            var user = await _userManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            var roles = await _userManager.GetRolesAsync(user);

            var listStudy = _projectsrepository.GetAll();

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


        public override Task<ProjectsDto> UpdateAsync(ListProjectsDto input)
        {
            return base.UpdateAsync(input);
        }
    }
}
