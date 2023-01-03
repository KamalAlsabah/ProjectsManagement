using Abp;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ProjectsManagement.ProjectWorkers.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectWorkerManagement.ProjectWorkers
{
    public interface IProjectWorkersAppService : IAsyncCrudAppService<ProjectWorkersDto, long, PagedProjectWorkerResultRequestDto, CreateProjectWorkerDto, UpdateInputDto>
    {
        Task<EditProjectWorkerDto> GetProjectWorkerForEdit(EntityDto<long> input);
        Task<List<NameValue<long>>> GetWorkers(EntityDto<long> input);

    }
}
