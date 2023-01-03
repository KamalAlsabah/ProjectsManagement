using Abp;
using ProjectsManagement.ProjectWorkers.Dto;
using System.Collections.Generic;

namespace ProjectsManagement.Web.Models.ProjectWorkers
{
    public class EditProjectWorkerModalViewModel
    {
        public EditProjectWorkerDto EditProjectWorkerDto { get; set; }
        public List<NameValue<long>> Users { get; set; }
        public List<NameValue<long>> Projects { get; set; }
    }
}
