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

        public Person()
        {
            Name = "Person";
            Type = SimulatedObjectType.Person;
        }

        public override void Start()
        {
        }

        public override void Step()
        {
        }

        public override void Stop()
        {
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
