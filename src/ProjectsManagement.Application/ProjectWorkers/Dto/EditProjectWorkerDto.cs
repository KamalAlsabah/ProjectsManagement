using Abp.Application.Services.Dto;
using ProjectsManagement.ProjectDatabase.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectsManagement.ProjectWorkers.Dto
{
    public class EditProjectWorkerDto : IEntityDto<long>
    {
        public long WorkerId { get; set; }
        public long ProjectId { get; set; }
        public long Id { get ; set; }
    }
}
