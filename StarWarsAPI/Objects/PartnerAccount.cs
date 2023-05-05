using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWarsAPI.Objects
{
    public class PartnerAccount : Person
    {
        public int PersonID { get; set; }
        public Species SpeciesObject { get; set; }
        public Planet PlanetObject { get; set; }
        public decimal OverallPercent { get; set; }
        public decimal InterestPercent { get; set; }
        public decimal PhysicalPercent { get; set; }
        public decimal BiologicalPercent { get; set; }
        public decimal AttractivenessPercent { get; set; }

        //Constructor
        public PartnerAccount() { }
    }
}
