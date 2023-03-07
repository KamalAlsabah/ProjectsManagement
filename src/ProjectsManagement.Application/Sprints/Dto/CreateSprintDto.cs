using ProjectsManagement.ProjectDatabase.Enums;
using System;

namespace ProjectsManagement.Sprints.Dto
{
    public class CreateSprintDto
    {
        public string NameL { get; set; }
        public string NameF { get; set; }
        public string Description { get; set; }
        public long ProjectId { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime ExpectedEndDate { get; set; } = DateTime.Now;
        public long WieghtOfHours { get; set; }

    }
}
