using Abp.Application.Services.Dto;

namespace ProjectsManagement.Suggestions.Dto
{
    public class SuggestionsDto : EntityDto<long>
    {


        public long ProjectId { get; set; }

        public long SupervisorId { get; set; }
        public string SupervisorName { get; set; }

        public string Description { get; set; }
        public string ProjectName { get; set; }

        public string Status { get; set; }

    }
}
