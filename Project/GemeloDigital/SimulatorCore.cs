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
        /// <summary>
        /// Devuelve la versión del simulador
        /// </summary>
        public static string Version { get { return version; } }

        public static SimulatorState State { get { return state; }  }

        public static int Steps { get { return steps; } }

        public static float Time { get { return steps * Constants.hoursPerStep; } }

        const string version = "4";
        static SimulatorState state;
        static int steps;

        static List<SimulatedObject> simulatedObjects;

        /// <summary>
        /// Inicia el sistema. Debe llamarse
        /// una vez antes de utilizar cualquier otra
        /// función.
        /// </summary>
        public static void Initialize()
        {
            simulatedObjects = new List<SimulatedObject>();

            state = SimulatorState.Stopped;
        }

        /// <summary>
        /// Inicia una simulación. La simulación debe
        /// estar parada
        /// </summary>
        public static void Start()
        {
            steps = 0;

            for(int i = 0; i< simulatedObjects.Count; i++)
            {
                simulatedObjects[i].Start();
            }

            state = SimulatorState.Running;
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

            steps++;
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

            state = SimulatorState.Stopped;
        }


        /// <summary>
        /// Finaliza el sistema. No pueden llamarse a
        /// más funciones después de ésta.
        /// </summary>
        public static void Finish()
        {
            simulatedObjects = null;

        }

        public static Person CreatePerson()
        {
            Person p = new Person();
            simulatedObjects.Add(p);

            return p;
        }

        public static PersonGenerator CreatePersonGenerator()
        {
            PersonGenerator g = new PersonGenerator();

            return g;
        }

        public static Facility CreateFacility(Point entrance, Point exit)
        {
            Facility f = new Facility(entrance, exit);
            simulatedObjects.Add(f);

            return f;
        }

        public static Point CreatePoint()
        {
            Point p = new Point();
            simulatedObjects.Add(p);

            return p;
        }

        public static Path CreatePath(Point p1, Point p2)
        {
            Path p = new Path(p1, p2);
            simulatedObjects.Add(p);

            return p;
        }

        public static List<SimulatedObject> GetObjects()
        {
            return simulatedObjects;
        }

        public static int CountObjectsOfType(SimulatedObjectType type)
        {
            int count = 0;

            for(int i = 0; i < simulatedObjects.Count; i++)
            {
                SimulatedObject o = simulatedObjects[i];
                if(o.Type == type || type == SimulatedObjectType.Any) { count ++; }
            }

            return count;
        }

        public static List<SimulatedObject> GetObjectsOfType(SimulatedObjectType type)
        {
            List<SimulatedObject> objects = new List<SimulatedObject>();
            
            for(int i = 0; i < simulatedObjects.Count; i++)
            {
                SimulatedObject o = simulatedObjects[i];
                if(o.Type == type || type == SimulatedObjectType.Any) { objects.Add(o); }
            }

            return objects;
        }

        public static Person AsPerson(SimulatedObject obj)
        {
            return (Person)obj;
        }

        public static Facility AsFacility(SimulatedObject obj)
        {
            return (Facility)obj;
        }

        public static Point AsPoint(SimulatedObject obj)
        {
            return (Point)obj;
        }

        public static Path AsPath(SimulatedObject obj)
        {
            return (Path)obj;
        }

        public static void DeleteObject(SimulatedObject obj)
        {
            simulatedObjects.Remove(obj);
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
