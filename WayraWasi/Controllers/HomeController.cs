using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WayraWasi.Data;
using WayraWasi.Models;

namespace WayraWasi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger; // Este es el login que viene por defecto en el programa
        private readonly IHomeRepository _homeRepository;

        public HomeController(ILogger<HomeController> logger, IHomeRepository homeRepository)
        {
            _logger = logger; // Para que siempre este logeado con el usuario (En este caso de administrador)
            _homeRepository = homeRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Disponibilidad()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Disponibilidad(DateTime fechaInicio, DateTime fechaFin)
        {
            var disponibilidad = await _homeRepository.ListarDisponibilidadPorFecha(fechaInicio, fechaFin);
            return View(disponibilidad);
        }

       /*public ActionResult Reportes()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Reportes(DateTime fechaInicio, DateTime fechaFin)
        {
            var disponibilidad = await _homeRepository.GenerarReservaPorFecha(fechaInicio, fechaFin);
            return View(disponibilidad);
        }*/

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
