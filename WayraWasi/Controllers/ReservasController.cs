using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WayraWasi.Data;
using WayraWasi.Data.Implementations;
using WayraWasi.Models;

namespace WayraWasi.Controllers
{
    public class ReservasController : Controller
    {
        private readonly ILogger<ReservasController> _logger;
        private readonly ReservaRepository _repository;

        public ReservasController(ILogger<ReservasController> logger, ReservaRepository repository)
        {
            _logger = logger; 
            _repository = repository;
        }


        // GET: ReservasController
        public async Task<ActionResult> Index()
        {
            var reserva = await _repository.ListarTodos();
            return View(reserva);
        }

        // GET: ReservasController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var reserva = await _repository.BuscadorId(id);
            return View(reserva);
        }

        // GET: ReservasController/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.Cabanias = await _repository.ListarCabanias();
            return View();
        }

        // POST: ReservasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reserva reserva)
        {
            try
            {
                var cabaniaDisponible = await _repository.BuscarCabaniaDisponibilidad(reserva.IdCabania,reserva.FechaEntrada,reserva.FechaSalida);
                if (cabaniaDisponible != null)
                {
                    TempData["ExistingCabain"] = "La cabaña ya se encuentra reservada en esas fechas";
                    ViewBag.Cabanias = await _repository.ListarCabanias();
                    return View(reserva);
                }

                // Veo si el numero de personas ingresado no excede la capacidad de la cabaña

                var cabania = await _repository.BuscarPorIDCabania(reserva.IdCabania);

                if (reserva.NumeroPersonas > cabania.Capacidad)
                {
                    ModelState.AddModelError("NumeroPersonas", $"El número de personas excede la capacidad de la cabaña seleccionada. La capacidad maxima es de {cabania.Capacidad}.");
                    ViewBag.Cabanias = await _repository.ListarCabanias();
                    return View(reserva);
                }
                await _repository.Crear(reserva);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ReservasController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            ViewBag.Cabanias = await _repository.ListarCabanias();

            var reserva = await _repository.BuscadorId(id);
            if (reserva == null)
                return NotFound();

            var cabaniaDisponible = await _repository.BuscarCabaniaDisponibilidad(reserva.IdCabania, reserva.FechaEntrada, reserva.FechaSalida);
            if (cabaniaDisponible != null)
            {
                TempData["ExistingCabain"] = "La cabaña ya se encuentra reservada en esas fechas";
                ViewBag.Cabanias = await _repository.ListarCabanias();
                return View(reserva);
            }

            return View(reserva);
        }

        // POST: ReservasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id,Reserva reserva)
        {
            if (id != reserva.IdReservacion)
            {
                return BadRequest();
            }
            try
            {
                /* Cambio de cabaña y asignacion de fechas nuevas para cada una osea que una se libera y la otra se ocupa en ese rango de fechas */
                var ReservaAntigua = await _repository.BuscadorId(id);

                var cabania = await _repository.BuscarPorIDCabania(reserva.IdCabania);

                if (reserva.NumeroPersonas > cabania.Capacidad)
                {
                    ModelState.AddModelError("NumeroPersonas", $"El número de personas excede la capacidad de la cabaña seleccionada. La capacidad maxima es de {cabania.Capacidad}.");
                    ViewBag.Cabanias = await _repository.ListarCabanias();
                    return View(reserva);
                }
                await _repository.Editar(reserva);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }

        // GET: ReservasController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var reserva = await _repository.BuscadorId(id);
            if (reserva == null)
                return NotFound();

            return View(reserva);
        }

        // POST: ReservasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmation(int id)
        {
            try
            {
                await _repository.Eliminar(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
