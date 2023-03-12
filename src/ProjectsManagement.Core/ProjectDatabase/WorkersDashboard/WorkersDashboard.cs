using Abp.Domain.Entities.Auditing;
using ProjectsManagement.Authorization.Users;
using ProjectsManagement.ProjectDatabase.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.ProjectDatabase.WorkersDashboard
{
    public class WorkersDashboard : FullAuditedEntity<long>
    {
        public long ProjectId { get; set; }
        public Projects Project { get; set; }  
        public long WorkerId { get; set; }
        public User Worker { get; set; }
        public double WorkerJobsCount { get; set; }
    }
}
