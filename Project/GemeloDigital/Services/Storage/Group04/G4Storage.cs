using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace GemeloDigital
{
    internal class G4Storage : Storage
    {
        List<string> list = new List<string>();
      

        public string nombreEscena;
        public string horaActual;

        internal override void Initialize()
        {
           Console.WriteLine("G4Storage: Initializing");
        }

        internal override void Finish()
        {
            Console.WriteLine("G4Storage: Finish");
        }

        internal override void LoadScene(string storageId)
        {
            Console.WriteLine("G4Storge: Load simulation" + storageId);
        }

        internal override void SaveScene(string storageId)
        {
            Console.WriteLine("G4Storge: Save simulation " + storageId);

            nombreEscena = storageId;

            FileStream fileOLD = new FileStream(nombreEscena, FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fileOLD, Encoding.UTF8);
            DateTime thisDay = DateTime.Now; // <-- Sacamos la fecha.

            // List<SimulatedObject> nombreLista = SimulatorCore.FindObjectsOfType(SimulatedObjectType.Objeto);
            List<SimulatedObject> listPointsObj   =  SimulatorCore.FindObjectsOfType(SimulatedObjectType.Point); 
            List<SimulatedObject> listFacilityObj =  SimulatorCore.FindObjectsOfType(SimulatedObjectType.Facility); 
            List<SimulatedObject> listPathObj     =  SimulatorCore.FindObjectsOfType(SimulatedObjectType.Path); 
            List<SimulatedObject> listPersonObj   =  SimulatorCore.FindObjectsOfType(SimulatedObjectType.Person); 


            // Creamos las listas 
            List<Point> pointList = new List<Point>();
            Point p;
            foreach (SimulatedObject obj in listPointsObj)
            {
                p = SimulatorCore.AsPoint(obj);
                pointList.Add(p);
            }

            List<Facility> facilitiesList = new List<Facility>();
            Facility f;
            foreach (SimulatedObject obj in listFacilityObj)
            {
                f = SimulatorCore.AsFacility(obj);
                facilitiesList.Add(f);
            }

            List<Path> pathList = new List<Path>();
            Path pt;
            foreach (SimulatedObject obj in listPathObj)
            { 
                pt = SimulatorCore.AsPath(obj);
                pathList.Add(pt);
            }

            List<Person> personList = new List<Person>();
            Person person;
            foreach (SimulatedObject obj in listPersonObj)
            { 
                person = SimulatorCore.AsPerson(obj);
                personList.Add(person);
            }


            writer.WriteLine("******** NOMBRE DE ESCENA ******** ");
            writer.WriteLine("Nombre Equipo: " + Environment.MachineName); // <-- Sacamos el nombre del equipo.
            writer.WriteLine("Fecha: " + thisDay);
            writer.WriteLine("\n *** INFO ***");
            writer.WriteLine("\n Personas: ");

            
            
            writer.WriteLine("Instalción " +  IsAtFacility);
            writer.WriteLine("Camino: " + IsAtPth);
            writer.WriteLine("Edad: " + Age);
            writer.WriteLine("Altura: " + Height);
            writer.WriteLine("Peso: " + Weight);
            writer.WriteLine("Dinero: " + Money);
            writer.WriteLine("\n Path: ");
            writer.WriteLine(" Punto 1: " + Point1);
            writer.WriteLine(" Punto 2: " + Point2);
            writer.WriteLine("\n *** FIN *** ");
            
        }

        internal override void DeleteScene(string storageId)
        {
            //Console.WriteLine("Deleting simulation " + storageId);
           
        }

        internal override List<string> ListScenes()
        {
            return list; list = new List<string>();
        }
    }
}
