using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Models
{
    public class Proveedor: IValidable, IComparable<Proveedor>
    {
        public int Id { get; set; }
        public static int UltimoId { get; set; } = 1;
        public string Nombre { get; set; }
        public int Telefono { get; set; }
        public string Direccion { get; set; }
        public double ValorDescuento { get; set; }

        public Proveedor()
        {
            Id= UltimoId;
            UltimoId++;
        }

        public Proveedor(string nombre, int telefono, string direccion, double valorDescuento)
        {
            Id = UltimoId;
            UltimoId++;
            Nombre = nombre;
            Telefono = telefono;
            Direccion = direccion;
            ValorDescuento = valorDescuento;
        }

        public void Validar()
        {
            ValidarNombre();
            ValidarTelefono();
            ValidarDireccion();
        }

        private void ValidarNombre()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new Exception("El nombre no puede estar vacío.");
            }
        }

        private void ValidarTelefono()
        {
            if (Telefono < 0)
            {
                throw new Exception("El teléfono no puede estar vacío.");
            }
        }

        private void ValidarDireccion()
        {
            if (string.IsNullOrEmpty(Direccion))
            {
                throw new Exception("La dirección no puede estar vacía.");
            }
        }

        // con el metodo Equals me devuelve true si el objeto que le paso es un proveedor y coincide el nombre.
        public override bool Equals(object? obj)
        {
            return base.Equals(obj) || (obj is Proveedor p &&
                Nombre == p.Nombre);
        }

        //ComparteTo orden alfabetico por nombre
        public int CompareTo(Proveedor? other)
        {
            return Nombre.CompareTo(other.Nombre);
        }

        public override string ToString()
        {
            return $"Nombre: {Nombre}. Teléfono: {Telefono}. Dirección: {Direccion}. Valor Descuento: {ValorDescuento}";
        }
    }
}
