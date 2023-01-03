using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ProjectsManagement.JobTasks.Dto;
using System.Threading.Tasks;

namespace ProjectsManagement.JobTasks
{
    public interface IJobTasksAppService : IAsyncCrudAppService<JobTasksDto, long, PagedJobTasksResultRequestDto, CreateJobTasksDto, UpDateInputJobTasksDto>
    {
        Task<EditJobTasksDto> GetJobTasksForEdit(EntityDto<long> input);

    }
}
