using Abp.Domain.Entities.Auditing;
using ProjectsManagement.Authorization.Users;
using ProjectsManagement.ProjectDatabase.Enums;
using ProjectsManagement.ProjectDatabase.Job;
using ProjectsManagement.ProjectDatabase.JobTask;
using ProjectsManagement.ProjectDatabase.Project;
using ProjectsManagement.ProjectDatabase.ProjectSupervisor;
using ProjectsManagement.ProjectDatabase.ProjectWorker;
using ProjectsManagement.ProjectDatabase.Sprint;
using ProjectsManagement.ProjectDatabase.SupervisorNotes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.ProjectDatabase.ProjectHistory
{
    public class ProjectHistory : FullAuditedEntity<long>
    {
        public long UserId { get; set; }
        public User User { get; set; }
        public long ProjectId { get; set; }
        public Projects Project { get; set; }
        public long? SprintId { get; set; }
        public Sprints Sprint { get; set; }
        public long? JobId { get; set; }
        public Jobs Job { get; set; }  
        public long? JobTasksId { get; set; }
        public JobTasks JobTasks { get; set; }   
        public long? ProjectWorkersId { get; set; }
        public ProjectWorkers ProjectWorkers { get; set; } 
        public long? ProjectSupervisorsId { get; set; }
        public ProjectSupervisors ProjectSupervisors { get; set; } 
        public long? SupervisorNotesId { get; set; }
        public ProjectsManagement.ProjectDatabase.SupervisorNotes.SupervisorNotes SupervisorNotes { get; set; }
        public DateTime CreationTime { get; set; } =DateTime.Now;
        public ProjectHistoryActions ProjectHistoryActions { get; set; }
        public ProjectHistoryColumns ProjectHistoryColumns { get; set; }
        public string Description { get; set; }

    }
}
