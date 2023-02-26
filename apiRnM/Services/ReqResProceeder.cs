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

        public string GetPersonData(string PersonName)
        {
            var persons = _apiCaller.GetPersons(PersonName);

            List<PersonResponse> personResponse = new List<PersonResponse>();

            for(int counter = 0; counter < persons.Count; counter++)
            {
                var origin = persons[counter].origin.ToString();
                var jelement = JsonDocument.Parse(origin).RootElement;
                var location = _apiCaller.GetDataFromApi(jelement.GetProperty("url").ToString());
                var parsedLocation = new Location();

                //TO EDIT encoding
                if (location != null)
                    parsedLocation = JsonSerializer.Deserialize<Location>(location);

                personResponse.Add(new PersonResponse()
                {
                    name = persons[0].name,
                    gender = persons[0].gender,
                    species = persons[0].species,
                    status = persons[0].status,
                    type = persons[0].type,
                    origin = new Origin
                    {
                        name = parsedLocation.name,
                        dimension = parsedLocation.dimension,
                        type = parsedLocation.type
                    }
                });
            }

            var response = JsonSerializer.Serialize(personResponse);
            return response;
        }
    }
}
