using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemeloDigital
{
    internal abstract class Storage
    {
        internal abstract void Initialize();
        
        internal abstract void Finish();
        internal abstract void SaveScene(string storageId);

        internal abstract void LoadScene(string storageId);
        internal abstract void DeleteScene(string storageId);
        internal abstract List<string> ListScenes();


    }
}
