using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.ProjectDetails.Dto
{
    public class ProjectDetailsDto : EntityDto<long>
    {
         public string ProjectName { get; set; }
        [UIHint("Summernote")]
        public string Details { get; set; }
    }
}
