using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.WorkersHistory.Dto
{
    public class WorkersHistoryListDto
    {
        public DateTime LogInTime { get; set; }
        public DateTime LogOutTime { get; set; }
        public string WorkerName { get; set; }
    }
}
