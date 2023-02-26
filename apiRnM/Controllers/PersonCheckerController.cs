using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Net.Http;
using System;
using System.Web;
using apiRnM.Interfaces;
using apiRnM.Utils;
using System.Xml.Linq;
using System.Net;

namespace apiRnM.Controllers
{
    [ApiController]
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
                return NotFound();

            JsonResult result = _proceeder.GetPersonData(name.ToLower());

            return result;
        }

        [HttpPost]
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
                return NotFound();

            return result;
        }

        
    }
}
