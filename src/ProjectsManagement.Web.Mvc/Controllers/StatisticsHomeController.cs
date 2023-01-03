using Microsoft.AspNetCore.Mvc;
using ProjectsManagement.Controllers;
using ProjectsManagement.Home.HomeStatistics;

namespace ProjectsManagement.Web.Controllers
{
    public class StatisticsHomeController : ProjectsManagementControllerBase
    {
        private readonly IHomeStatisticsAppService _IHomeStatisticsAppService;
        public StatisticsHomeController(IHomeStatisticsAppService IHomeStatisticsAppService)
        {
            _IHomeStatisticsAppService= IHomeStatisticsAppService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
