using Abp.Application.Services.Dto;
using ProjectsManagement.ProjectDatabase.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.ProjectWorkers.Dto
{
    public class UpdateInputDto:EntityDto<long>
    {
        public long WorkerId { get; set; }
        public long ProjectId { get; set; }
    }
}
