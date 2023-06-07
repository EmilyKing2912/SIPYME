using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using SIPYME.Logic;
using SIPYME.Models;
using System.Web.Security;
namespace SIPYME.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Iframe() {
        
            return View("Iframe", lalista());
        }

        private List<SelectListItem> lalista()
        {


            List<AreaTrabajo> Lista = Service.Service.listaAreaTrabajos();
            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem
            {
                Value = "",
                Text = "Seleccionar...",
            });

            foreach (AreaTrabajo d in Lista)
            {
                lst.Add(new SelectListItem
                {
                    Value = d.Nombre_area,
                    Text = d.Nombre_area
                });
            }

            ViewBag.ComboBoxData = lst;

            return lst;
        }
        public ActionResult IframeLimpio()
        {

            return View("IframeLimpio", lalista());

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpGet]
        public JsonResult MostrarTodasLasPymes()
        {
            //PARA mostrar pymes
            List<PymeModel> pLista = new List<PymeModel>();


            List<Pyme> pymes = Service.Service.ListaPyme();

            foreach (Pyme pyme in pymes)
            {
                PymeModel unitmodelo = new PymeModel();
                unitmodelo.pyme = pyme;
                unitmodelo.fotosProducto = Service.Service.listaFotosProductoPorPyme(pyme.Id);
                unitmodelo.fotosPyme = Service.Service.listaFotosPymePorPyme(pyme.Id);
                pLista.Add(unitmodelo);
            }

            return Json(new { data = pLista }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Register()
        {

            return View();
        }

        public ActionResult RegistrarUsuarioExterno()
        {
            return View();
        }

        public ActionResult SinPermiso()
        {
            ViewBag.Message = "No cuenta con permisos para ver esta página";

            return View();
        }


        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            if (String.IsNullOrWhiteSpace(usuario.Correo) || String.IsNullOrEmpty(usuario.Correo) ||
                 String.IsNullOrWhiteSpace(usuario.Contrasena) || String.IsNullOrEmpty(usuario.Contrasena))
            {

                ViewData["Mensaje"] = "Por favor rellene todos los campos";
                return View("index");

            }

            Usuario uFinal = Service.Service.retornaUsuario(usuario);


            if (uFinal is null)
            {
                ViewData["Mensaje"] = "Usuario no encontrado";
                return View("Index");
            }
            else
            {
                Session["usuario"] = uFinal;
                FormsAuthentication.SetAuthCookie(uFinal.Cedula, false);
                if (uFinal.Tipo.Equals(1))
                {
                    return RedirectToAction("ViewAdmin", "Administrador");
                }

                if (uFinal.Tipo.Equals(2)) // usuario externo
                {

                    return RedirectToAction("ViewUsuario", "Usuario");
                }
                
                if (uFinal.Tipo.Equals(3)) // plataformista
                {
                    return RedirectToAction("ViewPlataformista", "Plataformista");
                }
                return View("index");
            }

            // return RedirectToAction("Register");
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


            bool UFinal = Service.Service.registraUsuarioExterno(oUsuario);
            

            //ViewData["mensaje"] = mensaje;

            if (UFinal)
            {
               
                return RedirectToAction("index", "Home");
                
            }
            else
            {
                ViewData["Mensaje"] = "Error al agregar usuario, verifique que el usuario no se encuentre registrado";
                return View();
            }

        }
        //[HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["usuario"] = null;
            return RedirectToAction("index", "Home");
        }



    }
}