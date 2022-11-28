using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using TSAT.Data;
using TSAT.Models;
using Route = TSAT.Models.Route;

namespace TSAT.Controllers
{
    public class SurveyController : Controller
    {

        private ApplicationDbContext _db { get; set; }

        public SurveyController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            Destination mainland = new Destination()
            {
                Id = 1,
                Name = "Mainland"
            };

            Destination island = new Destination()
            {
                Id = 2,
                Name = "Island"
            };


            //create routes with that destination
            var routes = new List<Route>() {
                new Route() { Id = 001, Name = "via 3rd-Mainland-Bridge", DestinationId = 1 },
                new Route() { Id = 002, Name = "via Carter-Bridge", DestinationId = 1 },
                new Route() { Id = 003, Name = "via Eko-Bridge", DestinationId = 1 },
                new Route() { Id = 004, Name = "Via Lekki-Tow-Gate", DestinationId = 2 },
                new Route() { Id = 005, Name = "via Bonnie-Camp", DestinationId = 2 },
                new Route() { Id = 006, Name = "via Adeola Odeku'", DestinationId = 2 },
                new Route() { Id = 007, Name = "via Landmark'", DestinationId = 2 },
             };



            var ratings = new List<Rating>()
            {
                new Rating() {
                    Id = 1,
                    Text = "Smooth",
                    Score = 5
                },
                new Rating() {
                    Id = 1,
                    Text = "Slightly Congested",
                    Score = 4
                },
                new Rating() {
                    Id = 1,
                    Text = "Congested",
                    Score = 3
                },
                new Rating() {
                    Id = 1,
                    Text = "Highly Congested",
                    Score = 2
                },
                new Rating() {
                    Id = 1,
                    Text = "Blocked",
                    Score = 1
                },

            };

            var survey = new Survey()
            {

                UserName = User?.Identity?.Name!,
                RouteList = routes.Select(r => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id.ToString()
                }),
                RatingList = ratings.Select(r => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = r.Text,
                    Value = r.Score.ToString()
                }),

            };

            return View(survey);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Survey data)
        {
            if (ModelState.IsValid)
            {
                // create destinations

                if (data == null)
                {
                    return View("Index");
                }

                var survey = new Survey()
                {
                    UserName = User?.Identity?.Name!,
                    Route = data.Route,
                    Comment = data.Comment,
                    Score = data.Score
                };

                _db.Surveys.Add(survey);

                // save data to the database
                _db.SaveChanges();

                //return to dashboard with chart
                return RedirectToAction("Index", "Dashboard");

            }

            return View(data);

        }
    };

}

