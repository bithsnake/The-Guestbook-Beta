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
        Startsidan för hemsidan, som inte har något speciellt i sig.

        För att kunna använda något överhuvudaget i denna controller så måste användaren vara authentiserad, därav attributen "Authorize".
        Annars blir användaren länkad till login rutan igen.
    */
    [Authorize]
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
