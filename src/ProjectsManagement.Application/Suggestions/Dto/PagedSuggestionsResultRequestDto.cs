using Abp.Application.Services.Dto;

namespace ProjectsManagement.Suggestions.Dto
{
    public class PagedSuggestionsResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public long ProjectId { get; set; }

    }
}
