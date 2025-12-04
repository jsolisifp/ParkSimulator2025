using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemeloDigital
{
    internal partial class Storage02 : Storage
    {
        internal override void DeleteScene(string storageId)
        {

            Console.WriteLine("Deleting simulation " + storageId);
            lista_storages.Remove(storageId);
        }
    }
}
