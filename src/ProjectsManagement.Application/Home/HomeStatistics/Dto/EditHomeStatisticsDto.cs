using Abp.Application.Services.Dto;
using ProjectsManagement.ProjectDatabase.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectsManagement.Home.HomeStatistics.Dto
{
    public class EditHomeStatisticsDto : EntityDto<long>
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public EHomeStatistics Type { get; set; }

        public List<int> GrantedUserTypes { get; set; }
        public List<NameValueDto<int>> UserTypes { get; set; }
    }
}
