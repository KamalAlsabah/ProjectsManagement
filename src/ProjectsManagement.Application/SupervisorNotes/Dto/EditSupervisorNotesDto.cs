using Abp.Application.Services.Dto;
using ProjectsManagement.ProjectDatabase.Enums;

namespace ProjectsManagement.SupervisorNotes.Dto
{
    public class EditSupervisorNotesDto : EntityDto<long>
    {
        public string Note { get; set; }
        public long JobId { get; set; }
        public long JobTasksId { get; set; }
        public long SupervisorId { get; set; }
        public SupervisorNotesStatus Status { get; set; }

    }
}
