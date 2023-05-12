using apiRnM.Interfaces;
using apiRnM.Models;
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
            var episodeList = _apiCaller.GetEpisodes(episodeName);
            found = true;

            if(!personList.Any() || episodeList == null)
            {
                found = false;
                return false;
            }
            else
            {
                var episode = episodeList.First();
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

        public string GetPersonData(string PersonName)
        {
            var persons = _apiCaller.GetPersons(PersonName);
            string response = null;
            if(persons.Any())
            {
                List<PersonResponse> personResponse = new List<PersonResponse>();
                var person = persons.First();
                var origin = person.origin.ToString();
                var jelement = JsonDocument.Parse(origin).RootElement;
                var location = _apiCaller.GetDataFromApi(jelement.GetProperty("url").ToString());
                var parsedLocation = new Location();

                //TO EDIT encoding
                if (location != null)
                    parsedLocation = JsonSerializer.Deserialize<Location>(location);

                personResponse.Add(new PersonResponse()
                {
                    name = person.name,
                    gender = person.gender,
                    species = person.species,
                    status = person.status,
                    type = person.type,
                    origin = new Origin
                    {
                        name = parsedLocation.name,
                        dimension = parsedLocation.dimension,
                        type = parsedLocation.type
                    }
                });

                response = JsonSerializer.Serialize(personResponse);
            }

            
            return response;
        }
    }
}
