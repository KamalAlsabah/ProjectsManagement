using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ProjectsManagement.Authorization;

namespace ProjectsManagement
{
    [DependsOn(
        typeof(ProjectsManagementCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class ProjectsManagementApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<ProjectsManagementAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(ProjectsManagementApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
