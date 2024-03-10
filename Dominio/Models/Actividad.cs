using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Models
{
    public abstract class Actividad: IValidable, IComparable<Actividad>
    {
        public int Id { get; set; }
        public static int UltimoId { get; set; } = 1;
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public int CantMaxPersonas { get; set; }
        public int EdadMinima { get; set; }
        public double Costo { get; set; }

        
        public Actividad()
        {
            Id=UltimoId;
            UltimoId++;

        }

        protected Actividad(string nombre, string descripcion, DateTime fecha, int cantMaxPersonas, int edadMinima, double costo)
        {
            Id = UltimoId;
            UltimoId++;
            Nombre =nombre;
            Descripcion=descripcion;
            Fecha = fecha;
            CantMaxPersonas = cantMaxPersonas;
            EdadMinima = edadMinima;
            Costo = costo;
                
        }
        

        public virtual void Validar()
        {
            try
            {
                ValidarNombre();
                ValidarNombre25Max();
                ValidarDescripcion();
                ValidarFechaNoVacia();
                /*ValidarFechaPosteriorAlPresente();*/
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ValidarNombre()
        {
            if(string.IsNullOrEmpty(Nombre))
            {
                throw new Exception("El nombre no puede estar vacío.");
            }
        }

        private void ValidarNombre25Max()
        {
            if (Nombre.Length > 25)
            {
                throw new Exception("El nombre debe tener un máximo de 25 caracteres.");
            }
        }

        private void ValidarDescripcion()
        {
            if (string.IsNullOrEmpty(Descripcion))
            {
                throw new Exception("La descripción no puede estar vacía.");
            }
        }

        private void ValidarFechaNoVacia()
        {// Si la fecha tiene el valor mínimo posible quiere decir que no se ingresó una fecha
            if (Fecha == DateTime.MinValue) 
            {
                throw new Exception("La fecha no puede estar vacía");
            }
        }
        /*private void ValidarFechaPosteriorAlPresente()
        {
            if (Fecha < DateTime.Today)
            {
                throw new Exception("La fecha debe ser posterior al día actual.");
            }
        }*/

        // con el metodo Equals me devuelve true si el objeto que le paso es una actividad y coincide el nombre.
        public override bool Equals(object? obj)
        {
            return base.Equals(obj) || (obj is Actividad a &&
                Nombre == a.Nombre);
        }

        //CompareTo orden descendente segun costo
        public int CompareTo(Actividad? other)
        {
            if (Costo.CompareTo(other.Costo) == 1)
            {
                return 1;
            }
            else if (Costo.CompareTo(other.Costo) == -1)
            {
                return -1;
            }
            else
            {
                if(Nombre.CompareTo(other.Nombre) == 1)
                {
                    return 1;
                }
                else if(Nombre.CompareTo(other.Nombre) == -1)
                {
                    return -1;
                }
               
                return 0;
            }
        }

        public override string ToString()
        {
            string stringFecha= Fecha.ToShortDateString();
            return $"Id: {Id}. Nombre: {Nombre}. Descripción: {Descripcion}. Fecha: {stringFecha}. Cantidad máxima de personas: {CantMaxPersonas}. Edad mínima requerida: {EdadMinima} años. Costo: {Costo}. ";
        }

        //Métodos abstractos
        public abstract double CalcularCostoFinal(Huesped h);
        public abstract string GetTipo();

        public abstract string MostrarLugarOProveedor();


    }
}
