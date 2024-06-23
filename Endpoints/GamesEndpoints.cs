using GameStore.DTOs;

namespace GameStore;

public static class GamesEndpoints
{
    const string GetGameEndPointName = "GetGame";

    private static readonly List<GameDto> games = [
        new (
            1,
            "Street Fighter",
            "Fighting",
            19.99M,
            new DateOnly(1992, 7, 5)
        ),
        new (
            2,
            "Final Fantasy XIV",
            "RolePlaying",
            59.99M,
            new DateOnly(2010, 9, 30)
        ),
        new (
            3,
            "FIFA 23",
            "Sports",
            69.99M,
            new DateOnly(2022, 9, 27)
        ),
    ];

    // return: WebApplication
    // It can be chained by other WebApplication 
    // which is done by calling method
    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {

        var group = app.MapGroup("games");  //      games/...

        // Get /games
        group.MapGet("/", () => games);

        // Get /games/:id
        group.MapGet("/{id}", (int id) =>
        {
            GameDto? game = games.Find(game => game.Id == id);
            return game is null ? Results.NotFound()
                                : Results.Ok(game);
        })
            .WithName(GetGameEndPointName);

        // Post /games
        group.MapPost("/", (CreateGameDto newGame) =>
        {
            GameDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );

            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndPointName, new { id = game.Id });
        });

        // PUT /games
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = games.FindIndex(game => game.Id == id);
            // Invalid index
            if (index == -1)
            {
                return Results.NotFound();
            }
            games[index] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );

            return Results.NoContent();

        });

        // Delete /games/1

        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => id == game.Id);
            return Results.NoContent();
        }
        );
        
        return group;
    }
}
