using Abp.Application.Services.Dto;

namespace ProjectsManagement.SupervisorNotes.Dto
{
    public class PagedSupervisorNotesResultRequestDto: PagedResultRequestDto
    {
        public string? Keyword { get; set; }
        public long? JobId { get; set; }

    }
}
