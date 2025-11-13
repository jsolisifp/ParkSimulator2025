using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemeloDigital
{
    public class Constants
    {
        // Simulation

        internal const float hoursPerStep = 0.5f;

        // KPIs

        internal const string kpiNameEnergy = "E";


        // Person generator

        internal const int personsTotal = 2500;

        internal const int personAgeMin = 12;
        internal const int personAgeYoungMin = 16;
        internal const int personAgeAdultMin = 35;
        internal const int personAgeSeniorMin = 65;
        internal const int personAgeMax = 100;

        internal const float personHeightChildMin = 1.1f;
        internal const float personHeightChildMax = 1.6f;
        internal const float personHeightYoungMin = 1.6f;
        internal const float personHeightYoungMax = 2.0f;
        internal const float personHeightAdultMin = 1.6f;
        internal const float personHeightAdultMax = 2.0f;
        internal const float personHeightSeniorMin = 1.5f;
        internal const float personHeightSeniorMax = 1.8f;




    }
}
