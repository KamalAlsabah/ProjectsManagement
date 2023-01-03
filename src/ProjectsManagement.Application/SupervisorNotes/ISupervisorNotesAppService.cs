using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ProjectsManagement.SupervisorNotes.Dto;
using System.Threading.Tasks;

namespace ProjectsManagement.SupervisorNotes
{
    public interface ISupervisorNotesAppService : IAsyncCrudAppService<SupervisorNotesDto, long, PagedSupervisorNotesResultRequestDto, CreateSupervisorNotesDto, UpDateInputSupervisorNotesDto>
    {
        Task<EditSupervisorNotesDto> GetSupervisorNotesForEdit(EntityDto<long> input);

    }
}
