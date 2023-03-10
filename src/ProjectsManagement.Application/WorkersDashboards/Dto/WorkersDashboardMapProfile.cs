using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.WorkersDashboards.Dto
{
    public class WorkersDashboardMapProfile : Profile
    {
        public WorkersDashboardMapProfile()
        {
            CreateMap<CreateWorkersDashboardDto, ProjectDatabase.WorkersDashboard.WorkersDashboard>().ReverseMap();
            CreateMap<WorkersDashboardDto, ProjectDatabase.WorkersDashboard.WorkersDashboard>().ReverseMap();
            CreateMap<EditWorkersDashboardDto, ProjectDatabase.WorkersDashboard.WorkersDashboard>().ReverseMap();
            CreateMap<ListWorkersDashboardDto, ProjectDatabase.WorkersDashboard.WorkersDashboard>().ReverseMap();
            CreateMap<UpdateInputDto, ProjectDatabase.WorkersDashboard.WorkersDashboard>().ReverseMap();
        }
    }
}
