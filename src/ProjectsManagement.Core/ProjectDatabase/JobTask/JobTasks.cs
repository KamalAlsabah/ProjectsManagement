using Abp.Domain.Entities.Auditing;
using ProjectsManagement.ProjectDatabase.Job;

namespace ProjectsManagement.ProjectDatabase.JobTask
{
    public class JobTasks : FullAuditedEntity<long>
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
        public long? JobId { get; set; }
        public Jobs Job { get; set; }
    }
}
