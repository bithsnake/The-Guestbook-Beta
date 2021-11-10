using Microsoft.AspNetCore.Mvc;
using TheGuestBook.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using TheGuestBook.DataAccess;
using System.Dynamic;
using Microsoft.AspNetCore.Authorization;

// Här sköts all logic för GuestBook / Klotterplanket. Jag skapar en instans av länken till databasen vilket är ApplicationDbContext och anvnänder mig utav den för att hämta data från servern.
namespace TheGuestBook.Controllers
{
    [Authorize]
    public class GuestBookController : Controller
    {
        
        private readonly ApplicationDbContext _db;
        public GuestBookController(ApplicationDbContext db)
        {
            _db = db;
        }

        
        /*The normal view*/
        public IActionResult Index()
        {
            IEnumerable<Message> obj = _db.Message;
            ViewModel mymodel = new ViewModel();
            mymodel.MessageList = obj.Reverse(); //ändrar ordningen så att senaste inlägget kommer först, iom det är ett klotterplank
            mymodel.MessageObject = new Message();
            return View(mymodel);
        }


        // Skapar inlägg i gästboken med en POST request
        [HttpPost] 
        [ValidateAntiForgeryToken] 
        public IActionResult Create(Message obj)
        {
            if (ModelState.IsValid) //Here we validate the models state by checking if the asp-validation-for tags are sending us a valid flag, these validations has been set in the models properties.
            {
                obj.date = DateTime.Now;
                obj.UserName = User.Identity.Name.Split("@")[0]; /* Just nu lyckas jag endast hämta ut email adressen på användare eftersom Identity sparar hela mailadessen i propertyn Name. Jag ska undersöka senare om det går att hämta ut FirstName & LastName som jag själv har skapat som fält i sql databasen.*/
                /*The tag-helpers in the Create.cshtml binds the asp-for="name" with the properties to the model and passed on as a parampeter to this method.*/
                _db.Message.Add(obj); 
                _db.SaveChanges(); 

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index",obj); ; // gå tillbaka till vyn men behåll värderna
         }
        // GET - EDIT MESSAGE
        public IActionResult EditMessage(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            /*find only wirks with the primary key here*/
            var obj = _db.Message.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }
        // POST - EDIT MESSAGE
        [HttpPost] 
        [ValidateAntiForgeryToken] 
        public IActionResult EditMessage(Message obj)
        {
            obj.date = DateTime.Now; /* Uppdatera meddelandets datum */
            if (ModelState.IsValid) 
            {

                _db.Message.Update(obj); /*update the current row in the sql server*/
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        // GET - DELETE MESSAGE
        public IActionResult DeleteMessage(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            /*find only wirks with the primary key here*/
            var obj = _db.Message.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        // POST - DELETE MESSAGE CONFIRM
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteMessageConfirm(int? id)
        {
            var obj = _db.Message.Find(id);

            if(obj == null)
            {
                return NotFound();
            }
            _db.Message.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
