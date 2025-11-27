using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemeloDigital
{
    internal partial class Storage02 : Storage
    {
        internal override void LoadScene(string storageId)
        {
   
            Console.WriteLine("Storage02: Load simulation" + storageId);

            if (!File.Exists($"saves/{storageId}"))
            {
                Console.WriteLine($"La escena {storageId} no existe");
                Console.ReadLine();
                return;
            }

            FileStream fileLoad = new FileStream("saves/"+storageId, FileMode.Open, FileAccess.Read);  // point - facility-pat-person

            bytes= new byte[sizeof(int)];
            marcaFin = fileLoad.Read(bytes);

            fileLoad.Read(bytes);

            while (marcaFin!= 0) 
            {
                // Point tiene posicion X posicion Y y posicion Z, lee 3 floats // Guardar las cosas en la lista de puntos

                // Crear punto 
                SimulatorCore.CreatePoint();
                // add.Lista(punto) // simulatedObjects.add(point)

               

                // Facility

                

            }


     

            //foreach(var escena in listaEscena)
            //{
            //    Console.WriteLine("Escribe el nombre del fichero"); 
            //    Console.WriteLine($"Escena {escena}");

            //    string fichero = Console.ReadLine();
            //}

            //FileStream fileLoad = new FileStream(fichero, FileMode.Open, FileAccess.Read);
        }
    }
}
