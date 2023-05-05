using StarWarsAPI.Objects;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWarsAPI
{
    public class Algorithms
    {
        /// <summary>
        /// This method calculates how similar interests are between the personalAccount and partnerAccount.
        /// </summary>
        /// <param name="personalAccount"> An inputted personalAccount. </param>
        /// <param name="partnerAccount"> An inputted partnerAccount. </param>
        /// <returns> An interest percentage. </returns>
        private static decimal CalculateInterest(PersonalAccount personalAccount, PartnerAccount partnerAccount)
        {
            decimal MatchCount = 0;
            decimal ShipCount = 0;

            string[] PartnerStarships = partnerAccount.string_starships.Split(", ");
            string[] PersonalStarships = personalAccount.string_starships.Split(", ");

            foreach (var PartnerShipName in PartnerStarships)
            {
                foreach (var PersonalShipName in PersonalStarships)
                {
                    if (PersonalShipName.ToLower() == PartnerShipName.ToLower())
                    {
                        ShipCount++;
                    }
                }
            }

            if (partnerAccount.PlanetObject != null)
            {
                if (personalAccount.homeworld.ToLower() == partnerAccount.PlanetObject.name.ToLower())
                {
                    MatchCount = .5m;
                }
            }

            if (ShipCount > 0)
            {
                if (PartnerStarships.Count() - 1 > 0)
                {
                    MatchCount = MatchCount + (ShipCount / (PartnerStarships.Count() - 1)) / 2;
                }
                else
                {
                    MatchCount = MatchCount + .5m;
                }
            }

            return MatchCount * 100;
        }

        /// <summary>
        /// This method calculates the physical similarity between the personalAccount and partnerAccount.
        /// </summary>
        /// <param name="personalAccount"> An inputted personalAccount. </param>
        /// <param name="partnerAccount"> An inputted partnerAccount. </param>
        /// <returns> A physical percentage. </returns>
        private static decimal CalculatePhysical(PersonalAccount personalAccount, PartnerAccount partnerAccount)
        {
            decimal MatchCount = 0;
            decimal AgePercent = 0;
            decimal PersonalAge = decimal.Parse(personalAccount.birth_year);
            decimal PartnerAge = 0;

            if (decimal.TryParse(partnerAccount.birth_year,out PartnerAge))
            {
                if (Math.Abs(PersonalAge - PartnerAge) > 1)
                {

                    AgePercent = (.5m / Math.Abs(PersonalAge - PartnerAge)) / 1.1m;
                }
                else
                {
                    AgePercent = .25m;
                }
            }
            else
            {
                AgePercent = 0;
            }

            if (partnerAccount.gender != null)
            {
                if (personalAccount.gender.ToLower() != partnerAccount.gender.ToLower() && partnerAccount.gender.ToLower() != "n/a")
                {
                    MatchCount = .75m;
                }
            }

            MatchCount = (MatchCount + AgePercent);

            return MatchCount * 100;
        }

        /// <summary>
        /// This method calculates the biological similarity between the personalAccount and partnerAccount.
        /// </summary>
        /// <param name="personalAccount"> An inputted personalAccount. </param>
        /// <param name="partnerAccount"> An inputted partnerAccount. </param>
        /// <returns> A biological percentage. </returns>
        private static decimal CalculateBiological(PersonalAccount personalAccount, PartnerAccount partnerAccount)
        {
            decimal MatchCount = 0;

            if (partnerAccount.SpeciesObject != null)
            {
                if (personalAccount.species[0].ToString().ToLower() == partnerAccount.SpeciesObject.name.ToLower())
                {
                    MatchCount = 1;
                }
            }
            else
            {
                MatchCount = .5m;
            }

            return MatchCount * 100;
        }

        /// <summary>
        /// This method calculates the attractiveness between the personalAccount and partnerAccount.
        /// </summary>
        /// <param name="personalAccount"> An inputted personalAccount. </param>
        /// <param name="partnerAccount"> An inputted partnerAccount. </param>
        /// <returns> An attractiveness percentage. </returns>
        private static decimal CalculateAttractiveness(PersonalAccount personalAccount, PartnerAccount partnerAccount)
        {
            decimal MatchCount = 0;
            decimal HeightPercent = 0;
            decimal MassPercent = 0;
            decimal PartnerMass = 0;

            if (partnerAccount.eye_color != null && personalAccount.eye_color.ToLower() == partnerAccount.eye_color.ToLower())
            {
                MatchCount++;
            }

            if (partnerAccount.skin_color != null && personalAccount.skin_color.ToLower() == partnerAccount.skin_color.ToLower())
            {
                MatchCount++;
            }

            if (partnerAccount.height != null)
            {
                if (personalAccount.height == partnerAccount.height)
                {
                    HeightPercent = 1;
                }
                else
                {
                    HeightPercent = (1 / Math.Abs(decimal.Parse(personalAccount.height) - decimal.Parse(partnerAccount.height))) * 1.3m;

                    if (HeightPercent > 1)
                    {
                        HeightPercent = 1;
                    }
                }
            }

            if (partnerAccount.mass != null)
            {
                if (personalAccount.mass == partnerAccount.mass)
                {
                    MassPercent = 1;
                }
                else
                {
                    if (decimal.TryParse(partnerAccount.mass, out PartnerMass))
                    {
                        MassPercent = (1 / Math.Abs(decimal.Parse(personalAccount.mass) - decimal.Parse(partnerAccount.mass))) * 1.3m;

                        if (MassPercent > 1)
                        {
                            MassPercent = 1;
                        }
                    }
                    else
                    {
                        MassPercent = 0;
                    }
                }
            }

            MatchCount = (MatchCount + HeightPercent + MassPercent)/ 4;

            return MatchCount * 100;
        }

        /// <summary>
        /// This function fetches the compatability percentages, then applies them to the partnerAccount object.
        /// </summary>
        /// <param name="personalAccount"> The user's personal account (to be compared to the partnerAccount) </param>
        /// <param name="partnerAccount"> The partnerAccount to be updated. </param>
        /// <returns> An updated partnerAccount. </returns>
        public static PartnerAccount CalculateCompatability(PersonalAccount personalAccount,PartnerAccount partnerAccount)
        {
            if (partnerAccount != null)
            {
                decimal Interest = CalculateInterest(personalAccount, partnerAccount);
                decimal Physical = CalculatePhysical(personalAccount, partnerAccount);
                decimal Biological = CalculateBiological(personalAccount, partnerAccount);
                decimal Attractiveness = CalculateAttractiveness(personalAccount, partnerAccount);
                decimal Overall = (Interest + Physical + Biological + Attractiveness) / 4;

                partnerAccount.InterestPercent = Interest;
                partnerAccount.PhysicalPercent = Physical;
                partnerAccount.BiologicalPercent = Biological;
                partnerAccount.AttractivenessPercent = Attractiveness;
                partnerAccount.OverallPercent = Overall;
            }
            return partnerAccount;
        }
    }
}
