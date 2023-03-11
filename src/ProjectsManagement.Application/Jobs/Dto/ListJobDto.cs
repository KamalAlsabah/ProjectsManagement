using Abp.Application.Services.Dto;
using System;

namespace ProjectsManagement.Jobs.Dto
{
    public class ListJobDto : EntityDto<long>
    {
        public string Name { get; set; }
        //public string WorkerFullName { get; set; }
        public string Description { get; set; }
        public string SprintName { get; set; }
        public int ExpectedNoOfHours { get; set; }
        public int WieghtOfHours { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public int ActualNumberOfHours { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }

    }
}
