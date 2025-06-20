using AplicacionWeb.Filters;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace AplicacionWeb.Controllers
{
    [Authentication]
    public class VueloController : Controller
    {
        private Sistema _sistema = Sistema.Instancia;

        [HttpGet]
        public IActionResult Index(string mensaje, string error)
        {
            ViewBag.Mensaje = mensaje;
            ViewBag.Error = error;
            ViewBag.Aeropuertos = this._sistema.Aeropuertos;
            return View(this._sistema.Vuelos);
        }

        [HttpGet]
        public IActionResult Buscar(string codSalida, string codLlegada)
        {
            if (string.IsNullOrWhiteSpace(codSalida) && string.IsNullOrWhiteSpace(codLlegada))
            {
                return RedirectToAction("Index", new { error = "Debe ingresar al menos un código de aeropuerto para buscar." });
            }

            List<Vuelo> vuelosFiltrados = this._sistema.ListarVuelosPorAeropuertos(codSalida, codLlegada);

            if (vuelosFiltrados.Count == 0)
            {
                return RedirectToAction("Index", new { error = "No se encontraron vuelos con los datos ingresados." });
            }

            ViewBag.Aeropuertos = this._sistema.Aeropuertos;
            return View("Index", vuelosFiltrados);
        }

        [HttpGet]
        public IActionResult Detalle(string id)
        {
            Vuelo vuelo = this._sistema.ObtenerVuelo(id);

            return View(vuelo);
        }
    }
}
