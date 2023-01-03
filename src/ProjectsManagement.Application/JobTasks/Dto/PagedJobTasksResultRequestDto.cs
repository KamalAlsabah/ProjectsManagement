using Abp.Application.Services.Dto;

namespace ProjectsManagement.JobTasks.Dto
{
    public class PagedJobTasksResultRequestDto: PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public long JobId { get; set; }

    }
}
