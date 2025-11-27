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

        //FileStream fileOLD = new FileStream("fileOLD.txt", FileMode.Open, FileAccess.Read);
        //StreamReader reader = new StreamReader(fileOLD, Encoding.UTF8);
        //// Creamos el fichero donde lo escribira
        //FileStream fileNew = new FileStream("fileNEW.txt", FileMode.Create, FileAccess.Write);
        //StreamWriter writer = new StreamWriter(fileNew, Encoding.UTF8);

        public string nombreEscena;
        public string horaActual;
        

    
        internal override void Initialize()
        {
           Console.WriteLine("G4Storage: Initializing");
        }

        internal override void Finish()
        {
            //Console.WriteLine("DummyStorage: Finish");
        }

        internal override void LoadScene(string storageId)
        {
            //Console.WriteLine("DummyStorage: Load simulation" + storageId);
        }

        internal override void SaveScene(string storageId)
        {
            //Console.WriteLine("DummyStorage: Save simulation " + storageId);

            nombreEscena = storageId;

            FileStream fileOLD = new FileStream(nombreEscena, FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fileOLD, Encoding.UTF8);

            Console.WriteLine("Iniciar guardado");

            writer.WriteLine("Nombre Escena: " + nombreEscena);
            writer.WriteLine("Fecha: " + ); 




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
