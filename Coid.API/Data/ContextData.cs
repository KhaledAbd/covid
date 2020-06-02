using Coid.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Coid.API.Data
{
    public class ContextData : DbContext
    {
        public ContextData(DbContextOptions<ContextData> options): base(options){}

        public DbSet<User> Users{get; set;}
        public DbSet<Photo> Photos {get; set;}

        public DbSet<Vidoe> Vidoes{get; set;}

        public DbSet<Coron> Corons{get; set;}
    }
}