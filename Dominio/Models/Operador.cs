using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Models
{
    public class Operador: Usuario
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaIngreso { get; set; }

        public Operador() 
        {

        }

        public Operador(string nombre, string apellido, DateTime fechaIngreso, string email, string contrasenia):base (email, contrasenia)
        {
            Nombre = nombre;
            Apellido = apellido;
            FechaIngreso = fechaIngreso;
        }

        public override string GetTipo()
        {
            return "Operador";
        }
    }
}
