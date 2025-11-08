using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemeloDigital
{
    internal partial class Program
    {
        static string ObjectToString(SimulatedObject obj, bool brief)
        {
            string s;
            if(obj == null) { s = "Ninguno"; }
            else
            {
                s = obj.Name;
                s += "(" + obj.Type + ")";

                if(!brief)
                {
                    if(obj.Type == SimulatedObjectType.Facility)
                    {
                        Facility f = SimulatorCore.AsFacility(obj);
                        s += ": Consumo: " + f.PowerConsumed;

                        if(f.Entrances.Count > 0)
                        {
                            s += ": Entrada " + ObjectReferenceToString(f.Entrances[0]);
                        }

                        if(f.Exits.Count > 0)
                        {
                            s += ": Salida " + ObjectReferenceToString(f.Exits[0]);
                        }
                    }
                    else if(obj.Type == SimulatedObjectType.Person)
                    {
                        Person p = SimulatorCore.AsPerson(obj);

                        s += ": Edad " + p.Age;
                        s += ": Altura " + p.Height;
                        s += ": Peso " + p.Weight;
                        s += ": Dinero " + p.Money;

                        s += ": Instalación " + ObjectReferenceToString(p.IsAtFacility);
                        s += ": Camino " + ObjectReferenceToString(p.IsAtPath);
                    }
                    else if(obj.Type == SimulatedObjectType.Point)
                    {
                        Point p = SimulatorCore.AsPoint(obj);
                        s += ": Posición (" + p.Position.X + ", " + p.Position.Y + ", "  + p.Position.Z + ")";
                    }
                    else if(obj.Type == SimulatedObjectType.Path)
                    {
                        Path p = SimulatorCore.AsPath(obj);

                        s += ": Punto1 " + ObjectReferenceToString(p.Point1);
                        s += ": Punto2 " + ObjectReferenceToString(p.Point2);
                    }
                }
            }

            return s;
        }

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

        static SimulatedObject PickObject(string prompt, string typeName, SimulatedObjectType type)
        {
            List<SimulatedObject> objects = SimulatorCore.FindObjectsOfType(type);

            Console.WriteLine(typeName);

            for(int i = 0; i < objects.Count; i ++)
            {
                Console.WriteLine(i + ": " + objects[i].Name);
            }
            
            int index = AskIntegerBetween(prompt, 0, objects.Count - 1);

            return objects[index];
        }

        static SimulatedObject PickObjectOrNull(string prompt, string typeName, SimulatedObjectType type)
        {
            if(SimulatorCore.CountObjectsOfType(type) == 0)
            {
                Console.WriteLine(prompt + ": Asignando nulo porque no hay objetos de tipo " + type);
                Thread.Sleep(messageWaitTime);
                return null;
            }
            else
            {
                Console.WriteLine(typeName);
                Console.WriteLine("1.- Ninguno");
                Console.WriteLine("2.- Elegir");
                int option = AskIntegerBetween("Opción", 1, 2);
                if(option == 1) { return null; }
                else { return PickObject(prompt, typeName, type); }
            }

        }
    }
}
