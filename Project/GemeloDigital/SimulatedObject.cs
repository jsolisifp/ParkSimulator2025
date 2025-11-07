using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GemeloDigital
{
    public class SimulatedObject
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public SimulatedObjectType Type { get; set; }

        public SimulatedObject()
        {
            Id = Guid.NewGuid().ToString();
            Name = "Sin nombre";
            Type = SimulatedObjectType.Any;
        }

        public virtual void Start()
        {
            // Nothing to do
        }

        public virtual void Step()
        {
            // Nothing to do
        }
        public virtual void Stop()
        {
            // Nothing to do
        }

        public virtual float GetKPI(string kpi)
        {
            return 0;
        }

        public virtual void StartKPIRecording(string name)
        {
            // Nothing to do
        }

        public virtual void StopKPIRecording(string name)
        {
            // Nothing to do
        }
    }
}
