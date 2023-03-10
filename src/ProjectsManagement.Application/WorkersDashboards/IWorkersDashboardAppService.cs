using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ProjectsManagement.WorkersDashboards.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.WorkersDashboards
{
    public interface IWorkersDashboardAppService : IAsyncCrudAppService<WorkersDashboardDto, long, PagedWorkersDashboardResultRequestDto, CreateWorkersDashboardDto, UpdateInputDto>
    {
        Task<EditWorkersDashboardDto> GetWorkersDashboardForEdit(EntityDto input);

    }
}
