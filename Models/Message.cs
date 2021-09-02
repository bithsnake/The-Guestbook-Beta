using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

/*
 *  Model will represent a table that we want in our database
*/
namespace Kursmoment3.Models
{
    public class Message
    {

        [ForeignKey("Person")] // Refers to the primary key in the Person Model, and is used when migrating model to the sql server table
        public int ID { get; set; }

        [Required]  //Makes the username field in the message box required
        [DisplayName("Namn")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Ditt namn måste vara mellan 2 - 50 tecken.")]
        [MaxLength(200)]
        public string UserName { get; set; }
        [Required]  //Makes the message field in the message box required, no need for separate checks in javascript
        [DisplayName("Meddelande")] //Used for display name instead of the property name when usedin html + asp-for tag
        [MaxLength(2000)] // Sets the maximum length of a string in the property and the same in sql server
        [StringLength(2000, MinimumLength = 3,ErrorMessage ="Ditt meddelande måste vara mellan 3 - 2000 tecken.")]
        [Column(TypeName = "varchar(10)")] // sets the column type in the sql server, Entity Framework has a tendency to allow maxmum of letters, and that is not always prefered.
        public string UserMessage { get; set; }
        [DisplayName("skriven: ")]
        public DateTime date { get; set; }

    }
}
