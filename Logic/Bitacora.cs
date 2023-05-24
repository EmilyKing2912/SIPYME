using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIPYME.Logic
{
    public class Bitacora
    {
        String fecha;
        String descripcion;
        String cedula_editor;

        public string Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        public string CedulaEditor
        {
            get { return cedula_editor; }
            set { cedula_editor = value; }
        }




        public Bitacora() { }

        public Bitacora(string fecha, string descripcion, string cedula_editor)
        {
            this.fecha = fecha;
            this.descripcion = descripcion;
            this.cedula_editor = cedula_editor;
          
        }

    }
}