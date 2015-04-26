using System;
using System.Collections.Generic;
using System.Linq;

namespace VexTeamNetwork.Models
{
    public class SearchResultContainer
    {
        public string query { get; set; }
        public List<SearchResult> suggestions { get; set; }
    }

    public class SearchResult
    {
        public string value { get; set; }
        public Object data { get; set; }
    }

    public static class SearchResultExtensions
    {
        public static List<SearchResult> ToSearchResultList(this IQueryable<Team> list)
        {
            List<SearchResult> result = new List<SearchResult>();
            foreach(Team t in list)
                result.Add(new SearchResult() { value = t.Number + " - " + t.TeamName, data = new { category = "Teams", url = "/" + t.Number } });
            return result;
        }
        
        public static List<SearchResult> ToSearchResultList(this IQueryable<Competition> list)
        {
            List<SearchResult> result = new List<SearchResult>();
            foreach ( Competition t in list )
                result.Add(new SearchResult() { value = t.Name, data = new { category = "Events", url = "/" + t.Sku } });
            return result;
        }
    }
}