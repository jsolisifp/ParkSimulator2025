using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GemeloDigital
{
    internal class BinaryFileStorage : Storage
    {
        List<string> list = new List<string>();

        internal override void Initialize()
        {
            //Console.WriteLine("BinaryFileStorage: Initializing");

            list = new List<string>();

            list.Add("TestScene1");
            list.Add("TestScene2");
            list.Add("TestScene3");
        }

        internal override void Finish()
        {
            //Console.WriteLine("BinaryFileStorage: Finish");
        }

        internal override void LoadScene(string storageId)
        {
            //Console.WriteLine("BinaryFileStorage: Load simulation" + storageId);
        }

        internal override void SaveScene(string storageId)
        {
            //Console.WriteLine("BinaryFileStorage: Save simulation " + storageId);

            // Guardar puntos
            byte[] bytesNum = new byte[sizeof(int)];
            byte[] bytesStr;

            string scenePointsName = storageId + "points.dat";
            FileStream streamPoints = new FileStream(scenePointsName, FileMode.Create, FileAccess.Write);

            // Sacamos la lista de puntos (ojo! devuelve lista de simulated objetcs con type punto que hay convertir a puntos)

            List<SimulatedObject> objectsPoint = SimulatorCore.FindObjectsOfType(SimulatedObjectType.Point);
            List<Point> points = new List<Point>(); 
            Point point = new Point();
            
            foreach (SimulatedObject o in objectsPoint)
            {
               point = SimulatorCore.AsPoint(o);
               points.Add(point);
            }

            // Forma de guardar los puntos
            // Espacio en bytes del Id
            // Id (str)
            // Tamaño de bytes del nombre en UTF8
            // Nombre en UTF8
            // coordenadas en float (orden X,Y,Z)
            foreach(Point p in points)
            {
                bytesStr = System.Text.Encoding.UTF8.GetBytes(p.Id);
                streamPoints.Write(BitConverter.GetBytes(bytesStr.Length));
                streamPoints.Write(bytesStr);

                bytesStr = System.Text.Encoding.UTF8.GetBytes(p.Name);
                streamPoints.Write(BitConverter.GetBytes(bytesStr.Length));
                streamPoints.Write(bytesStr);

                bytesNum = BitConverter.GetBytes(p.Position.X);

                bytesNum = BitConverter.GetBytes(p.Position.Y);

                bytesNum = BitConverter.GetBytes(p.Position.Z);
            }

            streamPoints.Close();
            
        }

        internal override void DeleteScene(string storageId)
        {
            //Console.WriteLine("Deleting simulation " + storageId);
            list.Remove(storageId);
        }

        internal override List<string> ListScenes()
        {
            return list;list = new List<string>();
        }


    }
}
