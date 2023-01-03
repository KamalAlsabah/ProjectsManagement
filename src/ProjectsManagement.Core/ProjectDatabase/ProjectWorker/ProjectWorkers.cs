

using Abp.Domain.Entities.Auditing;
using ProjectsManagement.Authorization.Users;
using ProjectsManagement.ProjectDatabase.Project;

namespace ProjectsManagement.ProjectDatabase.ProjectWorker
{
    public class ProjectWorkers:FullAuditedEntity<long>
    {
        public long WorkerId { get; set; }
        public User Worker { get; set; }

        public long ProjectId { get; set; }
        public Projects Project { get; set; }
    }
}
