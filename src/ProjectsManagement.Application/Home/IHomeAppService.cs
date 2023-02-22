using ProjectsManagement.Home.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.Home
{
    public interface IHomeAppService
    {
        Task<HomeDto> GetDetatilsForHome();
    }
}
