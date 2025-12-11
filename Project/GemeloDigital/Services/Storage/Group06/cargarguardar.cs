//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Numerics;
//using System.Text;

//namespace GemeloDigital
//{
//    internal class Cargarguardar : Storage
//    {
//        List<string> list = new List<string>();

//        internal override void SaveScene(string storageId)
//        {
//            Console.WriteLine("Grupo 6: Save simulation " + storageId);
//            string filename = GetCorrectFilename(storageId);
//            Console.WriteLine("Guardando archivo en: " + System.IO.Path.GetFullPath(filename));

//            FileStream fichero = new FileStream(filename, FileMode.Create, FileAccess.Write);
//            byte[] bytes;

//            List<SimulatedObject> allObjects = SimulatorCore.GetObjects();
//            List<Point> points = new List<Point>();
//            List<Facility> facilities = new List<Facility>();
//            List<Path> paths = new List<Path>();
//            List<Person> persons = new List<Person>();

//            foreach (var obj in allObjects)
//            {
//                if (obj.Type == SimulatedObjectType.Point) points.Add(SimulatorCore.AsPoint(obj));
//                else if (obj.Type == SimulatedObjectType.Facility) facilities.Add(SimulatorCore.AsFacility(obj));
//                else if (obj.Type == SimulatedObjectType.Path) paths.Add(SimulatorCore.AsPath(obj));
//                else if (obj.Type == SimulatedObjectType.Person) persons.Add(SimulatorCore.AsPerson(obj));
//            }

//            // Guardar Points
//            bytes = BitConverter.GetBytes(points.Count); fichero.Write(bytes, 0, bytes.Length);
//            foreach (var po in points)
//            {
//                byte[] idBytes = Encoding.UTF8.GetBytes(po.Id);
//                fichero.Write(BitConverter.GetBytes(idBytes.Length), 0, sizeof(int));
//                fichero.Write(idBytes, 0, idBytes.Length);
//                byte[] nameBytes = Encoding.UTF8.GetBytes(po.Name);
//                fichero.Write(BitConverter.GetBytes(nameBytes.Length), 0, sizeof(int));
//                fichero.Write(nameBytes, 0, nameBytes.Length);
//                fichero.Write(BitConverter.GetBytes(po.Position.X), 0, sizeof(float));
//                fichero.Write(BitConverter.GetBytes(po.Position.Y), 0, sizeof(float));
//                fichero.Write(BitConverter.GetBytes(po.Position.Z), 0, sizeof(float));
//            }
//            // Guardar Facilities
//            bytes = BitConverter.GetBytes(facilities.Count);
//            fichero.Write(bytes, 0, bytes.Length);
//            foreach (var f in facilities)
//            {
//                byte[] idBytes = Encoding.UTF8.GetBytes(f.Id);
//                fichero.Write(BitConverter.GetBytes(idBytes.Length), 0, sizeof(int));
//                fichero.Write(idBytes, 0, idBytes.Length);
//                byte[] nameBytes = Encoding.UTF8.GetBytes(f.Name);
//                fichero.Write(BitConverter.GetBytes(nameBytes.Length), 0, sizeof(int));
//                fichero.Write(nameBytes, 0, nameBytes.Length);
//                fichero.Write(BitConverter.GetBytes(f.PowerConsumed), 0, sizeof(float));
//                fichero.Write(BitConverter.GetBytes(f.Entrances.Count), 0, sizeof(int));
//                foreach (var ent in f.Entrances)
//                {
//                    byte[] pid = Encoding.UTF8.GetBytes(ent.Id);
//                    fichero.Write(BitConverter.GetBytes(pid.Length), 0, sizeof(int));
//                    fichero.Write(pid, 0, pid.Length);
//                }
//                fichero.Write(BitConverter.GetBytes(f.Exits.Count), 0, sizeof(int));
//                foreach (var ext in f.Exits)
//                {
//                    byte[] pid = Encoding.UTF8.GetBytes(ext.Id);
//                    fichero.Write(BitConverter.GetBytes(pid.Length), 0, sizeof(int));
//                    fichero.Write(pid, 0, pid.Length);
//                }
//            }
//        }
//    }
//}
