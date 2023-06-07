using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIPYME.Logic;
using SIPYME.Models;
using SIPYME.Permisos;

namespace SIPYME.Controllers
{
    [PermisosRol(1)]
    public class AdministradorController : Controller
    {
        public ActionResult ViewAdmin()
        {
            return View();
        }
        //metodos pymes
        public ActionResult ListarPymesAdministrador()
        {
            return View("ListarPymesAdministrador", lalista());
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
        [HttpPost]
        public JsonResult EliminarFotoProducto(Foto objeto)
        {
            object resultado;
            string mensaje = string.Empty;
            resultado = Service.Service.elimnaFotoProducto(objeto.ID);

            return Json(new { resultado = resultado }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult EliminarFotoPyme(Foto objeto)
        {
            object resultado;
            string mensaje = string.Empty;
            resultado = Service.Service.elimnaFotoPyme(objeto.ID);

            return Json(new { resultado = resultado }, JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
        public JsonResult MostrarTodasLasPymes()
        {
            //PARA mostrar pymes
            List<PymeModel> pLista = new List<PymeModel>();
            List<Pyme> pymes = Service.Service.ListaTodasLasPymes();

            foreach (Pyme pyme in pymes)
            {
                PymeModel unitmodelo = new PymeModel();
                unitmodelo.pyme = pyme;
                unitmodelo.fotosProducto = Service.Service.listaFotosProductoPorPyme(pyme.Id);
                unitmodelo.fotosPyme = Service.Service.listaFotosPymePorPyme(pyme.Id);
                pLista.Add(unitmodelo);
            }

            return Json(new { data = pLista }, JsonRequestBehavior.AllowGet);
            ////
        }


        [HttpPost]
        public JsonResult PlataformistaRazonRechazo(Estado_pyme objeto)
        {
            object resultado2;
            int Resultado = 0;
            string mensaje = string.Empty;

            resultado2 = Service.Service.PlataformistaRazonRechazo(objeto, out mensaje, out Resultado);
            return Json(new { resultado = resultado2, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult EditaPyme(Pyme objeto, HttpPostedFileBase upload, HttpPostedFileBase[] producto, HttpPostedFileBase[] pyme)
        {
            object resultado;
            string mensaje = string.Empty;

            List<Foto> fotospyme = new List<Foto>();

            List<Foto> fotosproducto = new List<Foto>();

            objeto.Estado_pyme = "Pendiente";

            if (upload != null && upload.ContentLength > 0)
            {
                byte[] imagenData = null;
                using (var imagen = new BinaryReader(upload.InputStream))
                {
                    imagenData = imagen.ReadBytes(upload.ContentLength);
                }
                objeto.Logo = imagenData;
            }

            try
            {
                if (producto != null && producto.Length > 0)
                {


                    foreach (var archivo in producto)
                    {
                        if (archivo != null && archivo.ContentLength > 0)
                        {
                            byte[] imagenData = null;
                            using (var imagen = new BinaryReader(archivo.InputStream))
                            {
                                imagenData = imagen.ReadBytes(archivo.ContentLength);
                            }

                            Foto fotos = new Foto();
                            fotos.CantidadByte = imagenData;
                            fotos.PymeId = objeto.Id;

                            fotosproducto.Add(fotos);
                        }
                    }

                    // Ahora puedes asignar la lista "fotosproducto" a tu objeto o hacer cualquier otra operación necesaria
                }

                if (fotosproducto.Count != 0)
                    Service.Service.registrarFotosProducto(fotosproducto);


            }
            catch (Exception e)
            {

            }
            try
            {
                if (pyme != null && pyme.Length > 0)
                {


                    foreach (var archivo in pyme)
                    {
                        if (archivo != null && archivo.ContentLength > 0)
                        {
                            byte[] imagenData = null;
                            using (var imagen = new BinaryReader(archivo.InputStream))
                            {
                                imagenData = imagen.ReadBytes(archivo.ContentLength);
                            }

                            Foto fotos = new Foto();
                            fotos.CantidadByte = imagenData;
                            fotos.PymeId = objeto.Id;
                            fotospyme.Add(fotos);
                        }
                    }

                    // Ahora puedes asignar la lista "fotosproducto" a tu objeto o hacer cualquier otra operación necesaria
                }
                if (fotospyme.Count != 0)
                    Service.Service.registrarFotosPyme(fotospyme);


            }
            catch (Exception e)
            {

            }


            resultado = Service.Service.usuarioEditaPyme(objeto, out mensaje);





            ViewData["Mensaje"] = mensaje;
            return View("ListarPymes", lalista());



        }
        //private int registrarPyme(Pyme objeto) { 


        //}

        [HttpPost]
        public ActionResult RegistraPyme(Pyme objeto, HttpPostedFileBase upload, HttpPostedFileBase[] producto, HttpPostedFileBase[] pyme)
        {
            List<Foto> fotospyme = new List<Foto>();
            string mensaje = string.Empty;
            List<Foto> fotosproducto = new List<Foto>();
            try
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    byte[] imagenData = null;
                    using (var imagen = new BinaryReader(upload.InputStream))
                    {
                        imagenData = imagen.ReadBytes(upload.ContentLength);
                    }
                    objeto.Logo = imagenData;
                }



            }
            catch (Exception e)
            {
                mensaje = "No se pudo registrar la pyme";
                return Json(new { mensaje = mensaje }, JsonRequestBehavior.AllowGet);
            }


            objeto.Estado_pyme = "Pendiente";
            int id = Service.Service.MantenimientoIngresaPyme(objeto, out mensaje);
            if (id == 0)
            {
                ViewData["Mensaje"] = mensaje;
                return View("ListarPymesAdministrador", lalista());
            }




            try
            {
                if (producto != null && producto.Length > 0)
                {


                    foreach (var archivo in producto)
                    {
                        if (archivo != null && archivo.ContentLength > 0)
                        {
                            byte[] imagenData = null;
                            using (var imagen = new BinaryReader(archivo.InputStream))
                            {
                                imagenData = imagen.ReadBytes(archivo.ContentLength);
                            }

                            Foto fotos = new Foto();
                            fotos.CantidadByte = imagenData;
                            fotos.PymeId = id;

                            fotosproducto.Add(fotos);
                        }
                    }

                    // Ahora puedes asignar la lista "fotosproducto" a tu objeto o hacer cualquier otra operación necesaria
                }

                if (fotosproducto.Count != 0)
                    Service.Service.registrarFotosProducto(fotosproducto);


            }
            catch (Exception e)
            {

            }
            try
            {
                if (pyme != null && pyme.Length > 0)
                {


                    foreach (var archivo in pyme)
                    {
                        if (archivo != null && archivo.ContentLength > 0)
                        {
                            byte[] imagenData = null;
                            using (var imagen = new BinaryReader(archivo.InputStream))
                            {
                                imagenData = imagen.ReadBytes(archivo.ContentLength);
                            }

                            Foto fotos = new Foto();
                            fotos.CantidadByte = imagenData;
                            fotos.PymeId = id;
                            fotospyme.Add(fotos);
                        }
                    }

                    // Ahora puedes asignar la lista "fotosproducto" a tu objeto o hacer cualquier otra operación necesaria
                }
                if (fotospyme.Count != 0)
                    Service.Service.registrarFotosPyme(fotospyme);


            }
            catch (Exception e)
            {

            }

            ViewData["Mensaje"] = mensaje;
            return View("ListarPymesAdministrador", lalista());

        }


        [HttpPost]
        public JsonResult RechazaPyme(Pyme objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            resultado = Service.Service.PlataformistaRechazaPyme(objeto);
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult ActivaPyme(Pyme objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            resultado = Service.Service.PlataformistaApruebaPyme(objeto);
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult EliminarPyme(Pyme objeto)
        {
            object resultado;
            string mensaje = string.Empty;
            resultado = Service.Service.PlataformistaEliminaPyme(objeto);

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }













        //metodos de usuarios

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
