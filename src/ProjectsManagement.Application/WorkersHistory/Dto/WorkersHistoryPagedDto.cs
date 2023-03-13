using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.WorkersHistory.Dto
{
    public class WorkersHistoryPagedDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public long WorkerId  { get; set; }
    }
}
