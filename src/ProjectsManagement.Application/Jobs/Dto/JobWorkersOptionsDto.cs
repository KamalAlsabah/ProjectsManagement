using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.Jobs.Dto
{
    public class JobWorkersOptionsDto : EntityDto<long>
    {
        public string Name { get; set; }
        public bool IsSelected { get; set; }
        //public long? WorkerId { get; set; }

    }
}
