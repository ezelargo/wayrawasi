using FluentValidation;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using WayraWasi.Data;
using WayraWasi.Data.Implementations;
using WayraWasi.Models;

namespace WayraWasi.Controllers
{
    [Authorize]
    public class ReservasController : Controller
    {
        private readonly ILogger<ReservasController> _logger;
        private readonly ReservaRepository _repository;
        private readonly IValidator<Reserva> _validator;

        public ReservasController(ILogger<ReservasController> logger, ReservaRepository repository, IValidator<Reserva> validator)
        {
            _logger = logger;
            _repository = repository;
            _validator = validator;
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

        public IActionResult GenerarReporteReservas()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GenerarReporteReservas(DateTime fechaInicio, DateTime fechaFin)
        {
            var reserva = await _repository.GenerarReservaPorFecha(fechaInicio, fechaFin);

            var pdf = GenerarReportePDF(reserva);

            return File(pdf, "application/pdf", "Reportes de Reservas.pdf");
        }

        private byte[] GenerarReportePDF(IEnumerable<Reserva> reservas)
        {
            using (var stream = new MemoryStream())
            {
                if (stream.CanWrite)
                {
                    PdfWriter writer = new PdfWriter(stream);
                    PdfDocument pdf = new PdfDocument(writer);
                    Document document = new Document(pdf);

                    document.Add(new Paragraph($"Reporte generado el {DateTime.Now:yyyy-MM-dd}").SetTextAlignment(TextAlignment.CENTER).SetFontSize(18));
                    document.Add(new Paragraph("\n"));

                    Table table = new Table(5, true);

                    table.AddHeaderCell("Nombre Cliente");
                    table.AddHeaderCell("Fecha Entrada");
                    table.AddHeaderCell("Fecha Salida");
                    table.AddHeaderCell("Nombre de cabaña");
                    table.AddHeaderCell("Estado");

                    foreach (var reserva in reservas)
                    {
                        var cabania = _repository.BuscarPorIDCabania(reserva.IdCabania);
                        table.AddCell(reserva.NombreCliente);
                        table.AddCell(reserva.FechaEntrada.ToString());
                        table.AddCell(reserva.FechaSalida.ToString());
                        table.AddCell(cabania.Result.NombreCabania.ToString());
                        table.AddCell(reserva.Estado);
                    }

                    document.Add(table);
                    document.Close();
                }
                return stream.ToArray();
            }
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
            var cabania = await _repository.BuscarPorIDCabania(reserva.IdCabania);
            var validationResult = await _validator.ValidateAsync(reserva);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    if (!ModelState.ContainsKey(error.PropertyName) || ModelState[error.PropertyName]?.Errors.All(e => e.ErrorMessage != error.ErrorMessage) == true) // Solo se muestra una vez el error
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                }
                ViewBag.CabaniaSeleccionada = cabania;
                ViewBag.Cabanias = await _repository.ListarCabanias();
                return View(reserva);
            }

            await _repository.Crear(reserva);
            return RedirectToAction("Index");
        }

        // GET: ReservasController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            ViewBag.Cabanias = await _repository.ListarCabanias();

            var reserva = await _repository.BuscadorId(id);
            if (reserva == null)
                return NotFound();

            var cabania = await _repository.BuscarPorIDCabania(reserva.IdCabania);
            ViewBag.CabaniaSeleccionada = cabania;
            return View(reserva);
        }

        // POST: ReservasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Reserva reserva)
        {
            if (id != reserva.IdReservacion)
            {
                return BadRequest();
            }
            var cabania = await _repository.BuscarPorIDCabania(reserva.IdCabania);
            var validationResult = await _validator.ValidateAsync(reserva);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    if (!ModelState.ContainsKey(error.PropertyName) || ModelState[error.PropertyName]?.Errors.All(e => e.ErrorMessage != error.ErrorMessage) == true)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                }
                ViewBag.CabaniaSeleccionada = cabania;
                ViewBag.Cabanias = await _repository.ListarCabanias();
                return View(reserva);
            }       

            await _repository.Editar(reserva);
            return RedirectToAction("Index");
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