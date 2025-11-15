using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GemeloDigital.Services.MemoryStorage
{
    internal class MemoryStorage : Storage
    {
        Dictionary<string, MemoryStream> scenes;

        internal override void Initialize()
        {
            scenes = new Dictionary<string, MemoryStream>();
        }

        internal override void Finish()
        {
            foreach(KeyValuePair<string, MemoryStream> s in scenes)
            {
                s.Value.Dispose();
            }
        }

        internal override List<string> ListScenes()
        {
            List<string> names = scenes.Keys.ToList();

            names.Sort();

            return names;
        }

        internal override void LoadScene(string storageId)
        {
            Debug.Assert(scenes.ContainsKey(storageId), "Trying to load an scene that doesn't exist (storageId:" + storageId + ")");

            Stream stream = scenes[storageId];

            StreamSerializer serializer;
            serializer = new StreamSerializer(stream, Constants.serializerBufferInitialSize);

            int pointsCount = serializer.GetInt();
            for(int i = 0; i < pointsCount; i++)
            {
                string id = serializer.GetString();
                SimulatedObjectType type = (SimulatedObjectType)serializer.GetInt();
                Debug.Assert(type == SimulatedObjectType.Point);
                string name = serializer.GetString();
                Vector3 position = serializer.GetVector3();

                Point p = SimulatorCore.CreatePointWithId(id);
                p.Name = name;
                p.Position = position;                

            }

            int pathsCount = serializer.GetInt();
            for(int i = 0; i < pathsCount; i++)
            {
                string id = serializer.GetString();
                SimulatedObjectType type = (SimulatedObjectType)serializer.GetInt();
                Debug.Assert(type == SimulatedObjectType.Path);
                string name = serializer.GetString();
                int capacity = serializer.GetInt();
                string p1Id = serializer.GetString();
                string p2Id = serializer.GetString();

                SimulatedObject? o1 = SimulatorCore.FindObjectById(p1Id);
                Debug.Assert(o1 != null);
                Point p1 = SimulatorCore.AsPoint(o1);

                SimulatedObject? o2 = SimulatorCore.FindObjectById(p2Id);
                Debug.Assert(o2 != null);
                Point p2 = SimulatorCore.AsPoint(o2);

                Path p = SimulatorCore.CreatePathWithId(id, p1, p2);
                p.Name = name;
                p.CapacityPersons = capacity;

            }

            int facilitiesCount = serializer.GetInt();
            for(int i = 0; i < facilitiesCount; i++)
            {
                string id = serializer.GetString();
                SimulatedObjectType type = (SimulatedObjectType)serializer.GetInt();
                Debug.Assert(type == SimulatedObjectType.Facility);
                string name = serializer.GetString();
                float power = serializer.GetFloat();

                List<string> entrancesIds = serializer.GetReferenceList();
                List<string> exitsIds = serializer.GetReferenceList();

                Debug.Assert(entrancesIds.Count > 0);
                Debug.Assert(exitsIds.Count > 0);

                List<Point> entrances = new List<Point>();
                foreach(string entranceId in entrancesIds)
                {
                    SimulatedObject o = SimulatorCore.FindObjectById(entranceId);
                    Point p = SimulatorCore.AsPoint(o);
                    entrances.Add(p);
                }

                List<Point> exits = new List<Point>();
                foreach(string exitId in exitsIds)
                {
                    SimulatedObject o = SimulatorCore.FindObjectById(exitId);
                    Point p = SimulatorCore.AsPoint(o);
                    exits.Add(p);
                }

                Facility f = SimulatorCore.CreateFacilityWithId(id, entrances[0], exits[0]);
                f.Name = name;
                f.PowerConsumed = power;
                f.Entrances = entrances;
                f.Exits = exits;
            }

            int personCount = serializer.GetInt();
            for(int i = 0; i < personCount; i++)
            {
                string id = serializer.GetString();
                SimulatedObjectType type = (SimulatedObjectType)serializer.GetInt();
                Debug.Assert(type == SimulatedObjectType.Person);
                string name = serializer.GetString();
                int age = serializer.GetInt();
                float height = serializer.GetFloat();
                float weight = serializer.GetFloat();
                float money = serializer.GetFloat();
                string facilityId = serializer.GetReference();
                string pathId = serializer.GetReference();

                SimulatedObject? oF = facilityId.Length == 0 ? null : SimulatorCore.FindObjectById(facilityId);
                Facility? f = SimulatorCore.AsFacility(oF);

                SimulatedObject? oP = pathId.Length == 0 ? null : SimulatorCore.FindObjectById(pathId);
                Path? p = SimulatorCore.AsPath(oP);

                Person person = SimulatorCore.CreatePersonWithId(id);
                person.Name = name;
                person.Age = age;
                person.Height = height;
                person.Weight = weight;
                person.Money = money;
                person.IsAtFacility = f;
                person.IsAtPath = p;

            }

            stream.Position = 0;
        }

        internal override void SaveScene(string storageId)
        {
            if(!scenes.ContainsKey(storageId)) { scenes.Add(storageId, new()); }

            Stream stream = scenes[storageId];
            stream.SetLength(0);

            StreamSerializer serializer;
            serializer = new StreamSerializer(stream, Constants.serializerBufferInitialSize);


            List<SimulatedObject> points = SimulatorCore.FindObjectsOfType(SimulatedObjectType.Point);
            serializer.AddInt(points.Count);
            foreach(SimulatedObject o in points)
            {
                Point p = SimulatorCore.AsPoint(o);

                serializer.AddString(p.Id);
                serializer.AddInt((int)p.Type);
                serializer.AddString(p.Name);
                serializer.AddVector3(p.Position);
            }

            List<SimulatedObject> paths = SimulatorCore.FindObjectsOfType(SimulatedObjectType.Path);
            serializer.AddInt(paths.Count);
            foreach(SimulatedObject o in paths)
            {
                Path p = SimulatorCore.AsPath(o);

                serializer.AddString(p.Id);
                serializer.AddInt((int)p.Type);
                serializer.AddString(p.Name);
                serializer.AddInt(p.CapacityPersons);
                serializer.AddReference<Point>(p.Point1);
                serializer.AddReference<Point>(p.Point2);
            }

            List<SimulatedObject> facilities = SimulatorCore.FindObjectsOfType(SimulatedObjectType.Facility);
            serializer.AddInt(facilities .Count);
            foreach(SimulatedObject o in facilities )
            {
                Facility f = SimulatorCore.AsFacility(o);

                serializer.AddString(f.Id);
                serializer.AddInt((int)f.Type);
                serializer.AddString(f.Name);
                serializer.AddSingle(f.PowerConsumed);
                serializer.AddReferenceList<Point>(f.Entrances);
                serializer.AddReferenceList<Point>(f.Exits);
            }

            List<SimulatedObject> persons = SimulatorCore.FindObjectsOfType(SimulatedObjectType.Person);
            serializer.AddInt(persons .Count);
            foreach(SimulatedObject o in persons)
            {
                Person p = SimulatorCore.AsPerson(o);

                serializer.AddString(p.Id);
                serializer.AddInt((int)p.Type);
                serializer.AddString(p.Name);
                serializer.AddInt(p.Age);
                serializer.AddSingle(p.Height);
                serializer.AddSingle(p.Weight);
                serializer.AddSingle(p.Money);
                serializer.AddReference<Facility?>(p.IsAtFacility);
                serializer.AddReference<Path?>(p.IsAtPath);
            }

            byte[] dump = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(dump);
            File.WriteAllBytes("dump.bin", dump);

            stream.Position = 0;

        }

        internal override void DeleteScene(string storageId)
        {
            scenes[storageId].Dispose();
            scenes.Remove(storageId);
        }

    }
}
