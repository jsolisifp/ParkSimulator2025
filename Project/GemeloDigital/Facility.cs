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

        public string Name { get; set; }
        public float PowerConsumed { get; set; }

        List<Point> entrances;
        List<Point> exits;

        float powerConsumed;

        public Facility(Point entrance, Point exit)
        {
            entrances = new List<Point>();
            exits = new List<Point>();

            entrances.Add(entrance);
            exits.Add(exit);
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
