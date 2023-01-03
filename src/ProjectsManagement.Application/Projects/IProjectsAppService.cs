using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ProjectsManagement.Project.Dto;
using System.Threading.Tasks;

namespace ProjectsManagement.Project
{
    public interface IProjectsAppService : IAsyncCrudAppService<ProjectsDto, long, PagedProjectsResultRequestDto, CreateProjectsDto, ListProjectsDto>
    {
        Task<EditProjectsDto> GetProjectsForEdit(EntityDto<long> input);

    }
}
