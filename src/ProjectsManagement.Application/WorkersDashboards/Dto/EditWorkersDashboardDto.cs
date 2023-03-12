using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.WorkersDashboards.Dto
{
    public class EditWorkersDashboardDto : EntityDto<long>
    {
        public long ProjectId { get; set; }
        public long WorkerId { get; set; }
        public double WorkerJobsCount { get; set; }
    }
}
