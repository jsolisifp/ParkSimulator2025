using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GemeloDigital
{
    public class Path : SimulatedObject
    {

        /// <summary>
        /// Primer punto conectado por el camino
        /// </summary>
        public Point Point1 { get; set; }

        /// <summary>
        /// Segundo punto conectado por el camino
        /// </summary>
        public Point Point2 { get; set; }

        /// <summary>
        /// Personas máximas que pueden circular por el camino
        /// </summary>
        public int CapacityPersons { get; set; }

        /// <summary>
        /// Distancia entre los puntos
        /// </summary>
        public float Distance
        {
            get
            {
                return Vector3.Distance(Point1.Position, Point2.Position);
            }
        }

        internal Path(Point p1, Point p2)
        {
            Name = "Path";
            Type = SimulatedObjectType.Path;

            Point1 = p1;
            Point2 = p2;

        }


    }
}
