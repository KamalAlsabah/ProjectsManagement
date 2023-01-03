using Abp.AutoMapper;
using ProjectsManagement.Project.Dto;

namespace ProjectsManagement.Web.Models.Projects
{

    [AutoMapFrom(typeof(EditProjectsDto))]

    public class EditProjectsModalViewModel : EditProjectsDto
    {



    }
}
