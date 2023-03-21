using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ProjectsManagement.ProjectDetails.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.ProjectDetails
{
    public interface IProjectDetailsAppService : IAsyncCrudAppService<ProjectDetailsDto, long, PagedProjectDetailsResultRequestDto, CreateProjectDetailsDto, UpdateInputDto>
    {
        Task<EditProjectDetailsDto> GetProjectDetailsForEdit(EntityDto<long> input);
    }
}
