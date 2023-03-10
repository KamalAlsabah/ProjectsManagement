using Abp.Domain.Entities.Auditing;
using ProjectsManagement.Authorization.Users;
using ProjectsManagement.ProjectDatabase.Job;
using ProjectsManagement.ProjectDatabase.ProjectWorker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.ProjectDatabase.WorkersJobs
{
    public class WorkersJobs : FullAuditedEntity<long>
    {
        public long? WorkerId { get; set; }
        public User Worker { get; set; }
        public long? JobId { get; set; }
        public Jobs Job { get; set; }
    }
}
