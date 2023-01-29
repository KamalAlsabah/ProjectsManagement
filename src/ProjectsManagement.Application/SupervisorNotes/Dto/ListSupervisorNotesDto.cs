using Abp.Application.Services.Dto;
using ProjectsManagement.Authorization.Users;

namespace ProjectsManagement.SupervisorNotes.Dto
{
    public class ListSupervisorNotesDto: EntityDto<long>
    {
        public string Note { get; set; }
        public long JobId { get; set; }
        public string Status { get; set; }
        public ProjectsManagement.ProjectDatabase.Job.Jobs Job { get; set; }
        public long JobTasksId { get; set; }
        public ProjectsManagement.ProjectDatabase.JobTask.JobTasks JobTasks { get; set; }
        public long SupervisorId { get; set; }
        public User Supervisor { get; set; }
    }
}
