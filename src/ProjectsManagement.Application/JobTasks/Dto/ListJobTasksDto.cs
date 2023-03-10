using Abp.Application.Services.Dto;
using System;

namespace ProjectsManagement.JobTasks.Dto
{
    public class ListJobTasksDto : EntityDto<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string JobTaskStatus { get; set; }

        public long? JobId { get; set; }
        public DateTime CreationTime { get; set; }

    }
}
