using Abp.Domain.Entities.Auditing;
using ProjectsManagement.Authorization.Users;
using ProjectsManagement.ProjectDatabase.Enums;
using ProjectsManagement.ProjectDatabase.Project;

namespace ProjectsManagement.ProjectDatabase.Suggestion
{
    public class Suggestions : FullAuditedEntity<long>
    {
        public Projects Project { get; set; }
        public long ProjectId { get; set; }

        public User Supervisor { get; set; }
        public long SupervisorId { get; set; }

        public string Description { get; set; }

        public SuggestionStatus Status { get; set; }
    }
}

