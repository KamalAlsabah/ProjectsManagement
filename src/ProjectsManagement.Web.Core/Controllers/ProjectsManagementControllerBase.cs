using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace ProjectsManagement.Controllers
{
    public abstract class ProjectsManagementControllerBase: AbpController
    {
        protected ProjectsManagementControllerBase()
        {
            LocalizationSourceName = ProjectsManagementConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
