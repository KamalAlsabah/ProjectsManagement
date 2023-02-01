using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using ProjectsManagement.Controllers;
using ProjectsManagement.Web.Models.Home;
using System.Threading.Tasks;
using ProjectsManagement.Home.HomeStatistics;

namespace ProjectsManagement.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : ProjectsManagementControllerBase
    {
        private readonly IHomeStatisticsAppService _HomeStatisticsAppService;
        public HomeController(IHomeStatisticsAppService HomeStatisticsAppService)
        {
            _HomeStatisticsAppService = HomeStatisticsAppService;
        }
        public ActionResult Index()
        {
            IndexHomeStaticModalViewModel model = new IndexHomeStaticModalViewModel() { };
            return View(model);
        }
        public async Task<ActionResult> CreateModal()
        {
            var model = new CreateHomeStaticModalViewModel();

            model.createHomeStatisticsDto = new() { };
            model.userType = await _HomeStatisticsAppService.GetUserTypes();
            return PartialView("_CreateModal", model);
        }


    }
}
