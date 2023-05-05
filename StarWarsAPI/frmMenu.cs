using StarWarsAPI.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StarWarsAPI
{
    public partial class frmMenu : Form
    {
        private PartnerAccount TopMatch;
        private PartnerAccount CurrentPartner;
        private PersonalAccount CurrentPersonal = new PersonalAccount();

        public frmMenu()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This function handles the TopMatch account and UI.
        /// </summary>
        private void UpdateTopMatch()
        {
            if (TopMatch != null)
            {
                if (CurrentPartner.OverallPercent > TopMatch.OverallPercent || CurrentPartner.name == TopMatch.name)
                {
                    TopMatch = CurrentPartner;
                }
            }
            else
            {
                TopMatch = CurrentPartner;
            }

            //Update UI
            lblTopIDResult.Text = TopMatch.url.Split('/')[5];
            lblTopNameResult.Text = TopMatch.name;
            lblTopCompatabilityResult.Text = Math.Round(TopMatch.OverallPercent).ToString() + "%";
        }

        /// <summary>
        /// This function updates personal data, along with partner data.
        /// </summary>
        private void UpdatePersonal()
        {
            CurrentPersonal.UpdateBirthYear(nudPersonalAge.Value.ToString());
            CurrentPersonal.gender = txbPersonalGender.Text ?? "female";

            List<string> SpeciesList = new List<string>();
            SpeciesList.Add(txbPersonalSpecies.Text);

            CurrentPersonal.species = SpeciesList;
            CurrentPersonal.homeworld = txbPersonalPlanet.Text ?? "Tatooine";
            CurrentPersonal.eye_color = txbPersonalEye.Text ?? "blue";
            CurrentPersonal.skin_color = txbPersonalSkin.Text ?? "fair";
            CurrentPersonal.height = txbPersonalHeight.Value.ToString() ?? "172";
            CurrentPersonal.mass = nudPersonalMass.Value.ToString() ?? "77";
            CurrentPersonal.string_starships = txbPersonalStarships.Text ?? "X-wing";

            UpdatePartner();
            TopMatch = Algorithms.CalculateCompatability(CurrentPersonal, TopMatch);
        }

        /// <summary>
        /// This function retrieves API information and updates the partner data and displays it accordingly.
        /// </summary>
        private async void UpdatePartner()
        {
            CurrentPartner = await JSONHelper.GetPerson(nudPartnerID.Value.ToString());

            if (CurrentPartner != null)
            {
                //Planet Loading
                if (CurrentPartner.homeworld != null)
                {
                    string[] PlanetNum = CurrentPartner.homeworld.Split('/');
                    Planet PartnerPlanet = await JSONHelper.GetPlanet(PlanetNum[5]);

                    if (PartnerPlanet != null)
                    {
                        lblPartnerPlanetResult.Text = PartnerPlanet.name;
                        CurrentPartner.PlanetObject = PartnerPlanet;
                    }
                    else
                    {
                        lblPartnerPlanetResult.Text = "n/a";
                    }
                }

                //Species Loading
                if (CurrentPartner.species != null && CurrentPartner.species.Count > 0)
                {
                    string[] SpeciesNum = CurrentPartner.species[0].Split('/');
                    Species PartnerSpecies = await JSONHelper.GetSpecies(SpeciesNum[5]);

                    CurrentPartner.SpeciesObject = PartnerSpecies;

                    if (PartnerSpecies != null)
                    {
                        lblPartnerSpeciesResult.Text = PartnerSpecies.name;
                    }
                    else
                    {
                        lblPartnerSpeciesResult.Text = "n/a";
                    }
                }
                else
                {
                    lblPartnerSpeciesResult.Text = "n/a";
                }

                //Starship Loading
                string AllStarships = "";
                if (CurrentPartner.starships != null)
                {
                    foreach (var item in CurrentPartner.starships)
                    {
                        string[] starshipNum = item.ToString().Split('/');
                        Starship PartnerStarship = await JSONHelper.GetStarship(starshipNum[5]);

                        AllStarships += PartnerStarship.name + ", ";
                    }
                }  

                CurrentPartner.string_starships = AllStarships;

                if (AllStarships.Length > 0)
                {
                    lblPartnerStarshipsResult.Text = AllStarships;
                }
                else
                {
                    lblPartnerStarshipsResult.Text = "n/a";
                }

                //Update Partner Info
                lblPartnerNameResult.Text = CurrentPartner.name;

                CurrentPartner.UpdateBirthYear("");

                lblPartnerAgeResult.Text = CurrentPartner.birth_year;


                lblPartnerGenderResult.Text = CurrentPartner.gender;
                lblPartnerEyeResult.Text = CurrentPartner.eye_color;
                lblPartnerSkinResult.Text = CurrentPartner.skin_color;
                lblPartnerHairResult.Text = CurrentPartner.hair_color;
                lblPartnerHeightResult.Text = CurrentPartner.height;
                lblPartnerMassResult.Text = CurrentPartner.mass;

                //Update Compatability
                CurrentPartner = Algorithms.CalculateCompatability(CurrentPersonal, CurrentPartner);

                lblCompatabilityOverallResult.Text = Math.Round(CurrentPartner.OverallPercent).ToString() + "%";
                lblCompatabilityInterestResult.Text = Math.Round(CurrentPartner.InterestPercent).ToString() + "%";
                lblCompatabilityPhysicalResult.Text = Math.Round(CurrentPartner.PhysicalPercent).ToString() + "%";
                lblCompatabilityBiologicalResult.Text = Math.Round(CurrentPartner.BiologicalPercent).ToString() + "%";
                lblCompatabilityAttractivenessResult.Text = Math.Round(CurrentPartner.AttractivenessPercent).ToString() + "%";

                UpdateTopMatch();
            }
            else
            {
                //Update Partner Info
                lblPartnerNameResult.Text = "n/a";
                lblPartnerAgeResult.Text = "n/a";
                lblPartnerGenderResult.Text = "n/a";
                lblPartnerSpeciesResult.Text = "n/a";
                lblPartnerPlanetResult.Text = "n/a";
                lblPartnerEyeResult.Text = "n/a";
                lblPartnerSkinResult.Text = "n/a";
                lblPartnerHairResult.Text = "n/a";
                lblPartnerHeightResult.Text = "n/a";
                lblPartnerMassResult.Text = "n/a";

                //Update Compatability
                lblCompatabilityOverallResult.Text = "n/a";
                lblCompatabilityInterestResult.Text = "n/a";
                lblCompatabilityPhysicalResult.Text = "n/a";
                lblCompatabilityBiologicalResult.Text = "n/a";
                lblCompatabilityAttractivenessResult.Text = "n/a";
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            UpdatePartner();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdatePersonal();
            MessageBox.Show("Information updating, please wait...");
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {
            UpdatePersonal();
        }
    }
}
