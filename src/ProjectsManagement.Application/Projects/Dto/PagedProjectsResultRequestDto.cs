using Abp.Application.Services.Dto;

namespace ProjectsManagement.Project.Dto
{
    public class PagedProjectsResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
 
    }
}
