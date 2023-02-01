using Abp.Application.Services.Dto;
using ProjectsManagement.ProjectDatabase.Enums;
using System;

using System.ComponentModel.DataAnnotations;


namespace ProjectsManagement.ProjectHistory.Dto
{
    public class UpdateInputDto:EntityDto<long>
    {
        public long UserId { get; set; }
        public long ProjectId { get; set; }
        public long? SprintId { get; set; }
        public long? JobId { get; set; }
        public long? JobTasksId { get; set; }
        public long? ProjectWorkersId { get; set; }
        public long? ProjectSupervisorsId { get; set; }
        public long? SupervisorNotesId { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public ProjectHistoryActions ProjectHistoryActions { get; set; }
        public ProjectHistoryColumns ProjectHistoryColumns { get; set; }
        public string Description { get; set; }
    }
}
