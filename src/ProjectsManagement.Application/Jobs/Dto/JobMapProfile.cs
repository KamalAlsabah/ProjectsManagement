using AutoMapper;
using ProjectsManagement.Project.Dto;

namespace ProjectsManagement.Jobs.Dto
{
    public class JobMapProfile : Profile
    {
        public JobMapProfile()
        {


            CreateMap<CreateJobDto, ProjectDatabase.Job.Jobs>().ReverseMap();

            CreateMap<JobsDto, ProjectDatabase.Job.Jobs>().ReverseMap();


            CreateMap<EditJobDto, ProjectDatabase.Job.Jobs>().ReverseMap();

            CreateMap<ListJobDto, ProjectDatabase.Job.Jobs>().ReverseMap();
            CreateMap<UpdateInputDto, ProjectDatabase.Job.Jobs>().ReverseMap();
            CreateMap<JobWorkersOptionsDto,Authorization.Users.User>().ReverseMap();
            



        }
    }
}
