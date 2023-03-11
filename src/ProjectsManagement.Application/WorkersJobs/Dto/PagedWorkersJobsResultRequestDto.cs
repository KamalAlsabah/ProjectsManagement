using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.WorkersJobs.Dto
{
    public class PagedWorkersJobsResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
     }
}
