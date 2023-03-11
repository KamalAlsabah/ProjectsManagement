using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ProjectsManagement.WorkersJobs.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.WorkersJobs
{
    public interface IWorkersJobsAppService : IAsyncCrudAppService<WorkersJobsDto, long, PagedWorkersJobsResultRequestDto, CreateWorkersJobsDto, UpdateInputDto>
    {
        Task<EditWorkersJobsDto> GetWorkersJobsForEdit(EntityDto input);
        Task CreateJobWorkers(long JobId, long[] Arr);


    }
}
