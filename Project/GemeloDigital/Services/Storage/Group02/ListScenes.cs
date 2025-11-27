using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemeloDigital
{
    internal partial class Storage02 : Storage
    {
        internal override List<string> ListScenes()
        {
            return lista_storages; lista_storages = new List<string>();
        }
    }
}
