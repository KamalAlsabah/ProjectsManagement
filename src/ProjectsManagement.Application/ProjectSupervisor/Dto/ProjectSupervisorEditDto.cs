using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.ProjectSupervisor.Dto
{
    public class ProjectSupervisorEditDto : IEntityDto<long>
    {
        public long SupervisorId { get; set; }
        public long ProjectId { get; set; }
        public long Id { get ; set ; }
    }
}
