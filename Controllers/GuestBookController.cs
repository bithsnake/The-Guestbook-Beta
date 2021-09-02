using Microsoft.AspNetCore.Mvc;
using Kursmoment3.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Kursmoment3.DataAccess;
using System.Dynamic;
using Microsoft.AspNetCore.Authorization;

namespace Kursmoment3.Controllers
{
    [Authorize]
    public class GuestBookController : Controller
    {
        private readonly ApplicationDbContext _db;
        //Pull something from the application db with the constructor, using dependency injection
        //The "db" object will have an instance of the db context that the depency injetion will create
        //and pass it to you in your constuctor, you dnt have to worry about all the backend behind that
        //asp dtonet core takes care of everything
        public GuestBookController(ApplicationDbContext db)
        {
            _db = db;
        }


        /*The normal view*/
        public IActionResult Index()
        {
            IEnumerable<Message> obj = _db.Message; // assign the messageList that is a IEnumerable of type Message and store all the _db.Message table into this object
            ViewModel mymodel = new ViewModel();
            mymodel.MessageList = obj.Reverse(); //ändrar ordningen så att senaste inlägget kommer först, iom det är ett klotterplank
            mymodel.MessageObject = new Message();
            return View(mymodel);
        }

        /*Subpage when creating a new message*/
       /*GET - CREATE*/
        public IActionResult Create()
        {
            return View();
        }
        // POST - CREATE A MESSAGE
        [HttpPost] /*HttpPost attribute has to be used for asp dotnet to know that this is a POST request*/
        [ValidateAntiForgeryToken] /*This attribute helps defend against cross-site request forgery.*/
        public IActionResult Create(Message obj)
        {
            if (ModelState.IsValid) //Here we validate the models state by checking if the asp-validation-for tags are sending us a valid flag, these validations has been set in the models properties.
            {
                obj.date = DateTime.Now;
                /*The tag-helpers in the Create.cshtml binds the asp-for="name" with the properties to the model and passed on as a parampeter to this method.*/
                _db.Message.Add(obj); /*Add the message to the database in the "Message" table*/
                _db.SaveChanges(); /*Save the changes done on the database*/
                /*
                    Redirect to the index page when you click submit in the forms and Add the message to the database.
                    Since it is in the same controller , we dont have to define the controller name.
                */
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index",obj); ; // gå tillbaka till vyn men behåll värderna
            //return View(obj); ; // gå tillbaka till vyn men behåll värderna
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

        // POST - DELETE MESSAGE
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
