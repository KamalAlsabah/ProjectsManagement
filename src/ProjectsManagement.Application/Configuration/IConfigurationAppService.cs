using System.Threading.Tasks;
using ProjectsManagement.Configuration.Dto;

namespace ProjectsManagement.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
