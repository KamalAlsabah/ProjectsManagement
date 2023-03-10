using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.WorkersDashboards.Dto
{
    public class PagedWorkersDashboardResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public long ProjectId { get; set; }
    }
}
