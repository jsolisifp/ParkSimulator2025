using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemeloDigital
{
    public class Facility : SimulatedObject
    {
        /// <summary>
        /// 
        /// </summary>
        public List<Point> Entrances { get { return entrances; } }

        /// <summary>
        /// 
        /// </summary>
        public List<Point> Exits { get { return exits; } }

        /// <summary>
        /// 
        /// </summary>
        public float PowerConsumed { get; set; }

        List<Point> entrances;
        List<Point> exits;

        float powerConsumedTotal;

        internal Facility(Point entrance, Point exit)
        {
            Name = "Facility";
            Type = SimulatedObjectType.Facility;

            entrances = new List<Point>();
            exits = new List<Point>();

            entrances.Add(entrance);
            exits.Add(exit);
        }

        internal override void Start()
        {
            powerConsumedTotal = 0;

        }

        internal override void Step()
        {
            powerConsumedTotal += PowerConsumed * Constants.hoursPerStep;
        }

        internal override void Stop()
        {
        }

        internal override float GetKPI(string kpi)
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

        internal override void StartKPIRecording(string name)
        {
            base.StartKPIRecording(name);

        }

        internal override void StopKPIRecording(string name)
        {
            base.StopKPIRecording(name);

        }
    }
}
