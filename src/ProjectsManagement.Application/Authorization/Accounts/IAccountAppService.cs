using System.Threading.Tasks;
using Abp.Application.Services;
using ProjectsManagement.Authorization.Accounts.Dto;

namespace ProjectsManagement.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
