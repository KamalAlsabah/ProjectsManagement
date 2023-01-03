using Abp.Application.Services.Dto;

namespace ProjectsManagement.Home.HomeStatistics.Dto
{
    public class PagedHomeStatisticsResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
