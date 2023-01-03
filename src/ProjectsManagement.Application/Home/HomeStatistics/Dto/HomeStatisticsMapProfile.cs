using AutoMapper;
using ProjectsManagement.Project.Dto;

namespace ProjectsManagement.Home.HomeStatistics.Dto
{
    public class HomeStatisticsMapProfile : Profile
    {
        public HomeStatisticsMapProfile()
        {


            CreateMap<CreateHomeStatisticsDto, ProjectDatabase.Home.HomeStatistics>().ReverseMap();

            CreateMap<HomeStatisticsDto, ProjectDatabase.Home.HomeStatistics>().ReverseMap();


            CreateMap<EditHomeStatisticsDto, ProjectDatabase.Home.HomeStatistics>().ReverseMap();

            CreateMap<ListHomeStatisticsDto, ProjectDatabase.Home.HomeStatistics>().ReverseMap();
            CreateMap<UpdateInputDto, ProjectDatabase.Home.HomeStatistics>().ReverseMap();
            



        }
    }
}
