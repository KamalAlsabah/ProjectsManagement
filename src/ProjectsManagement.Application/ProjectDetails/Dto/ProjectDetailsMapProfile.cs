using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.ProjectDetails.Dto
{
    public class ProjectDetailsMapProfile : Profile
    {
        public ProjectDetailsMapProfile()
        {
            CreateMap<CreateProjectDetailsDto, ProjectDatabase.ProjectDetails.ProjectDetails>().ReverseMap();
            CreateMap<ProjectDetailsDto, ProjectDatabase.ProjectDetails.ProjectDetails>().ReverseMap();
            CreateMap<EditProjectDetailsDto, ProjectDatabase.ProjectDetails.ProjectDetails>().ReverseMap();
            CreateMap<ListProjectDetailsDto, ProjectDatabase.ProjectDetails.ProjectDetails>().ReverseMap();
            CreateMap<UpdateInputDto, ProjectDatabase.ProjectDetails.ProjectDetails>().ReverseMap();
        }
    }
}
