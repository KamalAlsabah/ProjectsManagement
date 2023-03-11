using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.WorkersJobs.Dto
{
    public class WorkersJobsDto : EntityDto<long>
    {

        public string JobName { get; set; }
        public string WorkerName { get; set; }
    }
}
