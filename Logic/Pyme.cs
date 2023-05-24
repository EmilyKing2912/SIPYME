using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIPYME.Logic
{
    public class Pyme
    {
            private int id;
            private string id_usuario; 
            private string nombre;
            private int numeroTelefono;
            private string correo;
            private string descripcion;
            private string areaTrabajo;
            private string longitud;
            private string latitud;
            private string estado_pyme;
            private byte[] logo;
            private string sitioWeb;
            private string webSocial;

        public Pyme()
        {
        }

        public Pyme(int id, string id_usuario, string nombre, int numeroTelefono, string correo, string descripcion, string areaTrabajo, string longitud, string latitud, string estado_pyme, byte[] logo, string sitioWeb, string webSocial)
        {
            this.id = id;
            this.id_usuario = id_usuario;
            this.nombre = nombre;
            this.numeroTelefono = numeroTelefono;
            this.correo = correo;
            this.descripcion = descripcion;
            this.areaTrabajo = areaTrabajo;
            this.longitud = longitud;
            this.latitud = latitud;
            this.estado_pyme = estado_pyme;
            this.logo = logo;
            this.sitioWeb = sitioWeb;
            this.webSocial = webSocial;
        }
        public Pyme(int id, string nombre, int numeroTelefono, string correo, string descripcion, string areaTrabajo, string longitud, string latitud, string estado_pyme, byte[] logo, string sitioWeb, string webSocial)
        {
            this.id = id;
            this.nombre = nombre;
            this.numeroTelefono = numeroTelefono;
            this.correo = correo;
            this.descripcion = descripcion;
            this.areaTrabajo = areaTrabajo;
            this.longitud = longitud;
            this.latitud = latitud;
            this.estado_pyme = estado_pyme;
            this.logo = logo;
            this.sitioWeb = sitioWeb;
            this.webSocial = webSocial;
        }


        public int Id
            {
                get { return id; }
                set { id = value; }
            }

            public string Nombre
            {
                get { return nombre; }
                set { nombre = value; }
            }
        public string Id_usuario
        {
            get { return id_usuario; }
            set { id_usuario = value; }
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

            public string Descripcion
            {
                get { return descripcion; }
                set { descripcion = value; }
            }

            public string AreaTrabajo
            {
                get { return areaTrabajo; }
                set { areaTrabajo = value; }
            }
        public string Latitud
        {
            get { return latitud; }
            set { latitud = value; }
        }
        public string Longitud
        {
            get { return longitud; }
            set { longitud = value; }
        }
         public string Estado_pyme
            {
                get { return estado_pyme; }
                set { estado_pyme = value; }
            }

        public byte[] Logo
            {
                get { return logo; }
                set { logo = value; }
            }

            public string SitioWeb
            {
                get { return sitioWeb; }
                set { sitioWeb = value; }
            }
        public string WebSocial
        {
            get { return webSocial; }
            set { webSocial = value; }
        }

        public override bool Equals(object obj)
        {
            return obj is Pyme pyme &&
                   id == pyme.id &&
                   id_usuario == pyme.id_usuario &&
                   nombre == pyme.nombre &&
                   numeroTelefono == pyme.numeroTelefono &&
                   correo == pyme.correo &&
                   descripcion == pyme.descripcion &&
                   areaTrabajo == pyme.areaTrabajo &&
                   longitud == pyme.longitud &&
                   latitud == pyme.latitud &&
                   estado_pyme == pyme.estado_pyme &&
                   EqualityComparer<byte[]>.Default.Equals(logo, pyme.logo) &&
                   sitioWeb == pyme.sitioWeb &&
                   webSocial == pyme.webSocial;
        }

        public override int GetHashCode()
        {
            int hashCode = -1637596215;
            hashCode = hashCode * -1521134295 + id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(id_usuario);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(nombre);
            hashCode = hashCode * -1521134295 + numeroTelefono.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(correo);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(descripcion);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(areaTrabajo);
            hashCode = hashCode * -1521134295 + longitud.GetHashCode();
            hashCode = hashCode * -1521134295 + latitud.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(estado_pyme);
            hashCode = hashCode * -1521134295 + EqualityComparer<byte[]>.Default.GetHashCode(logo);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(sitioWeb);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(webSocial);
            return hashCode;
        }
    }










    }
