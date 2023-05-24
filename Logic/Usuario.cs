using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SIPYME.Data;
namespace SIPYME.Logic
{
    public class Usuario
    {

        private string cedula;
        private string nombre;
        private string apellido1;
        private string apellido2;
        private int numeroTelefono;
        private string correo;
        private string contrasena;
        private int tipo_usuario;
        private int estado_usuario;

        public string Cedula
        {
            get { return cedula; }
            set { cedula = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Apellido1
        {
            get { return apellido1; }
            set { apellido1 = value; }
        }

        public string Apellido2
        {
            get { return apellido2; }
            set { apellido2 = value; }
        }

        public int NumeroTelefono
        {
            get { return numeroTelefono; }
            set { numeroTelefono = value; }
        }

        public string Correo
        {
            get { return correo; }
            set { correo = value; }
        }

        public string Contrasena
        {
            get { return contrasena; }
            set { contrasena = value; }
        }

        public int Tipo
        {
            get { return tipo_usuario; }
            set { tipo_usuario = value; }
        }

        public int Estado
        {
            get { return estado_usuario; }
            set { estado_usuario = value; }
        }



        public Usuario() { }

        public Usuario(string apellido1, string apellido2, int numTelefono, string correo, string contrasena, int tipo, int estado)
        {
            this.apellido1 = apellido1;
            this.apellido2 = apellido2;
            this.numeroTelefono = numTelefono;
            this.correo = correo;
            this.contrasena = contrasena;
            this.tipo_usuario = tipo;
            this.estado_usuario = estado;
        }


        //public bool Equals(Usuario usuario)
        //{
        //    if (usuario == null)
        //    {
        //        return false;
        //    }

        //    return cedula == usuario.cedula &&
        //                      nombre == usuario.nombre &&
        //                      apellido1 == usuario.apellido1 &&
        //                      apellido2 == usuario.apellido2 &&
        //                      numTelefono == usuario.numTelefono &&
        //                      correo == usuario.correo &&
        //                      contrasena == usuario.contrasena &&
        //                      tipo == usuario.tipo &&
        //                      estado == usuario.estado &&
        //                      Cedula == usuario.Cedula &&
        //                      Nombre == usuario.Nombre &&
        //                      Apellido1 == usuario.Apellido1 &&
        //                      Apellido2 == usuario.Apellido2 &&
        //                      NumTelefono == usuario.NumTelefono &&
        //                      Correo == usuario.Correo &&
        //                      Contrasena == usuario.Contrasena &&
        //                      Tipo == usuario.Tipo &&
        //                      Estado == usuario.Estado;
        //}

        //public override bool Equals(object obj)
        //{
        //    if (obj == null || GetType() != obj.GetType())
        //    {
        //        return false;
        //    }

        //    Usuario other = (Usuario)obj;
        //    return Equals(other);
        //}




    }
}
