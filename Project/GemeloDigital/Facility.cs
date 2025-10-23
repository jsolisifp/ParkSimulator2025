using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemeloDigital
{
    public class Facility : SimulatedObject
    {
        public string Name { get; set; }
        public float PowerConsumed { get; set; }

        float powerConsumed;

        public Facility()
        {
        }

        public override void Start()
        {
            powerConsumed = 10;

        }

        public override void Step()
        {
            powerConsumed += 1;

            Console.WriteLine("Power consumed " + powerConsumed);

        }

        public override void Stop()
        {
        }

        public override float GetKPI(string kpi)
        {
            if (kpi == "powerConsumed")
            {
                return powerConsumed;
            }
            else
            {
                return 0;
            }
        }

        public override void StartKPIRecording(string name)
        {
            base.StartKPIRecording(name);

        }

        public override void StopKPIRecording(string name)
        {
            base.StopKPIRecording(name);

        }
    }
}
