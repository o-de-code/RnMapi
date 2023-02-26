using apiRnM.Interfaces;
using apiRnM.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace apiRnM.Services
{
    public class ReqResProceeder : IProceeder
    {
        private IRnMApiCaller _apiCaller;

        public ReqResProceeder(IRnMApiCaller apiCaller)
        {
            _apiCaller = apiCaller;
        }
        
        public bool CheckPersonInEpisode(string personName, string episodeName, out bool found)
        {
            var personList = _apiCaller.GetPersons(personName);
            var episode = _apiCaller.GetEpisodes(episodeName).First();
            found = true;

            if(!personList.Any() || episode == null)
            {
                found = false;
                return false;
            }
            else
            {
                var allEpisodesQuery = personList.Select(
                    person => person.episode.Select(
                        episodeUrl => episodeUrl.Split('/').Last()))
                    .Distinct();

                foreach(var ep in allEpisodesQuery)
                {
                    if (ep.Contains(episode.id.ToString()))
                        return true;
                }
            }
            return false;
        }

        public JsonResult GetPersonData(string PersonName)
        {
            var persons = _apiCaller.GetPersons(PersonName);
            List<Location> locations = null;
            List<PersonResponse> personResponse = null;

            foreach (var person in persons)
            {
                var or = person.origin.ToString();
                var jdoc = JsonDocument.Parse(or);

                var jel = jdoc.RootElement;

                var location = _apiCaller.GetDataFromApi(jel.GetProperty("url").ToString());

                if (location != null)
                    locations.Add(JsonSerializer.Deserialize<List<Location>>(location).First());
            }

            for(int counter = 0; counter < persons.Count; counter++)
            {
                var origin = persons[counter].origin.ToString();
                var jelement = JsonDocument.Parse(origin).RootElement;
                var 
            }

            var response = new JsonResult(persons);
            return response;
        }
    }
}
