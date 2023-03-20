using Abp.Application.Services.Dto;
using ProjectsManagement.ProjectDatabase.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectsManagement.Jobs.Dto
{
    public class EditJobDto : IEntityDto<long>
    {
        public long Id { get; set; }
        public string NameL { get; set; }
        public string NameF { get; set; }
        public string Description { get; set; }
        public long? SprintId { get; set; }
        public int WieghtOfHours { get; set; }
        public DateTime StartDate { get; set; }
        public long ProjectId { get; set; }
        [Range(0,500)]
        public int ExpectedNoOfHours { get; set; }
        public JobStatus Status { get; set; }

    }
}
