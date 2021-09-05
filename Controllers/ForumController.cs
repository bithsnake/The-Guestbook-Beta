using Kursmoment3.Areas.Identity.Data;
using Kursmoment3.Data;
using Kursmoment3.DataAccess;
using Kursmoment3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Kursmoment3.Areas.Identity;
namespace Kursmoment3.Controllers
{
    [Authorize]
    public class ForumController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly Kursmoment3DbContext _userdb;
        private readonly UserManager<Kursmoment3User> _usermanager;
        public ForumController(ApplicationDbContext db, Kursmoment3DbContext userdb, UserManager<Kursmoment3User> usermanager)
        {
            _db = db;
            _userdb = userdb;
            _usermanager = usermanager;

        }
        /// <summary>
        /// Topic Page View
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            IEnumerable<Topic> obj = _db.Topic; // assign the messageList that is a IEnumerable of type Message and store all the _db.Message table into this object
            ForumViewModel mymodel = new()
            {
                //ändrar ordningen så att senaste inlägget kommer först, iom det är ett klotterplank
                TopicList = obj.Reverse()
            };
            Console.WriteLine($"Current user logged in: {User.Identity.Name}");
            return View(mymodel);
        }
        
        /// <summary>
        /// Topic Page View
        /// </summary>
        /// <returns></returns>
        public IActionResult Topic(int? ID)
        {
            IEnumerable<Post> obj = _db.Post; // assign the messageList that is a IEnumerable of type Message and store all the _db.Message table into this object
            Topic topic = _db.Topic.Find(ID);
            Kursmoment3User topicCreator = new Kursmoment3User();
            topicCreator = _userdb.Users.Find(topic.CreatedBy);
            
            ForumViewModel mymodel = new ForumViewModel();
            mymodel.User = topicCreator;
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
        [HttpPost] /*HttpPost attribute has to be used for asp dotnet to know that this is a POST request*/
        [ValidateAntiForgeryToken] /*This attribute helps defend against cross-site request forgery.*/
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
        [HttpPost] /*HttpPost attribute has to be used for asp dotnet to know that this is a POST request*/
        [ValidateAntiForgeryToken] /*This attribute helps defend against cross-site request forgery.*/
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

        /// <summary>
        /// Topic Sub-page
        /// </summary>
        /// <returns></returns>
        public IActionResult TopicPosts()
        {
            IEnumerable<Post> obj = _db.Post;
       
            ForumViewModel mymodel = new ForumViewModel
            {
                PostList = obj.Reverse()
            };
            return View(mymodel);
        }

    }
}
