﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIPYME.Data;
using SIPYME.Logic;

namespace Service
{
    public class Service
    {
        public static bool registraUsuarioExterno(Usuario u)
        {
            try
            {

                bool usu1 = UsuarioDao.registrarUsuarioExterno(u);
                return usu1;


            }
            catch (Exception e)
            {
                return false;
            }

        }

        public static Usuario retornaUsuario(Usuario u)
        {
            try
            {

                Usuario usu1 = UsuarioDao.read(u);
                return usu1;


            }
            catch (Exception e)
            {
                return null;
            }

        }
        //admin metodos


        public static bool AdminregistraUsuarioExterno(Usuario u)
        {
            try
            {

                bool usu1 = AdminDao.AdminregistraUsuarioExterno(u);
                return usu1;


            }
            catch (Exception e)
            {
                return false;
            }

        }
        public static bool PlataformistaRazonRechazo(Estado_pyme u,out string Mensaje, out int Resultado)
        {
                Mensaje = string.Empty;
            Resultado = 0;
                return PlataformistaDao.RegistraRazonRechazo(u, out Mensaje,out Resultado);
        
        }


        public static bool adminEditaUsuario(Usuario u, out string Mensaje)
        {
            Mensaje = string.Empty;
           
              return  AdminDao.edit(u,out Mensaje);
              

        }
        public static bool adminIngresaUsuario(Usuario u, out string Mensaje)
        {
            Mensaje = string.Empty;

            return AdminDao.AdminregistraUsuarioo(u, out Mensaje);


        }

        public static List<Usuario> AdminlistaUsuarios()
        {
            try
            {
                return AdminDao.listarUsuarios();

            }
            catch (Exception e)
            {
                return null;
            }

        }
        public static bool AdminEliminaUsuario(Usuario u)
        {
            try
            {
                AdminDao.eliminarPymesUsuario(u);
                AdminDao.eliminarUsuario(u);
                AdminDao.trg_after_delete_usuarios();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public static bool UsuarioEliminaPyme(Pyme p)
        {
            try
            {
                UsuarioDao.eliminarFotosProducto(p.Id);
                UsuarioDao.eliminarFotosPyme(p.Id);
                PlataformistaDao.eliminarEstadoPyme(p.Id);
                UsuarioDao.eliminarPyme(p);
               
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        public static bool PlataformistaEliminaPyme(Pyme p)
        {
            try
            {
                PlataformistaDao.eliminarFotosProducto(p.Id);
                PlataformistaDao.eliminarFotosPyme(p.Id);
                PlataformistaDao.eliminarEstadoPyme(p.Id);
                PlataformistaDao.eliminarPyme(p);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public static bool DesactivaUsuario(Usuario u)
        {
            try
            {
                AdminDao.desactivarUsuario(u);
                AdminDao.trg_after_update_usuarios();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public static bool ActivaUsuario(Usuario u)
        {
            try
            {

                AdminDao.activarUsuario(u);
                AdminDao.trg_after_update_usuarios();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public static List<Bitacora> AdminlistaBitacora()
        {
            try
            {
                return AdminDao.listarBitacora();

            }
            catch (Exception e)
            {
                return null;
            }

        }
         public static int usuarioIngresaPyme(Pyme u, out string mensaje)
        {
            mensaje = string.Empty;
            return UsuarioDao.UsuarioregistraPyme(u, out mensaje);


        }


        public static int MantenimientoIngresaPyme(Pyme u, out string mensaje)
        {
            mensaje = string.Empty;
            return PlataformistaDao.MantenimientoRegistraPyme(u, out mensaje);


        }






        public static bool registrarFotosPyme(List<Foto> lista) {

            try
            {
                foreach (Foto foto in lista)
                {
                    UsuarioDao.guardaFotoPyme(foto);
                }
                return true;
            }
            catch (Exception e) {

                return false;
            }

        }
        public static bool registrarFotosProducto(List<Foto> lista)
        {

            try
            {
                foreach (Foto foto in lista)
                {
                    UsuarioDao.guardaFotoProducto(foto);
                }
                return true;
            }
            catch (Exception e)
            {

                return false;
            }

        }
        public static bool usuarioEditaPyme(Pyme u, out string Mensaje)
        {
            Mensaje = string.Empty;

            return UsuarioDao.edit(u, out Mensaje);

        }
        public static List<Foto> listaFotosProductoPorPyme(int idpyme)
        { 
        
                    try
            {
                return UsuarioDao.listarFotosProducto(idpyme);

            }
            catch (Exception e)
            {
                return null;
            }

}
        public static List<Foto> listaFotosPymePorPyme(int idpyme)
        {

            try
            {
                return UsuarioDao.listarFotosPyme(idpyme);

            }
            catch (Exception e)
            {
                return null;
            }

        }





        public static List<AreaTrabajo> listaAreaTrabajos()
        {
            try
            {
                return UsuarioDao.listarAreasTrabajo();

            }
            catch (Exception e)
            {
                return null;
            }

        }
        public static List<Pyme> usuarioListaPyme(String ced)
        {
            try
            {
                return UsuarioDao.listarPymesUsuario(ced);

            }
            catch (Exception e)
            {
                return null;
            }

        }

        public static List<Pyme> ListaPyme()
        {
            try
            {
                return UsuarioDao.listarPymes();

            }
            catch (Exception e)
            {
                return null;
            }

        }

        public static List<Pyme> ListaTodasLasPymes()
        {
            try
            {
                return PlataformistaDao.listarTodasLasPymes();

            }
            catch (Exception e)
            {
                return null;
            }

        }

        public static bool PlataformistaRechazaPyme(Pyme u)
        {
            try
            {
                PlataformistaDao.rechazarPyme(u);
                AdminDao.trg_after_updestado_pymes();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public static bool PlataformistaApruebaPyme(Pyme u)
        {
            try
            {

                PlataformistaDao.aprobarPyme(u);
                AdminDao.trg_after_updestado_pymes();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public static bool AdminRechazaPyme(Pyme u)
        {
            try
            {
                AdminDao.rechazarPyme(u);
                AdminDao.trg_after_updestado_pymes();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        public static bool elimnaFotoProducto(int id)
        {
            try
            {
                UsuarioDao.eliminarFotosProductoByID(id);
           
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        public static bool elimnaFotoPyme(int id)
        {
            try
            {
                UsuarioDao.eliminarFotosPymeByID(id);
             
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public static bool AdminApruebaPyme(Pyme u)
        {
            try
            {

                AdminDao.aprobarPyme(u);
                AdminDao.trg_after_updestado_pymes();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }








    }
}