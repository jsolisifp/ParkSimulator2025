using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemeloDigital
{
    public class PersonGenerator
    {
        protected Random random;

        internal PersonGenerator()
        {
             random = new Random();

        }

        public virtual Person GeneratePerson()
        {
            Person person;

            person = SimulatorCore.CreatePerson();

            // Age is random

            int age = Constants.personAgeMin + random.Next() %
                      (Constants.personAgeMax - Constants.personAgeMin + 1);

            // Height depends on age

            float height;

            if(age <= Constants.personAgeYoungMin)
            {
                // Childs

                height = Constants.personHeightChildMin + random.NextSingle() * 
                         (Constants.personHeightChildMax - Constants.personHeightChildMin);
            }
            else if(age <= Constants.personAgeAdultMin)
            {
                // Adults

                height = Constants.personHeightAdultMin + random.NextSingle() *
                         (Constants.personHeightAdultMax - Constants.personHeightAdultMin);
            }
            else // age <= Constants.ageMax
            {
                // Seniors

                height = Constants.personHeightSeniorMin + random.NextSingle() *
                         (Constants.personHeightSeniorMax - Constants.personHeightSeniorMin);
            }

            person.Age = age;
            person.Height = height;

            return person;

        }

    }
}
