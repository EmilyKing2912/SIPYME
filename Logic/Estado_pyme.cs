using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIPYME.Logic
{
    public class Estado_pyme
    {
        int id;
        int idPyme;
        string razon_rechazo;


        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int IdPyme
        {
            get { return idPyme; }
            set { idPyme = value; }
        }

        public string Razon_rechazo
        {
            get { return razon_rechazo; }
            set { razon_rechazo = value; }
        }

        public Estado_pyme(int id, int id_pyme, string razon_rechazo)
        {
            this.id = id;
            this.idPyme = id_pyme;
            this.razon_rechazo = razon_rechazo;
        }

        public Estado_pyme()
        {
        }
    }




}
