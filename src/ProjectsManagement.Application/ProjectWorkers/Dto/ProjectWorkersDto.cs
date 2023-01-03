using Abp.Application.Services.Dto;
using ProjectsManagement.ProjectDatabase.Enums;
using ProjectsManagement.ProjectDatabase.Sprint;
using System;

namespace ProjectsManagement.ProjectWorkers.Dto
{
    public class ProjectWorkersDto : EntityDto<long>
    {
        public string WorkerFullName { get; set; }
        public string ProjectName { get; set; }

    }
}
