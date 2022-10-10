using Microsoft.EntityFrameworkCore;
using SuperHero.API.Models;

namespace SuperHero.API.Database
{
    public class AppDatabase : DbContext
    {
        public AppDatabase(DbContextOptions<AppDatabase> options) : base(options)
        { 
        }
        public DbSet<SuperHeroModel> SuperHeroes => Set<SuperHeroModel>();
    }
}
