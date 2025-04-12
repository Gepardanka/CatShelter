using CatShelter.Models;
using Microsoft.EntityFrameworkCore;

namespace CatShelter.Data
{
    public class AppDbContext : DbContext
    {
        public required DbSet<Cat> Cats { get; set; }
        public required DbSet<User> Users { get; set; }
        public required DbSet<Adoption> Adoptions { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
