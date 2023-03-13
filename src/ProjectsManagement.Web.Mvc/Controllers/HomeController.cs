using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using ProjectsManagement.Controllers;
using ProjectsManagement.Web.Models.Home;
using System.Threading.Tasks;
using ProjectsManagement.Home.HomeStatistics;
using Microsoft.AspNetCore.Identity;
using ProjectsManagement.Authorization.Users;
using ProjectsManagement.WorkersHistory;
using System;
using ProjectsManagement.Home;

namespace ProjectsManagement.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : ProjectsManagementControllerBase
    {
        private readonly UserManager _userManager;
        private readonly IHomeStatisticsAppService _HomeStatisticsAppService;
        private readonly IWorkersHistoryAppService _WorkersHistoryAppService;
        public HomeController(IHomeStatisticsAppService HomeStatisticsAppService,
            UserManager userManager,
            IWorkersHistoryAppService WorkersHistoryAppService
            )
        {
            _HomeStatisticsAppService = HomeStatisticsAppService;
            _userManager= userManager;
            _WorkersHistoryAppService= WorkersHistoryAppService;
        }
        public async Task<ActionResult> Index()
        {
            IndexHomeModalViewModel model = new IndexHomeModalViewModel() { };
            model.HomeDto =await _HomeStatisticsAppService.GetDetatilsForHome();
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
