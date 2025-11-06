using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemeloDigital
{
    public class PersonPropertiesGenerator
    {
        Random random;

        public PersonPropertiesGenerator()
        {
             random = new Random();

        }

        public int GenerateAge()
        {
            return Constants.personAgeMin + random.Next() % (Constants.personAgeMax - Constants.personAgeMin + 1);
        }

        public float GenerateHeight(int age)
        {
            float height;

            if(age >= Constants.personAgeMin && age <= 16)
            {
                height = 1.3f + random.NextSingle() * 0.2f;
            }
            else if(age <= 35)
            {
                height = 1.7f + random.NextSingle() * 0.2f;
            }
            else // age <= Constants.personAgeMax
            {
                height = 1.8f + random.NextSingle() * 0.2f;
            }

            return height;

        }

    }
}
