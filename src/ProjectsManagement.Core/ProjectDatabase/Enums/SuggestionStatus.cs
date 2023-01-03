using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ProjectsManagement.ProjectDatabase.Enums
{
    public enum SuggestionStatus
    {
        [Display(Name = "Approved")]

        Approved,
        [Display(Name = "Rejected")]

        Rejected,
        [Display(Name = "UnderRevision")]

        UnderRevision
    }
}
