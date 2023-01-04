using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace ProjectsManagement.Authorization
{
    public class ProjectsManagementAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

            //For Project 

            context.CreatePermission(PermissionNames.Pages_Projects, L("Projects"));
            context.CreatePermission(PermissionNames.Pages_Projects_CreateProject, L("CreateProject"));
            context.CreatePermission(PermissionNames.Pages_Projects_DeleteProject, L("DeleteProject"));
            context.CreatePermission(PermissionNames.Pages_Projects_EditProject, L("EditProject"));

            //For Project worker
            context.CreatePermission(PermissionNames.Pages_ProjectsWorkers, L("ProjectsWorkers"));

            //For Project supervisor
            context.CreatePermission(PermissionNames.Pages_ProjectsSupervisors, L("ProjectsSupervisors"));


        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, ProjectsManagementConsts.LocalizationSourceName);
        }
    }
}
