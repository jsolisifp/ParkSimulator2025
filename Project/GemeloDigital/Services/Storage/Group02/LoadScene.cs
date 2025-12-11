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

            Console.WriteLine("Storage02: Load simulation" + storageId + ".sb");

            if (!File.Exists($"saves/{storageId}"))
            {
                Console.WriteLine($"La escena {storageId} no existe");
                Console.ReadLine();
                return;
            }

            FileStream fileLoad = new FileStream("saves/" + storageId +".sb", FileMode.Open, FileAccess.Read);  // point - facility-pat-person


            int countObject = 0; // Tamaño del bloque del objeto

            bytes = new byte[sizeof(int)];
            fileLoad.Read(bytes); // se acaba el fichero
            countObject = BitConverter.ToInt32(bytes); // Bloque 1 : points

            for (int i = 0; i < countObject; i++)
            {
                //guid + longitud nombre + nombre + x + y+ z
                // fichaTemporal

                Point pointTemporal = SimulatorCore.CreatePoint();
                var posTemporal = pointTemporal.Position;

                bytes = new byte[16];
                pointTemporal.Id = fileLoad.Read(bytes).ToString(); // funsiona siuhhh
                
                bytes = new byte[sizeof(int)];
                fileLoad.Read(bytes);
                int tamañoName =BitConverter.ToInt32(bytes); // tamaño

                bytes = new byte[tamañoName];
                fileLoad.Read(bytes); // name
                pointTemporal.Name = System.Text.Encoding.UTF8.GetString(bytes);

                bytes = new byte[sizeof(float)];
                fileLoad.Read(bytes);
                posTemporal.X = BitConverter.ToSingle(bytes);

                bytes = new byte[sizeof(float)];
                fileLoad.Read(bytes);
                posTemporal.Y = BitConverter.ToSingle(bytes);

                bytes = new byte[sizeof(float)];
                fileLoad.Read(bytes);
                posTemporal.Z = BitConverter.ToSingle(bytes);

           
            }

            fileLoad.Read(bytes);
            countObject = BitConverter.ToInt32(bytes); // Bloque 2 : facility

            for (int i = 0; i < countObject; i++)
            {
                bytes = new byte[16];
            }

            fileLoad.Read(bytes);
            countObject = BitConverter.ToInt32(bytes); // Bloque 3 : path

            for (int i = 0; i < countObject; i++)
            {
                byte[] bytes2 = new byte[16];
               Path pathTemporal = SimulatorCore.CreatePathWithId(id, point1, point2);
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
