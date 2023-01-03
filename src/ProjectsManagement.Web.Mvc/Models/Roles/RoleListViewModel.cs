using System.Collections.Generic;
using ProjectsManagement.Roles.Dto;

namespace ProjectsManagement.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}
