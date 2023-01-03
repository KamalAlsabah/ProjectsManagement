using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.ProjectSupervisor.Dto
{
    public class ProjectSupervisorListDto :EntityDto<long>
    {
        public long SupervisorId { get; set; }
        public long ProjectId { get; set; }
        public string SupervisorUserName { get; set; }
        public string ProjectName { get; set; }

    }
}
