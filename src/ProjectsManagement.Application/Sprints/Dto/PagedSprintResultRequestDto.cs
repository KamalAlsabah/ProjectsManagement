using Abp.Application.Services.Dto;

namespace ProjectsManagement.Sprints.Dto
{
    public class PagedSprintResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public long ProjectId { get; set; }

    }
}
