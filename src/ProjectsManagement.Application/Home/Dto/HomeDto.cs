using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.Home.Dto
{
    public class HomeDto
    {
        public long? Projects { get; set; }
        public long? ProjectsWorkers { get; set; }
        public long? ProjectsSupervisors { get; set; }
        public long? Sprints { get; set; }
        public long? Jobs { get; set; }
        public long? JobTasks { get; set; }
        public List<ProjectDatabase.WorkersHistory.WorkersHistory> WorkersHistory { get; set; } = null;
        public List<ProjectSprintDto> ProjectSprintDto { get; set; }
        public List<ProjectWorkersHoursWieghtDto> ProjectWorkersHoursWieghtDto { get; set; }



    }
}
           