using Abp;
using ProjectsManagement.Authorization.Users;
using ProjectsManagement.Jobs.Dto;
using System.Collections.Generic;

namespace ProjectsManagement.Web.Models.Jobs
{
    public class CreateJobModalViewModel
    {
        public CreateJobDto CreateJobDto { get; set; }
        public List<NameValue<long>> Users { get; set; }
        public List<NameValue<long>> Sprints { get; set; }
    }
}
