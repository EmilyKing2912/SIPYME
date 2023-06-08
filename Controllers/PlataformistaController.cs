using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using SIPYME.Logic;
using System.Web.Mvc;
using System.IO;
using System.Collections;
using SIPYME.Models;
using SIPYME.Permisos;

namespace SIPYME.Controllers
{
    [PermisosRol(3)]
    
    public class PlataformistaController : Controller
    {
        // GET: Plataformista
        public ActionResult ViewPlataformista()
        {
            return View();
        }


        public ActionResult ListarPymesPlataformista()
        {
            return View("ListarPymesPlataformista", lalista());
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
        private List<SelectListItem> lalista()
        {


            List<AreaTrabajo> Lista = Service.Service.listaAreaTrabajos();
            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem
            {
                Value = "",
                Text = "Seleccionar  Area de Trabajo  de la Pyme",
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
            return View("ListarPymesPlataformista", lalista());



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
                return View("ListarPymesPlataformista", lalista());
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
            return View("ListarPymesPlataformista", lalista());

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
    }
}