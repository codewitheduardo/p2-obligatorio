using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace AplicacionWeb.Filters
{
    public class AdminFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            string logueado = context.HttpContext.Session.GetString("rol");

            if (string.IsNullOrWhiteSpace(logueado))
            {
                context.Result = new RedirectToActionResult("Index", "Home", new { error = "Debe de iniciar sesión como administrador." });
            }
            if (logueado != "Administrador")
            {
                context.Result = new RedirectToActionResult("Inicio", "Home", new { error = "Acceso restringido." });
            }
            base.OnActionExecuted(context);
        }
    }
}
