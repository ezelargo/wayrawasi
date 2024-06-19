using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WayraWasi.Data.Implementations;
using WayraWasi.Models;

namespace WayraWasi.Controllers
{
    [Authorize]
    public class CabaniasController : Controller
    {
        private readonly ILogger<CabaniasController> _logger;
        private readonly CabaniaRepository _repository;
        private readonly IValidator<Cabania> _validator;

        public CabaniasController(ILogger<CabaniasController> logger, CabaniaRepository repository, IValidator<Cabania> validator)
        {
            _logger = logger;
            _repository = repository;
            _validator = validator;
        }

        // GET: CabaniasController
        public async Task<ActionResult> Index()
        {
            var cabanias = await _repository.ListarTodos();
            return View(cabanias);
        }

        // GET: CabaniasController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var cabania = await _repository.BuscadorId(id);
            return View(cabania);
        }

        // GET: CabaniasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CabaniasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cabania cabania)
        {
            var validationResult = await _validator.ValidateAsync(cabania);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    if (!ModelState.ContainsKey(error.PropertyName) || ModelState[error.PropertyName]?.Errors.All(e => e.ErrorMessage != error.ErrorMessage) == true)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                }
                return View(cabania);
            }

            await _repository.Crear(cabania);
            return RedirectToAction("Index");
        }

        // GET: CabaniasController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var cabania = await _repository.BuscadorId(id);
            if (cabania == null)
                return NotFound();
            return View(cabania);
        }

        // POST: CabaniasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Cabania cabania)
        {
            var validationResult = await _validator.ValidateAsync(cabania);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    if (!ModelState.ContainsKey(error.PropertyName) || ModelState[error.PropertyName]?.Errors.All(e => e.ErrorMessage != error.ErrorMessage) == true)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                }

                return View(cabania);
            }

            try
            {
                await _repository.Editar(cabania);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(cabania);
            }
        }

        // GET: CabaniasController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var cabania = await _repository.BuscadorId(id);
            if (cabania == null)
                return NotFound();

            var validationResult = await _validator.ValidateAsync(cabania);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    if (!ModelState.ContainsKey(error.PropertyName) || ModelState[error.PropertyName]?.Errors.All(e => e.ErrorMessage != error.ErrorMessage) == true)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                }
                return View(cabania);
            }

            return View(cabania);
        }

        // POST: CabaniasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmation(int id)
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