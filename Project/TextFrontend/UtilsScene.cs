using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemeloDigital
{
    internal partial class Program
    {
        static string? PickScene()
        {
            string? result = null;
            List<string> scenes = SimulatorCore.ListScenes();

            if(scenes.Count > 0)
            {
                int i = 0;
                foreach(string s in scenes)
                {
                    Console.WriteLine(i + ": " + s);
                    i++;
                }

                int option = AskIntegerBetween("Escena", 0, scenes.Count - 1);

                result = scenes[option];
            }

            return result;
        }
    }
}
