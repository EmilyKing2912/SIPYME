using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIPYME.Logic;


namespace SIPYME.Permisos
{
    public class PermisosRolAttribute : ActionFilterAttribute
    {

        private int tipo_usuario;

        public PermisosRolAttribute(int tu)
        {
            tipo_usuario = tu;
        }


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (HttpContext.Current.Session["usuario"] != null)
            {
                Usuario usuario = HttpContext.Current.Session["usuario"] as Usuario;



                if (usuario.Tipo != this.tipo_usuario)
                {
                    filterContext.Result = new RedirectResult("~/Home/SinPermiso");
                }

                base.OnActionExecuting(filterContext);
            }

            else
            {
                base.OnActionExecuting(filterContext);
                filterContext.Result = new RedirectResult("~/Home/SinPermiso");
            }



        }

    }
}