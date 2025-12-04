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

            Console.WriteLine("******** NOMBRE DE ESCENA ******** ");
            Console.WriteLine("Nombre Equipo: " + Environment.MachineName); // <-- Sacamos el nombre del equipo.
            Console.WriteLine("Fecha: " + thisDay);
            Console.WriteLine(" ");


        




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
