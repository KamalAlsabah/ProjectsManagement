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
        [Display(Name = "Create")]
        Create,

        [Display(Name = "Update")]
        Update,

        [Display(Name = "Delete")]
        Delete
    }
}
