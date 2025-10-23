using System.Security.Cryptography.X509Certificates;
using System.Numerics;

namespace GemeloDigital
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SimulatorCore.Initialize();

            Person p1 = SimulatorCore.CreatePerson();
            p1.SetAge(18);
            p1.Age = 18;

            Person p2 = SimulatorCore.CreatePerson();
            p2.SetAge(14);
            p2.Age = 14;

            // Paths and points

            Point point1 = SimulatorCore.CreatePoint();
            point1.Position = new Vector3(7, 10, 0);

            Point point2 = SimulatorCore.CreatePoint();
            point1.Position = new Vector3(15, 4, 0);

            Point point3 = SimulatorCore.CreatePoint();
            point1.Position = new Vector3(15, 10, 0);

            Path path12 = SimulatorCore.CreatePath(point1, point2);
            Path path23 = SimulatorCore.CreatePath(point2, point3);

            Console.WriteLine("Path de 1 a 2 mide " + path12.Distance);
            Console.WriteLine("Path de 2 a 3 mide " + path23.Distance);

            Facility f1 = SimulatorCore.CreateFacility(point1, point1);
            f1.Name = "Entrada del parque";
            f1.PowerConsumed = 20;

            Facility f2 = SimulatorCore.CreateFacility(point1, point1);
            f2.Name = "Párquing";
            f2.PowerConsumed = 5;

            Facility f3 = SimulatorCore.CreateFacility(point3, point3);
            f3.Name = "Funicular";
            f3.PowerConsumed = 250;

            // Place people

            p1.IsAtFacility = f1;
            p2.IsAtFacility = f1;

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
