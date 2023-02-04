using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.ProjectDatabase.Enums
{
    public enum ProjectHistoryActions
    {
        [Display(Name = "Created")]
        Created,

        [Display(Name = "Updated")]
        Updated,

        [Display(Name = "Deleted")]
        Deleted
    }
}
