using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectsManagement.ProjectSupervisor.Dto;
using System.Collections.Generic;

namespace ProjectsManagement.Web.Models.ProjectSuprevisor
{
    public class EditProjectSupervisorModalViewModel
    {
        public ProjectSupervisorEditDto projectSupervisorEditDto { get; set; }
        public List<SelectListItem> Projects { get; set; }
        public List<SelectListItem> Supervisors { get; set; }
    }
}
