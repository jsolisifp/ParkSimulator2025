using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemeloDigital
{
    public class Person : SimulatedObject
    {
        /// <summary>
        /// Instalación en que está la persona
        /// </summary>
        public Facility? IsAtFacility { get; set; }


        /// <summary>
        /// Camino en que está la persona.
        /// </summary>
        public Path? IsAtPath { get; set; }

        /// <summary>
        /// Edad de la persona.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Altura en cm de la persona
        /// </summary>
        public float Height { get; set; }

        /// <summary>
        /// Peso en Kg de la persona
        /// </summary>
        public float Weight { get; set; }

        /// <summary>
        /// Dinero que lleva la persona
        /// </summary>
        public float Money { get; set; }

        internal Person()
        {
            Name = "Person";
            Type = SimulatedObjectType.Person;
        }

        internal override void Start()
        {
        }

        internal override void Step()
        {
        }

        internal override void Stop()
        {
        }

    }
}
