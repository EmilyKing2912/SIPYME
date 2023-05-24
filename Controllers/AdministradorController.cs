using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIPYME.Logic;
namespace SIPYME.Controllers
{
    public class AdministradorController : Controller
    {
        public ActionResult ViewAdmin()
        {
            return View();
        }
        
        [HttpGet]
        public JsonResult ListarUsuarios()
        {
            List<Usuario> uLista = new List<Usuario>();
            uLista = Service.Service.AdminlistaUsuarios();
            return Json(new { data = uLista }, JsonRequestBehavior.AllowGet);
        }
     
        public ActionResult AgregarUsuario()
        {
            return View();
        }
        public ActionResult Listausuarios()
        {
            return View();
        }

        public ActionResult Bitacora()
        {
            return View();
        }
        [HttpGet]
        public JsonResult ListarBitacora()
        {
            List<Bitacora> uLista = new List<Bitacora>();
            uLista = Service.Service.AdminlistaBitacora();
            return Json(new { data = uLista }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult RegistrarUsuarioExterno(Usuario oUsuario)
        {
            bool registrado;
            string mensaje;


            if (String.IsNullOrWhiteSpace(oUsuario.Cedula) || String.IsNullOrEmpty(oUsuario.Cedula) || String.IsNullOrWhiteSpace(oUsuario.Nombre) || String.IsNullOrEmpty(oUsuario.Nombre) || String.IsNullOrWhiteSpace(oUsuario.Apellido1) || String.IsNullOrEmpty(oUsuario.Apellido1) || String.IsNullOrWhiteSpace(oUsuario.Apellido2) || String.IsNullOrEmpty(oUsuario.Apellido2) || oUsuario.NumeroTelefono.Equals(null) || String.IsNullOrEmpty(oUsuario.Correo) || String.IsNullOrWhiteSpace(oUsuario.Correo) || String.IsNullOrEmpty(oUsuario.Contrasena) || String.IsNullOrWhiteSpace(oUsuario.Contrasena))
            {
                ViewData["Mensaje"] = "Por favor rellene todos los campos";
                return View();

            }


            bool UFinal = Service.Service.AdminregistraUsuarioExterno(oUsuario);


            //ViewData["mensaje"] = mensaje;

            if (UFinal)
            {
                return RedirectToAction("Listausuarios", "Administrador");

            }
            else
            {
                ViewData["Mensaje"] = "Error al agregar usuario, verifique que el usuario no se encuentre registrado";
                return View();
            }

        }

        [HttpPost]
        public JsonResult EditaUsuario(Usuario objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            resultado = Service.Service.adminEditaUsuario(objeto, out mensaje);
            return Json(new { resultado = resultado, mensaje = mensaje}, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult RegistraUsuario(Usuario objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            resultado = Service.Service.adminIngresaUsuario(objeto, out mensaje);
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult DesactivaUsuario(Usuario objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            resultado = Service.Service.DesactivaUsuario(objeto);
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult ActivarUsuario(Usuario objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            resultado = Service.Service.ActivaUsuario(objeto);
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult EliminarUsuario(Usuario objeto)
        {
            object resultado;
            string mensaje = string.Empty;
            resultado = Service.Service.AdminEliminaUsuario(objeto);

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }
    }
}
