using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemeloDigital
{
    public enum SimulatedObjectType
    {
        Any,
        Facility,
        Person,
        Point,
        Path
    }

    public enum SimulatorState
    {
        Stopped,
        Running
    }
}
