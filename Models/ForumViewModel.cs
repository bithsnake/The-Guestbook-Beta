using Kursmoment3.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Kursmoment3.Models
{
    public class ForumViewModel : Topic
    {
        public IEnumerable<Topic> TopicList { get; set; }
        public Topic Topicobject { get; set; }
        public IEnumerable<Post> PostList { get; set; }
        public Kursmoment3User User { get; set; }
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
