using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WayraWasi.Models;

namespace WayraWasi.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;

            return View();
        }
    }
}
