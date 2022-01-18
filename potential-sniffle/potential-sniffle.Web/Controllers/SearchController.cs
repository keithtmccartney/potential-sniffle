using Microsoft.AspNetCore.Mvc;
using potential_sniffle.Application.Searches.Queries.GetSearches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace potential_sniffle.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ApiControllerBase
    {
        /*// GET: api/<SearchController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<SearchController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SearchController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SearchController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SearchController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/

        // GET: api/<SearchController>
        [HttpGet]
        public async Task<IEnumerable<Search>> Get([FromQuery] GetSearchesQuery query)
        {
            return await Mediator.Send(query /*new GetSearchesQuery()*/);
        }
    }
}
