using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWarsAPI.Objects
{
    public class PersonalAccount : Person
    {
        public PartnerAccount closestMatch { get; set; }

        //Constructor
        public PersonalAccount() { }

        //Methods
        /// <summary>
        /// This function changes the birth_year to a usable age.
        /// </summary>
        /// <param name="BirthYear"> What the birth_year should be set to. </param>
        public override void UpdateBirthYear(string BirthYear)
        {
            this.birth_year = BirthYear;
        }
    }
}
