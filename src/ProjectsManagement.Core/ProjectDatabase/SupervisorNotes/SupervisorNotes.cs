using Abp.Domain.Entities.Auditing;
using ProjectsManagement.Authorization.Users;
using ProjectsManagement.ProjectDatabase.Enums;
using ProjectsManagement.ProjectDatabase.Job;
using ProjectsManagement.ProjectDatabase.JobTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.ProjectDatabase.SupervisorNotes
{
    public class SupervisorNotes:FullAuditedEntity<long>
    {
        public string Note { get; set; }
        public SupervisorNotesStatus Status { get; set; } = SupervisorNotesStatus.Under_review;
        public long? JobTasksId { get; set; }
        public JobTasks JobTasks { get; set; }
        public long JobId { get; set; }
        public Jobs Job { get; set; }
        public long SupervisorId { get; set; }
        public User Supervisor { get; set; }
    }
}
