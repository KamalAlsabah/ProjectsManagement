using ProjectsManagement.ProjectDatabase.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.Home.HomeStatistics.Dto
{
    public class CreateHomeStatisticsTypeDto
    {
        public List<HomeStatisticsUserTypes> HomeStatisticsUserTypes { get; set; }
        public ProjectsManagement.ProjectDatabase.Home.HomeStatistics HomeStatistics { get; set; }
    }
}
