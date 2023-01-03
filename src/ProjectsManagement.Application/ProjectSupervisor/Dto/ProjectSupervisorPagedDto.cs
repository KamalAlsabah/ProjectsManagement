using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.ProjectSupervisor.Dto
{
    public class ProjectSupervisorPagedDto : PagedResultRequestDto
    {
        public string KeyWord { get; set; }
        public long ProjectId { get; set; }
    }
}
