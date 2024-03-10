using Dominio.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Sistema
    {
        private List<Usuario> _usuarios { get; } = new List<Usuario>();
        private List<Actividad> _actividades { get; } = new List<Actividad>();
        private List<Reserva> _reservas { get; } = new List<Reserva>();
        private List<Proveedor> _proveedores { get; } = new List<Proveedor>();


        private static Sistema _instancia;

        private Sistema()
        {
            PrecargarDatos();
        }

        //Crear la instancia
        public static Sistema GetInstancia()
        {

            if (_instancia == null)
            {
                _instancia = new Sistema();
            }
            return _instancia;

        }


        //Precargar los datos
        private void PrecargarDatos()
        {
            try
            {
                //Precarga de proveedores
                Proveedor p1 = new Proveedor("DreamWorks S.R.L.", 23048549, "Suarez 3380 Apto 304", 10);
                Proveedor p2 = new Proveedor("Estela Umpierrez S.A.", 33459678, "Lima 2456", 0);
                Proveedor p3 = new Proveedor("TravelFun", 29152020, "Misiones 1140", 9);
                Proveedor p4 = new Proveedor("Rekreation S.A.", 29162019, "Bacacay 1211", 11);
                Proveedor p5 = new Proveedor("Alonso & Umpierrez", 24051920, "18 de Julio 1956 Apto 4", 0);
                Proveedor p6 = new Proveedor("Electric Blue", 26018945, "Cooper 678", 0);
                Proveedor p7 = new Proveedor("Lúdica S.A.", 26142967, "Dublin 560", 0);
                Proveedor p8 = new Proveedor("Gimenez S.R.L.", 29001010, "Andes 1190", 7);
                Proveedor p9 = new Proveedor("DelCampo S.R.L.", 22041120, "Agraciada 2512 Apto. 1", 8);
                Proveedor p10 = new Proveedor("Norberto Molina", 22001189, "Paraguay 2100", 9);

                //Alta de proveedores
                AltaProveedor(p1);
                AltaProveedor(p2);
                AltaProveedor(p3);
                AltaProveedor(p4);
                AltaProveedor(p5);
                AltaProveedor(p6);
                AltaProveedor(p7);
                AltaProveedor(p8);
                AltaProveedor(p9);
                AltaProveedor(p10);

                //Precarga y alta de actividades organizadas por el Hostal

                Actividad a1 = new OrgHostal("Pedro Acosta", "Sierra de las Ánimas", true, "Recorriendo las sierras", "Caminata de 4 horas subiendo y bajando cerros.", new DateTime(2023, 04, 15), 20, 12, 10);
                Actividad a2 = new OrgHostal("Karolina Cabello", "Playa de los recuerdos", true, "Juegos en la playa", "Diversión para jóvenes", DateTime.Today.AddDays(2), 15, 10, 9);
                Actividad a3 = new OrgHostal("Leandro Jorge", "Salón del Hostal", false, "Truco", "Juegos de cartas", new DateTime(2023, 08, 20), 4, 9, 0);
                Actividad a4 = new OrgHostal("Pedro Acosta", "Salón del Hostal", false, "Búsqueda del Tesoro", "Resolución de acertijos", new DateTime(2023, 08, 30), 9, 15, 11);
                Actividad a5 = new OrgHostal("Leandro Jorge", "Cocina del Hostal", false, "Darnos el gusto", "Cocinaremos con la guía de un Chef invitado especial", DateTime.Today, 6, 18, 15);
                Actividad a6 = new OrgHostal("Jimena Wilson", "Zona recreativa del Hostal", true, "Meditación guiada", "Un descanso del ajetreo diario", new DateTime(2023, 03, 23), 40, 5, 6);
                Actividad a7 = new OrgHostal("Marcela Freitas", "Fabrica de Cervezas Mastra", false, "Cerveza artesanal", "Recorrida guiada de la fabrica y degutación de distintas cervezas", new DateTime(2023, 09, 15), 50, 18, 5);
                Actividad a8 = new OrgHostal("Pablo Da Costa", "Sobre la costa del río", true, "Pesca artesanal", "Recorrida y pesca sobre la costa del río con expertos sobre las mejores técnicas", new DateTime(2023, 10, 13), 20, 10, 7.5);
                Actividad a9 = new OrgHostal("Gabriela Castaño", "Spa del Hostal", false, "Día de spa", "Disfrute de un día de spa completo con masajes y tratamientos faciales", new DateTime(2023, 08, 23), 25, 15, 7.5);
                Actividad a10 = new OrgHostal("Jorge Fernández", "Jardín del Hostal", true, "Clases de baile latino", "Una experiencia para disfutar y divertirse en familia", new DateTime(2023, 11, 03), 100, 8, 2.5);

                AltaActividad(a1);
                AltaActividad(a2);
                AltaActividad(a3);
                AltaActividad(a4);
                AltaActividad(a5);
                AltaActividad(a6);
                AltaActividad(a7);
                AltaActividad(a8);
                AltaActividad(a9);
                AltaActividad(a10);

                //Precarga y alta de actividades organizadas por terceros
                Actividad a11 = new OrgTercero(p5, true, new DateTime(2023, 06, 03), "Taller de Cerámica", "Retorno a nuestras raices a través de la creación de objetos en cerámica", new DateTime(2023, 08, 15), 25, 10, 6);
                Actividad a12 = new OrgTercero(p8, true, new DateTime(2022, 06, 03), "Castillo de Piria", "Conozca un ícono de la arquitectura local.", new DateTime(2023, 08, 16), 25, 0, 4);
                Actividad a13 = new OrgTercero(p4, false, new DateTime(2023, 04, 03), "Elegí tu peli", "Proyección de la película más votada", new DateTime(2023, 08, 17), 60, 0, 7);
                Actividad a14 = new OrgTercero(p5, true, new DateTime(2023, 06, 07), "Ciclismo aventura", "Recorremos los paisajes locales con las mejores vistas", DateTime.Today, 12, 18, 10);
                Actividad a15 = new OrgTercero(p8, false, new DateTime(2023, 06, 13), "Karaoke", "La fiesta del año la animás vos.", new DateTime(2023, 08, 19), 500, 18, 9);
                Actividad a16 = new OrgTercero(p5, true, new DateTime(2021, 12, 14), "Feria de artesanías", "Una de las ferias más populares de los balnearios. Conozca a nuestros artistas locales.", new DateTime(2023, 08, 20), 25, 0, 0);
                Actividad a17 = new OrgTercero(p4, true, new DateTime(2023, 02, 06), "Pueblos de la zona", "Conozca los pintorescos pueblos y la vida de sus habitantes", new DateTime(2023, 06, 12), 25, 0, 7.5);
                Actividad a18 = new OrgTercero(p8, false, new DateTime(2023, 06, 23), "Taller literario", "Comparta con un autor local y disfrute del arte de la poesía", new DateTime(2023, 05, 22), 15, 12, 8);
                Actividad a19 = new OrgTercero(p8, true, new DateTime(2022, 11, 08), "Paseo en barco", "Disfrute de las vistas serranas desde el mar", DateTime.Today, 20, 0, 10);
                Actividad a20 = new OrgTercero(p4, true, new DateTime(2023, 05, 14), "Fiesta de Sushi", "Aprenda a hacer sushi y disfrute de una cena inolvidable", new DateTime(2023, 08, 24), 18, 12, 16);
                Actividad a21 = new OrgTercero(p4, false, new DateTime(2022, 09, 27), "Noche de disfraces", "Nosotros te disfrazamos! Solo tenés que elegir tu personaje. No te pierdas de la mejor fiesta temática.", new DateTime(2023, 08, 25), 200, 04, 11.5);
                Actividad a22 = new OrgTercero(p5, true, new DateTime(2023, 06, 30), "Star Wars Night", "Concierto de la Orquesta Sinfónica del Sodre", new DateTime(2023, 08, 26), 1000, 0, 0);
                Actividad a23 = new OrgTercero(p5, false, new DateTime(2020, 09, 23), "Tarde de pintura al óleo", "Nosotros ponemos los materiales, vos poné la creatividad!", new DateTime(2023, 08, 16), 8, 10, 10);
                Actividad a24 = new OrgTercero(p4, true, new DateTime(2023, 07, 01), "Castillos de arena", "Explore su creatividad en nuestras hermosas playas", new DateTime(2023, 08, 20), 60, 4, 3.5);
                Actividad a25 = new OrgTercero(p8, false, new DateTime(2023, 02, 18), "Clases de equitación", "Disfrute del entorno natural con nuestros excelentes profesionales.", new DateTime(2023, 08, 19), 4, 8, 25);

                AltaActividad(a11);
                AltaActividad(a12);
                AltaActividad(a13);
                AltaActividad(a14);
                AltaActividad(a15);
                AltaActividad(a16);
                AltaActividad(a17);
                AltaActividad(a18);
                AltaActividad(a19);
                AltaActividad(a20);
                AltaActividad(a21);
                AltaActividad(a22);
                AltaActividad(a23);
                AltaActividad(a24);
                AltaActividad(a25);



                //Precarga de Huéspedes
                Huesped h1 = new Huesped("Juan", "Lopéz", TipoDocumento.CI, "41465548", "565", new DateTime(1940, 04, 15), 1, "juan@gmail.com", "juan1234");
                Huesped h2 = new Huesped("María", "Pérez", TipoDocumento.Pasaporte, "X0523821F", "A123", new DateTime(1995, 11, 03), 3, "mari@hotmail.com", "nomolestar");
                Huesped h3 = new Huesped("Cecilia", "Nuñez", TipoDocumento.CI, "55679004", "999F", new DateTime(1980, 10, 19), 2, "cenu@gmail.com", "123456789");
                Huesped h4 = new Huesped("Federico", "Álvarez", TipoDocumento.Otros, "215874561", "789", new DateTime(2000, 02, 22), 4, "fede@ort.edu.uy", "federico123");
                Huesped h5 = new Huesped("Carlos", "Hernandez", TipoDocumento.Otros, "ABC1234567", "4A", new DateTime(1970, 09, 09), 2, "carlos@carlos.com", "1234qqqq");
                Huesped h6 = new Huesped("Pepito", "Mendez", TipoDocumento.Otros, "qwerty1234", "5A", new DateTime(2007, 09, 09), 2, "pepito@mail.com", "1234aaaa");



                //Precarga de Operadores
                Operador o1 = new Operador("Juan", "Perez", new DateTime(2022, 09, 05), "juan@juan.com", "juan1234");
                Operador o2 = new Operador("Gabriela", "Fagundez", new DateTime(2010, 11, 05), "gfagundez@gmail.com", "fagundez2010");
                Operador o3 = new Operador("Clara", "Mendez", new DateTime(2015, 07, 12), "cmendez@gmail.com", "mendez2015");
                Operador o4 = new Operador("Fabricio", "Lema", new DateTime(2018, 05, 17), "flema@gmail.com", "lema201805");


                //Alta de Huéspedes en lista de usuarios
                AltaUsuario(h1);
                AltaUsuario(h2);
                AltaUsuario(h3);
                AltaUsuario(h4);
                AltaUsuario(h5);
                AltaUsuario(h6);
                AltaUsuario(o1);
                AltaUsuario(o2);
                AltaUsuario(o3);
                AltaUsuario(o4);

                //Precarga de Agendas/Reservas

                AltaReserva(new Reserva(a1, h4, "PENDIENTE_PAGO", new DateTime(2023, 04, 12)));
                AltaReserva(new Reserva(a14, h1, "CONFIRMADA", new DateTime(2023, 06, 18)));
                AltaReserva(new Reserva(a3, h3, "CONFIRMADA", new DateTime(2023, 02, 05)));
                AltaReserva(new Reserva(a25, h2, "PENDIENTE_PAGO", new DateTime(2023, 05, 29)));
                AltaReserva(new Reserva(a16, h1, "CONFIRMADA", new DateTime(2023, 06, 12)));
                AltaReserva(new Reserva(a19, h2, "CONFIRMADA", new DateTime(2023, 03, 18)));
                AltaReserva(new Reserva(a4, h4, "PENDIENTE_PAGO", new DateTime(2023, 05, 15)));
                AltaReserva(new Reserva(a23, h3, "PENDIENTE_PAGO", new DateTime(2023, 04, 30)));
                AltaReserva(new Reserva(a9, h2, "CONFIRMADA", new DateTime(2023, 05, 29)));
                AltaReserva(new Reserva(a7, h1, "CONFIRMADA", new DateTime(2023, 06, 14)));
                AltaReserva(new Reserva(a5, h3, "PENDIENTE_PAGO", new DateTime(2023, 06, 17)));
                AltaReserva(new Reserva(a14, h4, "CONFIRMADA", new DateTime(2023, 05, 16)));
                AltaReserva(new Reserva(a18, h1, "CONFIRMADA", new DateTime(2023, 06, 01)));
                AltaReserva(new Reserva(a3, h1, "CONFIRMADA", new DateTime(2023, 04, 01)));
                AltaReserva(new Reserva(a3, h2, "CONFIRMADA", new DateTime(2023, 05, 02)));
                AltaReserva(new Reserva(a3, h4, "CONFIRMADA", new DateTime(2023, 06, 03)));

            }
            catch { }
        }
        //Altas y validaciones
        public void AltaUsuario(Usuario u)
        {
            try
            {
                u.Validar();
                //Se valida que no se agreguen dos usuarios iguales a la lista
                ValidarUsuarioExistente(u);
                _usuarios.Add(u);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Se valida que no se agreguen dos usuarios iguales a la lista
        private void ValidarUsuarioExistente(Usuario u)
        {
            if (_usuarios.Contains(u)) //Llama al método Equals en Huesped
            {
                throw new Exception("Este usuario ya existe, no pueden estar registrados bajo el mismo mail y/o documento.");
            }
        }

        //Métodos de Alta a las listas
        public void AltaActividad(Actividad a)
        {
            try
            {
                a.Validar();
                //Se valida que no se agreguen dos actividades iguales a la lista
                ValidarActividadExistente(a);
                _actividades.Add(a);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Se valida que no se agreguen dos actividades iguales a la lista
        private void ValidarActividadExistente(Actividad a)
        {
            if (_actividades.Contains(a)) //Llama al método Equals en Actividad
            {
                throw new Exception("Esta actividad ya existe, no pueden estar registradas bajo el mismo nombre.");
            }
        }

      

        //Este metodo precarga reservas pero elimina la validación de Fechas pasadas para cumplir con la letra del Obligatorio 2 (SOLO USADO EN LAS PRECARGAS)
        public void AltaReserva(Reserva r)
        {
            try
            {
                r.Validar();
                ValidarCupo(r);
                ValidarHuespedNoTieneEsaReserva(r);
                r.AsignarCostoFinal();
                _reservas.Add(r);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //En este metodo se valida que la actividad elegida se efectue del dia actual en adelante
        //Usado solo al crear una nueva Agenda/Reserva
        public void AltaReservaNuevaAgenda(Reserva r)
        {
            try
            {
                r.ValidarNuevaAgenda();
                ValidarCupo(r);
                ValidarHuespedNoTieneEsaReserva(r);
                r.AsignarCostoFinal();
                _reservas.Add(r);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AltaProveedor(Proveedor p)
        {
            try
            {
                p.Validar();
                //Se valida que no se agreguen dos proveedores iguales a la lista
                ValidarProveedorExistente(p);
                _proveedores.Add(p);
            }
            catch (Exception)
            {
                throw;
            }

        }

        //Se valida que no se agreguen dos proveedores iguales a la lista
        private void ValidarProveedorExistente(Proveedor p)
        {
            if (_proveedores.Contains(p)) //Llama al método Equals en Proveedor
            {
                throw new Exception("Este proveedor ya existe, no pueden estar registrados bajo el mismo nombre.");
            }
        }

        //Métodos para mostrar las listas
        public List<Usuario> GetUsuarios()
        {
            return _usuarios;
        }

        public List<Actividad> GetActividades()
        {
            return _actividades;
        }



        public List<Proveedor> GetProveedores()
        {
            return _proveedores;
        }

        public List<Reserva> GetReservas()
        {
            return _reservas;
        }

        //Para mostrar la lista de Huéspedes se recorre la lista de usuarios, nos quedamos con los que son de tipo Huesped y se guardan en la listaRet
        public List<Huesped> GetHuespedes()
        {
            List<Huesped> listaRet = new List<Huesped>();

            foreach (Usuario u in _usuarios)
            {
                if (u.GetTipo() == "Huesped")
                {
                    //Es necesario considerar el objeto Usuario como Huésped para poder guardarlo en la lista
                    Huesped h = u as Huesped;
                    listaRet.Add(h);
                }
            }
            return listaRet;
        }

        public List<Proveedor> GetProveedoresOrdenAlfabetico()
        {
            //El método Sort llama al método CompareTo que se encuentra en Proveedor
            _proveedores.Sort();
            return _proveedores;
        }

        //Metodo(dt1, dt2, costo), devuelve actividades con precio mayor al dado. Se muestra con orden descendente por costo
        public List<Actividad> GetActividadesEntreFechasSegunCosto(DateTime fecha1, DateTime fecha2, double costo)
        {
            List<Actividad> listaRet = new List<Actividad>();
            //Reviso que la fecha 1 sea anterior a la fecha 2
            if (fecha1 > fecha2)
            {
                DateTime fechaAux = fecha1;
                fecha1 = fecha2;
                fecha2 = fechaAux;
            }
            //Recorro la lista de actividades y verifico que este entre las fechas dadas y que su costo sea mayor o igual al dado.
            foreach (Actividad a in _actividades)
            {
                if (a.Fecha >= fecha1 && a.Fecha <= fecha2 && a.Costo >= costo)
                {
                    //agrego las actividades que cumplen la consigna en la lista de retorno
                    listaRet.Add(a);
                }
            }

            //El método Sort llama al método CompareTo que se encuentra en Actividad
            //Reorganizo la lista segun su costo en forma decreciente. 
            listaRet.Sort();

            return listaRet;
        }

        //Convierto el string que me da el usuario a las opciones del Enum TipoDocumento
        public TipoDocumento StringATipoDocumento(string tipoDoc)
        {
            TipoDocumento t = TipoDocumento.Otros;

            switch (tipoDoc.ToUpper())
            {
                case "CI":
                    t = TipoDocumento.CI;
                    break;
                case "PASAPORTE":
                    t = TipoDocumento.Pasaporte;
                    break;
                default:
                    t = TipoDocumento.Otros;
                    break;
            }
            return t;
        }






        //Agregados Obligatorio 2

        //Login
        public Usuario Login(string email, string contrasenia)
        {

            foreach (Usuario u in _usuarios)
            {
                if (u.Email.Equals(email) && u.Contrasenia.Equals(contrasenia))
                {
                    return u;
                }
            }
            return null;
        }

        //Metodo(fecha), devuelve actividades segun la fecha indicada
        public List<Actividad> GetActividadesPorfecha(DateTime fecha)
        {
            List<Actividad> listaRet = new List<Actividad>();

            //Recorro la lista de actividades y verifico que coincida con la fecha dada
            foreach (Actividad a in _actividades)
            {
                if (a.Fecha.Date == fecha.Date)
                {
                    //agrego las actividades que cumplen la consigna en la lista de retorno
                    listaRet.Add(a);
                }
            }

            return listaRet;
        }

        public Proveedor GetProveedor(int id)
        {
            foreach (Proveedor p in _proveedores)
            {
                if (p.Id.Equals(id))
                {
                    return p;
                }
            }
            return null;
        }

        //Tenemos el id del proveedor y el valor del descuento. Se recorre la lista de proveedores y al encontrarlo se le asigna el descuento y se sale del foreach
        public void AsignarValorDescuento(int id, double descuento)
        {

            if (descuento >= 0 && descuento <= 100)
            {
                Proveedor p = GetProveedor(id);
                if (p != null)
                {
                    p.ValorDescuento = descuento;
                }
                else
                {
                    throw new Exception("El proveedor no puede estar vacío");
                }
            }
            else
            {
                throw new Exception("Debe ingresar un valor entre 0 y 100");
            }
        }

        public Operador GetOperador(int? logId)
        {
            foreach (Usuario u in _usuarios)
            {
                if (u.Id.Equals(logId))
                {
                    Operador o = u as Operador;
                    return o;
                }
            }
            return null;
        }

        //con el id de la actividad que nos llega por parámetros, la busco dentro de la lista de actividades
        public Actividad GetActividad(int id)
        {
            foreach (Actividad a in _actividades)
            {
                if (a.Id.Equals(id))
                {
                    return a;
                }
            }
            return null;
        }
        // me quedo con el login (id) del huesped y lo busco en la lista de usuarios
        public Huesped GetHuesped(int? id)
        {
            foreach (Usuario u in _usuarios)
            {
                if (u.Id.Equals(id))
                {
                    Huesped h = u as Huesped;
                    return h;
                }
            }
            return null;
        }

        public Huesped GetHuespedDocumento(string tipoDoc, string numDoc)
        {
            TipoDocumento documento = StringATipoDocumento(tipoDoc);
            foreach (Usuario u in _usuarios)
            {
                Huesped h = u as Huesped;
                if (h.TipoDocumento.Equals(documento) && h.NumDocumento.Equals(numDoc))
                {
                    return h;
                }

            }
            return null;
        }


        public Reserva GetReserva(int id)
        {
            foreach (Reserva r in _reservas)
            {
                if (r.Id.Equals(id))
                {
                    return r;
                }
            }
            return null;
        }

        public List<Reserva> GetReservasPorHuesped(Huesped h)
        {
            List<Reserva> listaRet = new List<Reserva>();
            foreach (Reserva r in _reservas)
            {
                if (r.Huesped.Id.Equals(h.Id))
                {
                    listaRet.Add(r);
                }
            }
            listaRet.Sort();
            return listaRet;
        }

        public void ConfirmarAgenda(int id)
        {
            Reserva r = GetReserva(id);
            if (r.EstadoReserva == "PENDIENTE_PAGO")
            {
                r.EstadoReserva = "CONFIRMADA";
                r.FechaReserva = DateTime.Today;
            }
        }

        public List<Reserva> GetReservasPorFecha(DateTime fecha)
        {
            List<Reserva> listaRet = new List<Reserva>();
            foreach (Reserva r in _reservas)
            {
                if (r.FechaReserva.Equals(fecha))
                {
                    listaRet.Add(r);
                }
            }
            return listaRet;
        }

        public List<Reserva> GetReservasDeHoy()
        {
            List<Reserva> listaRet = new List<Reserva>();
            foreach (Reserva r in _reservas)
            {
                if (r.FechaReserva.Equals(DateTime.Today))
                {
                    listaRet.Add(r);
                }
            }
            return listaRet;
        }

        public int CantidadDeReservasPorActividad(int idActividad)
        {
            int contador = 0;
            foreach (Reserva r in _reservas)
            {
                if (r.Actividad.Id == idActividad)
                {
                    contador++;
                }
            }
            return contador;
        }

        public void ValidarCupo(Reserva reserva)
        {
            if (reserva == null)
            {
                throw new Exception("La reserva no puede ser nula");
            }

            int cantidadesAgendas = CantidadDeReservasPorActividad(reserva.Actividad.Id);
            if (cantidadesAgendas >= reserva.Actividad.CantMaxPersonas)
            {
                throw new Exception("No quedan cupos para esta actividad");
            }
        }

        public void ValidarHuespedNoTieneEsaReserva(Reserva reserva)
        {
            foreach (Reserva r in _reservas)
            {
                if (r.Actividad.Equals(reserva.Actividad) && r.Huesped.Equals(reserva.Huesped))
                {
                    throw new Exception("Ya existe una reserva para este huesped y actividad");
                }
            }
        }
        //Devuelve la lista de reservas en orden cronológico y alfabético ascendente
        public List<Reserva> GetReservasConSort()
        {
            List<Reserva> listaRet = new List<Reserva>();
            listaRet = GetReservas();
            listaRet.Sort();
            return listaRet;
        }



        public List<Reserva> GetReservasDelHuespedFechaPosteriorAHoy(Huesped h)
        {
            List<Reserva> listaAux = new List<Reserva>();
            foreach (Reserva r in _reservas)
            {
                if (r.Huesped.Id.Equals(h.Id))
                {   
                    if(r.Actividad.Fecha >= DateTime.Today)
                    {
                        listaAux.Add(r);
                    }
                }

            }
            listaAux.Sort();
            return listaAux;
        }
    }
}
