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

            if (!File.Exists("saves/" + storageId))
            {
                Console.WriteLine($"La escena {storageId} no existe");
                Console.ReadLine();
                return;
            }

            FileStream ficha = new FileStream("saves/"+storageId, FileMode.Create,FileAccess.Write);
            //METERLE cosas
            //punto
            
            List<SimulatedObject> points = SimulatorCore.FindObjectsOfType(SimulatedObjectType.Point);
            foreach (SimulatedObject point in points)
            {
                Point p = SimulatorCore.AsPoint(point);
                SimulatorCore.simulatedObjects.Add(p); //<-- No hay permisos
            }




            ficha.Close();
            
            lista_storages.Add(storageId);//meter ficha a la lista
        }
    }
}
