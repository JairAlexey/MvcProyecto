using AvanceDelProyecto.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using AvanceDelProyecto.Services;//Es necesario

namespace AvanceDelProyecto.Controllers
{
    public class HomeController : Controller
    {
        // Dependencia para comunicarse con la API.
        private readonly IApiService _apiService;

        public HomeController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}