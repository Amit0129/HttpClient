using Common;
using Microsoft.EntityFrameworkCore;

namespace API.Context
{
    public class PersonDB_Context : DbContext
    {
        public PersonDB_Context(DbContextOptions options) :base(options)
        {

        }
        public DbSet<Person> PersonInfo { get; set; }
    }
}
