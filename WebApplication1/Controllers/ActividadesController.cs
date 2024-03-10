using Dominio;
using Dominio.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class ActividadesController : Controller
    {
        Sistema s = Sistema.GetInstancia();
        public IActionResult ListarActividades()
        {
            ViewBag.Actividades = Sistema.GetInstancia().GetActividadesPorfecha(DateTime.Today);
            return View();
        }

        public IActionResult ListarActividadesSegunFecha(DateTime fechaDada)
        {

            try
            {
                if (fechaDada == DateTime.MinValue)
                {
                    throw new Exception("Debe seleccionar una fecha");
                }

                ViewBag.Actividades = Sistema.GetInstancia().GetActividadesPorfecha(fechaDada);

            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;

            }

            return View("ListarActividades");
        }

    }
}