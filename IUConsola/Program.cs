using Dominio;
using Dominio.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IUConsola
{
    public class Program
    {
        static void Main(string[] args)
        {
            Sistema s = Sistema.GetInstancia();

            Console.WriteLine("* * * Bienvenido al Hostal <NombreDelHostal> * * *");
            Console.WriteLine("Ingrese el número de la opción deseada:");
            Console.WriteLine("1- Ver lista completa de actividades");
            Console.WriteLine("2- Ver lista de proveedores");
            Console.WriteLine("3- Ver lista de actividades dentro de un rango de fechas y con un precio mínimo.");
            Console.WriteLine("4- Ingresar promoción de proveedor");
            Console.WriteLine("5- Alta de huéspedes");
            Console.WriteLine("6- Mostrar lista de huéspedes");
            Console.WriteLine("7- Limpiar la consola");
            Console.WriteLine("0- Salir");

            int opcion = Int32.Parse(Console.ReadLine());

            while (opcion != 0)
            {
                // mientras la opción seleccionada sea distinta de cero, podemos ingresar a las distintas opciones del menú
                switch (opcion)
                {
                    //nos muestra la lista completa de actividades
                    case 1:
                            foreach (Actividad a in s.GetActividades())
                        {
                            Console.WriteLine(a.ToString());
                        }
                        break;

                    //nos muestra la lista de proveedores por orden alfabético
                    case 2:
                        foreach (Proveedor p in s.GetProveedoresOrdenAlfabetico())
                        {
                            Console.WriteLine(p.ToString());
                        }
                        break;

                    // nos devuelve una lista de actividades entre las fechas seleccionadas y a partir de un precio mínimo (tambien seleccionado) y por orden descendente por costo
                    case 3:
                        bool datosCorrectos = false;
                        DateTime fecha1 = DateTime.MinValue;
                        DateTime fecha2 = DateTime.MinValue;
                        double montoMin = 0;

                        while (!datosCorrectos)
                        {//Se va validando cada valor que se pide antes de pedir el siguiente
                            Console.WriteLine("Ingrese la fecha inicial con el formato AAAA, MM, DD:");
                            string f1 = Console.ReadLine();
                            if (!DateTime.TryParse(f1, out fecha1)) { Console.WriteLine("La fecha no se ingresó correctamente. Ingrese la fecha inicial con el formato AAAA, MM, DD:"); }
                            else
                            {

                                Console.WriteLine("Ingrese la fecha final con el formato AAAA, MM, DD:");
                                string f2 = Console.ReadLine();
                                if (!DateTime.TryParse(f2, out fecha2))
                                {
                                    Console.WriteLine("La fecha no se ingresó correctamente. Ingrese la fecha inicial con el formato AAAA, MM, DD:");
                                }
                                else
                                {
                                    Console.WriteLine("Ingrese el monto mínimo:");
                                    string montoIngresado = Console.ReadLine();
                                    if (!Double.TryParse(montoIngresado, out montoMin) || montoMin < 0)
                                    {
                                        Console.WriteLine("El monto no se ingresó correctamente. Ingrese un monto mínimo");
                                    }
                                    else
                                    {//Si todos los datos ingresados son correctos, se llama al método y se muestra la lista de actividades
                                        datosCorrectos = true;
                                        foreach (Actividad a in s.GetActividadesEntreFechasSegunCosto(fecha1, fecha2, montoMin))
                                        {
                                            Console.WriteLine(a.ToString());
                                        }
                                    }
                                }
                            }
                        }

                        break;

                    //pedimos el nombre del proveedor y el descuento a asignar, y se lo asignamos mediante el método AsignarValorDescuento.
                    case 4:
                        Console.WriteLine("Ingrese el nombre del proveedor:");
                        string nombreProv = Console.ReadLine();
                        double descuento = 0;

                        if (nombreProv == "")
                        {
                            Console.WriteLine("El nombre del proveedor no puede estar vacío.");
                        }
                        else
                        {
                            Console.WriteLine("Ingrese el descuento:");
                            string descuentoIngresado = Console.ReadLine();
                            if (!Double.TryParse(descuentoIngresado, out descuento) && descuento >= 0)
                            {
                                Console.WriteLine("El monto no se ingresó correctamente. Ingrese un monto mínimo");
                            }

                            //s.AsignarValorDescuento(nombreProv, descuento);
                        }
                        Console.WriteLine($"El descuento ({descuento}%) ha sido ingresado para el proveedor {nombreProv}.");
                        break;

                    //Se pide los datos del Huesped para poder darlo de alta, con try catch validamos los campos ingresados y en caso de que alguno esté incorrecto o
                    //el Huesped ya esté registrado, nos devuelve la leyenda correspondiente.
                    case 5:
                        Console.WriteLine("Ingrese el mail");
                        string email = Console.ReadLine();
                        Console.WriteLine("Ingrese la contraseña");
                        string contrasenia = Console.ReadLine();
                        Console.WriteLine("Ingrese el nombre");
                        string nombre = Console.ReadLine();
                        Console.WriteLine("Ingrese el apellido");
                        string apellido = Console.ReadLine();
                        Console.WriteLine("Seleccione tipo de documento");
                        string tipoDoc = Console.ReadLine();
                        TipoDocumento tipo = s.StringATipoDocumento(tipoDoc);
                        string numDoc = "";
                        if (tipo.Equals("CI"))
                        {
                            Console.WriteLine("Ingrese el número de documento sin puntos y sin guiones");
                            numDoc = Console.ReadLine();
                        }
                        {
                            Console.WriteLine("Ingrese el número de documento");
                            numDoc = Console.ReadLine();
                        }
                        
                        Console.WriteLine("Ingrese la habitación asignada");
                        string habitacion = Console.ReadLine();
                        Console.WriteLine("Ingrese la fecha de nacimiento con el formato AAAA, MM, DD");
                        DateTime fechaNacimiento = DateTime.MinValue;
                        string fechaNac = Console.ReadLine();
                        DateTime.TryParse(fechaNac, out fechaNacimiento);
                   
                        Console.WriteLine("Ingrese el nivel asignado (0 a 4)");
                        string nivelIngresado = Console.ReadLine();
                        int nivel = 0;
                        Int32.TryParse(nivelIngresado, out nivel);

                        try
                        {
                            s.AltaUsuario(new Huesped(nombre, apellido, tipo, numDoc, habitacion, fechaNacimiento, nivel, email, contrasenia));
                            Console.WriteLine("Usuario registrado con éxito");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        break;
                        //Se muestra la lista de Huéspedes
                    case 6:
                        foreach (Huesped h in s.GetHuespedes())
                        {
                            Console.WriteLine(h.ToString());
                        }
                        break;
                    case 7:
                        Console.Clear();
                        break;

                }//switch

                Console.WriteLine(" ");
                Console.WriteLine("* * * Bienvenido al Hostal <NombreDelHostal> * * *");
                Console.WriteLine("Ingrese el número de la opción deseada:");
                Console.WriteLine("2- Ver lista de proveedores");
                Console.WriteLine("3- Ver lista de actividades dentro de un rango de fechas y con un precio mínimo.");
                Console.WriteLine("3- Ver lista de proveedores");
                Console.WriteLine("4- Ingresar promoción de proveedor");
                Console.WriteLine("5- Alta de huéspedes");
                Console.WriteLine("6- Mostrar lista de huéspedes");
                Console.WriteLine("7- Limpiar la consola");
                Console.WriteLine("0- Salir");

                opcion = Int32.Parse(Console.ReadLine());

            }//while
        }
    }
}