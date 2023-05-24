using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using SIPYME.Logic;


namespace SIPYME.Data
{
    public class AdminDao
    {

        public static bool AdminregistraUsuarioExterno(Logic.Usuario u)
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
        public static bool AdminregistraUsuarioo(Logic.Usuario u, out String Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = string.Empty;
            try
            {

                using (MySqlConnection cn = new MySqlConnection(Conection.cn))
                {

                    MySqlCommand cmd = new MySqlCommand("registrarUsuarios", cn);
                    cmd.Parameters.AddWithValue("cedula1", u.Cedula);
                    cmd.Parameters.AddWithValue("nombre1", u.Nombre);
                    cmd.Parameters.AddWithValue("apellido11", u.Apellido1);
                    cmd.Parameters.AddWithValue("apellido21", u.Apellido2);
                    cmd.Parameters.AddWithValue("numeroTelefono1", u.NumeroTelefono);
                    cmd.Parameters.AddWithValue("correo1", u.Correo);
                    cmd.Parameters.AddWithValue("contraseña1", u.Contrasena);
                    cmd.Parameters.AddWithValue("tipo_usuario1", u.Tipo);
                    cmd.Parameters.AddWithValue("estado_usuario1", u.Estado);

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
                trg_after_insert_usuarios();
                return true;
            }

        }
        
        public static Usuario login(Usuario usuario)
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

                }

            }
            return u1;
        }
        public static bool edit(Usuario u, out string Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = string.Empty;
            try
            {

                using (MySqlConnection cn = new MySqlConnection(Conection.cn))
                {

                    MySqlCommand cmd = new MySqlCommand("editarUsuario", cn);
                    cmd.Parameters.AddWithValue("cedula1", u.Cedula);
                    cmd.Parameters.AddWithValue("nombre1", u.Nombre);
                    cmd.Parameters.AddWithValue("apellido11", u.Apellido1);
                    cmd.Parameters.AddWithValue("apellido21", u.Apellido2);
                    cmd.Parameters.AddWithValue("numeroTelefono1", u.NumeroTelefono);
                    cmd.Parameters.AddWithValue("correo1", u.Correo);
                    cmd.Parameters.AddWithValue("contraseña1", u.Contrasena);
                    cmd.Parameters.AddWithValue("tipo_usuario1", u.Tipo);
                    cmd.Parameters.AddWithValue("estado_usuario1", u.Estado);

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
            if(idautogenerado == 0)
            {
                return false;
            }
            else
            {
                trg_after_update_usuarios();
                return true;
            }
        }
            
        public static List<Usuario> listarUsuarios()
        {
            MySqlConnection cn = new MySqlConnection(Conection.cn);
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader reader;
            cn.Open();
            cmd.Connection = cn;
            cmd.CommandText = "SELECT * FROM Usuario WHERE tipo_usuario = 2 OR tipo_usuario = 3";
            reader = cmd.ExecuteReader();
            List<Usuario> usuarios = new List<Usuario>();
            while (reader.Read())
            {
                            Usuario usuario = new Usuario();
                            usuario.Cedula = reader.GetString("cedula");
                            usuario.Nombre = reader.GetString("nombre");
                            usuario.Apellido1 = reader.GetString("apellido1");
                            usuario.Apellido2 = reader.GetString("apellido2");
                            usuario.NumeroTelefono = reader.GetInt32("numeroTelefono");
                            usuario.Correo = reader.GetString("correo");
                            usuario.Contrasena = reader.GetString("contraseña");
                            usuario.Estado = reader.GetInt32("estado_usuario");
                            usuario.Tipo = reader.GetInt32("tipo_usuario");
                            usuarios.Add(usuario);
            }
            cn.Close();
            return usuarios;
        }
              
        public static void eliminarUsuario(Usuario usuario)
        {
            using (MySqlConnection cn = new MySqlConnection(Conection.cn))
            {
                string sql = "DELETE FROM Usuario WHERE cedula = @cedula";
                using (MySqlCommand command = new MySqlCommand(sql, cn))
                {
                    command.Parameters.AddWithValue("@cedula", usuario.Cedula);
                    cn.Open();
                    command.ExecuteNonQuery();
                }
            }
           // trg_after_delete_usuarios(); //lama al trigger de delete
        }
        public static void desactivarUsuario(Usuario usuario)
        {

            using (MySqlConnection cn = new MySqlConnection(Conection.cn))
            {
                string sql = "UPDATE Usuario SET  estado_usuario=2 WHERE cedula=@cedula";
                using (MySqlCommand command = new MySqlCommand(sql, cn))
                {
                    command.Parameters.AddWithValue("@cedula", usuario.Cedula);
                    command.Parameters.AddWithValue("@estado_usuario", usuario.Estado);
                    cn.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public static void activarUsuario(Usuario usuario)
        {

            using (MySqlConnection cn = new MySqlConnection(Conection.cn))
            {
                string sql = "UPDATE Usuario SET  estado_usuario=1 WHERE cedula=@cedula";
                using (MySqlCommand command = new MySqlCommand(sql, cn))
                {
                    command.Parameters.AddWithValue("@cedula", usuario.Cedula);
                    command.Parameters.AddWithValue("@estado_usuario", usuario.Estado);
                    cn.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public static Usuario retornaUsuarioPorCed(Usuario usuario)
        {
            Usuario u1 = new Usuario();
            using (MySqlConnection cn = new MySqlConnection(Conection.cn))
            {

                MySqlCommand cmd = new MySqlCommand("retornaUsuario", cn);
                cmd.Parameters.AddWithValue("cedula1", usuario.Cedula);
                
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

                }

            }
            return u1;
        }

        public static void trg_after_delete_usuarios()
        {
            string createTriggerQuery = @"
                DROP TRIGGER IF EXISTS after_delete_usuarios;
                CREATE TRIGGER after_delete_usuarios
                AFTER DELETE ON usuario
                FOR EACH ROW
                BEGIN
                    INSERT INTO Bitacora(fecha, descripcion, usuario_editor)
                    VALUES(
                        now(),
                        CONCAT('El usuario con la cédula : ', OLD.cedula, '  ha sido eliminado'),
                        @sessionValue
                    );
                END;
            ";

            using (MySqlConnection connection = new MySqlConnection(Conection.cn))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(createTriggerQuery, connection))
                {
                    Usuario sessionValue = HttpContext.Current.Session["usuario"] as Usuario;
                    String cedSession = sessionValue.Cedula;
                    command.Parameters.AddWithValue("@sessionValue", cedSession);

                    command.ExecuteNonQuery();
                    Console.WriteLine("Trigger after_delete_usuarios creado con éxito.");
                }
            }
        }


        public static void trg_after_insert_usuarios()
        {
            string createTriggerQuery = @"
                DROP TRIGGER IF EXISTS after_insert_usuarios;
                CREATE TRIGGER after_insert_usuarios
                AFTER INSERT ON usuario
                FOR EACH ROW
                BEGIN
                    INSERT INTO Bitacora(fecha, descripcion, usuario_editor)
                    VALUES(
                        now(),
                        CONCAT('Registro de usuario con el nombre: ', NEW.nombre,' ', NEW.apellido1, ' ' , NEW.apellido2, ', cédula: ', NEW.cedula),
                        @sessionValue
                    );
                END;
            ";

            using (MySqlConnection connection = new MySqlConnection(Conection.cn))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(createTriggerQuery, connection))
                {
                    Usuario sessionValue = HttpContext.Current.Session["usuario"] as Usuario;
                    String cedSession = sessionValue.Cedula;
                    command.Parameters.AddWithValue("@sessionValue", cedSession);

                    command.ExecuteNonQuery();
                    Console.WriteLine("Trigger after_insert_usuarios creado con éxito.");
                }
            }
        }


        public static void trg_after_update_usuarios()
        {
            string createTriggerQuery = @"
           DROP TRIGGER IF EXISTS after_update_usuarios;

            CREATE TRIGGER after_update_usuarios
            AFTER UPDATE ON usuario
            FOR EACH ROW
        BEGIN
        DECLARE changes VARCHAR(255);
        SET changes = '';

            IF OLD.numeroTelefono <> NEW.numeroTelefono THEN
        SET changes = CONCAT(changes, 'Número de teléfono ha cambiado de', OLD.numeroTelefono, ' a ', NEW.numeroTelefono, '. ');
            END IF;

            IF OLD.correo<> NEW.correo THEN
        SET changes = CONCAT(changes, 'Correo ha cambiado de ', OLD.correo, ' a ', NEW.correo, '. ');
            END IF;

            IF OLD.nombre<> NEW.nombre THEN
        SET changes = CONCAT(changes, 'Nombre ha cambiado de ', OLD.nombre, ' a ', NEW.nombre, '. ');
            END IF;

            IF OLD.apellido1<> NEW.apellido1 THEN
        SET changes = CONCAT(changes, 'Apellido 1 ha cambiado de ', OLD.apellido1, ' a ', NEW.apellido1, '. ');
            END IF;

            IF OLD.apellido2<> NEW.apellido2 THEN
        SET changes = CONCAT(changes, 'Apellido 2 ha cambiado de ', OLD.apellido2, ' a ', NEW.apellido2, '. ');
            END IF;

            IF OLD.contraseña<> NEW.contraseña THEN
        SET changes = CONCAT(changes, 'Contraseña ha cambiado. ');
            END IF;

            IF OLD.tipo_usuario<> NEW.tipo_usuario THEN
        SET changes = CONCAT(changes, 'Tipo de usuario ha cambiado de ', OLD.tipo_usuario, ' a ', NEW.tipo_usuario, '. ');
            END IF;

            IF OLD.estado_usuario<> NEW.estado_usuario THEN
        SET changes = CONCAT(changes, 'Estado de usuario ha cambiado de ', OLD.estado_usuario, ' a ', NEW.estado_usuario, '. ');
            END IF;

            IF changes<> '' THEN
               INSERT INTO Bitacora(fecha, descripcion, usuario_editor)
        VALUES(NOW(), CONCAT('El usuario con la cédula: ', NEW.cedula, ' ha sido editado. Cambios: ', changes), @sessionValue);
            END IF;
            END;

            ";

            using (MySqlConnection connection = new MySqlConnection(Conection.cn))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(createTriggerQuery, connection))
                {
                    Usuario sessionValue = HttpContext.Current.Session["usuario"] as Usuario;
                    String cedSession = sessionValue.Cedula;
                    command.Parameters.AddWithValue("@sessionValue", cedSession);

                    command.ExecuteNonQuery();
                    Console.WriteLine("Trigger after_update_usuarios creado con éxito.");
                }
            }
        }


        public static void trg_after_updestado_usuarios()
        {
            string createTriggerQuery = @"
                DROP TRIGGER IF EXISTS after_updestado_usuarios;
                CREATE TRIGGER after_updestado_usuarios
                AFTER UPDATE ON usuario
                FOR EACH ROW
             IF OLD.estado_usuario <> NEW.estado_usuario THEN
                    INSERT INTO Bitacora(fecha, descripcion, usuario_editor)
                    VALUES(
                    now(),
                    CONCAT('El usuario con la cédula: ', NEW.cedula, ' ha sido editado en su estado, anterior: ', OLD.estado_usuario, ', nuevo estado:', NEW.estado_usuario),
                    @sessionValue
                    );
                    END IF;
             END;
            ";

            using (MySqlConnection connection = new MySqlConnection(Conection.cn))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(createTriggerQuery, connection))
                {
                    Usuario sessionValue = HttpContext.Current.Session["usuario"] as Usuario;
                    String cedSession = sessionValue.Cedula;
                    command.Parameters.AddWithValue("@sessionValue", cedSession);

                    command.ExecuteNonQuery();
                    Console.WriteLine("Trigger after_updestado_usuarios creado con éxito.");
                }
            }
        }






        public static List<Bitacora> listarBitacora()
        {
            MySqlConnection cn = new MySqlConnection(Conection.cn);
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader reader;
            cn.Open();
            cmd.Connection = cn;
            cmd.CommandText = "SELECT * FROM Bitacora";
            reader = cmd.ExecuteReader();
            List<Bitacora> cambios = new List<Bitacora>();
            while (reader.Read())
            {
                Bitacora bitacora = new Bitacora();
                bitacora.Fecha = reader.GetString("fecha");
                bitacora.Descripcion = reader.GetString("descripcion");
                bitacora.CedulaEditor = reader.GetString("usuario_editor");
                cambios.Add(bitacora);
            }
            cn.Close();
            return cambios;
        }

        public static List<Pyme> listarPymes()
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
                String blobString = reader.GetString(reader.GetOrdinal("logo"));
                byte[] temp = Convert.FromBase64String(blobString); ;
                pyme.Logo = temp;
                pyme.SitioWeb = reader.GetString("sitioWeb");
                pyme.WebSocial = reader.GetString("webSocial");
                pymes.Add(pyme);
            }
            cn.Close();
            return pymes;
        }







    }
}