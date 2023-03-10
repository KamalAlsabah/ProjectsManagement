using ProjectsManagement.ProjectDatabase.Enums;
using System;

namespace ProjectsManagement.Jobs.Dto
{
    public class CreateJobDto
    {
        public string NameL { get; set; }
        public string NameF { get; set; }
        public string Description { get; set; }
        public long ProjectId { get; set; }
        public int WieghtOfHours { get; set; }

        public long? SprintId { get; set; }
        public long? WorkerId { get; set; }
        public int ExpectedNoOfHours { get; set; }

    }
}
