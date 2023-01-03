using Abp;
using ProjectsManagement.Suggestions.Dto;
using System.Collections.Generic;

namespace ProjectsManagement.Web.Models.Suggestions
{
    public class EditSuggestionsModalViewModel
    {
        public EditSuggestionsDto EditSuggestionsDto { get; set; }

        public List<NameValue<long>> Projects { get; set; }

    }
}
