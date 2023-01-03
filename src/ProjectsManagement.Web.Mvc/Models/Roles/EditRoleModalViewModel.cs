using Abp.AutoMapper;
using ProjectsManagement.Roles.Dto;
using ProjectsManagement.Web.Models.Common;

namespace ProjectsManagement.Web.Models.Roles
{
    [AutoMapFrom(typeof(GetRoleForEditOutput))]
    public class EditRoleModalViewModel : GetRoleForEditOutput, IPermissionsEditViewModel
    {
        public bool HasPermission(FlatPermissionDto permission)
        {
            return GrantedPermissionNames.Contains(permission.Name);
        }
    }
}
