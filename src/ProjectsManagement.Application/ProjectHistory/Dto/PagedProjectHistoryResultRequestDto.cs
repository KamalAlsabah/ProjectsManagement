using Abp.Application.Services.Dto;

namespace ProjectsManagement.ProjectHistory.Dto
{
    public class PagedProjectHistoryResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public long? ProjectId { get; set; }
 
    }
}
