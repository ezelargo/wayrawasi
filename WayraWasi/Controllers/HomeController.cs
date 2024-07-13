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
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IHomeRepository homeRepository,IConfiguration configuration)
        {
            _logger = logger; // Para que siempre este logeado con el usuario (En este caso de administrador)
            _homeRepository = homeRepository;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var dias = _configuration.GetValue<int>("ReservaIndex:DiasReservas");
            var proximasReservas = await _homeRepository.ProximasReservas(dias);
            _logger.LogInformation("Iniciando pagina principal"); //Se pueden registrar mensajes como este con los paquetes Serilog
            return View(proximasReservas);
        }

        public IActionResult Disponibilidad()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Disponibilidad(DateTime fechaInicio, DateTime fechaFin)
        {
            // Si la caba�a se encuentra en el horario entre la hora de checkIn y CheckOut de un dia a otro, se marcara como ocupado debido a que en ese tiempo no se puede reservar, ya sea para limpia, remodelar o cualquier otra cosa.
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
