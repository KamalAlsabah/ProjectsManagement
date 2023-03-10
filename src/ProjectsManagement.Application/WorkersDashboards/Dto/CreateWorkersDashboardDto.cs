using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.WorkersDashboards.Dto
{
    public class CreateWorkersDashboardDto
    {
        public long ProjectId { get; set; }
        public long WorkerId { get; set; }
        public int WorkerJobsCount { get; set; }
    }
}
