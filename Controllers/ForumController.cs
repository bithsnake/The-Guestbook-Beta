using TheGuestBook.Areas.Identity.Data;
using TheGuestBook.Data;
using TheGuestBook.DataAccess;
using TheGuestBook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TheGuestBook.Areas.Identity;
/*
    Den här kontrollern tar hand om allt som händer i forumet dvs läsa topics och poster. Härifrån visas även Topic vyerna "View"
 */
namespace TheGuestBook.Controllers
{
    [Authorize]
    public class ForumController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly TheGuestBookDbContext _userdb;
        private readonly UserManager<TheGuestBookUser> _usermanager;
        public ForumController(ApplicationDbContext db, TheGuestBookDbContext userdb, UserManager<TheGuestBookUser> usermanager)
        {
            _db = db;
            _userdb = userdb;
            _usermanager = usermanager;

        }

        //Detta är startsidan för Forumet
        /// <summary>
        /// Topic Page View
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            IEnumerable<Topic> obj = _db.Topic; // Här lägger jag Topic som en Ienumerable så att jag kan köra en foreach loop på alla Topics som hämtas från databasen.
            ForumViewModel mymodel = new()
            {
                //ändrar ordningen så att senaste inlägget kommer först, iom det är ett klotterplank
                TopicList = obj.Reverse()
            };
            return View(mymodel);
        }
        
        //Här skapar jag en egen model som jag matar in data i ForumViewModel för att kunna ta del av data från Post + Topic modellen genom att använda mig av ID jag får via parametern.
        /// <summary>
        /// Topic Page View
        /// </summary>
        /// <returns></returns>
        public IActionResult Topic(int? ID)
        {
            IEnumerable<Post> obj = _db.Post; 
            Topic topic = _db.Topic.Find(ID);
            ForumViewModel mymodel = new ForumViewModel();
            mymodel.CreatedByUser = topic.CreatedBy;
            mymodel.Topicobject = topic;
            mymodel.PostList = obj;

            return View(mymodel);
        }

        /// <summary>
        /// Create Topic POST method
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost] 
        [ValidateAntiForgeryToken] /* Denna attribut validerar post requesten för att undvika så kallad "cross-site request forgery".*/
        public IActionResult CreateTopic(Topic obj)
        {
            if (ModelState.IsValid) //Validate the model state, ie, no empty topics allowed
            {
                obj.CreatedBy = User.Identity.Name;
                obj.Date = DateTime.Now;

                /*The tag-helpers in the Create.cshtml binds the asp-for="name" with the properties to the model and passed on as a parampeter to this method.*/
                _db.Topic.Add(obj); /*Add the message to the database in the "Message" table*/
                _db.SaveChanges(); /*Save the changes done on the database*/
                /*
                    Redirect to the index page when you click submit in the forms and Add the message to the database.
                    Since it is in the same controller , we dont have to define the controller name.
                */
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", obj); ; // gå tillbaka till vyn men behåll värderna
                                                     //return View(obj); ; // gå tillbaka till vyn men behåll värderna
        }
        /// <summary>
        /// Create Post POST method
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost] 
        [ValidateAntiForgeryToken] 
        public IActionResult CreatePost(int? ID , Post obj)
        {
            if (ModelState.IsValid) //Validate the model state, ie, no empty topics allowed
            {
                obj.PostCreatedBy = User.Identity.Name;
                obj.Date = DateTime.Now;
                obj.TOPIC_ID = (int)ID;
                _db.Post.Add(obj); /*Add the message to the database in the "Message" table*/
                _db.SaveChanges(); /*Save the changes done on the database*/
                /*
                    Redirect to the index page when you click submit in the forms and Add the message to the database.
                    Since it is in the same controller , we dont have to define the controller name.
                */
                /*
                    Här anvädner jag RedirectRoute istället för att kunna stanna kvar i samma Topic beorende på ID när man gör en ny post i en topic.
                 */
                return RedirectToRoute(new
                {
                    controller = "Forum",
                    action = "Topic",
                    id = $"{ID}"
                });
            }
            return RedirectToAction("Index", obj); // gå tillbaka till vyn men behåll värderna
        }
        /// <summary>
        /// Delete Topic Check
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult DeleteTopic(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            /*find only wirks with the primary key here*/
            var obj = _db.Topic.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        /// <summary>
        /// Delete Topic POST method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteTopicConfirm(int? id)
        {
            var obj = _db.Topic.Find(id);

            if (obj == null)
            {
                return NotFound();
            }
            _db.Topic.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Denna metod tar alla Post'er från databasen så att man akn foreach loopa igenom alla på sidan
        /// <summary>
        /// Topic Sub-page
        /// </summary>
        /// <returns></returns>
        public IActionResult TopicPosts()
        {
            IEnumerable<Post> obj = _db.Post;

            ForumViewModel mymodel = new ForumViewModel
            {
                PostList = obj
            };
            return View(mymodel);
        }

    }
}
