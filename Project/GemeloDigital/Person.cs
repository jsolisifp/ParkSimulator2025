using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemeloDigital
{
    public class Person : SimulatedObject
    {
        /// <summary>
        /// 
        /// </summary>
        public Facility? IsAtFacility { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public Path? IsAtPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float Height { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float Weight { get; set; }

        /// <summary>
        /// 
        /// </summary>
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
