using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Kursmoment3.Models
{
    public class Topic
    {
        [ForeignKey("Person")]
        public int ID { get; set; }


        [Required]  //Makes the message field in the message box required, no need for separate checks in javascript
        [DisplayName("Ämne")] //Used for display name instead of the property name when usedin html + asp-for tag
        [Column(TypeName ="nvarchar(200)")]
        public string ForumTopic { get; set; }
        [Required]  //Makes the message field in the message box required, no need for separate checks in javascript
        [DisplayName("Beskrivning")] //Used for display name instead of the property name when usedin html + asp-for tag
        [StringLength(2000, MinimumLength = 3, ErrorMessage = "Ditt meddelande måste vara mellan 3 - 2000 tecken.")]
        [Column(TypeName = "nvarchar(4000)")]
        public string ForumTopicDescription { get; set; }
        public List<Post> Posts { get; set; }

        [DisplayName("Skapad av: ")]
        [Column(TypeName = "nvarchar(100)")]
        public string CreatedBy { get; set; }
        public DateTime Date { get; set; }
    }
}
