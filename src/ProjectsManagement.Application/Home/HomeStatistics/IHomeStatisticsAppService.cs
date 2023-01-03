using Abp.Application.Services.Dto;
using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectsManagement.Home.HomeStatistics.Dto;

namespace ProjectsManagement.Home.HomeStatistics
{
    public interface IHomeStatisticsAppService : IAsyncCrudAppService<HomeStatisticsDto, long, PagedHomeStatisticsResultRequestDto, CreateHomeStatisticsDto, UpdateInputDto>
    {
        Task<EditHomeStatisticsDto> GetHomeStatisticsForEdit(EntityDto input);
        Task<List<NameValueDto<int>>> GetUserTypes();
    }
}
