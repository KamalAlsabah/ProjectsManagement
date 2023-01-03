using Abp.Application.Services.Dto;

namespace ProjectsManagement.Jobs.Dto
{
    public class PagedJobResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public long ProjectId { get; set; }

    }
}
