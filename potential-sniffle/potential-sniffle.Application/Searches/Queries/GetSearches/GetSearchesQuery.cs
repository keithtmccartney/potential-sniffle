using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace potential_sniffle.Application.Searches.Queries.GetSearches
{
    public class GetSearchesQuery : IRequest<IEnumerable<Search>>
    {
        public string scrapeURL { get; set; }

        public string searchURL { get; set; }

        public string searchKeyword { get; set; }

        public int searchLimit { get; set; }
    }

    public class GetSearchesQueryHandler : IRequestHandler<GetSearchesQuery, IEnumerable<Search>>
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public async Task<List<KeyValuePair<string, int>>> GetSearchResults (GetSearchesQuery request)
        {
            if (request.searchKeyword.Contains(" "))
                request.searchKeyword = request.searchKeyword.Replace(" ", "+");

            // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
            HttpClient client = new HttpClient();

            string uri = string.Format("{0}/search?num={1}&q={2}", request.scrapeURL, request.searchLimit, request.searchKeyword);

            List<string> pageResults = new List<string>();

            List<KeyValuePair<string, int>> searchKeywordResults = new List<KeyValuePair<string, int>>();

            #region cite Regex
            // Define a regular expression.
            // < Character. Matches a "<" character (char code 60).
            // c Character. Matches a "c" character (char code 99). Case sensitive.
            // i Character. Matches a "i" character (char code 105). Case sensitive.
            // t Character. Matches a "t" character (char code 116). Case sensitive.
            // e Character. Matches a "e" character (char code 101). Case sensitive.
            // [ Character set. Match any character in the set.
            // \s Whitespace. Matches any whitespace character (spaces, tabs, line breaks).
            // ] Character set. Match any character in the set.
            // + Quantifier. Match 1 or more of the preceding token.
            // ( Capturing group #1. Groups multiple tokens together and creates a capture group for extracting a substring or using a backreference.
            // [^ Negated set. Match any character that is not in the set.
            // > Character. Matches a ">" character (char code 62).
            // ] Negated set. Match any character that is not in the set.
            // + Quantifier. Match 1 or more of the preceding token.
            // ) Capturing group #1. Groups multiple tokens together and creates a capture group for extracting a substring or using a backreference.
            // > Character. Matches a ">" character (char code 62).
            // ( Capturing group #2. Groups multiple tokens together and creates a capture group for extracting a substring or using a backreference.
            // (?: Non-capturing group. Groups muitiple tokens together without creating a capture group.
            // . Dot. Matches any character except line breaks.
            // (?! Negative lookahead. Specifies a group that can not match after the main expression (if it matches, the result is discarded).
            // \< Escaped character. Matches a "<" character (char code 60).
            // \/ Escaped character. Matches a "/" character (char code 47).
            // c Character. Matches a "c" character (char code 99). Case sensitive.
            // i Character. Matches a "i" character (char code 105). Case sensitive.
            // t Character. Matches a "t" character (char code 116). Case sensitive.
            // e Character. Matches a "e" character (char code 101). Case sensitive.
            // \> Escaped character. Matches a ">" character (char code 62).
            // ) Negative lookahead. Specifies a group that can not match after the main expression (if it matches, the result is discarded).
            // ) Non-capturing group. Groups muitiple tokens together without creating a capture group.
            // * Quantifier. Match 0 or more of the preceding token.
            // . Dot. Matches any character except line breaks.
            // ) Capturing group #2. Groups multiple tokens together and creates a capture group for extracting a substring or using a backreference.
            // < Character. Matches a "<" character (char code 60).
            // / ERROR: Unescaped forward slash. This may cause issues if copying/pasting this expression into code.
            // c Character. Matches a "c" character (char code 99). Case sensitive.
            // i Character. Matches a "i" character (char code 105). Case sensitive.
            // t Character. Matches a "t" character (char code 116). Case sensitive.
            // e Character. Matches a "e" character (char code 101). Case sensitive.
            // > Character. Matches a ">" character (char code 62).
            Regex cite = new Regex(@"<cite[\s]+([^>]+)>((?:.(?!\<\/cite\>))*.)</cite>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            #endregion

            #region page Regex
            // Define a regular expression.
            // < Character. Matches a "<" character (char code 60).
            // a Character. Matches a "a" character (char code 97). Case sensitive.
            // [ Character set. Match any character in the set.
            // \s Whitespace. Matches any whitespace character (spaces, tabs, line breaks).
            // ] Character set. Match any character in the set.
            // a Character. Matches a "a" character (char code 97). Case sensitive.
            // r Character. Matches a "r" character (char code 114). Case sensitive.
            // i Character. Matches a "i" character (char code 105). Case sensitive.
            // a Character. Matches a "a" character (char code 97). Case sensitive.
            // - Character. Matches a "-" character (char code 45).
            // l Character. Matches a "l" character (char code 108). Case sensitive.
            // a Character. Matches a "a" character (char code 97). Case sensitive.
            // b Character. Matches a "b" character (char code 98). Case sensitive.
            // e Character. Matches a "e" character (char code 101). Case sensitive.
            // l Character. Matches a "l" character (char code 108). Case sensitive.
            // + Quantifier. Match 1 or more of the preceding token.
            // ( Capturing group #1. Groups multiple tokens together and creates a capture group for extracting a substring or using a backreference.
            // [^ Negated set. Match any character that is not in the set.
            // > Character. Matches a ">" character (char code 62).
            // ] Negated set. Match any character that is not in the set.
            // + Quantifier. Match 1 or more of the preceding token.
            // ) Capturing group #1. Groups multiple tokens together and creates a capture group for extracting a substring or using a backreference.
            // > Character. Matches a ">" character (char code 62).
            // ( Capturing group #2. Groups multiple tokens together and creates a capture group for extracting a substring or using a backreference.
            // (?: Non-capturing group. Groups muitiple tokens together without creating a capture group.
            // . Dot. Matches any character except line breaks.
            // (?! Negative lookahead. Specifies a group that can not match after the main expression (if it matches, the result is discarded).
            // \< Escaped character. Matches a "<" character (char code 60).
            // \/ Escaped character. Matches a "/" character (char code 47).
            // a Character. Matches a "a" character (char code 97). Case sensitive.
            // \> Escaped character. Matches a ">" character (char code 62).
            // ) Negative lookahead. Specifies a group that can not match after the main expression (if it matches, the result is discarded).
            // ) Non-capturing group. Groups muitiple tokens together without creating a capture group.
            // * Quantifier. Match 0 or more of the preceding token.
            // . Dot. Matches any character except line breaks.
            // ) Capturing group #2. Groups multiple tokens together and creates a capture group for extracting a substring or using a backreference.
            // < Character. Matches a "<" character (char code 60).
            // / ERROR: Unescaped forward slash. This may cause issues if copying/pasting this expression into code.
            // a Character. Matches a "a" character (char code 97). Case sensitive.
            // > Character. Matches a ">" character (char code 62).
            Regex page = new Regex(@"<a[\s]aria-label+([^>]+)>((?:.(?!\<\/a\>))*.)</a>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            #endregion

            #region href Regex
            // Define a regular expression.
            // h Character. Matches a "h" character(char code 104).Case sensitive.
            // r Character. Matches a "r" character(char code 114).Case sensitive.
            // e Character. Matches a "e" character(char code 101).Case sensitive.
            // f Character. Matches a "f" character(char code 102).Case sensitive.
            // * Quantifier.Match 0 or more of the preceding token.
            // . Dot.Matches any character except line breaks.
            // = Character.Matches a "=" character(char code 61).
            // [ Character set.Match any character in the set.
            // ' Character. Matches a "'" character (char code 39).
            // " Character. Matches a """ character(char code 34).
            // " Character. Matches a """ character(char code 34).
            // ] Character set.Match any character in the set.
            // ( Capturing group #1. Groups multiple tokens together and creates a capture group for extracting a substring or using a backreference.
            // [^ Negated set.Match any character that is not in the set.
            // ' Character. Matches a "'" character (char code 39).
            // " Character. Matches a """ character(char code 34).
            // " Character. Matches a """ character(char code 34).
            // ] Negated set.Match any character that is not in the set.
            // + Quantifier.Match 1 or more of the preceding token.
            // ? Lazy.Makes the preceding quantifier lazy, causing it to match as few characters as possible.
            // ) Capturing group #1. Groups multiple tokens together and creates a capture group for extracting a substring or using a backreference.
            // [ Character set.Match any character in the set.
            // ' Character. Matches a "'" character (char code 39).
            // " Character. Matches a """ character(char code 34).
            // " Character. Matches a """ character(char code 34).
            // ] Character set.Match any character in the set.
            Regex href = new Regex(@"href*.=['""]([^'""]+?)['""]", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            #endregion

            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36");

            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                string responseGetStringAsync = await client.GetStringAsync(uri);

                // Find page matches.
                // The 'a' tag with "aria-label" attribute seems to be used only for the page links at the bottom.
                // Generally, the contents of the "<div role="navigation">" and "<h1 class="Uo8X3b OhScic zsYMMe">Page navigation</h1>" tags are taken.
                List<string> pageMatches = new List<string>(page.Matches(responseGetStringAsync).Cast<Match>().Select(m => m.Groups[0].Value).Distinct()).ToList();

                // Report on each page match.
                for (int i = 0; i < pageMatches.Count; i++)
                {
                    List<string> hrefMatches = new List<string>(href.Matches(pageMatches[i]).Cast<Match>().Select(m => m.Groups[0].Value).Distinct()).ToList();

                    foreach (string hrefMatch in hrefMatches)
                    {
                        pageResults.Add(request.scrapeURL + hrefMatch.Replace("href='", "").Replace("href=\"", "").TrimEnd('"'));
                    }
                }

                // Find cite matches.
                // The 'cite' tag currently skips the "Ad" links at the top and bottom,
                // the "People also ask" links in the middle,
                // the "Images for" and "Related searches" links at the bottom.
                // Generally, the contents of the "<div id="search">" and "<h1 class="Uo8X3b OhScic zsYMMe">Search Results</h1>" tags are taken.
                List<string> citeMatches = new List<string>(cite.Matches(responseGetStringAsync).Cast<Match>().Select(m => m.Groups[0].Value).Distinct().ToList());

                // Report on each cite match.
                for (int i = 0; i < citeMatches.Count; i++)
                {
                    if (citeMatches[i].Contains(request.searchURL))
                        searchKeywordResults.Add(new KeyValuePair<string, int>(citeMatches[i], i));
                }

                foreach (string pageResult in pageResults)
                {
                    if (citeMatches.Count < request.searchLimit)
                    {
                        responseGetStringAsync = await client.GetStringAsync(pageResult);

                        List<string> citeNextMatches = new List<string>(cite.Matches(responseGetStringAsync).Cast<Match>().Select(m => m.Groups[0].Value).Distinct().ToList());

                        // Report on each cite match.
                        for (int i = 0; i < citeNextMatches.Count; i++)
                        {
                            if (citeNextMatches[i].Contains(request.searchURL))
                                searchKeywordResults.Add(new KeyValuePair<string, int>(citeNextMatches[i], i));
                        }

                        citeMatches.AddRange(citeNextMatches);
                    }
                }

                // Populate an empty list with a zero entry.
                if (searchKeywordResults.Count == 0)
                    searchKeywordResults.Add(new KeyValuePair<string, int>(request.searchURL, 0));
            }
            catch (HttpRequestException e)
            {
                throw;
            }

            return searchKeywordResults;
        }

        public async Task<IEnumerable<Search>> Handle(GetSearchesQuery request, CancellationToken cancellationToken)
        {
            List<Search> searchResults = new List<Search>();

            List<KeyValuePair<string, int>> results = new List<KeyValuePair<string, int>>();

            results = await GetSearchResults(request);

            foreach (KeyValuePair<string, int> result in results)
            {
                Search search = new Search();

                search.citeMatch = result.Key;
                search.citeMatchPosition = result.Value;

                searchResults.Add(search);
            }

            var qh = searchResults;

            return await Task.FromResult(qh);
        }
    }
}
