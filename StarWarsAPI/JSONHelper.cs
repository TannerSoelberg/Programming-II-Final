using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using StarWarsAPI.Objects;

namespace StarWarsAPI
{
    public class JSONHelper
    {
        static readonly HttpClient Client = new HttpClient();

        /// <summary>
        /// This function fetches a person's data (and converts it into a "Person" object) from a given ID.
        /// </summary>
        /// <param name="PersonID"> This is a string ID. </param>
        /// <returns> A planet object. </returns>
        public static async Task<PartnerAccount> GetPerson(string PersonID)
        {
            PartnerAccount MyDeserializedClass = new PartnerAccount();

            if (int.TryParse(PersonID, out int ID))
            {
                try
                {
                    using HttpResponseMessage Response = await Client.GetAsync("https://swapi.dev/api/people/" + ID + "/");
                    Response.EnsureSuccessStatusCode();
                    string ResponseBody = await Response.Content.ReadAsStringAsync();

                    MyDeserializedClass = JsonConvert.DeserializeObject<PartnerAccount>(ResponseBody);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                }
                return MyDeserializedClass;
            }
            else
            {
                return MyDeserializedClass;
            }
        }

        /// <summary>
        /// This function retrieves a list of species objects.
        /// </summary>
        /// <returns> A list of species. </returns>
        public static async Task<Species> GetSpecies(string SpeciesID)
        {
            Species MyDeserializedClass = new Species();

            if (int.TryParse(SpeciesID, out int ID))
            {
                try
                {
                    using HttpResponseMessage Response = await Client.GetAsync("https://swapi.dev/api/species/" + ID + "/");
                    Response.EnsureSuccessStatusCode();
                    string ResponseBody = await Response.Content.ReadAsStringAsync();

                    MyDeserializedClass = JsonConvert.DeserializeObject<Species>(ResponseBody);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                }
                return MyDeserializedClass;
            }
            else
            {
                return MyDeserializedClass;
            }
        }

        /// <summary>
        /// This function returns a starship's data (and converts it into a "Starship" object) from a given ID.
        /// </summary>
        /// <param name="StarshipID"> This is a string ID. </param>
        /// <returns> A starship object. </returns>
        public static async Task<Starship> GetStarship(string StarshipID)
        {
            Starship MyDeserializedClass = new Starship();
            try
            {
                using HttpResponseMessage Response = await Client.GetAsync("https://swapi.dev/api/starships/" + StarshipID + "/");
                Response.EnsureSuccessStatusCode();
                string ResponseBody = await Response.Content.ReadAsStringAsync();

                MyDeserializedClass = JsonConvert.DeserializeObject<Starship>(ResponseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return MyDeserializedClass;
        }

        /// <summary>
        /// This function fetches a planet's data (and converts it into a "Planet" object) from a given ID.
        /// </summary>
        /// <param name="PlanetID"> This is a string ID. </param>
        /// <returns> A planet object. </returns>
        public static async Task<Planet> GetPlanet(string PlanetID)
        {
            Planet MyDeserializedClass = new Planet();

            if (int.TryParse(PlanetID, out int ID))
            {
                try
                {
                    using HttpResponseMessage Response = await Client.GetAsync("https://swapi.dev/api/planets/" + ID + "/");
                    Response.EnsureSuccessStatusCode();
                    string ResponseBody = await Response.Content.ReadAsStringAsync();

                    MyDeserializedClass = JsonConvert.DeserializeObject<Planet>(ResponseBody);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                }
                return MyDeserializedClass;
            }
            else
            {
                return MyDeserializedClass;
            }
        }
    }
}
