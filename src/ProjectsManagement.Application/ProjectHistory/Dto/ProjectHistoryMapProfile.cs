using AutoMapper;
using ProjectsManagement.ProjectDatabase.ProjectHistory;

namespace ProjectsManagement.ProjectHistory.Dto
{
    public class ProjectHistoryMapProfile : Profile
    {
        public ProjectHistoryMapProfile()
        {


            CreateMap<ProjectsManagement.ProjectHistory.Dto.CreateProjectHistoryDto, ProjectsManagement.ProjectDatabase.ProjectHistory.ProjectHistory>().ReverseMap();

            CreateMap<ProjectsManagement.ProjectHistory.Dto.ProjectHistoryDto, ProjectsManagement.ProjectDatabase.ProjectHistory.ProjectHistory>().ReverseMap();


            CreateMap<ProjectsManagement.ProjectHistory.Dto.EditProjectHistoryDto, ProjectsManagement.ProjectDatabase.ProjectHistory.ProjectHistory>().ReverseMap();

            CreateMap<ProjectsManagement.ProjectHistory.Dto.ListProjectHistoryDto, ProjectsManagement.ProjectDatabase.ProjectHistory.ProjectHistory>().ReverseMap();




        }
    }
}
