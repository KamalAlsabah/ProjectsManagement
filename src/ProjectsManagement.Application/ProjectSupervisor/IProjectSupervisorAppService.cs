using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ProjectsManagement.ProjectSupervisor.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.ProjectSupervisor
{
    public interface IProjectSupervisorAppService :IAsyncCrudAppService<ProjectSupervisorListDto,long,ProjectSupervisorPagedDto,ProjectSupervisorCreateDto, UpDateInputProjectSupervisorDto>
    {
        Task<ProjectSupervisorEditDto> GetProjectSupervisorForEdit(EntityDto<long> input);
        Task<ListResultDto<ComboboxItemDto>> GetSupervisors (EntityDto<long> input);
        Task<ListResultDto<ComboboxItemDto>> GetProjects();

    }
}
