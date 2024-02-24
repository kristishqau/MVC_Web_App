using BlogWebApp.Data;
using BlogWebApp.Interfaces;
using BlogWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebApp.Controllers
{
    [Authorize(Roles = UserRoles.Editor)]
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRespository;

        public DashboardController(IDashboardRepository dashboardRespository)
        {
            _dashboardRespository = dashboardRespository;
        }

        public async Task<IActionResult> Index()
        {
            var userPosts = await _dashboardRespository.GetAllUserPosts();
            var dashboardViewModel = new DashboardViewModel()
            {
                Posts = userPosts
            };
            return View(dashboardViewModel);
        }
    }
}
