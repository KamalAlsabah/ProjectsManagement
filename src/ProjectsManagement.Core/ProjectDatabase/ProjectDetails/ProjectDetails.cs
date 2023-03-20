using Abp.Domain.Entities.Auditing;
using ProjectsManagement.ProjectDatabase.Project;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.ProjectDatabase.ProjectDetails
{
    public class ProjectDetails : FullAuditedEntity<long>
    {
        public long ProjectId { get; set; }
        public Projects Project { get; set; }
        [UIHint("Summernote")]
        public string Details { get; set; }
    }
}
