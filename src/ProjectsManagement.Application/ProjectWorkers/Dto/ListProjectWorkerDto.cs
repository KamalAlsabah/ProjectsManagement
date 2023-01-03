using Abp.Application.Services.Dto;
using System;

namespace ProjectsManagement.ProjectWorkers.Dto
{
    public class ListProjectWorkerDto : EntityDto<long>
    {
        public string WorkerFullName { get; set; }
        public string ProjectName { get; set; }

    }
}
