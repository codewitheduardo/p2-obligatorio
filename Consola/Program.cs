using System.Runtime.CompilerServices;
using Dominio;

namespace Consola
{
    internal class Program
    {
        private static Sistema sistema;
        static void Main(string[] args)
        {
            sistema = Sistema.Instancia;

            int seleccion = -1;
            while (seleccion != 0)
            {
                seleccion = SolicitarInt("Aero Word\n> Seleccione la opción:\n 1 - Listar todos los clientes" +
                    "\n 2 - Listar vuelos por aeropuerto" +
                    "\n 3 - Alta de cliente ocasional" +
                    "\n 4 - Listar pasajes entre fechas" +
                "\n(0 para salir)", true, 4, 0);
                switch (seleccion)
                {
                    case 4:
                        MostrarPasajes();
                        break;
                    case 3:
                        AltaClienteOcasional();
                        break;
                    case 2:
                        MostrarVuelos();
                        break;
                    case 1:
                        MostrarClientes();
                        break;
                    case 0:
                        Console.WriteLine("Proceso finalizado. Que tengas un buen día.");
                        break;
                    default:
                        Console.WriteLine("Selección incorrecta");
                        break;
                }
            }
        }

        static int SolicitarInt(string mensaje, bool conRango, int maximo, int minimo)
        {
            int retorno = -1;
            bool seleccionCorrecta = false;
            while (!seleccionCorrecta)
            {
                try
                {
                    Console.WriteLine(mensaje);
                    if (conRango)
                    {
                        Console.WriteLine("El máximo permitido es " + maximo
                            + " y el mínimo es " + minimo);
                    }
                    string seleccionString = Console.ReadLine();
                    retorno = int.Parse(seleccionString);
                    if (conRango && (retorno < minimo || retorno > maximo))
                    {
                        Console.WriteLine("Número incorrecto");
                    }
                    else
                    {
                        seleccionCorrecta = true;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Solo se aceptan números");
                }
            }
            return retorno;
        }

        static void MostrarClientes() //recorre una lista de Clientes y los muestra
        {
            Console.Clear();
            try
            {
                List<Cliente> ListaDeClientes = sistema.ListarClientesEnUsuarios();

                if (ListaDeClientes.Count != 0 || ListaDeClientes != null)
                {
                    foreach (Cliente cliente in ListaDeClientes)
                    {
                        Console.WriteLine(cliente);
                    }
                }
                else
                {
                    Console.WriteLine("No hay clientes registrados en el sistema");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("-> No es posible realizar la operación\n" + e.Message);
            }
        }

        static void MostrarVuelos() //verifica si los datos ingresados con correctos, luego recorre un método de Sistema que devuelve la lista de vuelos y los muestra
        {
            Console.Clear();
            try
            {
                Console.WriteLine("> Ingrese el código (IATA) de aeropuerto:");
                string codigo = Console.ReadLine().ToUpper();

                if (!string.IsNullOrEmpty(codigo))
                {
                    if (sistema.ObtenerAeropuerto(codigo) != null)
                    {
                        List<Vuelo> ListaDeVuelos = sistema.ListarVuelosPorAeropuerto(codigo);

                        if (ListaDeVuelos.Count != 0 || ListaDeVuelos != null)
                        {
                            foreach (Vuelo vuelo in ListaDeVuelos)
                            {
                                Console.WriteLine(vuelo);
                            }
                        }
                        else
                        {
                            Console.WriteLine("El aeropuerto no está adherido a ningún vuelo");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No se encontró un aeropuerto con ese código");
                    }
                }
                else
                {
                    Console.WriteLine("Debe ingresar un código de aeropuerto válido");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("-> No es posible realizar la operación\n" + e.Message);
            }
        }

        static void AltaClienteOcasional() //verifica si los datos ingresados son correctos, luego los guarda en una lista por método de sistema
        {
            Console.Clear();
            try
            {
                Console.WriteLine("> Ingrese nombre:");
                string nombre = Console.ReadLine();

                Console.WriteLine("> Ingrese correo electrónico:");
                string correo = Console.ReadLine();

                Console.WriteLine("> Ingrese contraseña:");
                string password = Console.ReadLine();

                Console.WriteLine("> Ingrese número del documento de identidad:");
                string documento = Console.ReadLine();

                Console.WriteLine("> Ingrese nacionalidad:");
                string nacionalidad = Console.ReadLine();

                sistema.AltaClienteOcasional(correo, password, documento, nombre, nacionalidad);
                Console.WriteLine("El cliente fue ingresado correctamente");
            }
            catch (Exception e)
            {
                {
                    Console.WriteLine("-> No es posible realizar la operación\n" + e.Message);
                }
            }
        }

        static DateTime SolicitarDateTime(string mensaje)
        {
            bool esCorrecto = false;
            DateTime retorno = DateTime.Now;
            while (!esCorrecto)
            {
                try
                {
                    Console.WriteLine(mensaje);
                    retorno = DateTime.Parse(Console.ReadLine());
                    esCorrecto = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Formato de fecha incorrecto. Prueba dd/mm/yyyy");
                }
            }
            return retorno;
        }

        static void MostrarPasajes() //verifica si los datos ingresados son correctos, luego recorre un método de Sistema que devuelve la lista de pasajes y los muestra
        {
            Console.Clear();
            try
            {
                DateTime fecha1 = SolicitarDateTime("> Ingrese una fecha:");
                DateTime fecha2 = SolicitarDateTime("> Ingrese una fecha:");

                List<Pasaje> ListaDePasajes = sistema.ListarPasajesPorFechas(fecha1, fecha2);
                bool encontrado = false;

                if (ListaDePasajes.Count != 0 || ListaDePasajes != null)
                {
                    foreach (Pasaje pasaje in ListaDePasajes)
                    {
                        Console.WriteLine(pasaje);
                    }
                }
                else
                {
                    Console.WriteLine("No se encontraron pasajes para las fechas ingresadas");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("-> No es posible realizar la operación\n" + e.Message);
            }
        }
    }
}
