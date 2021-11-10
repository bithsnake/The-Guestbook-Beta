using TheGuestBook.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheGuestBook.DataAccess
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        //Här skapas ett så kallat Dbset för alla modeller som sedan migreras till databasen
        public DbSet<Person> Person { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Topic> Topic { get; set; }
        public DbSet<Post>  Post { get; set; }
    }
}
