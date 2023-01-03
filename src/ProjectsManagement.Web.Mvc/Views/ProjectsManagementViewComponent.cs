using Abp.AspNetCore.Mvc.ViewComponents;

namespace ProjectsManagement.Web.Views
{
    public abstract class ProjectsManagementViewComponent : AbpViewComponent
    {
        protected ProjectsManagementViewComponent()
        {
            LocalizationSourceName = ProjectsManagementConsts.LocalizationSourceName;
        }
    }
}
