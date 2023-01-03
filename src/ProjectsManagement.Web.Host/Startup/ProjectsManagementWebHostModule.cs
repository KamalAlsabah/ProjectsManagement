using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ProjectsManagement.Configuration;

namespace ProjectsManagement.Web.Host.Startup
{
    [DependsOn(
       typeof(ProjectsManagementWebCoreModule))]
    public class ProjectsManagementWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public ProjectsManagementWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ProjectsManagementWebHostModule).GetAssembly());
        }
    }
}
