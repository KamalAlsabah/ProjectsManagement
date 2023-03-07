using Abp.Domain.Entities.Auditing;
using ProjectsManagement.ProjectDatabase.Enums;
using ProjectsManagement.ProjectDatabase.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.ProjectDatabase.Sprint
{
    public class Sprints:FullAuditedEntity<long>
    {
        public string NameL { get; set; }
        public string NameF { get; set; }
        public string Description { get; set; }
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
        public SprintStatus Status { get; set; }
        public long ProjectId { get; set; }
        public long WieghtOfHours { get; set; }
        public Projects Project { get; set; }
        public DateTime StartDate { get; set; }=DateTime.Now;
        public DateTime ExpectedEndDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
    }
}
