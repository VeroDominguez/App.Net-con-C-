using Dominio;
using Dominio.Models;
using Microsoft.AspNetCore.Mvc;


namespace WebApplication1.Controllers
{
    public class UsuarioController : Controller
    {
        Sistema s = Sistema.GetInstancia();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string contrasenia)
        {
            if (string.IsNullOrEmpty(email))
            {
                ViewBag.msg = "El email no puede estar vacío";
                return View();
            }
            if (string.IsNullOrEmpty(contrasenia))
            {
                ViewBag.msg = "La contraseña no puede estar vacía";
                return View();
            }

            Usuario uBuscado = s.Login(email, contrasenia);

            if (uBuscado != null)
            {
                HttpContext.Session.SetInt32("LogueadoId", uBuscado.Id);
                HttpContext.Session.SetString("LogueadoTipo", uBuscado.GetTipo());
                if (uBuscado.GetTipo() == "Operador")
                {
                    Operador o = uBuscado as Operador;
                    HttpContext.Session.SetString("LogueadoNombre", o.Nombre);
                }
                else
                {
                    Huesped h = uBuscado as Huesped;
                    HttpContext.Session.SetString("LogueadoNombre", h.Nombre);
                }

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Msg = "Usuario no encontrado";
                return View();
            }

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["msgLogout"] = "Se ha cerrado la sesión.";
            return RedirectToAction("Index", "Home");
        }

        //Alta Huesped
        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("LogueadoId") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Create(string nombre, string apellido, TipoDocumento tipoDocumento, string numDocumento, string habitacion, DateTime fechaNacimiento, int nivel, string email, string contrasenia)
        {
            try
            {
                Huesped h = new Huesped(nombre, apellido, tipoDocumento, numDocumento, habitacion, fechaNacimiento, nivel, email, contrasenia);
                s.AltaUsuario(h);
                TempData["msgAltaCorrecta"] = "Huésped creado correctamente";
                HttpContext.Session.SetInt32("LogueadoId", h.Id);
                HttpContext.Session.SetString("LogueadoNombre", h.Nombre);
                HttpContext.Session.SetString("LogueadoTipo", h.GetTipo());
                return RedirectToAction("Index", "Home");

            }
            catch (Exception ex)
            {
                ViewBag.Msg = ex.Message;
            }

            return View();
        }

        public IActionResult ListarProveedores()
        {
            if (HttpContext.Session.GetInt32("LogueadoId") != null)
            {
                if (HttpContext.Session.GetString("LogueadoTipo") == "Operador")
                {
                    List<Proveedor> prov = s.GetProveedoresOrdenAlfabetico();
                    return View(prov);
                }
            }
            return RedirectToAction("Index", "Home");

        }

        public IActionResult Details()
        {
            int? logId = HttpContext.Session.GetInt32("LogueadoId");
            if (HttpContext.Session.GetInt32("LogueadoId") != null)
            {
                if (HttpContext.Session.GetString("LogueadoTipo") == "Operador")
                {
                    Operador o = s.GetOperador(logId);
                    return View(o);
                }
            }
            return RedirectToAction("Index", "Home");

        }

        public IActionResult DetallesHuesped()
        {
            if (HttpContext.Session.GetInt32("LogueadoId") != null && HttpContext.Session.GetString("LogueadoTipo") == "Huesped")
            {
                Huesped h = s.GetHuesped(HttpContext.Session.GetInt32("LogueadoId"));
                return View(h);
            }

            return RedirectToAction("Index", "Home");
        }

    }
}
