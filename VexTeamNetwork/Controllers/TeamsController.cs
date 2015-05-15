using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using VexTeamNetwork.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace VexTeamNetwork.Controllers
{
    public class TeamsController : Controller
    {
        private ApplicationContext db = new ApplicationContext();

        // GET: /Teams/
        public IActionResult Index()
        {
            return View(db.Teams);
        }
    }
}
