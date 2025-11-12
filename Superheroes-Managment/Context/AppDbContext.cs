using Microsoft.EntityFrameworkCore;
using Superheroes_Managment.Models;

namespace Superheroes_Managment.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Power> Powers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hero>().HasData(
                new Hero { Id = 1, Name = "Batman", Alias = "The Dark Knight" },
                new Hero { Id = 2, Name = "Spider-Man", Alias = "Friendly Neighbor"},
                new Hero { Id = 3, Name = "Flash", Alias = "The Fastest Man" },
                new Hero { Id = 4, Name = "Green Lantern" },
                new Hero { Id = 5, Name = "Thor", Alias = "God of Thunder" },
                new Hero { Id = 6, Name = "Captain America", Alias = "The First Avenger" }
                );

            modelBuilder.Entity<Power>().HasData(
                new Power { Id = 1, Name = "Detective Skills", Description = "Exceptional deduction", HeroId = 1 },
                new Power { Id = 2, Name = "Martial Arts", Description = "Hand-to-hand combat", HeroId = 1 },
                new Power { Id = 3, Name = "Spider Sense", Description = "Alerts to danger", HeroId = 2 },
                new Power { Id = 4, Name = "Web-Shooting", Description = "Spins spider webs", HeroId = 2 });
        }
    }
}
