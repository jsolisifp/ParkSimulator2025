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
            //Console.WriteLine("DummyStorage: Save simulation " + storageId);
            lista_storages.Add(storageId);
        }
    }
}
