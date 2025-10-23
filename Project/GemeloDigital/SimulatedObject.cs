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
        string name;

        public SimulatedObject()
        {
            name = "Sin nombre";
        }

        public string GetName()
        {
            return name;
        }

        public void SetName(string _name)
        {
            name = _name;
        }

        public virtual void Start()
        {
            Console.WriteLine("SimObj " + name + ": Started");
        }

        public virtual void Step()
        {
            Console.WriteLine("SimObj " + name + ": Stepped");
        }
        public virtual void Stop()
        {
            Console.WriteLine("SimObj " + name + ": Stopped");
        }

        public virtual float GetKPI(string kpi)
        {
            return 0;
        }

        public virtual void StartKPIRecording(string name)
        {
            Console.WriteLine("");
        }

        public virtual void StopKPIRecording(string name)
        {

        }
    }
}
