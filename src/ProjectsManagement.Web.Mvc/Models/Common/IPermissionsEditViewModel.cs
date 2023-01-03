using System.Collections.Generic;
using ProjectsManagement.Roles.Dto;

namespace ProjectsManagement.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}