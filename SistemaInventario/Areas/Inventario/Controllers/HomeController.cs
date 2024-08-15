using Microsoft.AspNetCore.Mvc;
using SistemaInventario.Modelos.ViewModels;
using System.Diagnostics;

namespace SistemaInventario.Areas.Inventario.Controllers
{
    [Area("Inventario")] //para cada controlador colocarle aque area pertenece AREA para este para un controlador especifico
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
