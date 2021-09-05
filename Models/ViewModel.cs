using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


// Denna model används för klotterplanket / The Guestbook som ärver av Message modellen för att kunna använda UserMessage propertien
// Denna finns att hitta i GuestBookController.cs
namespace Kursmoment3.Models
{
    public class ViewModel : Message
    {
        public IEnumerable<Message> MessageList { get; set; }
        public Message MessageObject { get; set; }
    }
}
