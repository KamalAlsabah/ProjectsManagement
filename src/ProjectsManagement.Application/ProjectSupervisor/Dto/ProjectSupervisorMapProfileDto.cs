using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectsManagement.ProjectSupervisor;
using ProjectsManagement.ProjectDatabase.ProjectSupervisor;

namespace ProjectsManagement.ProjectSupervisor.Dto
{
    public class ProjectSupervisorMapProfileDto :Profile
    {
        public ProjectSupervisorMapProfileDto()
        {

            CreateMap<ProjectSupervisors, ProjectSupervisorListDto>().ReverseMap();
            CreateMap<ProjectSupervisors, ProjectSupervisorCreateDto>().ReverseMap();
            CreateMap<ProjectSupervisors, ProjectSupervisorEditDto>().ReverseMap();
            CreateMap<ProjectSupervisors, UpDateInputProjectSupervisorDto>().ReverseMap();


        }
    }
}
