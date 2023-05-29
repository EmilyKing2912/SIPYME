using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SIPYME.Logic;
namespace SIPYME.Models
{
    public class PymeModel
    {
        public Pyme pyme { get; set; }
        public List<Foto> fotosProducto { get; set; }
        public List<Foto> fotosPyme { get; set; }

        public PymeModel()
        {
        }
    }
}