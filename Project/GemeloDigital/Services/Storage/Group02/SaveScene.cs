using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemeloDigital
{
    internal partial class Storage02 : Storage
    {
        internal override void SaveScene(string storageId)
        {
            FileStream ficha = new FileStream(storageId, FileMode.Create,FileAccess.Write);
            //METERLE TODO

            byte[] bytes;
            string linea = null;
            //punto
            //meter "punto"
            linea = "punto";
            BitConverter.GetBytes();



            ficha.Close();
            
            lista_storages.Add(storageId);//meter ficha a la lista
        }
    }
}
