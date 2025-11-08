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
        /// 
        /// </summary>
        public Point Point1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Point Point2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CapacityPersons { get; set; }

        /// <summary>
        /// 
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
