using Abp;
using ProjectsManagement.JobTasks.Dto;
using System.Collections.Generic;

namespace ProjectsManagement.Web.Models.JobTasks
{
    public class EditJobTasksModalViewModel
    {
        public EditJobTasksDto EditJobTasksDto { get; set; }

        public List<NameValue<long>> Jobs { get; set; }

    }
}
