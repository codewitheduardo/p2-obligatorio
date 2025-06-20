using AplicacionWeb.Filters;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace AplicacionWeb.Controllers
{
    [Authentication]
    public class PasajeController : Controller
    {
        private Sistema _sistema = Sistema.Instancia;

        [HttpPost]
        public IActionResult Comprar(string id, DateTime fecha, Equipaje? equipaje)
        {
            try
            {
                if (equipaje == null)
                {
                    throw new Exception("Debe de seleccionar un tipo de equipaje para realizar la compra");
                }
                Vuelo vuelo = this._sistema.ObtenerVuelo(id);
                Cliente cliente = this._sistema.ObtenerCliente(HttpContext.Session.GetString("documento"));
                this._sistema.EmitirPasaje(vuelo, fecha, cliente, equipaje.Value);

                return RedirectToAction("Index", "Vuelo", new { mensaje = $"Pasaje reservado para el vuelo {vuelo.Numero} el {fecha.ToString("dd MMM yyyy")}" });
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Vuelo", new { error = e.Message });
            }
        }

        [HttpGet]
        public IActionResult MisPasajes()
        {
            this._sistema.OrdenarPasajesPorPrecio();
            Cliente cliente = this._sistema.ObtenerCliente(HttpContext.Session.GetString("documento"));

            return View(this._sistema.ObtenerPasajesPorCliente(cliente.Documento));
        }

        [AdminFilter]
        [HttpGet]
        public IActionResult Todos()
        {
            this._sistema.OrdenarPasajesPorFecha();

            return View(this._sistema.Pasajes);
        }
    }
}
