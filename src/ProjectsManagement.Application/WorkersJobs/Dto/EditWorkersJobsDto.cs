using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.WorkersJobs.Dto
{
    public class EditWorkersJobsDto : EntityDto<long>
    {
        public long JobId { get; set; }
        public long WorkerId { get; set; }
    }
}
