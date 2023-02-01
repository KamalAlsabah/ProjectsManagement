using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ProjectsManagement.ProjectHistory.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.ProjectHistory
{
    public interface IProjectHistoryAppService : IAsyncCrudAppService<ProjectHistoryDto, long, PagedProjectHistoryResultRequestDto, CreateProjectHistoryDto, UpdateInputDto>
    {
        Task<EditProjectHistoryDto> GetProjectHistoryForEdit(EntityDto input);

    }
}
