using Abp.Domain.Entities.Auditing;
using ProjectsManagement.ProjectDatabase.Enums;
using System;

namespace ProjectsManagement.ProjectDatabase.Project
{
    public class Projects : FullAuditedEntity<long>
    {
        public string NameL { get; set; }
        public string NameF { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
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
        public string TestUrl { get; set; }
        public ProjectStatus Status { get; set; }
    }
}
