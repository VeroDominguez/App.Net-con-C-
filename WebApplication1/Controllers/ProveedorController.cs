using Dominio;
using Dominio.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApplication1.Controllers
{
    public class ProveedorController : Controller
    {
        Sistema s = Sistema.GetInstancia();


        public IActionResult ListarProveedores()
        {
            if (HttpContext.Session.GetInt32("LogueadoId") != null)
            {
                if (HttpContext.Session.GetString("LogueadoTipo") == "Operador")
                {
                    List<Proveedor> p = s.GetProveedoresOrdenAlfabetico();
                    return View(p);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetInt32("LogueadoId") != null)
            {
                if (HttpContext.Session.GetString("LogueadoTipo") == "Operador")
                {
                    Proveedor pBuscado = s.GetProveedor(id);
                    return View(pBuscado);
                }

            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Edit(Proveedor p)
        {
            try
            {
                s.AsignarValorDescuento(p.Id, p.ValorDescuento);
                TempData["msgDescuentoAsignado"] = "Descuento asignado con éxito.";
                return RedirectToAction("ListarProveedores", "Proveedor");
            }
            catch (Exception ex)
            {
                ViewBag.msg = ex.Message;
            }
            return View();
        }
    }
}
