using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursmoment3.Models
{
    public class ViewModel : Message
    {
        public IEnumerable<Message> MessageList { get; set; }
        public Message MessageObject { get; set; }
    }
}
