using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIPYME.Logic
{
    public class Estado_pyme
    {
        int id;
        int id_pyme;
        string razon_rechazo;


        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int IdPyme
        {
            get { return id_pyme; }
            set { id_pyme = value; }
        }

        public string Razon_rechazo
        {
            get { return razon_rechazo; }
            set { razon_rechazo = value; }
        }

        public Estado_pyme(int id, int id_pyme, string razon_rechazo)
        {
            this.id = id;
            this.id_pyme = id_pyme;
            this.razon_rechazo = razon_rechazo;
        }


    }




}
}