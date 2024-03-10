using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Models
{
    public class OrgTercero : Actividad
    {
        public Proveedor Proveedor { get; set; }
        public bool Confirmada { get; set; }
        public DateTime? FechaConfirmacion { get; set; }

        public OrgTercero()
        {

        }

        public OrgTercero(Proveedor p, bool confirmada, DateTime fechaConf, string nombre, string descripcion, DateTime fecha, int cantMaxPers, int edadMin, double costo) : base(nombre, descripcion, fecha, cantMaxPers, edadMin, costo)
        {
            Proveedor = p;
            Confirmada = confirmada;
            FechaConfirmacion = fechaConf;
        }

        public override void Validar()
        {
            try
            {
                base.Validar();
                //Solo validamos que el proveedor no sea nulo, más adelante se agregan y validan la confirmacion y su fecha
                ValidarProveedor();
            }
            catch
            {
                throw;
            }
        }

        private void ValidarProveedor()
        {
            if (Proveedor == null)
            {
                throw new Exception("El proveedor no puede estar vacío.");
            }
        }

        //Se llama en el ToString de Reserva
        public override string GetTipo()
        {
            return "OrgTercero";
        }

        public override string ToString()
        {
          

            string ret = base.ToString();
            ret += $"Nombre del Proveedor: {Proveedor.Nombre}.";

            if (Confirmada)
            {
                ret += "Confirmada.";
            }
            else
            {
                ret += "No confirmada.";
            }

            ret += "Fecha de Confirmación: " + FechaConfirmacion;

            return ret;
        }

        //Si el proveedor aplicara un descuento, se calcula em este método
        public override double CalcularCostoFinal(Huesped h)
        {
            double ret = Costo;

            if(Proveedor.ValorDescuento != 0)
            {
                ret = Costo - Costo * Proveedor.ValorDescuento / 100;
            }

            return ret;
        }


        public void ConfirmarActividad()
        {
            //Cambiar Confirmada a true y setear la fecha de confirmacion a DateTime.Now --> Para el proximo obligatorio
        }

        public override string MostrarLugarOProveedor()
        {
            return "Nombre del proveedor: " + this.Proveedor.Nombre;
        }
    }
}
