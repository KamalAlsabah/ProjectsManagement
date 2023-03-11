using Abp;
using ProjectsManagement.Jobs.Dto;
using System.Collections.Generic;

namespace ProjectsManagement.Web.Models.Jobs
{
    public class EditJobModalViewModel
    {
        public EditJobDto EditJobDto { get; set; }
        public List<NameValue<long>> Users { get; set; }
        public List<NameValue<long>> Sprints { get; set; }
        public List<JobWorkersOptionsDto> Workers { get; set; }


    }
}
