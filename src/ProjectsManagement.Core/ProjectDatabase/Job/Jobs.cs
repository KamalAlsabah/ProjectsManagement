using Abp.Domain.Entities.Auditing;
using ProjectsManagement.Authorization.Users;
using ProjectsManagement.ProjectDatabase.Enums;
using ProjectsManagement.ProjectDatabase.Project;
using ProjectsManagement.ProjectDatabase.Sprint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.ProjectDatabase.Job
{
    public class Jobs:FullAuditedEntity<long>
    {
        public string NameF { get; set; }
        public string NameL { get; set; }
        public string Name
        {
            get
            {
                var name = "";
                if (System.Globalization.CultureInfo.CurrentUICulture.Name != "ar" && !string.IsNullOrEmpty(this.NameF))
                {
                    name = this.NameF;
                }
                else
                {
                    name = this.NameL;
                }
                return name;
            }
        }
        public string Description { get; set; }
        public long ProjectId { get; set; }
        public Projects Project { get; set; }
        public long? SprintId { get; set; }
        public Sprints Sprint { get; set; }
        public int WieghtOfHours  { get; set; }
        public long? WorkerId { get; set; }
        public User Worker { get; set; }
        public int ExpectedNoOfHours { get; set; }
        public int ActualNumberOfHours  { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;

        public DateTime StartDate { get; set; }=DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
        public JobStatus Status { get; set; }
    }
}
