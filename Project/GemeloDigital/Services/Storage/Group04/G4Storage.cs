using System;
using System.Collections.Generic;
using System.Linq;
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

            SimulatorCore.FindObjectsOfType() // <-- para sacar la lista de los objetos por tipos.

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
