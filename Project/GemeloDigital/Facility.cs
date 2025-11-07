using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemeloDigital
{
    public class Facility : SimulatedObject
    {
        public List<Point> Entrances { get { return entrances; } }
        public List<Point> Exits { get { return exits; } }

        public float PowerConsumed { get; set; }

        List<Point> entrances;
        List<Point> exits;

        float powerConsumedTotal;

        public Facility(Point entrance, Point exit)
        {
            Name = "Facility";
            Type = SimulatedObjectType.Facility;

            entrances = new List<Point>();
            exits = new List<Point>();

            entrances.Add(entrance);
            exits.Add(exit);
        }

        public override void Start()
        {
            powerConsumedTotal = 0;

        }

        public override void Step()
        {
            powerConsumedTotal += PowerConsumed * Constants.hoursPerStep;
        }

        public override void Stop()
        {
        }

        public override float GetKPI(string kpi)
        {
            if (kpi == Constants.kpiNameEnergy)
            {
                return powerConsumedTotal;
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
