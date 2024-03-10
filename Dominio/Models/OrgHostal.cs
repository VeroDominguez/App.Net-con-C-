using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Models
{
    public class OrgHostal: Actividad
    {
        public string PersonaResponsable { get; set; }
        public string Lugar { get; set; }
        public bool AlAireLibre { get; set; }
        
        public OrgHostal()
        {
          
        }

        public OrgHostal(string persResp, string lugar, bool aireLibre, string nombre, string descripcion, DateTime fecha, int cantMaxPers, int edadMin, double costo):base(nombre, descripcion, fecha, cantMaxPers, edadMin, costo)
        {
            PersonaResponsable = persResp;
            Lugar = lugar;
            AlAireLibre = aireLibre;
        }

        public override void Validar()
        {
            try
            {
                base.Validar();
                ValidarPersResp();
                ValidarLugar();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ValidarPersResp()
        {
            if (string.IsNullOrEmpty(PersonaResponsable))
            {
                throw new Exception("El nombre de la persona responsable no puede estar vacío.");
            }
        }

        private void ValidarLugar()
        {
            if (string.IsNullOrEmpty(Lugar))
            {
                throw new Exception("El lugar no puede estar vacío.");
            }
        }

        //Se llama en el ToString de Reserva
        public override string GetTipo()
        {
            return "OrgHostal";
        }

        public override string ToString()
        {
            string ret = base.ToString(); 
            ret += $"Persona Responsable: {PersonaResponsable}. Lugar: {Lugar}.";

            if(AlAireLibre)
            {
                ret += "Al aire Libre.";
            }
            else
            {
                ret += "En espacio cerrado.";
            }
            
            
            return ret;
        }

        //Si el nivel del huésped está entre 1 y 4, se aplica el descuento correspondiente. Si no, se devuelve el costo original.
        public override double CalcularCostoFinal(Huesped h)
        {
            double ret = Costo;
            if (h.Nivel == 2) 
            {
                ret = Costo - Costo * 0.1;
            }
            else if (h.Nivel == 3) 
            {
                ret = Costo - Costo * 0.15;
            }
            else if (h.Nivel == 4)
            {
                ret = Costo - Costo * 0.2;
            }

            return ret;
        }

        public override string MostrarLugarOProveedor()
        {
            return "Lugar de la actividad: " + this.Lugar;
        }


    }
}
