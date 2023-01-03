using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjectsManagement.ProjectDatabase.Enums
{
    public enum JobStatus
    {
        [Display(Name = "ToDo")]

        ToDo,
        [Display(Name = "InProgress")]

        InProgress,
        [Display(Name = "Done")]

        Done
    }
}
