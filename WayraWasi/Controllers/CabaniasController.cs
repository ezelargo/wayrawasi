using Microsoft.AspNetCore.Mvc;
using WayraWasi.Data;
using WayraWasi.Data.Implementations;
using WayraWasi.Models;

namespace WayraWasi.Controllers
{
    public class CabaniasController : Controller
    {
        private readonly ILogger<CabaniasController> _logger;
        private readonly CabaniaRepository _repository; // Utilizo los repositories para no interactuar con el IGenericRepository(Ya que los metodos existen pero esta es solo una interfaz mientras que el repositorio tiene todo el codigo necesario) ni el modelo directamente
        private readonly ReservaRepository _reservaRepository;

        public CabaniasController(ILogger<CabaniasController> logger, CabaniaRepository repository, ReservaRepository reservaRepository)
        {
            _logger = logger;
            _repository = repository;
            _reservaRepository = reservaRepository;
        }


        // GET: CabaniasController
        public async Task<ActionResult> Index()
        {
            var Cabania = await _repository.ListarTodos();
            return View(Cabania);
        }

        // GET: CabaniasController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var Cabania = await _repository.BuscadorId(id);
            return View(Cabania);
        }

        // GET: CabaniasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CabaniasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Cabania Cabania)
        {
                await _repository.Crear(Cabania);
                return RedirectToAction("Index");
        }

        // GET: CabaniasController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var Cabania = await _repository.BuscadorId(id);
            if (Cabania == null)
                return NotFound();
            return View(Cabania);
        }

        // POST: CabaniasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Cabania Cabania)
        {
            try
            {
                await _repository.Editar(Cabania);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }

        // GET: CabaniasController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var Cabania = await _repository.BuscadorId(id);
            if (Cabania == null)
                return NotFound();

            var reservas = await _reservaRepository.BuscarPorIDCabania(id); // Buscar si hay alguna cabaña relacionada a alguna reserva
            if (reservas != null)
            {
                TempData["ErrorMessage"] = "No se puede eliminar la cabaña porque tiene reservas asociadas. Elimina las reservas primero.";
                return RedirectToAction("Index");
            }

            return View(Cabania);
        }

        // POST: CabaniasController/Delete/5
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
