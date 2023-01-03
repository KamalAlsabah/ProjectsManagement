using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ProjectsManagement.EntityFrameworkCore;
using ProjectsManagement.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace ProjectsManagement.Web.Tests
{
    [DependsOn(
        typeof(ProjectsManagementWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class ProjectsManagementWebTestModule : AbpModule
    {
        public ProjectsManagementWebTestModule(ProjectsManagementEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ProjectsManagementWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(ProjectsManagementWebMvcModule).Assembly);
        }
    }
}