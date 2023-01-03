using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;

namespace ProjectsManagement.Home.HomeStatistics.Dto
{
    public class ListHomeStatisticsDto : EntityDto<long>
    {

        public string Name { get; set; }
        public double Value { get; set; }
        public string Icon { get; set; }

    }
}
