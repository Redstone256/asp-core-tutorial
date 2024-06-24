using GameStore.Entities;

namespace GameStore.Entities;

public class Game
{
    public int Id { get; set; }

    public required string Name { get; set; }

// Association: One to One relation
// between Game and Genre
    public int GenreId{ get; set; }
    public Genre? Genre { get; set; }

    public DateOnly ReleaseDate { get; set; }
}
