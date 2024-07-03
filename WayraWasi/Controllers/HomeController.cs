using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WayraWasi.Data;
using WayraWasi.Models;

namespace WayraWasi.Controllers
{
    [Authorize(Roles = "Administrador")] // Se restringe el acceso solo al administrador
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

        public IActionResult Disponibilidad()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Disponibilidad(DateTime fechaInicio, DateTime fechaFin)
        {
            var disponibilidad = await _homeRepository.ListarDisponibilidadPorFecha(fechaInicio, fechaFin);
            ViewBag.FechaInicio = fechaInicio;
            ViewBag.FechaFin = fechaFin;
            return View(disponibilidad);
        }

        [AllowAnonymous] // Permite acceder a cualquier tipo de usuario solo a esta funcion
        public IActionResult Privacy()
        {
            return View();
        }


        [Route("/Home/Error/{code:int}")]
        public IActionResult Error(int codigo) {

            return View(new ErrorViewModel { RequestId = "404", ErrorMessage = $"Ocurrio un error, Codigo de Error {codigo}"});
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
