using Abp.Application.Services.Dto;
using ProjectsManagement.ProjectDatabase.Enums;
using ProjectsManagement.ProjectDatabase.Sprint;
using System;

namespace ProjectsManagement.Sprints.Dto
{
    public class SprintsDto : EntityDto<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProjectName { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
