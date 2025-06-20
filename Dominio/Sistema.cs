using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Dominio.Ordenes;

namespace Dominio
{
    public class Sistema
    {
        private static Sistema _instancia;
        private List<Usuario> _usuarios;
        private List<Avion> _aviones;
        private List<Aeropuerto> _aeropuertos;
        private List<Ruta> _rutas;
        private List<Vuelo> _vuelos;
        private List<Pasaje> _pasajes;

        public static Sistema Instancia
        {
            get
            {
                if (Sistema._instancia == null)
                {
                    Sistema._instancia = new Sistema();
                }
                return Sistema._instancia;
            }
        }

        public List<Usuario> Usuarios
        {
            get { return _usuarios; }

        }

        public List<Avion> Aviones
        {
            get { return _aviones; }
        }

        public List<Aeropuerto> Aeropuertos
        {
            get { return _aeropuertos; }
        }

        public List<Ruta> Rutas
        {
            get { return _rutas; }
        }

        public List<Vuelo> Vuelos
        {
            get { return _vuelos; }
        }

        public List<Pasaje> Pasajes
        {
            get { return _pasajes; }
        }
        private Sistema()
        {
            this._usuarios = new List<Usuario>();
            this._aviones = new List<Avion>();
            this._aeropuertos = new List<Aeropuerto>();
            this._rutas = new List<Ruta>();
            this._vuelos = new List<Vuelo>();
            this._pasajes = new List<Pasaje>();
            PrecargarUsuarios();
            PrecargarAviones();
            PrecargarAeropuertos();
            PrecargarRutas();
            PrecargarVuelos();
            PrecargarPasajes();
        }

        //Usuarios
        public void AgregarUsuario(Usuario user) //precarga de datos usuario
        {
            user.Validar();
            this._usuarios.Add(user);
        }

        private void ExisteCliente(Cliente cliente) //valida si ya existe un cliente
        {
            if (this._usuarios.Contains(cliente))
            {
                throw new Exception("Ya existe un cliente registrado con esa cédula.");
            }
        }

        public void AltaClienteOcasional(string correo, string password, string documento, string nombre, string nacionalidad) //precarga de datos cliente ocasional
        {
            Ocasional cliente = new Ocasional(correo, password, documento, nombre, nacionalidad);
            cliente.Validar();
            this.ExisteCliente(cliente);
            this.AgregarUsuario(cliente);
        }

        public Cliente ObtenerCliente(string documento) //busca clientes específicos
        {
            Cliente retorno = null;
            int i = 0;
            while (i < this._usuarios.Count && retorno == null)
            {
                if (this._usuarios[i] is Cliente cliente && cliente.Documento == documento)
                {
                    retorno = cliente;
                }
                i++;
            }
            if (retorno == null)
            {
                throw new Exception("El cliente no fue encontrado en el sistema.");
            }

            return retorno;
        }

        public List<Cliente> ListarClientesEnUsuarios() //recorre usuarios y retorna una lista de clientes
        {
            List<Cliente> retorno = new List<Cliente>();

            foreach (Usuario usuario in this._usuarios)
            {
                if (usuario is Cliente cliente)
                {
                    retorno.Add(cliente);
                }
            }
            return retorno;
        }

        public void ModificarPuntos(Premium clientePremium, int nuevosPuntos) //modifica los puntos de un cliente premium específico
        {
            if (nuevosPuntos < 0)
            {
                throw new Exception("Los puntos no pueden ser negativos.");
            }
            clientePremium.Puntos = nuevosPuntos;
        }

        public void CambiarElegibilidad(Ocasional clienteOcasional, bool? nuevoValor) //modifica la elegibilidad de un cliente ocasinal específico
        {
            if (nuevoValor == null)
            {
                throw new Exception("Debe especificar la elegibilidad para un cliente ocasional.");
            }
            clienteOcasional.ElegibleRegalo = nuevoValor.Value;
        }

        public Usuario Login(string correo, string password) //recorre usuarios y retorna al usuario logueado
        {
            Usuario retorno = null;
            int i = 0;
            while (i < this._usuarios.Count && retorno == null)
            {
                if (this._usuarios[i].Correo == correo && this._usuarios[i].Password == password)
                {
                    retorno = this._usuarios[i];
                }
                i++;
            }
            if (retorno == null)
            {
                throw new Exception("Correo o contraseña inválidos. Verifique e intente nuevamente.");
            }
            return retorno;
        }

        //Aviones
        public void AgregarAvion(Avion avion) //precarga de datos avion
        {
            avion.Validar();
            this._aviones.Add(avion);
        }

        public Avion ObtenerAvion(string modelo) //busca aviones específicos
        {
            Avion retorno = null;
            int i = 0;
            while (i < this._aviones.Count && retorno == null)
            {
                if (this._aviones[i].Modelo == modelo)
                {
                    retorno = this._aviones[i];
                }
                i++;
            }
            return retorno;
        }

        //Aeropuertos
        public void AgregarAeropuerto(Aeropuerto aeropuerto) //precarga de datos aeropuerto
        {
            aeropuerto.Validar();
            this.ExisteAeropuerto(aeropuerto);
            this._aeropuertos.Add(aeropuerto);
        }

        public void ExisteAeropuerto(Aeropuerto aeropuerto) //valida si ya existe un aeropuerto
        {
            if (this._aeropuertos.Contains(aeropuerto))
            {
                throw new Exception("Ya existe un aeropuerto registrado con ese código IATA.");
            }
        }

        public Aeropuerto ObtenerAeropuerto(string codigo) //busca aeropuertos específicos
        {
            Aeropuerto retorno = null;
            int i = 0;
            while (i < this._aeropuertos.Count && retorno == null)
            {
                if (this._aeropuertos[i].Codigo == codigo)
                {
                    retorno = this._aeropuertos[i];
                }
                i++;
            }
            return retorno;
        }

        //Rutas
        public void AgregarRuta(Ruta ruta) //precarga de datos ruta
        {
            ruta.Validar();
            this._rutas.Add(ruta);
        }

        public Ruta ObtenerRuta(int id) //busca rutas específicas
        {
            Ruta retorno = null;
            int i = 0;
            while (i < this._rutas.Count && retorno == null)
            {
                if (this._rutas[i].Id == id)
                {
                    retorno = this._rutas[i];
                }
                i++;
            }
            return retorno;
        }

        //Vuelos
        public void AgregarVuelo(Vuelo vuelo) //precarga de datos vuelo
        {
            vuelo.Validar();
            this._vuelos.Add(vuelo);
        }

        public Vuelo ObtenerVuelo(string numero) //busca vuelos específicos
        {
            Vuelo retorno = null;
            int i = 0;
            while (i < this._vuelos.Count && retorno == null)
            {
                if (this._vuelos[i].Numero == numero)
                {
                    retorno = this._vuelos[i];
                }
                i++;
            }
            if (retorno == null)
            {
                throw new Exception("El vuelo no fue encontrado en el sistema.");
            }
            return retorno;
        }

        public List<Vuelo> ListarVuelosPorAeropuertos(string codSalida, string codLlegada) //recorre vuelos y retorna una lista de vuelos filtrada por aeropuertos especificos
        {
            List<Vuelo> retorno = new List<Vuelo>();

            foreach (Vuelo vuelo in this._vuelos)
            {
                string salida = vuelo.Ruta.Salida.Codigo.ToUpper();
                string llegada = vuelo.Ruta.Llegada.Codigo.ToUpper();

                bool ingresoSalida = !string.IsNullOrWhiteSpace(codSalida);
                bool ingresoLlegada = !string.IsNullOrWhiteSpace(codLlegada);

                if (ingresoSalida && ingresoLlegada)
                {
                    if (salida == codSalida.ToUpper() && llegada == codLlegada.ToUpper())
                    {
                        retorno.Add(vuelo);
                    }
                }
                else if (ingresoSalida)
                {
                    if (salida == codSalida.ToUpper() || llegada == codSalida.ToUpper())
                    {
                        retorno.Add(vuelo);
                    }
                }
                else if (ingresoLlegada)
                {
                    if (salida == codLlegada.ToUpper() || llegada == codLlegada.ToUpper())
                    {
                        retorno.Add(vuelo);
                    }
                }
            }
            return retorno;
        }

        //Pasajes
        public void AgregarPasaje(Pasaje pasaje) //precarga de datos pasaje
        {
            this._pasajes.Add(pasaje);
        }

        public void EmitirPasaje(Vuelo vuelo, DateTime fecha, Cliente cliente, Equipaje equipaje)
        {
            Pasaje pasaje = new Pasaje(vuelo, fecha, cliente, equipaje);
            pasaje.Validar();
            AgregarPasaje(pasaje);
        }

        public List<Pasaje> ListarPasajesPorFechas(DateTime fecha1, DateTime fecha2) //recorre pasajes y retorna una lista de pasajes dadas dos fechas específicas
        {
            List<Pasaje> retorno = new List<Pasaje>();

            foreach (Pasaje pasaje in this._pasajes)
            {
                if (pasaje.Fecha >= fecha1 && pasaje.Fecha <= fecha2 || pasaje.Fecha <= fecha1 && pasaje.Fecha >= fecha2)
                {
                    retorno.Add(pasaje);
                }
            }
            return retorno;
        }

        public void OrdenarPasajesPorPrecio() //ordena la lista de pasajes por precio de forma descendente
        {
            this._pasajes.Sort();
        }

        public void OrdenarPasajesPorFecha() //ordena la lista de pasajes por fecha
        {
            this._pasajes.Sort(new OrdenPasajePorFecha());
        }

        public List<Pasaje> ObtenerPasajesPorCliente(string documento) //busca pasajes específicos
        {
            List<Pasaje> retorno = new List<Pasaje>();

            foreach (Pasaje pasaje in this._pasajes)
            {
                if (pasaje.Pasajero.Documento == documento)
                {
                    retorno.Add(pasaje);
                }
            }
            return retorno;
        }

        //Precargas
        private void PrecargarUsuarios()
        {
            // administradores
            Administrador administrador1 = new Administrador("cvargas@mail.com", "cvargas123", "Carlos");
            AgregarUsuario(administrador1);

            Administrador administrador2 = new Administrador("luis.gomez@mail.com", "LuisPass2023", "Luis");
            AgregarUsuario(administrador2);

            // clientes premium
            Cliente premium1 = new Premium("ana.garcia@mail.com", "AnaSecurePass2023", "55598765", "Ana Garcia", "Madrid, España");
            AgregarUsuario(premium1);

            Cliente premium2 = new Premium("jorge.martinez@mail.com", "JorgePass456", "55512345", "Jorge Martinez", "Buenos Aires, Argentina");
            AgregarUsuario(premium2);

            Cliente premium3 = new Premium("martin.lopez@mail.com", "Martini2024", "55545678", "Martin Lopez", "Lima, Perú");
            AgregarUsuario(premium3);

            Cliente premium4 = new Premium("luisa.rodriguez@mail.com", "LuisaR2023", "55533444", "Luisa Rodriguez", "Santiago, Chile");
            AgregarUsuario(premium4);

            Cliente premium5 = new Premium("juan.perez@mail.com", "JuanPerez123", "55587654", "Juan Perez", "Asunción, Paraguay");
            AgregarUsuario(premium5);

            // clientes ocasionales
            Cliente ocasional1 = new Ocasional("camila.rivera@mail.com", "Camila456", "55522331", "Camila Rivera", "Quito, Ecuador");
            AgregarUsuario(ocasional1);

            Cliente ocasional2 = new Ocasional("facundo.m@mail.com", "FacuPass2024", "55533442", "Facundo Margat", "Bogotá, Colombia");
            AgregarUsuario(ocasional2);

            Cliente ocasional3 = new Ocasional("sofia.diaz@mail.com", "SofiD123", "55577889", "Sofia Diaz", "Caracas, Venezuela");
            AgregarUsuario(ocasional3);

            Cliente ocasional4 = new Ocasional("bruno.torres@mail.com", "Bruno321", "55566543", "Bruno Torres", "Ciudad de México, México");
            AgregarUsuario(ocasional4);

            Cliente ocasional5 = new Ocasional("valentina.perez@mail.com", "ValePass789", "55599887", "Valentina Perez", "Lima, Perú");
            AgregarUsuario(ocasional5);
        }
        private void PrecargarAviones()
        {
            Avion a1 = new Avion("Boeing", "787-9", 296, 14100, 25);
            AgregarAvion(a1);

            Avion a2 = new Avion("Airbus", "A320neo", 180, 4500, 16);
            AgregarAvion(a2);

            Avion a3 = new Avion("Embraer", "E195-E2", 140, 3500, 13);
            AgregarAvion(a3);

            Avion a4 = new Avion("Bombardier", "CRJ900", 100, 3000, 11);
            AgregarAvion(a4);
        }

        private void PrecargarAeropuertos()
        {
            Aeropuerto a1 = new Aeropuerto("EZE", "Buenos Aires", 850.50m, 320.75m);
            AgregarAeropuerto(a1);

            Aeropuerto a2 = new Aeropuerto("JFK", "Nueva York", 920.00m, 400.00m);
            AgregarAeropuerto(a2);

            Aeropuerto a3 = new Aeropuerto("LAX", "Los Ángeles", 700.00m, 280.00m);
            AgregarAeropuerto(a3);

            Aeropuerto a4 = new Aeropuerto("CDG", "París", 810.25m, 315.60m);
            AgregarAeropuerto(a4);

            Aeropuerto a5 = new Aeropuerto("NRT", "Tokio", 950.00m, 420.00m);
            AgregarAeropuerto(a5);

            Aeropuerto a6 = new Aeropuerto("GRU", "São Paulo", 675.40m, 290.90m);
            AgregarAeropuerto(a6);

            Aeropuerto a7 = new Aeropuerto("MAD", "Madrid", 880.00m, 355.00m);
            AgregarAeropuerto(a7);

            Aeropuerto a8 = new Aeropuerto("FRA", "Fráncfort", 935.80m, 390.70m);
            AgregarAeropuerto(a8);

            Aeropuerto a9 = new Aeropuerto("BCN", "Barcelona", 600.00m, 270.00m);
            AgregarAeropuerto(a9);

            Aeropuerto a10 = new Aeropuerto("MEX", "Ciudad de México", 710.00m, 310.00m);
            AgregarAeropuerto(a10);

            Aeropuerto a11 = new Aeropuerto("SCL", "Santiago", 645.00m, 295.50m);
            AgregarAeropuerto(a11);

            Aeropuerto a12 = new Aeropuerto("LIM", "Lima", 725.00m, 275.00m);
            AgregarAeropuerto(a12);

            Aeropuerto a13 = new Aeropuerto("BOG", "Bogotá", 700.00m, 260.00m);
            AgregarAeropuerto(a13);

            Aeropuerto a14 = new Aeropuerto("MIA", "Miami", 850.00m, 310.00m);
            AgregarAeropuerto(a14);

            Aeropuerto a15 = new Aeropuerto("YYZ", "Toronto", 780.00m, 330.00m);
            AgregarAeropuerto(a15);

            Aeropuerto a16 = new Aeropuerto("LHR", "Londres", 990.00m, 450.00m);
            AgregarAeropuerto(a16);

            Aeropuerto a17 = new Aeropuerto("AMS", "Ámsterdam", 860.00m, 300.00m);
            AgregarAeropuerto(a17);

            Aeropuerto a18 = new Aeropuerto("SYD", "Sídney", 940.00m, 410.00m);
            AgregarAeropuerto(a18);

            Aeropuerto a19 = new Aeropuerto("DOH", "Doha", 890.00m, 395.00m);
            AgregarAeropuerto(a19);

            Aeropuerto a20 = new Aeropuerto("JNB", "Johannesburgo", 725.00m, 280.00m);
            AgregarAeropuerto(a20);
        }
        private void PrecargarRutas()
        {
            Ruta r1 = new Ruta(ObtenerAeropuerto("EZE"), ObtenerAeropuerto("JFK"), 8530.75m);
            AgregarRuta(r1);

            Ruta r2 = new Ruta(ObtenerAeropuerto("LAX"), ObtenerAeropuerto("CDG"), 9120.00m);
            AgregarRuta(r2);

            Ruta r3 = new Ruta(ObtenerAeropuerto("NRT"), ObtenerAeropuerto("SYD"), 7820.50m);
            AgregarRuta(r3);

            Ruta r4 = new Ruta(ObtenerAeropuerto("GRU"), ObtenerAeropuerto("MEX"), 7400.00m);
            AgregarRuta(r4);

            Ruta r5 = new Ruta(ObtenerAeropuerto("MAD"), ObtenerAeropuerto("FRA"), 1420.35m);
            AgregarRuta(r5);

            Ruta r6 = new Ruta(ObtenerAeropuerto("BCN"), ObtenerAeropuerto("LHR"), 1150.20m);
            AgregarRuta(r6);

            Ruta r7 = new Ruta(ObtenerAeropuerto("SCL"), ObtenerAeropuerto("LIM"), 2450.00m);
            AgregarRuta(r7);

            Ruta r8 = new Ruta(ObtenerAeropuerto("BOG"), ObtenerAeropuerto("MIA"), 3400.00m);
            AgregarRuta(r8);

            Ruta r9 = new Ruta(ObtenerAeropuerto("YYZ"), ObtenerAeropuerto("AMS"), 6100.45m);
            AgregarRuta(r9);

            Ruta r10 = new Ruta(ObtenerAeropuerto("DOH"), ObtenerAeropuerto("JNB"), 6200.00m);
            AgregarRuta(r10);

            Ruta r11 = new Ruta(ObtenerAeropuerto("JFK"), ObtenerAeropuerto("LAX"), 3980.75m);
            AgregarRuta(r11);

            Ruta r12 = new Ruta(ObtenerAeropuerto("EZE"), ObtenerAeropuerto("SCL"), 1440.00m);
            AgregarRuta(r12);

            Ruta r13 = new Ruta(ObtenerAeropuerto("CDG"), ObtenerAeropuerto("MAD"), 1250.00m);
            AgregarRuta(r13);

            Ruta r14 = new Ruta(ObtenerAeropuerto("AMS"), ObtenerAeropuerto("LHR"), 490.25m);
            AgregarRuta(r14);

            Ruta r15 = new Ruta(ObtenerAeropuerto("NRT"), ObtenerAeropuerto("DOH"), 9600.80m);
            AgregarRuta(r15);

            Ruta r16 = new Ruta(ObtenerAeropuerto("SYD"), ObtenerAeropuerto("JNB"), 11000.00m);
            AgregarRuta(r16);

            Ruta r17 = new Ruta(ObtenerAeropuerto("GRU"), ObtenerAeropuerto("BOG"), 4350.30m);
            AgregarRuta(r17);

            Ruta r18 = new Ruta(ObtenerAeropuerto("LIM"), ObtenerAeropuerto("MEX"), 3200.00m);
            AgregarRuta(r18);

            Ruta r19 = new Ruta(ObtenerAeropuerto("BCN"), ObtenerAeropuerto("FRA"), 1350.00m);
            AgregarRuta(r19);

            Ruta r20 = new Ruta(ObtenerAeropuerto("MIA"), ObtenerAeropuerto("YYZ"), 2200.45m);
            AgregarRuta(r20);

            Ruta r21 = new Ruta(ObtenerAeropuerto("EZE"), ObtenerAeropuerto("LHR"), 11200.00m);
            AgregarRuta(r21);

            Ruta r22 = new Ruta(ObtenerAeropuerto("CDG"), ObtenerAeropuerto("AMS"), 430.60m);
            AgregarRuta(r22);

            Ruta r23 = new Ruta(ObtenerAeropuerto("JFK"), ObtenerAeropuerto("DOH"), 10800.75m);
            AgregarRuta(r23);

            Ruta r24 = new Ruta(ObtenerAeropuerto("LAX"), ObtenerAeropuerto("SYD"), 12050.90m);
            AgregarRuta(r24);

            Ruta r25 = new Ruta(ObtenerAeropuerto("SCL"), ObtenerAeropuerto("GRU"), 1620.00m);
            AgregarRuta(r25);

            Ruta r26 = new Ruta(ObtenerAeropuerto("NRT"), ObtenerAeropuerto("LAX"), 8780.25m);
            AgregarRuta(r26);

            Ruta r27 = new Ruta(ObtenerAeropuerto("MEX"), ObtenerAeropuerto("JFK"), 3600.00m);
            AgregarRuta(r27);

            Ruta r28 = new Ruta(ObtenerAeropuerto("FRA"), ObtenerAeropuerto("LHR"), 1030.00m);
            AgregarRuta(r28);

            Ruta r29 = new Ruta(ObtenerAeropuerto("MAD"), ObtenerAeropuerto("BCN"), 620.10m);
            AgregarRuta(r29);

            Ruta r30 = new Ruta(ObtenerAeropuerto("BOG"), ObtenerAeropuerto("LIM"), 2650.00m);
            AgregarRuta(r30);
        }
        private void PrecargarVuelos()
        {
            Vuelo v1 = new Vuelo("AR1234", ObtenerRuta(1), ObtenerAvion("787-9"), new List<Dia> { Dia.LUNES, Dia.JUEVES });
            AgregarVuelo(v1);

            Vuelo v2 = new Vuelo("AF567", ObtenerRuta(2), ObtenerAvion("787-9"), new List<Dia> { Dia.MARTES, Dia.VIERNES });
            AgregarVuelo(v2);

            Vuelo v3 = new Vuelo("JL890", ObtenerRuta(3), ObtenerAvion("787-9"), new List<Dia> { Dia.SABADO });
            AgregarVuelo(v3);

            Vuelo v4 = new Vuelo("LA221", ObtenerRuta(4), ObtenerAvion("787-9"), new List<Dia> { Dia.DOMINGO });
            AgregarVuelo(v4);

            Vuelo v5 = new Vuelo("IB100", ObtenerRuta(5), ObtenerAvion("A320neo"), new List<Dia> { Dia.MIERCOLES });
            AgregarVuelo(v5);

            Vuelo v6 = new Vuelo("VY345", ObtenerRuta(6), ObtenerAvion("A320neo"), new List<Dia> { Dia.LUNES });
            AgregarVuelo(v6);

            Vuelo v7 = new Vuelo("LA555", ObtenerRuta(7), ObtenerAvion("A320neo"), new List<Dia> { Dia.JUEVES, Dia.DOMINGO });
            AgregarVuelo(v7);

            Vuelo v8 = new Vuelo("AV789", ObtenerRuta(8), ObtenerAvion("E195-E2"), new List<Dia> { Dia.MARTES });
            AgregarVuelo(v8);

            Vuelo v9 = new Vuelo("AC666", ObtenerRuta(9), ObtenerAvion("787-9"), new List<Dia> { Dia.VIERNES });
            AgregarVuelo(v9);

            Vuelo v10 = new Vuelo("QR999", ObtenerRuta(10), ObtenerAvion("787-9"), new List<Dia> { Dia.MIERCOLES });
            AgregarVuelo(v10);

            Vuelo v11 = new Vuelo("DL432", ObtenerRuta(11), ObtenerAvion("A320neo"), new List<Dia> { Dia.LUNES, Dia.MIERCOLES });
            AgregarVuelo(v11);

            Vuelo v12 = new Vuelo("AR110", ObtenerRuta(12), ObtenerAvion("A320neo"), new List<Dia> { Dia.SABADO });
            AgregarVuelo(v12);

            Vuelo v13 = new Vuelo("AF220", ObtenerRuta(13), ObtenerAvion("A320neo"), new List<Dia> { Dia.JUEVES });
            AgregarVuelo(v13);

            Vuelo v14 = new Vuelo("KL340", ObtenerRuta(14), ObtenerAvion("CRJ900"), new List<Dia> { Dia.DOMINGO });
            AgregarVuelo(v14);

            Vuelo v15 = new Vuelo("JL990", ObtenerRuta(15), ObtenerAvion("787-9"), new List<Dia> { Dia.MARTES });
            AgregarVuelo(v15);

            Vuelo v16 = new Vuelo("QF450", ObtenerRuta(16), ObtenerAvion("787-9"), new List<Dia> { Dia.VIERNES });
            AgregarVuelo(v16);

            Vuelo v17 = new Vuelo("LA777", ObtenerRuta(17), ObtenerAvion("A320neo"), new List<Dia> { Dia.MIERCOLES });
            AgregarVuelo(v17);

            Vuelo v18 = new Vuelo("LA222", ObtenerRuta(18), ObtenerAvion("E195-E2"), new List<Dia> { Dia.LUNES });
            AgregarVuelo(v18);

            Vuelo v19 = new Vuelo("LH888", ObtenerRuta(19), ObtenerAvion("A320neo"), new List<Dia> { Dia.JUEVES });
            AgregarVuelo(v19);

            Vuelo v20 = new Vuelo("AA333", ObtenerRuta(20), ObtenerAvion("CRJ900"), new List<Dia> { Dia.DOMINGO });
            AgregarVuelo(v20);

            Vuelo v21 = new Vuelo("BA111", ObtenerRuta(21), ObtenerAvion("787-9"), new List<Dia> { Dia.SABADO });
            AgregarVuelo(v21);

            Vuelo v22 = new Vuelo("AF444", ObtenerRuta(22), ObtenerAvion("CRJ900"), new List<Dia> { Dia.MARTES });
            AgregarVuelo(v22);

            Vuelo v23 = new Vuelo("QR123", ObtenerRuta(23), ObtenerAvion("787-9"), new List<Dia> { Dia.JUEVES });
            AgregarVuelo(v23);

            Vuelo v24 = new Vuelo("QF987", ObtenerRuta(24), ObtenerAvion("787-9"), new List<Dia> { Dia.LUNES });
            AgregarVuelo(v24);

            Vuelo v25 = new Vuelo("LA620", ObtenerRuta(25), ObtenerAvion("E195-E2"), new List<Dia> { Dia.VIERNES });
            AgregarVuelo(v25);

            Vuelo v26 = new Vuelo("NH431", ObtenerRuta(26), ObtenerAvion("787-9"), new List<Dia> { Dia.SABADO });
            AgregarVuelo(v26);

            Vuelo v27 = new Vuelo("AM456", ObtenerRuta(27), ObtenerAvion("A320neo"), new List<Dia> { Dia.MIERCOLES });
            AgregarVuelo(v27);

            Vuelo v28 = new Vuelo("LH102", ObtenerRuta(28), ObtenerAvion("CRJ900"), new List<Dia> { Dia.DOMINGO });
            AgregarVuelo(v28);

            Vuelo v29 = new Vuelo("IB321", ObtenerRuta(29), ObtenerAvion("CRJ900"), new List<Dia> { Dia.LUNES });
            AgregarVuelo(v29);

            Vuelo v30 = new Vuelo("AV202", ObtenerRuta(30), ObtenerAvion("E195-E2"), new List<Dia> { Dia.JUEVES });
            AgregarVuelo(v30);
        }
        private void PrecargarPasajes()
        {
            Pasaje p1 = new Pasaje(ObtenerVuelo("AR1234"), new DateTime(2025, 4, 21), ObtenerCliente("55598765"), Equipaje.BODEGA);
            AgregarPasaje(p1);

            Pasaje p2 = new Pasaje(ObtenerVuelo("AF567"), new DateTime(2025, 4, 22), ObtenerCliente("55522331"), Equipaje.CABINA);
            AgregarPasaje(p2);

            Pasaje p3 = new Pasaje(ObtenerVuelo("JL890"), new DateTime(2025, 4, 26), ObtenerCliente("55533444"), Equipaje.LIGHT);
            AgregarPasaje(p3);

            Pasaje p4 = new Pasaje(ObtenerVuelo("LA221"), new DateTime(2025, 4, 27), ObtenerCliente("55599887"), Equipaje.BODEGA);
            AgregarPasaje(p4);

            Pasaje p5 = new Pasaje(ObtenerVuelo("IB100"), new DateTime(2025, 4, 23), ObtenerCliente("55512345"), Equipaje.CABINA);
            AgregarPasaje(p5);

            Pasaje p6 = new Pasaje(ObtenerVuelo("VY345"), new DateTime(2025, 4, 21), ObtenerCliente("55533442"), Equipaje.LIGHT);
            AgregarPasaje(p6);

            Pasaje p7 = new Pasaje(ObtenerVuelo("LA555"), new DateTime(2025, 4, 24), ObtenerCliente("55545678"), Equipaje.BODEGA);
            AgregarPasaje(p7);

            Pasaje p8 = new Pasaje(ObtenerVuelo("AV789"), new DateTime(2025, 4, 22), ObtenerCliente("55566543"), Equipaje.LIGHT);
            AgregarPasaje(p8);

            Pasaje p9 = new Pasaje(ObtenerVuelo("AC666"), new DateTime(2025, 4, 25), ObtenerCliente("55587654"), Equipaje.CABINA);
            AgregarPasaje(p9);

            Pasaje p10 = new Pasaje(ObtenerVuelo("QR999"), new DateTime(2025, 4, 23), ObtenerCliente("55598765"), Equipaje.LIGHT);
            AgregarPasaje(p10);

            Pasaje p11 = new Pasaje(ObtenerVuelo("DL432"), new DateTime(2025, 4, 21), ObtenerCliente("55522331"), Equipaje.BODEGA);
            AgregarPasaje(p11);

            Pasaje p12 = new Pasaje(ObtenerVuelo("AR110"), new DateTime(2025, 4, 26), ObtenerCliente("55533444"), Equipaje.CABINA);
            AgregarPasaje(p12);

            Pasaje p13 = new Pasaje(ObtenerVuelo("AF220"), new DateTime(2025, 4, 24), ObtenerCliente("55577889"), Equipaje.LIGHT);
            AgregarPasaje(p13);

            Pasaje p14 = new Pasaje(ObtenerVuelo("KL340"), new DateTime(2025, 4, 27), ObtenerCliente("55533442"), Equipaje.CABINA);
            AgregarPasaje(p14);

            Pasaje p15 = new Pasaje(ObtenerVuelo("JL990"), new DateTime(2025, 4, 22), ObtenerCliente("55512345"), Equipaje.BODEGA);
            AgregarPasaje(p15);

            Pasaje p16 = new Pasaje(ObtenerVuelo("QF450"), new DateTime(2025, 4, 25), ObtenerCliente("55599887"), Equipaje.LIGHT);
            AgregarPasaje(p16);

            Pasaje p17 = new Pasaje(ObtenerVuelo("LA777"), new DateTime(2025, 4, 23), ObtenerCliente("55545678"), Equipaje.CABINA);
            AgregarPasaje(p17);

            Pasaje p18 = new Pasaje(ObtenerVuelo("LA222"), new DateTime(2025, 4, 21), ObtenerCliente("55587654"), Equipaje.LIGHT);
            AgregarPasaje(p18);

            Pasaje p19 = new Pasaje(ObtenerVuelo("LH888"), new DateTime(2025, 4, 24), ObtenerCliente("55533442"), Equipaje.BODEGA);
            AgregarPasaje(p19);

            Pasaje p20 = new Pasaje(ObtenerVuelo("AA333"), new DateTime(2025, 4, 27), ObtenerCliente("55522331"), Equipaje.LIGHT);
            AgregarPasaje(p20);

            Pasaje p21 = new Pasaje(ObtenerVuelo("BA111"), new DateTime(2025, 4, 26), ObtenerCliente("55533444"), Equipaje.CABINA);
            AgregarPasaje(p21);

            Pasaje p22 = new Pasaje(ObtenerVuelo("AF444"), new DateTime(2025, 4, 22), ObtenerCliente("55577889"), Equipaje.BODEGA);
            AgregarPasaje(p22);

            Pasaje p23 = new Pasaje(ObtenerVuelo("QR123"), new DateTime(2025, 4, 24), ObtenerCliente("55566543"), Equipaje.LIGHT);
            AgregarPasaje(p23);

            Pasaje p24 = new Pasaje(ObtenerVuelo("QF987"), new DateTime(2025, 4, 21), ObtenerCliente("55545678"), Equipaje.CABINA);
            AgregarPasaje(p24);

            Pasaje p25 = new Pasaje(ObtenerVuelo("AV202"), new DateTime(2025, 4, 24), ObtenerCliente("55533442"), Equipaje.LIGHT);
            AgregarPasaje(p25);
        }
    }
}
