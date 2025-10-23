using System.Security.Cryptography.X509Certificates;

namespace GemeloDigital
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SimulatorCore.Initialize();

            Person p1 = SimulatorCore.CreatePerson();
            Person p2 = SimulatorCore.CreatePerson();
            Facility f1 = SimulatorCore.CreateFacility();
            f1.Name = "Avión";
            f1.PowerConsumed = 120;
            Facility f2 = SimulatorCore.CreateFacility();
            f2.Name = "Túnel del terror";
            f2.PowerConsumed = 40;

            SimulatorCore.Start();

            for(int i = 0; i < 600; i++)
            {
                SimulatorCore.Step();
            }

            SimulatorCore.Stop();

            Console.WriteLine("KPI: " + SimulatorCore.GetGeneralKPI("powerConsumed"));

            SimulatorCore.Finish();
        }
    }
}
