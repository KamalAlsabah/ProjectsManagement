using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.WorkersDashboards.Dto
{
    public class ListWorkersDashboardDto : EntityDto<long>
    {
        public string ProjectName { get; set; }
        public string WorkerName { get; set; }
        public int WorkerJobsCount { get; set; }
    }
}
