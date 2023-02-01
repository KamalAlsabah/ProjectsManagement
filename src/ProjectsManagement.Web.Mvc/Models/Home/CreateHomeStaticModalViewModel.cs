using Abp.Application.Services.Dto;
using ProjectsManagement.Home.HomeStatistics.Dto;
using System.Collections.Generic;

namespace ProjectsManagement.Web.Models.Home
{
    public class CreateHomeStaticModalViewModel
    {
        public CreateHomeStatisticsDto createHomeStatisticsDto { get; set; }
        public List<NameValueDto<int>> userType{get;set;}
    }
}
