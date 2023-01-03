using Abp;
using ProjectsManagement.Suggestions.Dto;
using System.Collections.Generic;

namespace ProjectsManagement.Web.Models.Suggestions
{
    public class CreateSuggestionsModalViewModel
    {
        public CreateSuggestionsDto CreateSuggestionsDto { get; set; }
        public List<NameValue<long>> Projects { get; set; }
    }
}
