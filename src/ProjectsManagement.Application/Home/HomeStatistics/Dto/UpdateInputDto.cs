using Abp.Application.Services.Dto;
using ProjectsManagement.ProjectDatabase.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.Home.HomeStatistics.Dto
{
    public class UpdateInputDto:EntityDto<long>
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public EHomeStatistics Type { get; set; }
        public List<int> UserTypes { get; set; }
    }
}
