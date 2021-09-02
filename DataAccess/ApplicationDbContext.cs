using Kursmoment3.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursmoment3.DataAccess
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        /*Skapa tabellerna i SQL servern med dessa modeller*/
        public DbSet<Person> Person { get; set; }
        public DbSet<Message> Message { get; set; }
    }
}
