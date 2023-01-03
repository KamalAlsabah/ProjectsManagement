using Abp.Application.Services.Dto;

namespace ProjectsManagement.ProjectWorkers.Dto
{
    public class PagedProjectWorkerResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }

    }
}
