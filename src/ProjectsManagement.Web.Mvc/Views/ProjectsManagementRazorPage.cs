using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace ProjectsManagement.Web.Views
{
    public abstract class ProjectsManagementRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected ProjectsManagementRazorPage()
        {
            LocalizationSourceName = ProjectsManagementConsts.LocalizationSourceName;
        }
    }
}
