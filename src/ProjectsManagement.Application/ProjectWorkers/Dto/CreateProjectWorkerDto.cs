using ProjectsManagement.ProjectDatabase.Enums;
using System;

namespace ProjectsManagement.ProjectWorkers.Dto
{
    public class CreateProjectWorkerDto
    {
        public long WorkerId { get; set; }
        public long ProjectId { get; set; }


    }
}
