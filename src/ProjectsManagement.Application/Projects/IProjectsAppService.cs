using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ProjectsManagement.Project.Dto;
using ProjectsManagement.Projects.Dto;
using System.Threading.Tasks;

namespace ProjectsManagement.Project
{
    public interface IProjectsAppService : IAsyncCrudAppService<ProjectsDto, long, PagedProjectsResultRequestDto, CreateProjectsDto, UpdateInputDto>
    {
        Task<EditProjectsDto> GetProjectsForEdit(EntityDto<long> input);

    }
}
