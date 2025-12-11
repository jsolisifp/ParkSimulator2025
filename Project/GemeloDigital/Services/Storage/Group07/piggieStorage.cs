using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Numerics;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace GemeloDigital
{
    internal class piggieStorage : Storage
    {
        List<string> list = new List<string>();
        List<SimulatedObject> objects;

        public string currentDirectory = Directory.GetCurrentDirectory();
        

        internal override void Initialize()
        {
            

        }
        internal override void Finish()
        {
            
        }
        internal override void newScene()
        {

        }
        internal override void LoadScene(string storageId)
        {
           
        }

        internal override void SaveScene(string storageId)
        {
            List<Path> paths = new List<Path>();
            List<Point> points = new List<Point>();
            List<Facility> facilities = new List<Facility>();
            List<Person> persons = new List<Person>();

            // TODO: cambiar de int a entero en todos los id y mandar el tamaño primero 

            objects = SimulatorCore.GetObjects();
            Directory.CreateDirectory(currentDirectory + "\\" + storageId);

            for (int i = 0; i < objects.Count; i++) 
            {
                if (objects[i].Type == SimulatedObjectType.Facility) 
                {
                    Facility facility;
                    Point pointTemp = new Point();
                    Facility temp = new Facility(pointTemp, pointTemp);

                    facility = SimulatorCore.AsFacility(objects[i]);

                    temp.Id = facility.Id;
                    temp.Name = facility.Name;
                    temp.Entrances = facility.Entrances;
                    temp.Exits = facility.Exits;
                    temp.PowerConsumed = facility.PowerConsumed;

                    facilities.Add(temp);

                }
                else if (objects[i].Type == SimulatedObjectType.Person)
                {
                    Person person;
                    Person temp = new Person();

                    person = SimulatorCore.AsPerson(objects[i]);

                    temp.Name = person.Name;
                    temp.Age = person.Age;
                    temp.Height = person.Height;
                    temp.Weight = person.Weight;
                    temp.IsAtFacility = person.IsAtFacility;
                    temp.IsAtPath = person.IsAtPath;
                    temp.Money = person.Money;
                    temp.Id = person.Id;

                    persons.Add(temp);

                }
                else if (objects[i].Type == SimulatedObjectType.Point)
                {
                    Point point;
                    Point temp = new Point();

                    point = SimulatorCore.AsPoint(objects[i]);

                    temp.Name = point.Name;
                    temp.Id = point.Id;
                    temp.Position = point.Position;

                    points.Add(temp);

                }
                else if (objects[i].Type == SimulatedObjectType.Path)
                {
                    Path path;
                    Point pointTemp = new Point();
                    Path temp = new Path(pointTemp, pointTemp);

                    path = SimulatorCore.AsPath(objects[i]);

                    temp.Name = path.Name;
                    temp.Id = path.Id;
                    temp.CapacityPersons = path.CapacityPersons;
                    temp.Point1.Id = path.Point1.Id;
                    temp.Point2.Id = path.Point2.Id;

                    paths.Add(temp);
                }

            }

            // Separar por listas y almacenar en ficheros distintos
            FileStream fichero = new FileStream(storageId + "\\Personas", FileMode.Create, FileAccess.Write);

            for (int i = 0; i < persons.Count; i++) 
            {
                byte[] bytes = new byte[sizeof(int)];
                int length = persons[i].Name.Length;
                bytes = BitConverter.GetBytes(length);
                fichero.Write(bytes);

                bytes = new byte[length];
                bytes = Encoding.ASCII.GetBytes(persons[i].Name);
                fichero.Write(bytes);

                bytes = new byte[sizeof(int)];
                bytes = BitConverter.GetBytes(persons[i].Age);
                fichero.Write(bytes);

                bytes = new byte[sizeof(float)];
                bytes = BitConverter.GetBytes(persons[i].Height);
                fichero.Write(bytes);

                bytes = new byte[sizeof(float)];
                bytes = BitConverter.GetBytes(persons[i].Weight);
                fichero.Write(bytes);

                if (persons[i].IsAtFacility != null) 
                {
                    bytes = new byte[sizeof(int)];
                    length = persons[i].IsAtFacility.Id.Length;
                    bytes = BitConverter.GetBytes(length);
                    fichero.Write(bytes);

                    bytes = new byte[length];
                    bytes = Encoding.ASCII.GetBytes(persons[i].IsAtFacility.Id);
                    fichero.Write(bytes);

                }

                if (persons[i].IsAtPath != null)
                {
                    bytes = new byte[sizeof(int)];
                    length = persons[i].IsAtPath.Id.Length;
                    bytes = BitConverter.GetBytes(length);
                    fichero.Write(bytes);

                    bytes = new byte[length];
                    bytes = Encoding.ASCII.GetBytes(persons[i].IsAtPath.Id);
                    fichero.Write(bytes);
                }

                bytes = new byte[sizeof(float)];
                bytes = BitConverter.GetBytes(persons[i].Money);
                fichero.Write(bytes);

                bytes = new byte[sizeof(int)];
                length = persons[i].Id.Length;
                bytes = BitConverter.GetBytes(length);
                fichero.Write(bytes);

                bytes = new byte[length];
                bytes = Encoding.ASCII.GetBytes(persons[i].Id);
                fichero.Write(bytes);

            }

            fichero.Close();

            fichero = new FileStream(storageId + "\\Puntos", FileMode.Create, FileAccess.Write);

            for (int i = 0; i < points.Count; i++) 
            {
                byte[] bytes = new byte[sizeof(int)];
                int length = points[i].Name.Length;
                bytes = BitConverter.GetBytes(length);
                fichero.Write(bytes);

                bytes = new byte[length];
                bytes = Encoding.ASCII.GetBytes(points[i].Name);
                fichero.Write(bytes);

                bytes = new byte[sizeof(int)];
                length = points[i].Id.Length;
                bytes = BitConverter.GetBytes(length);
                fichero.Write(bytes);

                bytes = new byte[length];
                bytes = Encoding.ASCII.GetBytes(points[i].Id);
                fichero.Write(bytes);

                bytes = new byte[sizeof(float)];
                bytes = BitConverter.GetBytes(points[i].Position.X);
                fichero.Write(bytes);

                bytes = new byte[sizeof(float)];
                bytes = BitConverter.GetBytes(points[i].Position.Y);
                fichero.Write(bytes);

                bytes = new byte[sizeof(float)];
                bytes = BitConverter.GetBytes(points[i].Position.Z);
                fichero.Write(bytes);

            }

            fichero.Close();

            

            fichero = new FileStream(storageId + "\\Paths", FileMode.Create, FileAccess.Write);

            for (int i = 0; i< paths.Count; i++)
            {
                byte[] bytes = new byte[sizeof(int)];
                int length = paths[i].Name.Length;
                bytes = BitConverter.GetBytes(length);
                fichero.Write(bytes);

                bytes = new byte[length];
                bytes = Encoding.ASCII.GetBytes(paths[i].Name);
                fichero.Write(bytes);

                bytes = new byte[sizeof(int)];
                length = paths[i].Id.Length;
                bytes = BitConverter.GetBytes(length);
                fichero.Write(bytes);

                bytes = new byte[length];
                bytes = Encoding.ASCII.GetBytes(paths[i].Id);
                fichero.Write(bytes);

                bytes = new byte[sizeof(int)];
                bytes = BitConverter.GetBytes(paths[i].CapacityPersons);
                fichero.Write(bytes);

                bytes = new byte[sizeof(int)];
                length = paths[i].Point1.Id.Length;
                bytes = BitConverter.GetBytes(length);
                fichero.Write(bytes);

                bytes = new byte[length];
                bytes = Encoding.ASCII.GetBytes(paths[i].Point1.Id);
                fichero.Write(bytes);

                bytes = new byte[sizeof(int)];
                length = paths[i].Point1.Id.Length;
                bytes = BitConverter.GetBytes(length);
                fichero.Write(bytes);

                bytes = new byte[length];
                bytes = Encoding.ASCII.GetBytes(paths[i].Point2.Id);
                fichero.Write(bytes);
            }

            fichero.Close();

            fichero = new FileStream(storageId + "\\Facilities", FileMode.Create, FileAccess.Write);

            for (int i = 0; i < facilities.Count; i++) 
            {
                byte[] bytes = new byte[sizeof(int)];
                int length = facilities[i].Name.Length;
                bytes = BitConverter.GetBytes(length);
                fichero.Write(bytes);

                bytes = new byte[length];
                bytes = Encoding.ASCII.GetBytes(facilities[i].Name);
                fichero.Write(bytes);

                bytes = new byte[sizeof(int)];
                length = facilities[i].Id.Length;
                bytes = BitConverter.GetBytes(length);
                fichero.Write(bytes);

                bytes = new byte[length];
                bytes = Encoding.ASCII.GetBytes(facilities[i].Id);
                fichero.Write(bytes);


                bytes = new byte[sizeof(int)];
                int tamaño = facilities[i].Exits.Count;
                length = facilities[i].Exits[tamaño - 1].Id.Length;
                bytes = BitConverter.GetBytes(length);
                fichero.Write(bytes);

                bytes = new byte[length];
                bytes = Encoding.ASCII.GetBytes(facilities[i].Exits[tamaño - 1].Id);
                fichero.Write(bytes);

                bytes = new byte[sizeof(int)];
                tamaño = facilities[i].Entrances.Count;
                length = facilities[i].Entrances[tamaño - 1].Id.Length;
                bytes = BitConverter.GetBytes(length);
                fichero.Write(bytes);

                bytes = new byte[length];
                bytes = Encoding.ASCII.GetBytes(facilities[i].Entrances[tamaño - 1].Id);
                fichero.Write(bytes);
            }

            list.Add(storageId);
        }

        internal override void DeleteScene(string storageId)
        {
           
            list.Remove(storageId);
        }

        internal override List<string> ListScenes()
        {
            return list;
        }

    }
}
