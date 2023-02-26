using apiRnM.Interfaces;
using apiRnM.Models;
using Microsoft.Extensions.Options;
using apiRnM.Utils;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Xml.Linq;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Routing.Template;
using System.Runtime.InteropServices;

namespace apiRnM.Services
{
    public class RickAndMortyApiCaller : IRnMApiCaller
    {
        public RnMAppSettings RnMSettings;
        private HttpClient client = new HttpClient();

        string queryParamsName = "/?name=";


        public RickAndMortyApiCaller(IOptions<RnMAppSettings> RnMSettings)
        {
            this.RnMSettings = RnMSettings.Value;
        }
        
        public List<Person> GetPersons(string name)
        {
            var url = string.Concat(RnMSettings.characters, queryParamsName, name.ToLower());
            var personResult = new List<Person>();

            var characters = GetDataFromApi(url);    

            if(characters != null)
                personResult = JsonSerializer.Deserialize<List<Person>>(characters);

            return personResult;
        }

        public List<Episode> GetEpisodes(string episode)
        {
            var url = string.Concat(RnMSettings.episodes, queryParamsName, episode.ToLower());
            var episodeResult = new List<Episode>();

            var episodes = GetDataFromApi(url);

            if (episodes != null)
                episodeResult = JsonSerializer.Deserialize<List<Episode>>(episodes);

            return episodeResult;
        }

        public List<Location> GetLocations(string location)
        {
            var url = string.Concat(RnMSettings.locations, queryParamsName, location.ToLower());
            var locationResult = new List<Location>();

            var locations = GetDataFromApi(url);

            if (locations != null)
                locationResult = JsonSerializer.Deserialize<List<Location>>(locations);


            return locationResult;
        }

        public string GetDataFromApi(string url)
        {
            try
            {
                var response = client.GetStringAsync(url).Result;

                var jdoc = JsonDocument.Parse(response);

                var jel = jdoc.RootElement;

                return jel.GetProperty("results").ToString();
            }
            catch
            {
                return null;
            }
            
        }
    }
}
