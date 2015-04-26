using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VexTeamNetwork.Models;

namespace VexTeamNetwork.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private NetworkContext db = new NetworkContext();
        public JsonResult Autocomplete(string term)
        {
            term = term.ToLower();
            HashSet<SearchResult> items = new HashSet<SearchResult>(db.Teams.Where(t =>
                t.Number.ToLower().ToLower().Contains(term) ||
                t.Organization.ToLower().Contains(term) ||
                t.City.ToLower().Contains(term) || t.Region.ToLower().Contains(term) ||
                t.TeamName.ToLower().Contains(term)).ToSearchResultList());
            items.UnionWith(db.Competitions.Where(c =>
                c.Sku.ToLower().Contains(term) ||
                c.Name.ToLower().Contains(term) ||
                c.City.ToLower().Contains(term) || c.Region.ToLower().Contains(term)).ToSearchResultList());
            JsonResult result = Json(
                new SearchResultContainer()
                {
                    suggestions = items.ToList(),
                    query = term
                },
                JsonRequestBehavior.AllowGet);
            return result;
        }
    }
}