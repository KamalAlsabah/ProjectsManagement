using Abp.Application.Services.Dto;
using System;

namespace ProjectsManagement.JobTasks.Dto
{
    public class JobTasksDto: EntityDto<long>
    {
        public string Name { get; set; }

        public string NameF { get; set; }
        public string NameL { get; set; }
        public string JobTaskStatus { get; set; }

        public string Description { get; set; }
        public long? JobId { get; set; }
        public string JobName { get; set; }
        public DateTime CreationTime { get; set; }



    }
}
