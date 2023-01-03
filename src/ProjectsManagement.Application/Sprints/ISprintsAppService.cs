using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ProjectsManagement.Sprints.Dto;
using System.Threading.Tasks;

namespace SprintManagement.Sprints
{
    public interface ISprintsAppService : IAsyncCrudAppService<SprintsDto, long, PagedSprintResultRequestDto, CreateSprintDto, UpdateInputDto>
    {
        Task<EditSprintDto> GetSprintForEdit(EntityDto input);

    }
}
