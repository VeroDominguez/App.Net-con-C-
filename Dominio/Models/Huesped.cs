using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Models
{
    public class Huesped: Usuario
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public TipoDocumento TipoDocumento{ get; set; }
        public string NumDocumento { get; set; }
        public string Habitacion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Nivel { get; set; }

        public Huesped()
        {
           
        }

        public Huesped(string nombre, string apellido, TipoDocumento tipoDocumento, string numDocumento, string habitacion, DateTime fechaNacimiento, int nivel, string email, string contrasenia):base (email, contrasenia)
        {
           
            Nombre= nombre;
            Apellido= apellido;
            TipoDocumento= tipoDocumento;
            NumDocumento = numDocumento;
            Habitacion = habitacion;
            FechaNacimiento= fechaNacimiento;
            Nivel = nivel;
        }


        public override void Validar()
        {
            try
            {
                base.Validar();
                ValidarTipoDocumentoCI();
                ValidarHabitacion();
                ValidarNombre();
                ValidarApellido();
                ValidarFechaNoVacia();
                ValidarFechaAnteriorAlPresente();
                ValidarNivel();

            }

            catch 
            {
                throw;
            }
        }

        // si el tipo de documento seleccionado es CI, se realizan 3 validaciones para validar la misma (los otros tipos de documentos no llevan vakidaciones)
        private void ValidarTipoDocumentoCI()
        {
            if (TipoDocumento == TipoDocumento.CI)
            {
                try 
                { 
                    ValidarCI();
                    ValidarCINum();
                    ValidarCIDigito();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        // se valida que la CI ingresada contenga 7 u 8 caracteres y se solicita que el usuaario la ingrese sin puntos y sin el guión 
        private void ValidarCI()
        {
            
            if (NumDocumento.Length != 7 && NumDocumento.Length != 8)
            {
                throw new Exception("La cédula solo puede tener 7 u 8 caracteres.");
            }
        }

        // se valida que los caracteres ingresados sean solo números
        private void ValidarCINum()
        {
            int CIConvertida;
            bool CIString = int.TryParse(NumDocumento, out CIConvertida);
            if (!CIString)
            {
                throw new Exception("La cédula solo puede contener caracteres numéricos.");
            }
        }

        //se valida que el último dígito sea correcto, se toman los primeros 7 números y se multiplican cada uno (respetando el lugar) por 2987634 y se suman los resultados.
        //A la suma total se le busca el número mayor que termina en 0 y se le resta el resultado de la suma.
        //Finalmente se compara el resultado de esta resta con el digito ingresado, si no coinciden es porque el digito es incorrecto.
        private void ValidarCIDigito()
        {
            int[] digitos = { 2, 9, 8, 7, 6, 3, 4 };
            int suma = 0;
            string cIVerificar = NumDocumento;


            // dentro del if lo que hago es agregarle un cero a las ci que no tienen el primer número de millon
            if (cIVerificar.Length == 7)
            {
                cIVerificar = "0" + cIVerificar;
            }

            //el length de todas las ci es 8, pero solo debo recorrer los primeros 7 y dejar afuera el dígito verificador
            for (int i = 0; i < cIVerificar.Length - 1; i++)
            {
                suma += digitos[i] * Int32.Parse((cIVerificar[i]).ToString());
            }
            
            string sumaTotal = suma.ToString();
            int ultNUmeroSuma = Int32.Parse((sumaTotal[sumaTotal.Length - 1]).ToString());
            int digitoVerificadorCI = Int32.Parse(cIVerificar[cIVerificar.Length - 1].ToString());

            int digitoAVerificar = 0;
            if(ultNUmeroSuma != 0)
            {
                digitoAVerificar = 10 - ultNUmeroSuma;
            }

            if (digitoAVerificar != digitoVerificadorCI)
            {
                throw new Exception("El dígito verificador es incorrecto, cédula invalida.");
            }
        }

        // se valida que habitación, nombre, apellido y fecha de nacimiento no estén vacíos.
        private void ValidarHabitacion()
        {
            if (string.IsNullOrEmpty(Habitacion))
            {
                throw new Exception("La habitación no puede estar vacía.");
            }
        }


        private void ValidarNombre()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new Exception("El nombre no puede estar vacío.");
            }
        }

        private void ValidarApellido()
        {
            if (string.IsNullOrEmpty(Apellido))
            {
                throw new Exception("El apellido no puede estar vacío.");
            }
        }

        private void ValidarFechaNoVacia()
        {
            if (FechaNacimiento == DateTime.MinValue)
            {
                throw new Exception("La fecha no puede estar vacía");
            }
        }

        // se valida que la fecha ingresada sea anterior al dia de hoy
        private void ValidarFechaAnteriorAlPresente()
        {
            if (FechaNacimiento > DateTime.Today)
            {
                throw new Exception("La fecha de nacimiento debe ser anterior al día de hoy.");
            }
        }

        private void ValidarNivel()
        {
            if (Nivel < 1 || Nivel > 4)
            {
                throw new Exception("El nivel debe estar comprendido entre 1 y 4.");
            }
        }


        public override string GetTipo()
        {
            return "Huesped";
        }

        // con el metodo Equals me devuelve true si el objeto que le paso es un huesped y coincide el tipo de documento y el número de documento.
        // previamente verifica que no exista otro usuario regsitrado con el mismo mail (función de Equals en usuario)

        public override bool Equals(object? obj)
        {
            return base.Equals(obj) || (obj is Huesped huesped &&
                TipoDocumento == huesped.TipoDocumento &&
                NumDocumento == huesped.NumDocumento);
        }

        public override string ToString()
        {
            return $"Nombre: {Nombre}. Apellido: {Apellido}. Tipo de documento: {TipoDocumento}";
        }

        public int CalcularEdad()
        {
            int edad =  DateTime.Today.Year - FechaNacimiento.Year;
            //Si su cumpleaños no ocurrió aún le restamos un año
            
            if(DateTime.Today < FechaNacimiento)
            {
                edad = edad - 1;
            }
            
            return edad;
        }


    }
}
