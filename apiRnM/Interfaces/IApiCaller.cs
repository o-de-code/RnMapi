using System.Text.Json;

namespace apiRnM.Interfaces
{
    public interface IApiCaller
    {
        public string GetDataFromApi(string url);
    }
}
