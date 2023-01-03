using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ProjectsManagement.Suggestions.Dto;
using System.Threading.Tasks;

namespace ProjectsManagement.Suggestions
{
    public interface ISuggestionsAppService : IAsyncCrudAppService<SuggestionsDto, long, PagedSuggestionsResultRequestDto, CreateSuggestionsDto, UpDateInputSuggestionsDto>
    {
        Task<EditSuggestionsDto> GetSuggestionsForEdit(EntityDto<long> input);

    }
}
