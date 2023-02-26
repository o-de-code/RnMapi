using Microsoft.AspNetCore.Mvc;

namespace apiRnM.Interfaces
{
    public interface IProceeder
    {
        public bool CheckPersonInEpisode(string PersonName, string EpisodeName, out bool found);
        public string GetPersonData(string PersonName);
    }
}
