using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemeloDigital
{
    internal partial class Storage02: Storage
    {
        internal override void Initialize()
        {
            Console.WriteLine("DummyStorage: Initializing");
            lista_storages = new List<string>();






            //lista_storages.Add("Guardado1.dat");
            //lista_storages.Add("Guardado2.dat");
            //lista_storages.Add("Guardado3.dat");
        }

    }
}
