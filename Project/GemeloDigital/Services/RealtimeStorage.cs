using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemeloDigital
{
    internal abstract class RealtimeStorage
    {
        // Lifecycle
        // Initialize -> Called when simulator is initialized
        // Finish -> Called when simulator is finished
        // NewScene -> Called when a scene is cleared. You can discard any cached recordings.
        // LoadScene -> Called when a scene is loaded. You can cache some recordings.
        // SaveScene -> Called when a scene is saved. You can write your cached recordings.
        // DeleteScene -> Called when a scene is deleted. You can remove all recording from this scene.
        // Start -> Called when simulator is started. Recording starts
        // Step -> Called when simulator is stepped. Samples are taken
        // Stop -> Called when simulator is stopped. Recording stops

        internal abstract void Initialize();
        internal abstract void Finish();

        internal abstract void NewScene();
        internal abstract void LoadScene(string sceneStorageId);
        internal abstract void SaveScene(string sceneStorageId);
        internal abstract void DeleteScene(string sceneStorageId);

        internal abstract void Start();
        internal abstract void Step();
        internal abstract void Stop();

        // Enable or disable recording specific kpis.
        // Not available when simulation is running.

        internal abstract void TrackGeneralKPI(string kpiName);
        internal abstract void UntrackGeneralKPI(string kpiName);
        
        internal abstract void TrackObjectKPI(string objectId, string kpiName);
        internal abstract void UntrackObjectKPI(string objectId, string kpiName);

        internal abstract void DisableAllKPIRecordings();

        // Retrieving records
        // If called while the simulation is running, you can retrieve the kpis recorded so far
        // When the simulation is stopped, you can retrieve all the kpis recorded
        // If no kpis exist for the current scene, or you ask for a kpi that has not been recorded,
        // you will retrieve empty lists or zero
        // If another recording from the scene have been loaded you will retrieve that kpis

        internal abstract List<KPIRecord> GetGeneralKPIRecords(string kpiName, float fromTime = 0.0f, float toTime = Single.MaxValue);
        internal abstract List<KPIRecord> GetObjectKPIRecords(string objectId, string kpiName, float fromTime = 0.0f, float toTime = Single.MaxValue);
        internal abstract KPIRecordingInfo GetKPIRecordingInfo();

        // Manage previous recordings of the current scene.
        // Not available while simulation is running.

        internal abstract void LoadKPIRecording(string recordingId);
        internal abstract List<string> ListKPIRecordings();
        internal abstract KPIRecordingInfo GetKPIRecordingInfo(string recordingId);
        internal abstract void DeleteKPIRecording(string recordingId);
    }
}
