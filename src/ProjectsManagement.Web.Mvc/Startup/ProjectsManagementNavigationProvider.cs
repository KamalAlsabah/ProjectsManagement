using Abp.Application.Navigation;
using Abp.Authorization;
using Abp.Localization;
using ProjectsManagement.Authorization;

namespace ProjectsManagement.Web.Startup
{
    /// <summary>
    /// This class defines menus for the application.
    /// </summary>
    public class ProjectsManagementNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                 .AddItem(
                    new MenuItemDefinition(
                        PageNames.Home,
                        L("HomePage"),
                        url: "",
                        icon: "fas fa-home",
                        requiresAuthentication: true
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.About,
                        L("About"),
                        url: "About",
                        icon: "fas fa-info-circle"
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Projects,
                        L("Projects"),
                        url: "Projects",
                        icon: "fas fa-home",
                        requiresAuthentication: true
                    )
                )
           .AddItem(
                    new MenuItemDefinition(
                        PageNames.Users,
                        L("Users"),
                        url: "Users",
                        icon: "fas fa-users",
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Users)
                    )

                )
               //.AddItem(
               //     new MenuItemDefinition(
               //         PageNames.Tenants,
               //         L("Tenants"),
               //         url: "Tenants",
               //         icon: "fas fa-building",
               //         permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Tenants)
               //     )
               // )
                //.AddItem(
                //    new MenuItemDefinition(
                //        PageNames.StatisticsHome,
                //        L("StatisticsHome"),
                //        url: "StatisticsHome",
                //        icon: "fas fa-theater-masks"
                //     ))

                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Roles,
                        L("Roles"),
                        url: "Roles",
                        icon: "fas fa-theater-masks",
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Roles)
                    )
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, ProjectsManagementConsts.LocalizationSourceName);
        }
    }
}