using Abp;
using ProjectsManagement.JobTasks.Dto;
using System.Collections.Generic;

namespace ProjectsManagement.Web.Models.JobTasks
{
    public class CreateJobTasksModalViewModel
    {
        public CreateJobTasksDto CreateJobTasksDto { get; set; }
        public List<NameValue<long>> Jobs { get; set; }
    }
}
