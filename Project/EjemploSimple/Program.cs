using System.Security.Cryptography.X509Certificates;
using System.Numerics;

namespace GemeloDigital
{
    internal class Program
    {
        const int errorWaitTime = 2000;
        const string tab = "  ";

        static string ObjectReferenceToString(SimulatedObject obj)
        {
            string s;

            if(obj == null) { s = "Ninguno"; }
            else { s = obj.Name + "(" + obj.Type + ")"; }

            return s;
        }

        static void PrintObject(SimulatedObject obj)
        {
            Console.WriteLine(tab + "Nombre:  " + obj.Name);
            Console.WriteLine(tab + "Tipo:  " + obj.Type);

            if(obj.Type == SimulatedObjectType.Facility)
            {
                Facility f = SimulatorCore.AsFacility(obj);

                Console.WriteLine(tab + "Energia consumida (KW/h): " + f.PowerConsumed);
                Console.WriteLine(tab + "Entrada: " + (f.Entrances.Count > 0 ? ObjectReferenceToString(f.Entrances[0]) : "ninguna"));
                Console.WriteLine(tab + "Salida: " + (f.Exits.Count > 0 ? ObjectReferenceToString(f.Exits[0]) : "ninguna"));
            }
            else if(obj.Type == SimulatedObjectType.Person)
            {
                Person p = SimulatorCore.AsPerson(obj);

                Console.WriteLine(tab + "Edad: " + p.Age);
                Console.WriteLine(tab + "Altura: " + p.Height);
                Console.WriteLine(tab + "Peso: " + p.Weight);
                Console.WriteLine(tab + "Está en instalacion: " + ObjectReferenceToString(p.IsAtFacility));
                Console.WriteLine(tab + "Está en camino: " + ObjectReferenceToString(p.IsAtPath));

            }
            else if(obj.Type == SimulatedObjectType.Point)
            {
                Point p = SimulatorCore.AsPoint(obj);

                Console.WriteLine(tab + "Posicion: [" + p.Position.X + ", " + p.Position.Y + ", " + p.Position.Z + "]");
            }
            else if(obj.Type == SimulatedObjectType.Path)
            {
                Path p = SimulatorCore.AsPath(obj);

                Console.WriteLine(tab + "Punto 1: " + ObjectReferenceToString(p.Point1));
                Console.WriteLine(tab + "Punto 2: " + ObjectReferenceToString(p.Point2));

            }

        }

        static SimulatedObject ChooseObject(string prompt, string typeName, SimulatedObjectType type)
        {
            List<SimulatedObject> objects = SimulatorCore.GetObjectsOfType(type);

            Console.WriteLine(typeName);

            for(int i = 0; i < objects.Count; i ++)
            {
                Console.WriteLine(i + ": " + objects[i].Name);
            }
            
            int index = AskIntegerBetween(prompt, 0, objects.Count - 1);

            return objects[index];
        }

        static SimulatedObject ChooseObjectOrNull(string prompt, string typeName, SimulatedObjectType type)
        {
            if(SimulatorCore.CountObjectsOfType(type) == 0)
            {
                Console.WriteLine(prompt + ": Asignando nulo porque no hay objetos de tipo " + type);
                Thread.Sleep(errorWaitTime);
                return null;
            }
            else
            {
                Console.WriteLine(typeName);
                Console.WriteLine("1.- Ninguno");
                Console.WriteLine("2.- Elegir");
                int option = AskIntegerBetween("Opción", 1, 2);
                if(option == 1) { return null; }
                else { return ChooseObject(prompt, typeName, type); }
            }

        }

        static string AskString(string prompt)
        {
            string line = "";
            bool done = false;

            while(!done)
            {
                Console.Write(prompt  + ">");
                line = Console.ReadLine();
                line = line.Trim();
                if(line.Length <= 0) { Console.WriteLine("Tienes que escribir un texto"); }
                else { done = true; }
            }

            return line;
        }

        static int AskInteger(string prompt)
        {
            int number = -1;
            string line;
            bool done = false;

            while(!done)
            {
                Console.Write(prompt + "> ");
                line = Console.ReadLine();
                if(!Int32.TryParse(line, out number))
                {
                    Console.WriteLine("Introduce un número entero ");
                }
                else { done = true; }
            }

            return number;
        }

        static int AskIntegerBetween(string prompt, int minimum, int maximum)
        {

            int number = -1;
            bool done = false;

            while(!done)
            {
                number = AskInteger(prompt + "[" + minimum + "-" + maximum + "]");
                if(number >= minimum && number <= maximum) { done = true; }
                else { Console.WriteLine("Introduce un número entre " + minimum + " y " + maximum); }
            }

            return number;
        }

        static float AskSingle(string prompt)
        {
            float number = -1;
            string line;
            bool done = false;

            while(!done)
            {
                Console.Write(prompt + "> ");
                line = Console.ReadLine();
                if(!Single.TryParse(line, out number))
                {
                    Console.WriteLine("Introduce un número real");
                }
                else { done = true; }
            }

            return number;
        }

        static float AskSingleBetween(string prompt, float minimum, float maximum)
        {

            float number = -1;
            bool done = false;

            while(!done)
            {
                number = AskSingle(prompt + "[" + minimum + "-" + maximum + "]");
                if(number >= minimum && number <= maximum) { done = true; }
                else { Console.WriteLine("Introduce un número entre " + minimum + " y " + maximum); }
            }

            return number;
        }

        static void AskPersonProperties(Person p)
        {
            p.Age = AskIntegerBetween("Edad", 12, 100);
            p.Height = AskSingleBetween("Altura", 120, 200);
            p.Weight = AskSingleBetween("Peso", 35, 120);
            p.Money = AskSingleBetween("Dinero", 10, 500);

            SimulatedObject obj = ChooseObjectOrNull("Instalación", "Instalaciones", SimulatedObjectType.Facility);
            Facility facility = SimulatorCore.AsFacility(obj);
            p.IsAtFacility = facility;

            if(facility != null)
            {
                p.IsAtPath = null;
            }
            else
            {
                obj = ChooseObjectOrNull("Camino", "Caminos", SimulatedObjectType.Path);
                Path path = SimulatorCore.AsPath(obj);
                p.IsAtPath = path;
            }

        }

        static void AskPointProperties(Point p)
        {
            Vector3 position;
            position.X = AskSingle("PosX");
            position.Y = AskSingle("PosY");
            position.Z = AskSingle("PosZ");

            p.Position = position;
        }

        static void AskFacilityProperties(Facility f, bool isCreate)
        {
            f.PowerConsumed = AskSingleBetween("Energía(KW/h)", 100, 1000);

            if(!isCreate)
            {
                if(SimulatorCore.CountObjectsOfType(SimulatedObjectType.Point) > 0)
                {
                    SimulatedObject o1 = ChooseObject("Entrada", "Puntos", SimulatedObjectType.Point);
                    SimulatedObject o2 = ChooseObject("Salida", "Puntos", SimulatedObjectType.Point);

                    Point p1 = SimulatorCore.AsPoint(o1);
                    Point p2 = SimulatorCore.AsPoint(o2); 

                    f.Entrances.Clear();
                    f.Entrances.Add(p1);
                    f.Exits.Clear();
                    f.Exits.Add(p2);
                }
                else
                {
                    Console.WriteLine("No hay puntos en la escena para usar como entrada o salida");
                    Thread.Sleep(errorWaitTime);
                }

            }

        }

        static void AskPathProperties(Path p)
        {
            bool done = false;

            Point p1 = null;
            Point p2 = null;

            while(!done)
            {
                SimulatedObject o1 = ChooseObject("Punto 1", "Puntos", SimulatedObjectType.Point);
                SimulatedObject o2 = ChooseObject("Punto 2", "Puntos", SimulatedObjectType.Point);

                p1 = SimulatorCore.AsPoint(o1);
                p2 = SimulatorCore.AsPoint(o2);

                if(p1 != p2) {  done = true; }
                else { Console.WriteLine("Los puntos deben ser distintos"); }
            }

            p.Point1 = p1;
            p.Point2 = p2;
        }


        static void Main(string[] args)
        {
            SimulatorCore.Initialize();

            int option = -1;
            int menu = 0;

            SimulatedObject selectedObject = null;

            while(menu != 0 || option != 0)
            {
                Console.WriteLine(".--------------------------------------.");
                Console.WriteLine("|                                      |");
                Console.WriteLine("|            PARK SIMULATOR            |");
                Console.WriteLine("|                                      |");
                Console.WriteLine(".--------------------------------------.");
                Console.WriteLine();

                List<SimulatedObject> objects = SimulatorCore.GetObjects();


                Console.WriteLine("-------- Objetos simulados (" + objects.Count + ")  ---------");
                Console.WriteLine();
                for(int i = 0; i < objects.Count; i++)
                {
                    SimulatedObject o = objects[i];

                    string name = o.Name;
                    SimulatedObjectType type = o.Type;
                    Console.WriteLine(tab + i + ": " + name + " [" + type + "]");
                }
                Console.WriteLine();
                Console.WriteLine("-------  Objeto seleccionado   --------");
                Console.WriteLine();

                if(selectedObject == null) { Console.WriteLine(tab + "Ningún objeto seleccionado"); }
                else { PrintObject(selectedObject); }

                Console.WriteLine();
                if(menu == 0)
                {
                    Console.WriteLine("-------------- Menú principal --------------");
                    Console.WriteLine();
                    Console.WriteLine(tab + tab + "1.- Objetos");
                    Console.WriteLine(tab + tab + "2.- Simulación");
                    Console.WriteLine(tab + tab + "3.- KPIs");
                    Console.WriteLine();
                    Console.WriteLine(tab + tab + "0.- Salir");
                    Console.WriteLine();
                    Console.WriteLine("--------------------------------------------");
                    Console.WriteLine();

                    option = AskIntegerBetween("Opción", 0, 3);

                    if(option == 1) { menu = 1; }
                    else if(option == 2) { menu = 2; }
                    else if(option == 3) { menu = 3; }

                }
                else if(menu == 1)
                {
                    Console.WriteLine("-------------- Objetos --------------");
                    Console.WriteLine();
                    Console.WriteLine(tab + tab + "1.- Seleccionar");
                    Console.WriteLine(tab + tab + "2.- Crear.");
                    Console.WriteLine(tab + tab + "3.- Modificar.");
                    Console.WriteLine(tab + tab + "4.- Eliminar.");
                    Console.WriteLine();
                    Console.WriteLine(tab + tab + "0.- Atrás");
                    Console.WriteLine();
                    Console.WriteLine("--------------------------------------------");
                    Console.WriteLine();

                    option = AskIntegerBetween("Opción", 0, 4);


                    if(option == 1)
                    {
                        if(SimulatorCore.CountObjectsOfType(SimulatedObjectType.Any) <= 0)
                        {
                            Console.WriteLine("No hay objetos para seleccionar en la escena");
                            Thread.Sleep(errorWaitTime);
                        }
                        else
                        {
                            selectedObject = ChooseObject("Objeto", "Listado", SimulatedObjectType.Any);
                        }
                    }
                    else if(option == 2) { menu = 12; }
                    else if(option == 3)
                    {
                        if(selectedObject == null)
                        {
                            Console.WriteLine("Selecciona un objeto primero.");
                            Thread.Sleep(errorWaitTime);
                        }
                        else
                        {
                            selectedObject.Name = AskString("Nombre");

                            if(selectedObject.Type == SimulatedObjectType.Person)
                            {
                                Person p = SimulatorCore.AsPerson(selectedObject);
                                AskPersonProperties(p);
                            }
                            else if(selectedObject.Type == SimulatedObjectType.Facility)
                            {
                                Facility f = SimulatorCore.AsFacility(selectedObject);
                                AskFacilityProperties(f, false);
                            }
                            else if(selectedObject.Type == SimulatedObjectType.Point)
                            {
                                Point p = SimulatorCore.AsPoint(selectedObject);
                                AskPointProperties(p);
                            }
                            else if(selectedObject.Type == SimulatedObjectType.Path)
                            {
                                Path p = SimulatorCore.AsPath(selectedObject);
                                AskPathProperties(p);
                            }

                        }
                    }
                    else if(option == 4)
                    {
                        if(selectedObject == null)
                        {
                            Console.WriteLine("Selecciona un objeto primero");
                            Thread.Sleep(errorWaitTime);
                        }
                        else
                        {
                            SimulatorCore.DeleteObject(selectedObject);
                            selectedObject = null;
                            menu = 0;
                        }

                    }
                    else { menu = 0; }

                }
                else if(menu == 12)
                {
                    Console.WriteLine("-------------- Crear objeto --------------");
                    Console.WriteLine();
                    Console.WriteLine(tab + tab + "1.- Persona");
                    Console.WriteLine(tab + tab + "2.- Instalación");
                    Console.WriteLine(tab + tab + "3.- Punto");
                    Console.WriteLine(tab + tab + "4.- Camino");
                    Console.WriteLine();
                    Console.WriteLine(tab + tab + "0.- Atrás");
                    Console.WriteLine();
                    Console.WriteLine("--------------------------------------------");
                    Console.WriteLine();

                    option = AskIntegerBetween("Opción", 0, 4);

                    if(option == 1)
                    {
                        Person p = SimulatorCore.CreatePerson();

                        AskPersonProperties(p);
                    }
                    else if(option == 2)
                    {
                        if(SimulatorCore.CountObjectsOfType(SimulatedObjectType.Point) < 2)
                        {
                            Console.WriteLine("Se necesitan al menos dos puntos para poder crear una instalación");
                            Thread.Sleep(errorWaitTime);

                        }
                        else
                        {
                            SimulatedObject o1 = ChooseObject("Entrada", "Puntos", SimulatedObjectType.Point);
                            SimulatedObject o2 = ChooseObject("Salida", "Puntos", SimulatedObjectType.Point);

                            Point p1 = SimulatorCore.AsPoint(o1);
                            Point p2 = SimulatorCore.AsPoint(o2); 

                            Facility f = SimulatorCore.CreateFacility(p1, p2);

                            f.Name = AskString("Nombre");

                            AskFacilityProperties(f, true);

                            menu = 0;

                        }


                    }
                    else if(option == 3)
                    {
                        Point p = SimulatorCore.CreatePoint();
                        p.Name = AskString("Nombre");
                        
                        AskPointProperties(p);

                        menu = 0;

                    }
                    else if(option == 4)
                    {
                        List<SimulatedObject> points = SimulatorCore.GetObjectsOfType(SimulatedObjectType.Point);

                        if(points.Count > 1)
                        {
                            bool done = false;

                            Point p1 = null;
                            Point p2 = null;

                            while(!done)
                            {
                                SimulatedObject o1 = ChooseObject("Punto 1", "Puntos", SimulatedObjectType.Point);
                                SimulatedObject o2 = ChooseObject("Punto 2", "Puntos", SimulatedObjectType.Point);

                                p1 = SimulatorCore.AsPoint(o1);
                                p2 = SimulatorCore.AsPoint(o2);

                                if(p1 != p2) {  done = true; }
                                else { Console.WriteLine("Los puntos deben ser distintos"); }
                            }

                            Path path = SimulatorCore.CreatePath(p1, p2);
                            path.Name = AskString("Nombre");                            
                        
                        }
                        else
                        {
                            Console.WriteLine("No hay al menos dos puntos que unir en un camino");
                            Thread.Sleep(errorWaitTime);
                        }
                    }
                    else if(option == 0)
                    {
                        menu = 1;
                    }
                }

                Console.Clear();


            }

            SimulatorCore.Finish();

        }


    }
}
