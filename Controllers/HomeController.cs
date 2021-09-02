using Kursmoment3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Kursmoment3.DataAccess;
using Microsoft.AspNetCore.Authorization;

namespace Kursmoment3.Controllers
{
    /*
        För att kunna använda något överhuvudaget i denna controller så måste användaren vara authentiserad, därav attributen "Authorize".
    Annars blir användaren länkad till login rutan igen
        */
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        //Pull something from the application db with the constructor, using dependency injection
        //The "db" object will have an instance of the db context that the depency injetion will create
        //and pass it to you in your constuctor, you dnt have to worry about all the backend behind that
        //asp dtonet core takes care of everything
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Person obj)
        {
           Console.WriteLine($"Person logging in: {obj}");
           return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
