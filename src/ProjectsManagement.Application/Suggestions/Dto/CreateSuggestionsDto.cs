using ProjectsManagement.ProjectDatabase.Enums;

namespace ProjectsManagement.Suggestions.Dto
{
    public class CreateSuggestionsDto
    {

        public long ProjectId { get; set; }

        public long SupervisorId { get; set; }

        public string Description { get; set; }

        public SuggestionStatus Status { get; set; }
    }
}
