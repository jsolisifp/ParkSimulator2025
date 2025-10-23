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



        // Property definiendo expresamente una variable privada
        // donde guardar el valor
        public int Age { get { return age;  } set { age = value;  } }

        // Property resumida: el compilador ya generará la variable
        // privada internamente
        public float Height { get; set; }

        public float Weight { get; set; }
        public float Money { get; set; }

        int age;

        public override void Start()
        {
        }

        public override void Step()
        {
        }

        public override void Stop()
        {
        }

        // Getter
        public int GetAge()
        {
            return age;
        }

        // Setter
        public void SetAge(int value)
        {
            age = value;
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
