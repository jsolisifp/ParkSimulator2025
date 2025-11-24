using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemeloDigital
{
    internal class DummyRealtimeStorage : RealtimeStorage
    {
        internal struct TrackedObjectInfo
        {
            internal string Id { get; set; }
            internal string Name { get; set; }
            internal SimulatedObjectType Type { get; set; }
        }

        // Tracked

        HashSet<string> trackedGeneralKpis;
        Dictionary<string, HashSet<string>> trackedObjectKpis;
        Dictionary<string, TrackedObjectInfo> trackedObjectInfo;

        // Recording

        string recordingId;
        DateTime creationDate;
        float duration;

        Dictionary<string, List<KPIRecord>> generalKpiRecords;
        Dictionary<string, Dictionary<string, List<KPIRecord>>> objectKpiRecords;

        internal override void Initialize()
        {
            recordingId = "";
            duration = 0;
            creationDate = new();

            trackedGeneralKpis = new();
            trackedObjectKpis = new();
            trackedObjectInfo = new();

            generalKpiRecords = new();
            objectKpiRecords = new();

            //Console.WriteLine("DummyRTStorage: Initialize");
        }

        internal override void Finish()
        {
            //Console.WriteLine("DummyRTStorage: Finishing");
        }

        internal override void NewScene()
        {
            //Console.WriteLine("DummyRTStorage: New scene");

            recordingId = "";
            duration = 0;

            trackedGeneralKpis.Clear();
            trackedObjectKpis.Clear();
            trackedObjectInfo.Clear();

            generalKpiRecords.Clear();
            objectKpiRecords.Clear();
        }

        internal override void LoadScene(string sceneStorageId)
        {
            //Console.WriteLine("DummyRTStorage: Loading kpis for scene " + sceneStorageId);
        }

        internal override void SaveScene(string sceneStorageId)
        {
            //Console.WriteLine("DummyRTStorage: Saving kpis for scene " + sceneStorageId);
        }

        internal override void DeleteScene(string sceneStorageId)
        {
            //Console.WriteLine("DummyRTStorage: Deleting kpis for scene " + sceneStorageId);
        }

        internal override void Start()
        {
            //Console.WriteLine("DummyRTStorage: Starting kpi recording");

            Guid guid = Guid.NewGuid();
            recordingId = guid.ToString();
            creationDate = DateTime.Now;
            duration = 0;
            
            generalKpiRecords.Clear();
            objectKpiRecords.Clear();

            foreach(string kpi in trackedGeneralKpis)
            {
                generalKpiRecords.Add(kpi, new());
            }

            foreach(KeyValuePair<string, HashSet<string>> keyValue in trackedObjectKpis)
            {
                string objId = keyValue.Key;

                objectKpiRecords.Add(objId, new());

                foreach(string kpi in keyValue.Value)
                {
                    objectKpiRecords[objId].Add(kpi, new());
                }
            }
        }

        internal override void Step()
        {
            //Console.WriteLine("DummyRTStorage: Recording kpis for step");

            duration += Constants.hoursPerStep;

            foreach(string kpi in trackedGeneralKpis)
            {
                KPIRecord r = KPIRecord.Create(SimulatorCore.GetGeneralKPI(kpi), duration);
                generalKpiRecords[kpi].Add(r);
            }

            foreach(KeyValuePair<string, HashSet<string>> keyValue in trackedObjectKpis)
            {
                string objId = keyValue.Key;

                foreach(KeyValuePair<string, List<KPIRecord>> kpiToRecords in objectKpiRecords[objId])
                {
                    SimulatedObject simObj = SimulatorCore.FindObjectById(objId);
                    KPIRecord r = KPIRecord.Create(SimulatorCore.GetObjectKPI(simObj, kpiToRecords.Key), duration);
                    objectKpiRecords[objId][kpiToRecords.Key].Add(r);
                }
            }
        }

        internal override void Stop()
        {
            //Console.WriteLine("DummyRTStorage: Stopping kpi recordings");
        }


        internal override void TrackGeneralKPI(string kpiName)
        {
            //Console.WriteLine("Enabling general kpi recording for " + kpiName);

            if(!trackedGeneralKpis.Contains(kpiName)) { trackedGeneralKpis.Add(kpiName); }
        }

        internal override void UntrackGeneralKPI(string kpiName)
        {
            //Console.WriteLine("Disabling general kpi recording for " + kpiName);

            if(trackedGeneralKpis.Contains(kpiName)) { trackedGeneralKpis.Remove(kpiName); }
        }
        
        internal override void TrackObjectKPI(string objectId, string kpiName)
        {
            //Console.WriteLine("Enabling object " + objectId + " kpi recording for " + kpiName);

            if(!trackedObjectKpis.ContainsKey(objectId)) { trackedObjectKpis.Add(objectId, new()); }
            if(!trackedObjectKpis[objectId].Contains(kpiName)) { trackedObjectKpis[objectId].Add(kpiName); }

            SimulatedObject simObj = SimulatorCore.FindObjectById(objectId);
            if(!trackedObjectInfo.ContainsKey(objectId))
            { trackedObjectInfo.Add(objectId, new() { Id = objectId ,Name = simObj.Name, Type = simObj.Type }); }

        }

        internal override void UntrackObjectKPI(string objectId, string kpiName)
        {
            //Console.WriteLine("Disabling object " + objectId + " kpi recording for " + kpiName);
            if(!trackedObjectKpis.ContainsKey(objectId)) { return; }
            if(!trackedObjectKpis[objectId].Contains(kpiName)) { return; }

            trackedObjectKpis[objectId].Remove(kpiName);
            if(trackedObjectKpis[objectId].Count == 0) { trackedObjectKpis.Remove(objectId); }
        }

        internal override void DisableAllKPIRecordings()
        {
            //Console.WriteLine("Disabling all kpi recordings");
            generalKpiRecords.Clear();
            objectKpiRecords.Clear();

        }

        internal override List<KPIRecord> GetGeneralKPIRecords(string kpiName, float fromTime = 0.0f, float toTime = Single.MaxValue)
        {
            if(!generalKpiRecords.ContainsKey(kpiName)) { return new(); }

            return generalKpiRecords[kpiName].FindAll((r) => r.Timestamp >= fromTime && (r.Timestamp <= toTime || toTime < 0));
        }

        internal override List<KPIRecord> GetObjectKPIRecords(string objectId, string kpiName, float fromTime = 0.0f, float toTime = Single.MaxValue)
        {
            if(!objectKpiRecords.ContainsKey(objectId)) { return new(); }
            if(!objectKpiRecords[objectId].ContainsKey(kpiName)) { return new(); }

            return objectKpiRecords[objectId][kpiName].FindAll((r) => r.Timestamp >= fromTime && (r.Timestamp <= toTime || toTime < 0));
        }

        internal override KPIRecordingInfo GetKPIRecordingInfo()
        {
            //Console.WriteLine("DummyRTStorage: returning recording info");

            KPIRecordingInfo info = new();
            info.Id = recordingId;
            info.Duration = duration;
            info.CreationDate = creationDate;
            info.GeneralKpis = generalKpiRecords.Keys.ToList();

            List<ObjectKPIInfo> objectsInfo = new();

            foreach(KeyValuePair<string, Dictionary<string, List<KPIRecord>>> keyValue in objectKpiRecords)
            {
                string objId = keyValue.Key;

                foreach(KeyValuePair<string, List<KPIRecord>> kpiToRecords in objectKpiRecords[objId])
                {
                    ObjectKPIInfo objectInfo = new();
                    objectInfo.Kpi = kpiToRecords.Key;
                    objectInfo.ObjectName = trackedObjectInfo[objId].Name;
                    objectInfo.ObjectId = objId;
                    objectInfo.ObjectType = trackedObjectInfo[objId].Type;

                    objectsInfo.Add(objectInfo);
                }
            }

            info.ObjectKpis = objectsInfo;

            return info;
        }

        internal override void LoadKPIRecording(string recordingId)
        {
            //Console.WriteLine("DummyRTStorage: Loading kpi recording");
        }

        internal override List<string> ListKPIRecordings()
        {
            //Console.WriteLine("DummyRTStorage: Returning recording list");
            return new List<string>();
        }

        internal override KPIRecordingInfo GetKPIRecordingInfo(string recordingId)
        {
            //Console.WriteLine("DummyRTStorage: Returning recording info for recording " + recordingId);

            return new KPIRecordingInfo();
        }

        internal override void DeleteKPIRecording(string recordingId)
        {
            //Console.WriteLine("DummyRTStorage: Deleting kpi recording " + recordingId);
        }
    }
}
