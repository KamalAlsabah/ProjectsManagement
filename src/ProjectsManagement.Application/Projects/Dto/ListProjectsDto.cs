using Abp.Application.Services.Dto;
using System;

namespace ProjectsManagement.Project.Dto
{
    public class ListProjectsDto : EntityDto<long>
    {
        public string Name{ get; set; }
        public string NameL{ get; set; }
        public string NameF{ get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TestUrl { get; set; }
        public string Status { get; set; }
    }
}
