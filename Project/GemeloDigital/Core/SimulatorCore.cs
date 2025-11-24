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

        /// <summary>
        /// Devuelve el estado del simulador
        /// </summary>
        public static SimulatorState State { get { return state; }  }

        /// <summary>
        /// Devuelve los pasos que se han simulado desde el último start
        /// </summary>
        public static int Steps { get { return steps; } }

        /// <summary>
        /// Devuelve el tiempo en horas que se ha simulado desde el último start
        /// </summary>
        public static float Time { get { return steps * Constants.hoursPerStep; } }

        const string version = "4";
        static SimulatorState state;
        static int steps;

        static List<SimulatedObject> simulatedObjects;

        static Storage storage;

        static RealtimeStorage realtimeStorage;

        /// <summary>
        /// Inicia el simulador. Debe llamarse
        /// una vez antes de utilizar cualquier otra
        /// función de esta clase.
        /// </summary>
        public static void Initialize()
        {
            simulatedObjects = new List<SimulatedObject>();

            state = SimulatorState.Stopped;

            storage = new DummyStorage();

            storage.Initialize();

            realtimeStorage = new DummyRealtimeStorage();

            realtimeStorage.Initialize();
        }

        /// <summary>
        /// Arranca una simulación. El estado del simulador
        /// debe ser Stopped
        /// </summary>
        public static void Start()
        {
            steps = 0;

            for(int i = 0; i< simulatedObjects.Count; i++)
            {
                simulatedObjects[i].Start();
            }

            realtimeStorage.Start();

            state = SimulatorState.Running;
        }

        /// <summary>
        /// Ejecuta un paso de simulación. El estado del simulador
        /// debe ser Running
        /// </summary>
        public static void Step()
        {

            for (int i = 0; i < simulatedObjects.Count; i++)
            {
                simulatedObjects[i].Step();
            }

            realtimeStorage.Step();

            steps++;
        }

        /// <summary>
        /// Detiene la simulación. El estado del simulador debe
        /// ser running
        /// </summary>
        public static void Stop()
        {
            realtimeStorage.Stop();

            for (int i = 0; i < simulatedObjects.Count; i++)
            {
                simulatedObjects[i].Stop();
            }


            state = SimulatorState.Stopped;
        }


        /// <summary>
        /// Finaliza el simulador. No pueden llamarse a
        /// más funciones de la clase después de ésta.
        /// </summary>
        public static void Finish()
        {
            storage.Finish();
            realtimeStorage.Finish();

            simulatedObjects = null;

        }


        /// <summary>
        /// Crea una persona con propiedades por defecto y la 
        /// añade al listado de objetos simulados
        /// </summary>
        /// <returns>La persona creada</returns>
        public static Person CreatePerson()
        {
            Person p = new Person();
            simulatedObjects.Add(p);

            return p;
        }

        /// <summary>
        /// Crea un objeto que permite crear personas fácilmente
        /// con unas propiedades aleatorizadas
        /// </summary>
        /// <returns>El generador de personas</returns>
        public static PersonGenerator CreatePersonGeneratorUtility()
        {
            PersonGenerator g = new PersonGenerator();

            return g;
        }

        /// <summary>
        /// Crea una instalación con propiedades por defecto y la 
        /// añade al listado de objetos simulados
        /// </summary>
        /// <returns>La instalación creada</returns>
        public static Facility CreateFacility(Point entrance, Point exit)
        {
            Facility f = new Facility(entrance, exit);
            simulatedObjects.Add(f);

            return f;
        }

        /// <summary>
        /// Crea un punto con propiedades por defecto y lo
        /// añade al listado de objetos simulados
        /// </summary>
        /// <returns>El punto creado</returns>
        public static Point CreatePoint()
        {
            Point p = new Point();
            simulatedObjects.Add(p);

            return p;
        }

        /// <summary>
        /// Crea un camino entre los dos puntos, que deben ser diferentes
        /// </summary>
        /// <param name="p1">Primer punto que conecta el camino</param>
        /// <param name="p2">Segundo punto que conecta el camino</param>
        /// <returns>El camino creado</returns>
        public static Path CreatePath(Point p1, Point p2)
        {
            Path p = new Path(p1, p2);
            simulatedObjects.Add(p);

            return p;
        }

        /// <summary>
        /// Elimina un objeto del listado de objetos simulados
        /// </summary>
        /// <param name="obj">El objeto a eliminar</param>
        public static void DeleteObject(SimulatedObject obj)
        {
            simulatedObjects.Remove(obj);
        }

        /// <summary>
        /// Devuelve el listado de objetos simulados
        /// </summary>
        /// <returns>El listado de objetos simulados</returns>
        public static List<SimulatedObject> GetObjects()
        {
            return simulatedObjects;
        }


        /// <summary>
        /// Cuenta los objetos de un cierto tipo que existen en la lista
        /// de objetos simulados
        /// </summary>
        /// <param name="type">Tipo de objeto a contar</param>
        /// <returns>Cantidad de objetos encontrados</returns>
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

        /// <summary>
        /// Busca los objetos de un cierto tipo y los devuelve en una lista
        /// </summary>
        /// <param name="type">Tipo de objetos a recopilar</param>
        /// <returns>Objetos del tipo encontrados</returns>
        public static List<SimulatedObject> FindObjectsOfType(SimulatedObjectType type)
        {
            List<SimulatedObject> objects = new List<SimulatedObject>();
            
            for(int i = 0; i < simulatedObjects.Count; i++)
            {
                SimulatedObject o = simulatedObjects[i];
                if(o.Type == type || type == SimulatedObjectType.Any) { objects.Add(o); }
            }

            return objects;
        }

        /// <summary>
        /// Convierte el objeto en una persona.
        /// </summary>
        /// <param name="obj">El objeto a convertir</param>
        /// <returns>El objeto convertido en persona</returns>
        public static Person AsPerson(SimulatedObject obj)
        {
            return (Person)obj;
        }

        /// <summary>
        /// Convierte el objeto en una instalación.
        /// </summary>
        /// <param name="obj">El objeto a convertir</param>
        /// <returns>El objeto convertido en instalación</returns>
        public static Facility AsFacility(SimulatedObject obj)
        {
            return (Facility)obj;
        }

        /// <summary>
        /// Convierte el objeto en un punto.
        /// </summary>
        /// <param name="obj">El objeto a convertir</param>
        /// <returns>El objeto convertido en punto</returns>
        public static Point AsPoint(SimulatedObject obj)
        {
            return (Point)obj;
        }

        /// <summary>
        /// Convierte el objeto en un camino.
        /// </summary>
        /// <param name="obj">El objeto a convertir</param>
        /// <returns>El objeto convertido en un camino</returns>
        public static Path AsPath(SimulatedObject obj)
        {
            return (Path)obj;
        }

        /// <summary>
        /// Devuelve el valor de un KPI general.
        /// Sólo puede llamarse con la simulación corriendo.
        /// </summary>
        /// <param name="kpi">Nombre del KPI</param>
        /// <returns>Valor del KPI</returns>
        public static float GetGeneralKPI(string kpi)
        {
            float total = 0;

            for(int i = 0; i < simulatedObjects.Count; i++)
            {
                total += simulatedObjects[i].GetKPI(kpi);
            }

            return total;
        }

        /// <summary>
        /// Devuelve el valor de un KPI de un objeto
        /// Sólo puede llamarse con la simulación corriendo.
        /// </summary>
        /// <param name="obj">El objeto</param>
        /// <param name="kpi">Nombre del KPI</param>
        /// <returns></returns>
        public static float GetObjectKPI(SimulatedObject obj, string kpi)
        {
            return obj.GetKPI(kpi);
        }

        /// <summary>
        /// Habilita la grabación de un KPI general
        /// Sólo puede llamarse con la simulación parada.
        /// </summary>
        /// <param name="kpi"></param>
        public static void TrackGeneralKPI(string kpi)
        {
            realtimeStorage.TrackGeneralKPI(kpi);
        }

        /// <summary>
        /// Deshabilita la grabación de un KPI general
        /// Sólo puede llamarse con la simulación parada.
        /// </summary>
        /// <param name="kpi"></param>
        public static void UntrackGeneralKPI(string kpi)
        {
            realtimeStorage.UntrackGeneralKPI(kpi);
        }

        /// <summary>
        /// Habilita la grabación de un KPI de objeto.
        /// Sólo puede llamarse con la simulación parada.
        /// </summary>
        /// <param name="simObj">El objeto</param>
        /// <param name="kpi">El KPI</param>
        public static void TrackObjectKPI(SimulatedObject simObj, string kpi)
        {
            realtimeStorage.TrackObjectKPI(simObj.Id, kpi);
        }

        /// <summary>
        /// Deshabilita la grabación de un KPI de objeto.
        /// Sólo puede llamarse con la simulación parada.
        /// </summary>
        /// <param name="simObj"></param>
        /// <param name="kpi"></param>
        public static void UntrackObjectKPI(SimulatedObject simObj, string kpi)
        {
            realtimeStorage.UntrackObjectKPI(simObj.Id, kpi);
        }

        /// <summary>
        /// Deshabilita todas la grabaciones de KPIs 
        /// Sólo puede llamarse con la simulación parada.
        /// </summary>
        public static void UntrackAllKPIs()
        {
            realtimeStorage.DisableAllKPIRecordings();
        }

        /// <summary>
        /// Devuelve una lista de todos los registros grabados para
        /// un kpi general en el intervalo de tiempo especificado.
        /// </summary>
        /// <param name="kpi"></param>
        /// <param name="fromTime"></param>
        /// <param name="toTime">Si es -1 se devolverán todos los registros disponibles</param>
        /// <returns>Los registros o una lista vacía si no existe ninguno</returns>
        public static List<KPIRecord> GetGeneralKPIRecords(string kpi, float fromTime, float toTime)
        {
            return realtimeStorage.GetGeneralKPIRecords(kpi, fromTime, toTime);
        }

        /// <summary>
        /// Devuelve una lista de todos los registros para
        /// un kpi de objeto en el intervalo de tiempo especificado
        /// </summary>
        /// <param name="simObjId"></param>
        /// <param name="kpi"></param>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <returns></returns>
        public static List<KPIRecord> GetObjectKPIRecords(string simObjId, string kpi, float fromTime, float toTime)
        {
            return realtimeStorage.GetObjectKPIRecords(simObjId, kpi, fromTime, toTime);
        }

        /// <summary>
        /// Permite consultar información acerca de última grabación
        /// </summary>
        /// <returns></returns>
        public static KPIRecordingInfo GetKPIRecordingInfo()
        {
            return realtimeStorage.GetKPIRecordingInfo();
        }

        /// <summary>
        /// Sustituye los registros de KPIs actuales por los de la grabación especificada
        /// </summary>
        /// <param name="recordingId"></param>
        public static void LoadKPIRecording(string recordingId)
        {
            realtimeStorage.LoadKPIRecording(recordingId);
        }

        /// <summary>
        /// Lista las grabaciones de KPIs disponibles para la escena actual
        /// </summary>
        /// <returns>Los ids de las grabaciones</returns>
        public static List<string> ListKPIRecordings()
        {
            return realtimeStorage.ListKPIRecordings();
        }

        /// <summary>
        /// Devuelve una estructura con información sobre la grabación de kpis
        /// especificada.
        /// </summary>
        /// <param name="recordingId"></param>
        /// <returns></returns>
        internal KPIRecordingInfo GetKPIRecordingInfo(string recordingId)
        {
            return realtimeStorage.GetKPIRecordingInfo(recordingId);
        }

        /// <summary>
        /// Elimina la grabación de kpis especificada
        /// </summary>
        /// <param name="recordingId"></param>
        internal void DeleteKPIRecording(string recordingId)
        {
            realtimeStorage.DeleteKPIRecording(recordingId);
        }

        /// <summary>
        /// Deja la simulación en su estado inicial.
        /// La simulación debe estar parada para poder llamar
        /// a esta función
        /// </summary>
        public static void NewScene()
        {
            simulatedObjects.Clear();

            realtimeStorage.NewScene();
        }

        /// <summary>
        /// Carga una simulación del storage
        /// La simulación debe estar parada para poder llamar
        /// a esta función
        /// </summary>
        public static void LoadScene(string storageId)
        {
            simulatedObjects.Clear();

            storage.LoadScene(storageId);
            realtimeStorage.LoadScene(storageId);
        }

        /// <summary>
        /// Guarda una simulación en el storage
        /// La simulación debe estar parada para poder llamar
        /// a esta función
        /// </summary>
        public static void SaveScene(string storageId)
        {
            storage.SaveScene(storageId);
            realtimeStorage.SaveScene(storageId);
        }

        /// <summary>
        /// Devuelve la lista de simulaciones almacenadas
        /// </summary>
        /// <returns></returns>
        public static List<string> ListScenes()
        {
            return storage.ListScenes();
        }

        /// <summary>
        /// Elimina una simulación almacenada
        /// </summary>
        public static void DeleteScene(string storageId)
        {
            storage.DeleteScene(storageId);
            realtimeStorage.DeleteScene(storageId);
        }

        internal static SimulatedObject? FindObjectById(string id)
        {
            SimulatedObject? result = null;
            int i = 0;

            while(result == null && i < simulatedObjects.Count)
            {
                SimulatedObject o = simulatedObjects[i];
                if(o.Id == id) { result = o; } 
                else { i++; }
            }

            return result;
        }

        internal static Point CreatePointWithId(string id)
        {
            Point p = CreatePoint();
            p.Id = id;

            return p;
        }

        internal static Path CreatePathWithId(string id, Point point1, Point point2)
        {
            Path p = CreatePath(point1, point2);
            p.Id = id;

            return p;
        }

        internal static Facility CreateFacilityWithId(string id, Point entrance, Point exit)
        {
            Facility f = CreateFacility(entrance, exit);
            f.Id = id;

            return f;
        }

        internal static Person CreatePersonWithId(string id)
        {
            Person p = CreatePerson();
            p.Id = id;

            return p;
        }
        
    }
}
