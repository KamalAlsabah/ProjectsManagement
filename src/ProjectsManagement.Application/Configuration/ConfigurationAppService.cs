using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using ProjectsManagement.Configuration.Dto;

namespace ProjectsManagement.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : ProjectsManagementAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
