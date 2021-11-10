using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheGuestBook.Models
{
    public class Person
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("Namn")]
        [Required]
        [MaxLength(50)]

        public string UserName { get; set; }
        [Required]
        [MaxLength(50)]
        public string Mailadress { get; set; }
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
    }
}
