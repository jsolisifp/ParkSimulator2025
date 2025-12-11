using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

            FileStream fileLoad = new FileStream("saves/" + storageId, FileMode.Open, FileAccess.Read);  // point - facility-pat-person


            int countObject = 0; // Tamaño del bloque del objeto

            bytes = new byte[sizeof(int)];
            fileLoad.Read(bytes); // se acaba el fichero
            countObject = BitConverter.ToInt32(bytes); // Bloque 1 : points

            for (int i = 0; i < countObject; i++)
            {
                //guid + nombre+ longitud nombre + nombre + x + y+ z


               
            }

            fileLoad.Read(bytes);
            countObject = BitConverter.ToInt32(bytes); // Bloque 2 : facility

            for (int i = 0; i < countObject; i++)
            {
            }

            fileLoad.Read(bytes);
            countObject = BitConverter.ToInt32(bytes); // Bloque 3 : path

            for (int i = 0; i < countObject; i++)
            {
               // SimulatorCore.CreatePath(point1, point2);
            }

            fileLoad.Read(bytes);
            countObject = BitConverter.ToInt32(bytes); // Bloque 4 : person
            
            for (int i = 0; i < countObject; i++)
            {
                SimulatorCore.CreatePerson();
            }

            fileLoad.Close();
            // Point tiene posicion X posicion Y y posicion Z, lee 3 floats // Guardar las cosas en la lista de puntos



            // Crear punto 




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
