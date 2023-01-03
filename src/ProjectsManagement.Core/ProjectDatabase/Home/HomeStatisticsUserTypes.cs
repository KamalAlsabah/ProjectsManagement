using Abp.Domain.Entities.Auditing;
using ProjectsManagement.Authorization.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.ProjectDatabase.Home
{
    public class HomeStatisticsUserTypes:FullAuditedEntity<long>
    {
        public long HomeStatisticsId { get; set; }
        public HomeStatistics HomeStatistics { get; set; }
        public Role UserType { get; set; }
        public int UserTypeId { get; set; }

    }
}
