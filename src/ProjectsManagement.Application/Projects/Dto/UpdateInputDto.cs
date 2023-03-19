using Abp.Application.Services.Dto;
using ProjectsManagement.ProjectDatabase.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.Projects.Dto
{
    public class UpdateInputDto : IEntityDto<long>
    {
        public long Id { get; set; }

        public string NameL { get; set; }
        public string NameF { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TestUrl { get; set; }
        public ProjectStatus Status { get; set; }
    }
}
