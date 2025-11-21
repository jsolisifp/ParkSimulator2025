using System;
using System.Collections.Generic;
using System.Linq;
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
            list.Add(storageId);
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
