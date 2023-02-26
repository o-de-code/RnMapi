using Microsoft.AspNetCore.Mvc;
using apiRnM.Interfaces;
using apiRnM.Utils;


namespace apiRnM.Controllers
{
    [ApiController]
    [ResponseCache(Duration=20)]
    [Route("api/v1")]
    public class PersonCheckerController : Controller
    {
        private IProceeder _proceeder;

        public PersonCheckerController(IProceeder proceeder)
        {
            _proceeder = proceeder;
        }

        [HttpGet]
        [Route("person")]
        public ActionResult<string> Person(string name)
        {
            if (name is null)
                return NotFound("Not Found");

            string result = _proceeder.GetPersonData(name.ToLower());

            if (result == null || result == "[]")
                return NotFound("Not Found");

            return result;
        }

        [HttpPost]
        [ResponseCache(Duration = 20)]
        [Route("check-person")]
        public ActionResult<bool> CheckPerson([FromBody] RequestBodyCheckPersonEpisode body)
        {
            if (body == null)
                return NotFound();

            bool found;
            var name = body.personName.ToLower();
            var episode = body.episodeName.ToLower();
            var result = _proceeder.CheckPersonInEpisode(name, episode, out found);

            if (!found)
                return NotFound("Not Found");

            return result;
        }

        
    }
}
