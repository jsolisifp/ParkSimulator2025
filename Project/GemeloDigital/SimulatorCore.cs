using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemeloDigital
{
    public class SimulatorCore
    {
        public string Version { get { return version; } }

        const string version = "3";

        static int step;

        static List<SimulatedObject> simulatedObjects;

        /// <summary>
        /// Inicia el sistema. Debe llamarse
        /// una vez antes de utilizar cualquier otra
        /// función.
        /// </summary>
        public static void Initialize()
        {
            simulatedObjects = new List<SimulatedObject>();

            Console.WriteLine("Simulator initialized");
        }

        /// <summary>
        /// Inicia una simulación. La simulación debe
        /// estar parada
        /// </summary>
        public static void Start()
        {
            step = 0;

            for(int i = 0; i< simulatedObjects.Count; i++)
            {
                simulatedObjects[i].Start();
            }

            Console.WriteLine("Simulation started");
        }

        /// <summary>
        /// Realiza un paso de simulación. La simulación
        /// debe estar arrancada
        /// </summary>
        public static void Step()
        {

            for (int i = 0; i < simulatedObjects.Count; i++)
            {
                simulatedObjects[i].Step();
            }

            step++;

            Console.WriteLine("Simulation stepped");
        }

        /// <summary>
        /// Para la simulación. La simulación debe estar
        /// arrancada
        /// </summary>
        public static void Stop()
        {
            for (int i = 0; i < simulatedObjects.Count; i++)
            {
                simulatedObjects[i].Stop();
            }

            Console.WriteLine("Simulation stopped");
        }


        /// <summary>
        /// Finaliza el sistema. No pueden llamarse a
        /// más funciones después de ésta.
        /// </summary>
        public static void Finish()
        {
            simulatedObjects = null;

            Console.WriteLine("Simulator finished");
        }

        public static Person CreatePerson()
        {
            Person p = new Person();

            simulatedObjects.Add(p);

            return p;
        }

        public static Facility CreateFacility()
        {
            Facility f = new Facility();
            simulatedObjects.Add(f);

            return f;
        }


        public static void DeleteObject(SimulatedObject simObject)
        {
            simulatedObjects.Remove(simObject);
        }

        public static float GetGeneralKPI(string kpi)
        {
            float total = 0;

            for(int i = 0; i < simulatedObjects.Count; i++)
            {
                total += simulatedObjects[i].GetKPI(kpi);
            }

            return total;
        }

        public static float GetObjectKPI(SimulatedObject simObj, string kpiName)
        {
            return simObj.GetKPI(kpiName);
        }

        public static void StartGeneralKPIRecording(string name)
        {
            //...
        }

        public static void StopGeneralKPIRecording(string name)
        {
            //...
        }

        public static void StartObjectKPIRecording(SimulatedObject simObj, string kpiName)
        {
            simObj.StartKPIRecording(kpiName);
        }

        public static void StopObjectKPIRecording(SimulatedObject simObj, string kpiName)
        {
            simObj.StopKPIRecording(kpiName);
        }






    }
}
