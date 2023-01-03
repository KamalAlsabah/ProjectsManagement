using Abp.Application.Services;
using ProjectsManagement.MultiTenancy.Dto;

namespace ProjectsManagement.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

