using Abp.Application.Services.Dto;
using ProjectsManagement.ProjectDatabase.Enums;
using ProjectsManagement.ProjectDatabase.Sprint;
using System;
using System.Collections.Generic;

namespace ProjectsManagement.Home.HomeStatistics.Dto
{
    public class HomeStatisticsDto : EntityDto<long>
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public string Icon { get; set; }
    }

}
