using Dominio;
using Dominio.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class AgendaController : Controller
    {
        Sistema s = Sistema.GetInstancia();

        public IActionResult Agendar(int id)
        {
            //conrolar que exista una personaz logueada y que ademas sea huesped. Si es huesped, sigo de largo
            if (HttpContext.Session.GetString("LogueadoTipo") != null && HttpContext.Session.GetString("LogueadoTipo") == "Huesped")
            {
                try
                {
                    Huesped h = s.GetHuesped(HttpContext.Session.GetInt32("LogueadoId"));
                    Actividad a = s.GetActividad(id);
                    Reserva r = new Reserva(a, h, "PENDIENTE_PAGO", DateTime.Today);
                    s.AltaReservaNuevaAgenda(r);
                    ViewBag.Reserva = r;
                    return View("ResultadoReserva");

                }
                catch (Exception e)
                {
                    ViewBag.Error = e.Message;
                    return View("ResultadoReserva");
                }
            }
            else
            {
                return RedirectToAction("Login", "Usuario");
            }
        }


        public IActionResult ListarAgendas()
        {
            if (HttpContext.Session.GetInt32("LogueadoId") != null)
            {
                if (HttpContext.Session.GetString("LogueadoTipo") == "Operador")
                {
                    List<Reserva> agendas = s.GetReservasConSort();
                    return View(agendas);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ListarAgendasPorHuesped()
        {
            if (HttpContext.Session.GetInt32("LogueadoId") != null)
            {
                if (HttpContext.Session.GetString("LogueadoTipo") == "Operador")
                {
                    return View();
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]

        public IActionResult ListarAgendasPorHuesped(string tipoDoc, string numDoc)
        {
            //El tipo de documento no se puede validar que esté vacío porque siempre va a devolver algo (es un select)
            //Se valida que el numero de documento no este vacio:
            if (string.IsNullOrEmpty(numDoc))
            {
                ViewBag.msg = "El número de documento no puede estar vacío.";
                return View();
            }
            Huesped h = s.GetHuespedDocumento(tipoDoc, numDoc);
            if (h != null)
            {
                List<Reserva> agendasHuesped = s.GetReservasPorHuesped(h);
                if (agendasHuesped.Count == 0)
                {
                    ViewBag.msgSinAgendaHuesped = $"El huésped {h.Nombre} {h.Apellido} no tiene agendas.";
                    return View();
                }
                return View(agendasHuesped);
            }
            ViewBag.msg = "Datos Erróneos";
            return View();

        }

        //Listado de Agendas por fecha. Por defecto muestra las agendas del dia actual.
        public IActionResult ListarAgendasPorFecha()
        {
            if (HttpContext.Session.GetInt32("LogueadoId") != null)
            {
                if (HttpContext.Session.GetString("LogueadoTipo") == "Operador")
                {
                    List<Reserva> agendas = s.GetReservasDeHoy();
                    if (agendas.Count == 0)
                    {
                        ViewBag.msg = "No hay agendas en el día de hoy.";
                        return View(agendas);
                    }
                    return View(agendas);
                }
            }
            return RedirectToAction("Index", "Home");
        }



        [HttpPost]
        public IActionResult ListarAgendasPorFecha(DateTime fecha)
        {
            if (fecha != DateTime.MinValue)
            {
                List<Reserva> agendasPorFecha = s.GetReservasPorFecha(fecha);
                if (agendasPorFecha.Count == 0)
                {
                    ViewBag.msgNoAgendaFechaBuscada = "No hay agendas en la fecha buscada.";
                    return View(agendasPorFecha);
                }
            }
            ViewBag.msgFaltaIngresarFecha = "Debe ingresar una fecha.";

            //Si no se selecciono una fecha se vuelve a mostrar la vista principal con las agenda del dia actual.
            List<Reserva> agendas = s.GetReservasDeHoy();
            if (agendas.Count == 0)
            {
                ViewBag.msg = "No hay agendas en el día de hoy.";
                return View(agendas);
            }
            return View(agendas);
        }


        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetInt32("LogueadoId") != null)
            {
                if (HttpContext.Session.GetString("LogueadoTipo") == "Operador")
                {
                    Reserva r = s.GetReserva(id);
                    s.ConfirmarAgenda(id);
                    TempData["msgAgendaConfirmada"] = "Agenda Confirmada con éxito";
                    return RedirectToAction("ListarAgendas");
                }
            }
            return RedirectToAction("Index", "Home");

        }

        public IActionResult AgendasDelHuesped()
        {
            if (HttpContext.Session.GetInt32("LogueadoId") != null)
            {
                if (HttpContext.Session.GetString("LogueadoTipo") == "Huesped")
                {
                    Huesped h = s.GetHuesped(HttpContext.Session.GetInt32("LogueadoId"));
                    List<Reserva> agendasHuesped = s.GetReservasDelHuespedFechaPosteriorAHoy(h);
                    if (agendasHuesped.Count == 0)
                    {
                        ViewBag.Msg = "Usted no tiene agendas.";
                    }
                    return View(agendasHuesped);
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
