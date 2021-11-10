using TheGuestBook.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Den här modellen används för att lagra data från modeller som är relevanta för forumet

namespace TheGuestBook.Models
{
    public class ForumViewModel : Topic
    {
        public IEnumerable<Topic> TopicList { get; set; }
        public Topic Topicobject { get; set; }
        public IEnumerable<Post> PostList { get; set; }
        public TheGuestBookUser User { get; set; }
        public string CreatedByUser { get; set; }
        public string UserPost { get; set; }
        public ForumViewModel()
        {
            Post post = new Post();
            UserPost = post.UserPost;
            Topic topic = new Topic();
            Topicobject = topic;
        }
        

    }
}
