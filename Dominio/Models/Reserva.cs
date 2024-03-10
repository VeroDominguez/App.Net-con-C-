using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Models
{
    public class Reserva : IValidable, IComparable<Reserva>
    {
        public int Id { get; set; }
        public static int UltimoId { get; set; } = 1;
        public Actividad Actividad { get; set; }
        public Huesped Huesped { get; set; }
        public double CostoFinal { get; set; }
        public string EstadoReserva { get; set; }
        public DateTime? FechaReserva { get; set; }

        public Reserva()
        {
            Id = UltimoId;
            UltimoId++;
        }

        public Reserva(Actividad actividad, Huesped huesped, string estado, DateTime fechaReserva)
        {
            Id = UltimoId;
            UltimoId++;
            Actividad = actividad;
            Huesped = huesped;
            EstadoReserva = estado;
            FechaReserva = fechaReserva;
        }

       

        public void Validar()
        {
            try
            {
                ValidarActividad();
                ValidarHuesped();
                ValidarEstadoReserva();
                ValidarFechaReserva();
                //ValidarFechaPosteriorAHoy();
                ValidarEdadApta();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ValidarNuevaAgenda()
        {
            try
            {
                ValidarActividad();
                ValidarHuesped();
                ValidarEstadoReserva();
                ValidarFechaReserva();
                ValidarFechaPosteriorAHoy();
                ValidarEdadApta();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ValidarActividad()
        {
            if (Actividad == null)
            {
                throw new Exception("La actividad no puede estar vacía.");
            }
        }

        private void ValidarHuesped()
        {
            if (Huesped == null)
            {
                throw new Exception("El huésped no puede estar vacío.");
            }
        }



        private void ValidarEstadoReserva()
        {
            if (string.IsNullOrEmpty(EstadoReserva))
            {
                throw new Exception("El estado de la reserva no puede estar vacío.");
            }

        }

        private void ValidarFechaReserva()
        {//Si la fecha que aparece es la mínima posible quiere decir que no se ingresó ninguna fecha
            if (FechaReserva == DateTime.MinValue)
            {
                throw new Exception("La fecha de la reserva no puede estar vacía.");
            }
        }


        private void ValidarFechaPosteriorAHoy()
        {//Si la fecha de la actividad es anterior a hoy, debe mostrar error
            if (Actividad.Fecha.Date < DateTime.Today)
            {
                throw new Exception("No se pueden reservar actividades con fechas pasadas.");
            }
        }


        private void ValidarEdadApta()
        {
            if (Huesped.CalcularEdad() < Actividad.EdadMinima)
            {
                throw new Exception("El huesped no tiene la edad minima para la actividad.");
            }
        }

        public void AsignarCostoFinal()
        {
            double costoFinal = Actividad.CalcularCostoFinal(Huesped);
            CostoFinal = costoFinal;
            if (CostoFinal == 0)
            {
                EstadoReserva = "CONFIRMADA";
            }
            else
            {
                EstadoReserva = "PENDIENTE_PAGO";
            }

        }

        public override string ToString()
        {
            string ret = $"Nombre del huésped: {Huesped.Nombre} {Huesped.Apellido}\n" +
                $"Actividad: {Actividad.Nombre}\n" +
                $"Fecha: {Actividad.Fecha.ToShortDateString()}";

            //Agrego información según el responsable de la Actividad
            if (Actividad.GetTipo() == "OrgHostal")
            {
                OrgHostal orgAux = Actividad as OrgHostal;
                ret += $"Lugar: {orgAux.Lugar}";
            }
            else
            {
                OrgTercero orgAux = Actividad as OrgTercero;
                ret += $"Nombre del Proveedor: {orgAux.Nombre}";

            }
            //Se debe verificar si es gratuita o no
            if (CostoFinal == 0)
            {
                ret += "Actividad gratuita";
            }
            else
            {
                ret += $"Costo Final: {CostoFinal}";
            }

            ret += $"Estado de la reserva: {EstadoReserva}";


            return ret;
        }

        public int CompareTo(Reserva? other)
        {

            if (Actividad.Fecha.CompareTo(other.Actividad.Fecha) == 1)
            {
                return 1;
            }
            else if (Actividad.Fecha.CompareTo(other.Actividad.Fecha) == -1)
            {
                return -1;
            }
            else
            {
                if (Actividad.Nombre.CompareTo(other.Actividad.Nombre) == 1)
                {
                    return 1;
                }
                if (Actividad.Nombre.CompareTo(other.Actividad.Nombre) == -1)
                {
                    return -1;
                }
                return 0;
            }
        }
    }
}
