using Microsoft.AspNetCore.Mvc;
using WayraWasi.Data;
using WayraWasi.Data.Implementations;
using WayraWasi.Models;

namespace WayraWasi.Controllers
{
    public class ReservasController : Controller
    {
        private readonly ILogger<CabaniasController> _logger;
        private readonly ReservaRepository _repository;

        public ReservasController(ILogger<CabaniasController> logger, ReservaRepository repository)
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReservasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reserva reserva)
        {
            try
            {
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
            var reserva = await _repository.BuscadorId(id);
            if (reserva == null)
                return NotFound();
            return View(reserva);
        }

        // POST: ReservasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Reserva reserva)
        {
            try
            {
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
