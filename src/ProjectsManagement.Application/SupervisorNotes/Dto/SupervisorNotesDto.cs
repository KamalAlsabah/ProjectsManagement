using Abp.Application.Services.Dto;

namespace ProjectsManagement.SupervisorNotes.Dto
{
    public class SupervisorNotesDto : EntityDto<long>
    {
        public string Note { get; set; }
        public string Status { get; set; }
        public long JobId { get; set; }
        public string JobName { get; set; }
        public long JobTasksId { get; set; }
        public string JobTasksName { get; set; }
        public long SupervisorId { get; set; }
        public string SupervisorUserName { get; set; }


    }
}
