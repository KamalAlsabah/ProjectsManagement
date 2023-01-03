using Abp;
using Abp.Domain.Entities.Auditing;
using ProjectsManagement.ProjectDatabase.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.ProjectDatabase.Home
{
    public class HomeStatistics:FullAuditedEntity<long>
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public EHomeStatistics Type { get; set; }
        public List<HomeStatisticsUserTypes> UserTypes { get; set; }
    }
}
