using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GemeloDigital
{
    public class Point : SimulatedObject
    {
        /// <summary>
        /// Coordenadas del punto
        /// </summary>
        public Vector3 Position { get; set; }

        internal Point()
        {
            Name = "Point";
            Type = SimulatedObjectType.Point;
        }

        // Cuando alguien intente imprimir un Point, mostrara las posiciones en formato texto, que viene de la clase Point y de la libreria Vector
  
        public override string ToString()
        {
            return Position.X + " , " + Position.Y + " , "+ Position.Z;
        }
        
    }
}
