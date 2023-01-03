using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ProjectsManagement.Jobs.Dto;
using System.Threading.Tasks;

namespace JobManagement.Jobs
{
    public interface IJobsAppService : IAsyncCrudAppService<JobsDto, long, PagedJobResultRequestDto, CreateJobDto, UpdateInputDto>
    {
        Task<EditJobDto> GetJobForEdit(EntityDto input);

    }
}
