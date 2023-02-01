using Abp.Application.Services.Dto;
using ProjectsManagement.ProjectDatabase.Enums;
using System;

namespace ProjectsManagement.ProjectHistory.Dto
{
    public class ProjectHistoryDto : EntityDto<long>
    {

        public string ProjectName { get; set; }
        public string UserName { get; set; }
        public string SprintName { get; set; }
        public string JobName { get; set; }
        public string JobTasksName { get; set; }
        public string ProjectWorkersName { get; set; }
        public string ProjectSupervisorsName { get; set; }
        public string SupervisorNotesName { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public ProjectHistoryActions ProjectHistoryActions { get; set; }
        public ProjectHistoryColumns ProjectHistoryColumns { get; set; }
        public string Description { get; set; }



    }
}
