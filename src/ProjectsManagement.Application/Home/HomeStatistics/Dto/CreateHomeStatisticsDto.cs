using ProjectsManagement.ProjectDatabase.Enums;
using System;
using System.Collections.Generic;

namespace ProjectsManagement.Home.HomeStatistics.Dto
{
    public class CreateHomeStatisticsDto
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public EHomeStatistics Type { get; set; }
        public List<int> UserTypesId { get; set; }
    }
}
