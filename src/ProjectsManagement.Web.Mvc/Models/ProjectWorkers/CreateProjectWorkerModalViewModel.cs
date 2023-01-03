using Abp;
using ProjectsManagement.Authorization.Users;
using ProjectsManagement.ProjectWorkers.Dto;
using System.Collections.Generic;

namespace ProjectsManagement.Web.Models.ProjectWorkers
{
    public class CreateProjectWorkerModalViewModel
    {
        public CreateProjectWorkerDto CreateProjectWorkerDto { get; set; }
        public List<NameValue<long>> Users { get; set; }
    }
}
