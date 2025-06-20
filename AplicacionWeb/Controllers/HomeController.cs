using AplicacionWeb.Filters;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace AplicacionWeb.Controllers
{
    public class HomeController : Controller
    {
        private Sistema _sistema = Sistema.Instancia;

        [HttpGet]
        public IActionResult Index()
        {
            string mensaje = Request.Query["mensaje"];
            string error = Request.Query["error"];

            ViewBag.Mensaje = mensaje;
            ViewBag.Error = error;
            return View();
        }

        [HttpPost]
        public IActionResult Index(string correo, string password)
        {
            try
            {
                Usuario logueado = this._sistema.Login(correo, password);
                HttpContext.Session.SetString("correo", correo);
                HttpContext.Session.SetString("rol", logueado.GetType().Name);

                if (logueado is Cliente cliente)
                {
                    HttpContext.Session.SetString("documento", cliente.Documento);
                    HttpContext.Session.SetString("nombre", cliente.Nombre);

                    if (cliente is Premium clientePremium)
                    {
                        HttpContext.Session.SetInt32("puntos", clientePremium.Puntos);
                    }
                }
                if (logueado is Administrador administrador)
                {
                    HttpContext.Session.SetString("apodo", administrador.Apodo);
                }
                return RedirectToAction("Inicio");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", new { error = e.Message });
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [Authentication]
        [HttpGet]
        public IActionResult Inicio(string error)
        {
            ViewBag.Error = error;
            ViewBag.Vuelos = this._sistema.Vuelos.Count();
            return View();
        }
    }
}
