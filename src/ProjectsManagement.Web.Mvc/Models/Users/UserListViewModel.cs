using System.Collections.Generic;
using ProjectsManagement.Roles.Dto;

namespace ProjectsManagement.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
