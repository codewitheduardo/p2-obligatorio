using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AplicacionWeb.Filters
{
    public class Authentication : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            string logueado = context.HttpContext.Session.GetString("correo");

            if (string.IsNullOrWhiteSpace(logueado))
            {
                context.Result = new RedirectToActionResult("Index", "Home", new { error = "Debe de iniciar sesión." });
            }
            base.OnActionExecuted(context);
        }
    }
}
