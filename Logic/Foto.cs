using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIPYME.Logic
{
    public class Foto
    {
        private int id;
        private int pymeId;
        private byte[] cantidadByte;

        public Foto()
        {
        }
        public int PymeId
        {
            get { return pymeId; }
            set { pymeId = value; }
        }
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public byte[] CantidadByte
        {
            get { return cantidadByte; }
            set { cantidadByte = value; }
        }
        public Foto(int id, int pymeId, byte[] cantidadByte)
        {
            this.id = id;
            this.pymeId = pymeId;
            this.cantidadByte = cantidadByte;
        }

        public override bool Equals(object obj)
        {
            return obj is Foto foto &&
                   id == foto.id &&
                   pymeId == foto.pymeId &&
                   EqualityComparer<byte[]>.Default.Equals(cantidadByte, foto.cantidadByte);
        }

        public override int GetHashCode()
        {
            int hashCode = -1000350010;
            hashCode = hashCode * -1521134295 + id.GetHashCode();
            hashCode = hashCode * -1521134295 + pymeId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<byte[]>.Default.GetHashCode(cantidadByte);
            return hashCode;
        }
    }
}
