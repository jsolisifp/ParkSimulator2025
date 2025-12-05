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
            byte[] bytesInt = new byte[sizeof(int)];
            byte[] bytesFloat = new byte[sizeof(float)];
            byte[] bytesStr;

            // esta variable nos permite que cada escena tenga el nombre distinto referente al num que le corresponde
            string scenePointsName = storageId + "points.dat";
            FileStream streamPoints = new FileStream(scenePointsName, FileMode.Create, FileAccess.Write);

            // Sacamos la lista de puntos (ojo! devuelve lista de simulated objetcs con type punto que hay convertir a puntos)

            List<SimulatedObject> objectsPoint = SimulatorCore.FindObjectsOfType(SimulatedObjectType.Point);
            List<Point> points = new List<Point>();
            Point point;
            
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

                bytesFloat = BitConverter.GetBytes(p.Position.X);
                streamPoints.Write(bytesFloat);

                bytesFloat = BitConverter.GetBytes(p.Position.Y);
                streamPoints.Write(bytesFloat);

                bytesFloat = BitConverter.GetBytes(p.Position.Z);
                streamPoints.Write(bytesFloat);
            }

            streamPoints.Close();



            // Guardar facilities
            string sceneFacilitiesName = storageId + "facilities.dat";
            FileStream streamFacilities = new FileStream(sceneFacilitiesName, FileMode.Create, FileAccess.Write);

            // Sacamos la lista de facilities

            List<SimulatedObject> objectsFacilty = SimulatorCore.FindObjectsOfType(SimulatedObjectType.Facility);
            List<Facility> facilities = new List<Facility>();
            Facility facility;

            foreach (SimulatedObject o in objectsFacilty)
            {
                facility = SimulatorCore.AsFacility(o);
                facilities.Add(facility);
            }

            // Forma de guardar las facilities
            // Espacio en bytes del Id
            // Id (str)
            // Tamaño de bytes del nombre en UTF8
            // Nombre en UTF8
            // Float power consumed total
            // Cantidad de puntos en la lista de entrada (int)--> Entrances.Count
            // cada una de esas entradas --> int ID
            // Cantidad de puntos en la lista de salida (int)--> Exits.Count
            // cada una de esas entradas --> int ID

            foreach (Facility f in facilities)
            {
                bytesStr = System.Text.Encoding.UTF8.GetBytes(f.Id);
                streamFacilities.Write(BitConverter.GetBytes(bytesStr.Length));
                streamFacilities.Write(bytesStr);

                bytesStr = System.Text.Encoding.UTF8.GetBytes(f.Name);
                streamFacilities.Write(BitConverter.GetBytes(bytesStr.Length));
                streamFacilities.Write(bytesStr);

                bytesFloat = BitConverter.GetBytes(f.PowerConsumed);
                streamFacilities.Write(bytesFloat);

                bytesInt = BitConverter.GetBytes(f.Entrances.Count());
                streamFacilities.Write(bytesInt);
                foreach (Point p in f.Entrances)
                {
                    bytesStr = System.Text.Encoding.UTF8.GetBytes(p.Id);
                    streamFacilities.Write(BitConverter.GetBytes(bytesStr.Length));
                    streamFacilities.Write(bytesStr);
                }

                bytesInt = BitConverter.GetBytes(f.Exits.Count());
                streamFacilities.Write(bytesInt);
                foreach (Point p in f.Exits)
                {
                    bytesStr = System.Text.Encoding.UTF8.GetBytes(p.Id);
                    streamFacilities.Write(BitConverter.GetBytes(bytesStr.Length));
                    streamFacilities.Write(bytesStr);
                }
            }

            streamFacilities.Close();


            // Guardar Paths
            string scenePathsName = storageId + "paths.dat";
            FileStream streamPaths = new FileStream(scenePathsName, FileMode.Create, FileAccess.Write);

            // Sacamos la lista de paths

            List<SimulatedObject> objectsPath = SimulatorCore.FindObjectsOfType(SimulatedObjectType.Path);
            List<Path> paths = new List<Path>();
            Path path;

            foreach (SimulatedObject o in objectsPath)
            {
                path = SimulatorCore.AsPath(o);
                paths.Add(path);
            }

            // Forma de guardar los paths
            // Espacio en bytes del Id
            // Id (str)
            // Tamaño de bytes del nombre en UTF8
            // Nombre en UTF8
            // capacity persons (int)
            // espacio en bytes del Id del punto1
            // id del punto1
            // espacio en bytes del Id del punto2
            // id del punto2

            foreach(Path p in paths)
            {
                bytesStr = System.Text.Encoding.UTF8.GetBytes(p.Id);
                streamPaths.Write(BitConverter.GetBytes(bytesStr.Length));
                streamPaths.Write(bytesStr);

                bytesStr = System.Text.Encoding.UTF8.GetBytes(p.Name);
                streamPaths.Write(BitConverter.GetBytes(bytesStr.Length));
                streamPaths.Write(bytesStr);

                bytesInt = BitConverter.GetBytes(p.CapacityPersons);
                streamPaths.Write(bytesInt);

                bytesStr = System.Text.Encoding.UTF8.GetBytes(p.Point1.Id);
                streamPaths.Write(BitConverter.GetBytes(bytesStr.Length));
                streamPaths.Write(bytesStr);

                bytesStr = System.Text.Encoding.UTF8.GetBytes(p.Point2.Id);
                streamPaths.Write(BitConverter.GetBytes(bytesStr.Length));
                streamPaths.Write(bytesStr);
            }

            streamPaths.Close();


            // Guardar persons
            string scenePersonsName = storageId + "persons.dat";
            FileStream streamPersons = new FileStream(scenePersonsName, FileMode.Create, FileAccess.Write);

            // Sacamos la lista de personas

            List<SimulatedObject> objectsPerson = SimulatorCore.FindObjectsOfType(SimulatedObjectType.Person);
            List<Person> persons = new List<Person>();
            Person person;

            foreach (SimulatedObject o in objectsPerson)
            {
                person = SimulatorCore.AsPerson(o);
                persons.Add(person);
            }

            // Forma de guardar las persons
            // Espacio en bytes del Id
            // Id (str)
            // Tamaño de bytes del nombre en UTF8
            // Nombre en UTF8
            // age (int)
            // height (float)
            // weight (float)
            // money (float)
            // is at facility (? si es null ponemos LA PALABRA "null", si no es null id de la facility)
            // is at path

            foreach (Person p in persons)
            {
                bytesStr = System.Text.Encoding.UTF8.GetBytes(p.Id);
                streamPersons.Write(BitConverter.GetBytes(bytesStr.Length));
                streamPersons.Write(bytesStr);

                bytesStr = System.Text.Encoding.UTF8.GetBytes(p.Name);
                streamPersons.Write(BitConverter.GetBytes(bytesStr.Length));
                streamPersons.Write(bytesStr);

                bytesInt = BitConverter.GetBytes(p.Age);
                streamPersons.Write(bytesInt);

                bytesFloat = BitConverter.GetBytes(p.Height);
                streamPersons.Write(bytesFloat);

                bytesFloat = BitConverter.GetBytes(p.Weight);
                streamPersons.Write(bytesFloat);

                bytesFloat = BitConverter.GetBytes(p.Money);
                streamPersons.Write(bytesFloat);

                if(p.IsAtFacility == null)
                {
                    string empty = "null";
                    bytesStr = System.Text.Encoding.UTF8.GetBytes(empty);
                    streamPersons.Write(BitConverter.GetBytes(bytesStr.Length));
                    streamPersons.Write(bytesStr);
                }
                else
                {
                    bytesStr = System.Text.Encoding.UTF8.GetBytes(p.IsAtFacility.Id);
                    streamPersons.Write(BitConverter.GetBytes(bytesStr.Length));
                    streamPersons.Write(bytesStr);
                }

                if (p.IsAtPath == null)
                {
                    string empty = "null";
                    bytesStr = System.Text.Encoding.UTF8.GetBytes(empty);
                    streamPersons.Write(BitConverter.GetBytes(bytesStr.Length));
                    streamPersons.Write(bytesStr);
                }
                else
                {
                    bytesStr = System.Text.Encoding.UTF8.GetBytes(p.IsAtPath.Id);
                    streamPersons.Write(BitConverter.GetBytes(bytesStr.Length));
                    streamPersons.Write(bytesStr);
                }
            }

            streamPersons.Close();
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
