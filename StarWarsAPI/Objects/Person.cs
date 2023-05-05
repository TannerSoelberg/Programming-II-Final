using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWarsAPI.Objects
{
    public class Person
    {
        public string name { get; set; }

        public string height { get; set; }

        public string mass { get; set; }

        public string hair_color { get; set; }

        public string skin_color { get; set; }

        public string eye_color { get; set; }

        public string birth_year { get; set; }

        public string gender { get; set; }

        public string homeworld { get; set; }

        public List<string> films { get; set; }

        public List<string> species { get; set; }

        public List<object> vehicles { get; set; }

        public List<object> starships { get; set; }

        public DateTime created { get; set; }

        public DateTime edited { get; set; }

        public string string_starships { get; set; }

        public string url { get; set; }

        //Constructor
        public Person() { }

        //Methods
        /// <summary>
        /// This function converts the loaded birth year into a usable age.
        /// </summary>
        /// <param name="BirthYear"> An optional string, to be used if there is no preset BirthYear. </param>
        public virtual void UpdateBirthYear(string BirthYear)
        {
            decimal PersonAge;
            if (this.birth_year != null)
            {
                if (decimal.TryParse(this.birth_year.Split("BBY")[0], out PersonAge))
                {
                    this.birth_year = PersonAge.ToString();
                }
            }
            else
            {
                this.birth_year = BirthYear;
            }
        }
    }
}
