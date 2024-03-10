using Dominio;
using Dominio.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {

        Sistema s = Sistema.GetInstancia();
        public IActionResult Index()
        {
            int? logueadoId = HttpContext.Session.GetInt32("LogueadoId");
            string logueadoNombre = HttpContext.Session.GetString("LogueadoNombre");
            if( logueadoId != null)
            {
                ViewBag.MsgBienvenida = $"Hola {logueadoNombre}";
            }
            return View();
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        

      
    }
}