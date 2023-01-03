using Abp.Application.Services.Dto;
using ProjectsManagement.ProjectDatabase.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectsManagement.Sprints.Dto
{
    public class EditSprintDto : IEntityDto<long>
    {
        public long Id { get; set; }
        public string NameL { get; set; }
        public string NameF { get; set; }
        public string Description { get; set; }
        public long ProjectId { get; set; }
        public SprintStatus Status { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime ExpectedEndDate { get; set; } = DateTime.Now;

    }
}
