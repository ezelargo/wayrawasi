using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
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

        private byte[] GenerarReportePDF(IEnumerable<Reserva> reservas) // El Byte sirve para representar un archivo PDF en memoria, pudiendo enviarse a una respuesta HTTP para su descarga
        {
            using (var stream = new MemoryStream())
            {
                if (stream.CanWrite)
                {
                    PdfWriter writer = new PdfWriter(stream);
                    PdfDocument pdf = new PdfDocument(writer);
                    Document document = new Document(pdf);


                    /* Generacion de reportes por PDF */
                    document.Add(new Paragraph($"Reporte de Reservas generado el {DateTime.Now.ToString("yyyy-MM-dd")}").SetTextAlignment(TextAlignment.CENTER).SetFontSize(18));
                    document.Add(new Paragraph("\n"));                    

                    Table table = new Table(5, true);

                    table.AddHeaderCell("Nombre Cliente");
                    table.AddHeaderCell("Fecha Entrada");
                    table.AddHeaderCell("Fecha Salida");
                    table.AddHeaderCell("Nombre de cabaña");
                    table.AddHeaderCell("Estado");

                    foreach (var reserva in reservas) // Me imprime una
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
            try
            {
                var cabania = await _repository.BuscarPorIDCabania(reserva.IdCabania);

                var cabaniaDisponible = await _repository.BuscarCabaniaDisponibilidad(reserva.IdCabania,reserva.FechaEntrada,reserva.FechaSalida);
                if (cabaniaDisponible != null)
                {
                    TempData["ExistingCabain"] = "La cabaña ya se encuentra reservada en esas fechas";
                    ViewBag.CabaniaSeleccionada = cabania;
                    ViewBag.Cabanias = await _repository.ListarCabanias();
                    return View(reserva);
                }

                // Veo si el numero de personas ingresado no excede la capacidad de la cabaña
                if (reserva.NumeroPersonas > cabania.Capacidad)
                {
                    ModelState.AddModelError("NumeroPersonas", $"El número de personas excede la capacidad de la cabaña seleccionada. La capacidad maxima es de {cabania.Capacidad}.");
                    ViewBag.CabaniaSeleccionada = cabania;
                    ViewBag.Cabanias = await _repository.ListarCabanias();
                    return View(reserva);
                }

                if (reserva.FechaEntrada == reserva.FechaSalida)
                {
                    // Usar misma reserva
                    TempData["SameDate"] = "Debe haber al menos un dia de diferencia entre fechas";
                    ViewBag.CabaniaSeleccionada = cabania;
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

            var cabania = await _repository.BuscarPorIDCabania(reserva.IdCabania);

            var cabaniaDisponible = await _repository.BuscarCabaniaDisponibilidad(reserva.IdCabania, reserva.FechaEntrada, reserva.FechaSalida);
            if (cabaniaDisponible != null)
            {
                TempData["ExistingCabain"] = "La cabaña ya se encuentra reservada en esas fechas";
                ViewBag.CabaniaSeleccionada = cabania;
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
                    ViewBag.CabaniaSeleccionada = cabania;
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
