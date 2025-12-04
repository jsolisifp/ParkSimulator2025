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
            Console.WriteLine("Storage02: Initializing");
            lista_storages = new List<string>();

            string carpeta = "saves";

            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }

        }

    }
}
