using System.Threading.Tasks;
using Abp.Application.Services;
using ProjectsManagement.Sessions.Dto;

namespace ProjectsManagement.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
