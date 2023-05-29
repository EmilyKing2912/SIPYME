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
namespace SIPYME.Controllers
{
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


        [HttpGet]
        public JsonResult MostrarTodasLasPymes()
        {
            //PARA mostrar pymes
            List<Pyme> pLista = new List<Pyme>();
            pLista = Service.Service.ListaTodasLasPymes();
            return Json(new { data = pLista }, JsonRequestBehavior.AllowGet);
            ////
        }




        [HttpPost]
        public ActionResult EditaPyme(Pyme objeto)
        {
            object resultado;
            string mensaje = string.Empty;
            objeto.Estado_pyme = "Pendiente";
            resultado = Service.Service.usuarioEditaPyme(objeto, out mensaje);

            ViewData["Mensaje"] = mensaje;
            return View("ListarPymesPlataformista", lalista());



        }
        //private int registrarPyme(Pyme objeto) { 


        //}
        [HttpPost]
        public ActionResult EliminarPyme(Pyme objeto)
        {
            object resultado;
            string mensaje = string.Empty;
            resultado = Service.Service.UsuarioEliminaPyme(objeto);

            return View("ListarPymes", lalista());

        }

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





    }
}