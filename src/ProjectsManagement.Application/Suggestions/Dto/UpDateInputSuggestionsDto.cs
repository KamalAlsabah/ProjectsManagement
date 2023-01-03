using Abp.Application.Services.Dto;
using ProjectsManagement.ProjectDatabase.Enums;

namespace ProjectsManagement.Suggestions.Dto
{
    public class UpDateInputSuggestionsDto : IEntityDto<long>
    {

        public long Id { get; set; }

        public string Description { get; set; }

        public SuggestionStatus Status { get; set; }
    }
}
