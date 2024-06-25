using GameStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data;


public class GameStoreContext(DbContextOptions<GameStoreContext> options)
    : DbContext(options)
{
    // DbSet used to query and save instances
    public DbSet<Game> Games => Set<Game>();

    public DbSet<Genre> Genres => Set<Genre>();

}
