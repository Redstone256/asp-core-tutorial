using GameStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data;


public class GameStoreContext(DbContextOptions<GameStoreContext> options)
    : DbContext(options)
{
    // DbSet used to query and save instances
    public DbSet<Game> Games => Set<Game>();

    public DbSet<Genre> Genres => Set<Genre>();

// OnModelCreating executed as soon as migration created automatically
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // populate DB Table for "Genre" Entity with static data

        modelBuilder.Entity<Genre>().HasData(
            new { Id = 1, Name = "Fighting" },
            new { Id = 2, Name = "Roleplaying" },
            new { Id = 3, Name = "Sports" },
            new { Id = 4, Name = "Racing" },
            new { Id = 5, Name = "Kids and Farming" }
        );
    }

}
