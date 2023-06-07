﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using SIPYME.Logic;

namespace SIPYME.Data
{
    public class PlataformistaDao
    {

            public static bool registrarUsuarioExterno(Logic.Usuario u)
            {
                using (MySqlConnection cn = new MySqlConnection(Conection.cn))
                {
                    MySqlCommand cmd = new MySqlCommand("registrarUsuariosExternos", cn);
                    cmd.Parameters.AddWithValue("cedula1", u.Cedula);
                    cmd.Parameters.AddWithValue("nombre", u.Nombre);
                    cmd.Parameters.AddWithValue("apellido1", u.Apellido1);
                    cmd.Parameters.AddWithValue("apellido2", u.Apellido2);
                    cmd.Parameters.AddWithValue("numeroTelefono", u.NumeroTelefono);
                    cmd.Parameters.AddWithValue("correo1", u.Correo);
                    cmd.Parameters.AddWithValue("contraseña", u.Contrasena);


                    cmd.Parameters.Add("registrado", MySqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", MySqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cn.Open();

                    cmd.ExecuteNonQuery();

                    bool registrado = Convert.ToBoolean(cmd.Parameters["registrado"].Value);
                    string mensaje = cmd.Parameters["mensaje"].Value.ToString();
                    if (registrado)
                        return true;
                    return false;

                }
            }
        public static void eliminarPyme(Pyme pyme)
        {


            using (MySqlConnection cn = new MySqlConnection(Conection.cn))
            {
                string sql = "DELETE FROM Pyme WHERE id = @id";
                using (MySqlCommand command = new MySqlCommand(sql, cn))
                {
                    command.Parameters.AddWithValue("@id", pyme.Id);
                    cn.Open();
                    command.ExecuteNonQuery();
                }
            }
            // trg_after_delete_usuarios(); //lama al trigger de delete
        }
        public static void eliminarFotosPyme(int idPy)
        {
            using (MySqlConnection cn = new MySqlConnection(Conection.cn))
            {
                string sql = "DELETE FROM  fotos_pyme WHERE id_Pyme =" + idPy;
                using (MySqlCommand command = new MySqlCommand(sql, cn))
                {
                    cn.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public static void eliminarFotosProducto(int idPy)
        {
            using (MySqlConnection cn = new MySqlConnection(Conection.cn))
            {
                string sql = "DELETE FROM  fotos_producto WHERE id_Pyme =" + idPy;
                using (MySqlCommand command = new MySqlCommand(sql, cn))
                {

                    cn.Open();
                    command.ExecuteNonQuery();
                }
            }
        }


        // elimina todos los estados que ha tenido la pyme
        public static void eliminarEstadoPyme(int idPy)
        {
            using (MySqlConnection cn = new MySqlConnection(Conection.cn))
            {
                string sql = "DELETE FROM  estado_pyme WHERE id_Pyme =" + idPy;
                using (MySqlCommand command = new MySqlCommand(sql, cn))
                {

                    cn.Open();
                    command.ExecuteNonQuery();
                }
            }
        }


        //public static void eliminarPymesSegunIdUsuario(String id)
        //{
        //    using (MySqlConnection cn = new MySqlConnection(Conection.cn))
        //    {
        //        string sql = "DELETE FROM Pyme WHERE id = @id";
        //        using (MySqlCommand command = new MySqlCommand(sql, cn))
        //        {
        //            command.Parameters.AddWithValue("@id", pyme.Id);
        //            cn.Open();
        //            command.ExecuteNonQuery();
        //        }
        //    }
        //    // trg_after_delete_usuarios(); //lama al trigger de delete
        //}

        public static Usuario read(Usuario usuario)
            {
                Usuario u1 = new Usuario();
                using (MySqlConnection cn = new MySqlConnection(Conection.cn))
                {

                    MySqlCommand cmd = new MySqlCommand("loginUsuario", cn);
                    cmd.Parameters.AddWithValue("correo1", usuario.Correo);
                    cmd.Parameters.AddWithValue("contraseña1", usuario.Contrasena);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cn.Open();

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        dr.Read();

                        u1.Cedula = dr["cedula"].ToString();
                        u1.Nombre = dr["nombre"].ToString();
                        u1.Apellido1 = dr["apellido1"].ToString();
                        u1.Apellido2 = dr["apellido2"].ToString();
                        u1.NumeroTelefono = Convert.ToInt32(dr["numeroTelefono"]);
                        u1.Correo = dr["correo"].ToString();
                        u1.Contrasena = dr["contraseña"].ToString();
                        u1.Tipo = Convert.ToInt32(dr["tipo_usuario"]);
                        u1.Estado = Convert.ToInt32(dr["estado_usuario"]);
                        //si no funciona poner cedula1 y correo1
                    }

                }
                return u1;
            }
            public static bool guardaFotoProducto(Foto foto)
            {
                bool registrado = false;
                using (MySqlConnection cn = new MySqlConnection(Conection.cn))
                {

                    MySqlCommand cmd = new MySqlCommand("registrarFotoProducto", cn);
                    cmd.Parameters.AddWithValue("p_foto", foto.CantidadByte);
                    cmd.Parameters.AddWithValue("p_id_pyme", foto.PymeId);
                    cmd.Parameters.Add("resultado", MySqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", MySqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cn.Open();

                    cmd.ExecuteNonQuery();


                    registrado = Convert.ToBoolean(cmd.Parameters["resultado"].Value);



                }
                if (registrado)
                    return true;
                return false;

            }

            public static bool guardaFotoPyme(Foto foto)
            {
                bool registrado = false;
                using (MySqlConnection cn = new MySqlConnection(Conection.cn))
                {

                    MySqlCommand cmd = new MySqlCommand("registrarFotoPyme", cn);
                    cmd.Parameters.AddWithValue("p_foto", foto.CantidadByte);
                    cmd.Parameters.AddWithValue("p_id_pyme", foto.PymeId);
                    cmd.Parameters.Add("resultado", MySqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", MySqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cn.Open();
                    cmd.ExecuteNonQuery();


                    registrado = Convert.ToBoolean(cmd.Parameters["resultado"].Value);



                }
                if (registrado)
                    return true;
                return false;

            }

        public static bool RegistraRazonRechazo(Estado_pyme ep, out string Mensaje, out int Resultado)
        {
            Mensaje = string.Empty;
            Resultado = 0;
            try
            {

                using (MySqlConnection cn = new MySqlConnection(Conection.cn))
                {
                    MySqlCommand cmd = new MySqlCommand("cambiarEstadoRechazo", cn);
                    cmd.Parameters.AddWithValue("p_id_pyme", ep.IdPyme);
                    cmd.Parameters.AddWithValue("p_razonRechazo", ep.Razon_rechazo);
                    cmd.Parameters.Add("resultado", MySqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", MySqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    cn.Open();

                    cmd.ExecuteNonQuery();

                    Mensaje = cmd.Parameters["mensaje"].Value.ToString();
                    Resultado = Convert.ToInt32(cmd.Parameters["resultado"].Value);
                }
            }
            catch (Exception ex)
            {

                Mensaje = ex.Message;
            }
            if (Resultado == 0)
            {
                return false;
            }
            else
            {
                return true;



            }
        }




            public static int MantenimientoRegistraPyme(Logic.Pyme u, out String Mensaje)
            {
                int idautogenerado = 0;
                Mensaje = string.Empty;
                try
                {

                    using (MySqlConnection cn = new MySqlConnection(Conection.cn))
                    {

                        MySqlCommand cmd = new MySqlCommand("registrarPyme", cn);
                        cmd.Parameters.AddWithValue("p_id_usuario", u.Id_usuario);
                        cmd.Parameters.AddWithValue("p_nombre", u.Nombre);
                        cmd.Parameters.AddWithValue("p_numeroTelefono", u.NumeroTelefono);
                        cmd.Parameters.AddWithValue("p_correo", u.Correo);
                        cmd.Parameters.AddWithValue("p_descripcion", u.Descripcion);
                        cmd.Parameters.AddWithValue("p_area_trabajo", u.AreaTrabajo);
                        cmd.Parameters.AddWithValue("p_longitud", u.Longitud);
                        cmd.Parameters.AddWithValue("p_latitud", u.Latitud);
                        cmd.Parameters.AddWithValue("p_estado", u.Estado_pyme);
                        cmd.Parameters.AddWithValue("p_logo", u.Logo);
                        cmd.Parameters.AddWithValue("p_sitioWeb", u.SitioWeb);
                        cmd.Parameters.AddWithValue("p_webSocial", u.WebSocial);
                        cmd.Parameters.Add("resultado", MySqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("mensaje", MySqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cn.Open();

                        cmd.ExecuteNonQuery();

                        idautogenerado = Convert.ToInt32(cmd.Parameters["resultado"].Value);
                        Mensaje = cmd.Parameters["mensaje"].Value.ToString();

                    }
                }
                catch (Exception ex)
                {
                    idautogenerado = 0;
                    Mensaje = ex.Message;
                }
                if (idautogenerado == 0)
                {
                    return 0;
                }
                else
                {
                    return idautogenerado;
                }

            }
            public static bool edit(Pyme u, out string Mensaje)
            {
                int idautogenerado = 0;
                Mensaje = string.Empty;
                try
                {

                    using (MySqlConnection cn = new MySqlConnection(Conection.cn))
                    {

                        MySqlCommand cmd = new MySqlCommand("editarPyme", cn);

                        cmd.Parameters.AddWithValue("p_id", u.Id);

                        cmd.Parameters.AddWithValue("p_id_usuario", u.Id_usuario);

                        cmd.Parameters.AddWithValue("p_nombre", u.Nombre);
                        cmd.Parameters.AddWithValue("p_numeroTelefono", u.NumeroTelefono);
                        cmd.Parameters.AddWithValue("p_correo", u.Correo);
                        cmd.Parameters.AddWithValue("p_descripcion", u.Descripcion);
                        cmd.Parameters.AddWithValue("p_area_trabajo", u.AreaTrabajo);
                        cmd.Parameters.AddWithValue("p_longitud", u.Longitud);
                        cmd.Parameters.AddWithValue("p_latitud", u.Latitud);
                        cmd.Parameters.AddWithValue("p_estado", u.Estado_pyme);
                        cmd.Parameters.AddWithValue("p_logo", u.Logo);
                        cmd.Parameters.AddWithValue("p_sitioWeb", u.SitioWeb);
                        cmd.Parameters.AddWithValue("p_webSocial", u.WebSocial);

                        cmd.Parameters.Add("resultado", MySqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("mensaje", MySqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cn.Open();

                        cmd.ExecuteNonQuery();

                        idautogenerado = Convert.ToInt32(cmd.Parameters["resultado"].Value);
                        Mensaje = cmd.Parameters["mensaje"].Value.ToString();

                    }
                }
                catch (Exception ex)
                {
                    idautogenerado = 0;
                    Mensaje = ex.Message;
                }
                if (idautogenerado == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            public static List<AreaTrabajo> listarAreasTrabajo()
            {
                MySqlConnection cn = new MySqlConnection(Conection.cn);
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataReader reader;
                cn.Open();
                cmd.Connection = cn;
                cmd.CommandText = "SELECT * FROM area_trabajo";
                reader = cmd.ExecuteReader();
                List<AreaTrabajo> areastrabajo = new List<AreaTrabajo>();
                while (reader.Read())
                {
                    AreaTrabajo ar = new AreaTrabajo();
                    ar.Identificador = reader.GetInt32("identificador");
                    ar.Nombre_area = reader.GetString("nombre_area");

                    areastrabajo.Add(ar);
                }
                cn.Close();
                return areastrabajo;
            }


            public static List<Pyme> listarPymesUsuario(string usuarioCed)
            {
                MySqlConnection cn = new MySqlConnection(Conection.cn);
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataReader reader;
                cn.Open();
                cmd.Connection = cn;
                cmd.CommandText = "SELECT * FROM Pyme WHERE id_Usuario= " + usuarioCed;
                reader = cmd.ExecuteReader();
                List<Pyme> pymes = new List<Pyme>();
                while (reader.Read())
                {
                    Pyme pyme = new Pyme();
                    pyme.Id = int.Parse(reader.GetString("id"));
                    pyme.Id_usuario = reader.GetString("id_Usuario");
                    pyme.Nombre = reader.GetString("nombre");
                    pyme.NumeroTelefono = int.Parse(reader.GetString("numeroTelefono"));
                    pyme.Correo = reader.GetString("correo");
                    pyme.Descripcion = reader.GetString("descripcion");
                    pyme.AreaTrabajo = reader.GetString("area_trabajo");
                    pyme.Longitud = reader.GetString("Longitud");
                    pyme.Latitud = reader.GetString("Latitud");
                    pyme.Estado_pyme = reader.GetString("estado");
                    if (!reader.IsDBNull(reader.GetOrdinal("logo")))
                    {
                        byte[] longblobData = (byte[])reader["logo"];
                        pyme.Logo = longblobData;
                    }
                    else
                    {
                        pyme.Logo = null;
                    }
                    pyme.SitioWeb = reader.IsDBNull(reader.GetOrdinal("sitioWeb")) ? null : reader.GetString("sitioWeb");
                    pyme.WebSocial = reader.IsDBNull(reader.GetOrdinal("webSocial")) ? null : reader.GetString("webSocial");

                    pymes.Add(pyme);
                }
                cn.Close();
                return pymes;
            }


            public static List<Pyme> listarTodasLasPymes()
            {
                MySqlConnection cn = new MySqlConnection(Conection.cn);
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataReader reader;
                cn.Open();
                cmd.Connection = cn;
                cmd.CommandText = "SELECT * FROM Pyme";
                reader = cmd.ExecuteReader();
                List<Pyme> pymes = new List<Pyme>();
                while (reader.Read())
                {
                    Pyme pyme = new Pyme();
                    pyme.Id = int.Parse(reader.GetString("id"));
                    pyme.Id_usuario = reader.GetString("id_Usuario");
                    pyme.Nombre = reader.GetString("nombre");
                    pyme.NumeroTelefono = int.Parse(reader.GetString("numeroTelefono"));
                    pyme.Correo = reader.GetString("correo");
                    pyme.Descripcion = reader.GetString("descripcion");
                    pyme.AreaTrabajo = reader.GetString("area_trabajo");
                    pyme.Longitud = reader.GetString("Longitud");
                    pyme.Latitud = reader.GetString("Latitud");
                    pyme.Estado_pyme = reader.GetString("estado");
                    if (!reader.IsDBNull(reader.GetOrdinal("logo")))
                    {
                        byte[] longblobData = (byte[])reader["logo"];
                        pyme.Logo = longblobData;
                    }
                    else
                    {
                        pyme.Logo = null;
                    }
                    pyme.SitioWeb = reader.IsDBNull(reader.GetOrdinal("sitioWeb")) ? null : reader.GetString("sitioWeb");
                    pyme.WebSocial = reader.IsDBNull(reader.GetOrdinal("webSocial")) ? null : reader.GetString("webSocial");

                    pymes.Add(pyme);
                }
                cn.Close();
                return pymes;
            }

        public static void rechazarPyme(Pyme p)
        {

            using (MySqlConnection cn = new MySqlConnection(Conection.cn))
            {
                string sql = "UPDATE Pyme SET  estado = 'Rechazada'  WHERE id="+ p.Id;
                using (MySqlCommand command = new MySqlCommand(sql, cn))
                {
                    cn.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public static void aprobarPyme(Pyme p)
        {

            using (MySqlConnection cn = new MySqlConnection(Conection.cn))
            {
                string sql = "UPDATE Pyme SET  estado = 'Aprobada'  WHERE id=" + p.Id;
                using (MySqlCommand command = new MySqlCommand(sql, cn))
                {
                    cn.Open();
                    command.ExecuteNonQuery();
                }
            }
        }








    }
    }










