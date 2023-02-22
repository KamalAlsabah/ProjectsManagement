using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.WorkersHistory.Dto
{
    public class UpdateInputDto : EntityDto<long>
    {
        public DateTime LogInTime { get; set; }
        public DateTime LogOutTime { get; set; }
        public long WorkerId { get; set; }
        public long TotalHours { get; set; }
    }
}
