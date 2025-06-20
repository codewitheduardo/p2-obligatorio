using AplicacionWeb.Filters;
using Dominio;
using Dominio.Ordenes;
using Microsoft.AspNetCore.Mvc;

namespace AplicacionWeb.Controllers
{
    [AdminFilter]
    public class AdminController : Controller
    {
        private Sistema _sistema = Sistema.Instancia;

        [HttpGet]
        public IActionResult Clientes(string mensaje, string error)
        {
            ViewBag.Mensaje = mensaje;
            ViewBag.Error = error;
            List<Cliente> listaDeClientes = this._sistema.ListarClientesEnUsuarios();
            listaDeClientes.Sort();

            return View(listaDeClientes);
        }

        [HttpPost]
        public IActionResult EditarCliente(string documento, int nuevosPuntos, bool? nuevoValor)
        {
            try
            {
                Cliente cliente = this._sistema.ObtenerCliente(documento);

                if (cliente is Premium clientePremium)
                {
                    this._sistema.ModificarPuntos(clientePremium, nuevosPuntos);
                }

                if (cliente is Ocasional clienteOcasional)
                {
                    this._sistema.CambiarElegibilidad(clienteOcasional, nuevoValor);
                }
                return RedirectToAction("Clientes", new { mensaje = $"El cliente {cliente.Nombre} se ha editado con éxito." });
            }
            catch (Exception e)
            {
                return RedirectToAction("Clientes", new { error = e.Message });
            }
        }
    }
}
