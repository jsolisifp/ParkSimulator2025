using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemeloDigital
{
    public class Person : SimulatedObject
    {
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
