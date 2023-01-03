using Abp;
using ProjectsManagement.Authorization.Users;
using ProjectsManagement.Sprints.Dto;
using System.Collections.Generic;

namespace ProjectsManagement.Web.Models.Sprints
{
    public class CreateSprintModalViewModel
    {
        public CreateSprintDto CreateSprintDto { get; set; }
    }
}
