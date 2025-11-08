using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemeloDigital
{
    public class Person : SimulatedObject
    {
        public Facility? IsAtFacility { get; set; }


        public Path? IsAtPath { get; set; }


        public int Age { get; set; }
        public float Height { get; set; }

        public float Weight { get; set; }
        public float Money { get; set; }

        internal Person()
        {
            Name = "Person";
            Type = SimulatedObjectType.Person;
        }

        internal override void Start()
        {
        }

        internal override void Step()
        {
        }

        internal override void Stop()
        {
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
