using Abp.Domain.Entities.Auditing;
using ProjectsManagement.Authorization.Users;
using ProjectsManagement.ProjectDatabase.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.ProjectDatabase.ProjectSupervisor
{
    public class ProjectSupervisors:FullAuditedEntity<long>
    {
        public long SupervisorId { get; set; }
        public User Supervisor { get; set; }

        public long ProjectId { get; set; }
        public Projects Project { get; set; }
    }
}
