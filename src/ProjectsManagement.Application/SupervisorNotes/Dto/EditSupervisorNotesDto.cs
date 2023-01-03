using Abp.Application.Services.Dto;

namespace ProjectsManagement.SupervisorNotes.Dto
{
    public class EditSupervisorNotesDto : EntityDto<long>
    {
        public string Note { get; set; }
        public long JobId { get; set; }
        public long SupervisorId { get; set; }

    }
}
