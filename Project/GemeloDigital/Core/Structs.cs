using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemeloDigital
{
    public struct KPIRecord
    {
        public float Value { get { return value; } }
        public float Timestamp { get { return timestamp; } }

        float value;
        float timestamp;

        public static KPIRecord Create(float value, float timestamp)
        {
            KPIRecord r = new();
            r.value = value;      
            r.timestamp = timestamp;

            return r;
        }
    }

    public struct ObjectKPIInfo
    {
        public string ObjectId { get; set; }
        public string ObjectName { get; set; }
        public SimulatedObjectType ObjectType { get; set; }

        public string Kpi { get; set; }
    }

    public struct KPIRecordingInfo
    {
        public string Id { get; set; }
        public DateTime CreationDate { get; set; }
        public float Duration { get; set; }
        public List<string> GeneralKpis { get; set; }
        public List<ObjectKPIInfo> ObjectKpis { get; set; }

        public KPIRecordingInfo()
        {
            Id = "";
            CreationDate = DateTime.Now;
            Duration = 0;
            GeneralKpis = new List<string>();
            ObjectKpis = new List<ObjectKPIInfo>();
        }

    }

}
