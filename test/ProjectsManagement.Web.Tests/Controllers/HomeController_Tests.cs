using System.Threading.Tasks;
using ProjectsManagement.Models.TokenAuth;
using ProjectsManagement.Web.Controllers;
using Shouldly;
using Xunit;

namespace ProjectsManagement.Web.Tests.Controllers
{
    public class HomeController_Tests: ProjectsManagementWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}