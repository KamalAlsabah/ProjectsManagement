using Abp.Domain.Entities.Auditing;
using ProjectsManagement.Authorization.Users;
using ProjectsManagement.ProjectDatabase.ProjectWorker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.ProjectDatabase.WorkersHistory
{
    public class WorkersHistory : FullAuditedEntity<long>
    {
        public DateTime LogInTime { get; set; } = DateTime.Now;
        public DateTime LogOutTime { get; set; } = DateTime.Now;
        public DateTime CreationTime { get; set; }=DateTime.Now;
        public long TotalHours { 
            get {
                return (long)(LogOutTime - LogInTime).TotalHours;
            } 
        }
        public long WorkerId { get; set; }
        public User Worker { get; set; }
    }
}
