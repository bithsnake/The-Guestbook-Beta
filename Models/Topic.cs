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
        [MaxLength(2000)] // Sets the maximum length of a string in the property and the same in sql server
        [StringLength(2000, MinimumLength = 3, ErrorMessage = "Ditt meddelande måste vara mellan 3 - 2000 tecken.")]
        [Column(TypeName ="nvarchar(10)")]
        public string ForumTopic { get; set; }
        [Required]  //Makes the message field in the message box required, no need for separate checks in javascript
        [DisplayName("Beskrivning")] //Used for display name instead of the property name when usedin html + asp-for tag
        [MaxLength(2000)] // Sets the maximum length of a string in the property and the same in sql server
        [StringLength(2000, MinimumLength = 3, ErrorMessage = "Ditt meddelande måste vara mellan 3 - 2000 tecken.")]
        [Column(TypeName = "nvarchar(10)")]
        public string ForumTopicDescription { get; set; }
        public List<Post> Posts { get; set; }

        [DisplayName("Skapad av: ")]
        public DateTime date { get; set; }
    }
}
