using Microsoft.AspNetCore.Mvc;
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
    public class SearchController : ControllerBase
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

        [HttpGet]
        //public async Task<ActionResult<TodosVm>> Get()
        public async Task Get(string url, string keyword)
        //public IEnumerable<string> Get()
        {
            //return await Mediator.Send(new GetTodosQuery());
            //-----
            //return new string[] { "value1", "value2" };

            url = "http://www.infotrack.co.uk/";

            keyword = "land+registry+search";

            // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
            HttpClient client = new HttpClient();

            string google = "https://www.google.co.uk";

            int result = 100;

            string uri = string.Format("{0}/search?num={1}&q={2}", google, result, keyword);

            int previous = 0;
            int current = 1;
            int next = 2;

            List<KeyValuePair<string, int>> keywordResults = new List<KeyValuePair<string, int>>();

            // Define a regular expression for repeated words.
            // (?<= Positive lookbehind. Matches a group before the main expression without including it in the result. WARNING: The "positive lookbehind" feature may not be supported in all browsers.
            // < Character. Matches a "<" character (char code 60).
            // a Character. Matches a "a" character (char code 97). Case sensitive.
            // . Dot. Matches any character except line breaks.
            // * Quantifier. Match 0 or more of the preceding token.
            // > Character. Matches a ">" character (char code 62).
            // . Dot. Matches any character except line breaks.
            // + Quantifier. Match 1 or more of the preceding token.
            // (?= Positive lookahead. Matches a group after the main expression without including it in the result.
            // < Character. Matches a "<" character (char code 60).
            // \/ Escaped character. Matches a "/" character (char code 47).
            // a Character. Matches a "a" character (char code 97). Case sensitive.
            // > Character. Matches a ">" character (char code 62).
            //Regex regex = new Regex(@"(?<=< a.*>).+ (?=<\/ a >)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            Regex cite = new Regex(@"<cite[\s]+([^>]+)>((?:.(?!\<\/cite\>))*.)</cite>", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            Regex page = new Regex(@"<a[\s]aria-label+([^>]+)>((?:.(?!\<\/a\>))*.)</a>", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36");

            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                /*HttpResponseMessage response = await client.GetAsync(uri);

                response.EnsureSuccessStatusCode();

                string responseReadAsStringAsync = await response.Content.ReadAsStringAsync();*/

                // Above three lines can be replaced with new helper method below
                string responseGetStringAsync = await client.GetStringAsync(uri);

                // Find matches.
                /*MatchCollection matchCollection = regex.Matches(responseGetStringAsync);*/

                // Report on each match.
                /*foreach (Match match in matchCollection)
                {
                    GroupCollection groups = match.Groups;
                }*/

                var citeMatches = cite.Matches(responseGetStringAsync).Cast<Match>().Select(m => m.Groups[0].Value).Distinct();

                var pageMatches = page.Matches(responseGetStringAsync).Cast<Match>().Select(m => m.Groups[0].Value).Distinct();

                /*for (int i = 0; i <= matchCollection.Count; i++)
                {
                    if (OddNumber(i))
                    {
                    }
                }*/
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        /*public static bool OddNumber(int number)
        {
            return number % 2 != 0;
        }*/
    }
}
