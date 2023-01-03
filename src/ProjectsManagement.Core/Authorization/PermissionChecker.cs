using Abp.Authorization;
using ProjectsManagement.Authorization.Roles;
using ProjectsManagement.Authorization.Users;

namespace ProjectsManagement.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
