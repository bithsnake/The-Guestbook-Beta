using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TheGuestBook.Models
{
    public class Post
    {

        [ForeignKey("Topic")]
        public int PostID { get; set; }
        public int TOPIC_ID { get; set; }

        [StringLength(2000, MinimumLength = 2, ErrorMessage = "Ditt meddelande måste vara mellan 2 - 2000 tecken.")]
        [DisplayName("Användare: ")] //Used for display name instead of the property name when usedin html + asp-for tag
        [Column(TypeName = "varchar(100)")] // sets the column type in the sql server, Entity Framework has a tendency to allow maxmum of letters, and that is not always prefered.
        public string PostCreatedBy { get; set; }

        [StringLength(2000, MinimumLength = 2, ErrorMessage = "Ditt meddelande måste vara mellan 2 - 2000 tecken.")]
        [DisplayName("Meddelande")] //Used for display name instead of the property name when usedin html + asp-for tag
        [MaxLength(2000)] // Sets the maximum length of a string in the property and the same in sql server
        [Column(TypeName = "varchar(10)")] // sets the column type in the sql server, Entity Framework has a tendency to allow maxmum of letters, and that is not always prefered.
        public string UserPost { get; set; }

        [DisplayName("skriven: ")]
        public DateTime Date { get; set; }
    }
}
