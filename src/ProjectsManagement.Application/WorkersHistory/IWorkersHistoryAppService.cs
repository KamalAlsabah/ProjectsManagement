
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ProjectsManagement.WorkersHistory.Dto;
using System.Threading.Tasks;

namespace ProjectsManagement.WorkersHistory
{
    public interface IWorkersHistoryAppService : IAsyncCrudAppService<WorkersHistoryDto, long, WorkersHistoryPagedDto, WorkersHistoryCreateDto, UpdateInputDto>
    {
        Task<WorkersHistoryEditDto> GetWorkersHistoryForEdit(EntityDto input);
        Task<WorkersHistoryDto> GetHistoryByUserId(long UserId);
        Task<double> GetTodayTotalHours(long userId);
        Task CreateHistory(bool input);
        Task<bool> IsUserOnline();
    }
}
