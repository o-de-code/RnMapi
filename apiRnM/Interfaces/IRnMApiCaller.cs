using apiRnM.Models;
using System.Collections.Generic;

namespace apiRnM.Interfaces
{
    public interface IRnMApiCaller : IApiCaller
    {
        public List<Person> GetPersons(string name);
        public List<Episode> GetEpisodes(string episode);
        public List<Location> GetLocations(string location);
    }
}
