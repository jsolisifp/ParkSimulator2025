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

        public PersonGenerator()
        {
             random = new Random();

        }

        public virtual Person GeneratePerson()
        {
            Person person;

            person = SimulatorCore.CreatePerson();

            // Age is random

            int age = Constants.ageMin + random.Next() % (Constants.ageMax - Constants.ageMin + 1);

            // Height depends on age

            float height;

            if(age <= Constants.ageYoungMin)
            {
                // Childs

                height = Constants.heightChildMin + random.NextSingle() * (Constants.heightChildMax - Constants.heightChildMin);
            }
            else if(age <= Constants.ageAdultMin)
            {
                // Adults

                height = Constants.heightAdultMin + random.NextSingle() * (Constants.heightAdultMax - Constants.heightAdultMin);
            }
            else // age <= Constants.ageMax
            {
                // Seniors

                height = Constants.heightSeniorMin + random.NextSingle() * (Constants.heightSeniorMax - Constants.heightSeniorMin);
            }

            person.Age = age;
            person.Height = height;

            return person;

        }

    }
}
