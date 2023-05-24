using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIPYME.Logic
{
    public class AreaTrabajo
    {
        int identificador;
        String nombre_area;

        public int Identificador
        {
            get { return identificador; }
            set { identificador = value; }
        }

        public string Nombre_area
        {
            get { return nombre_area; }
            set { nombre_area = value; }
        }
        public AreaTrabajo() { }
        public AreaTrabajo(int ident, String nom)
        {
            identificador = ident;
            nombre_area = nom;
        }

    }
}