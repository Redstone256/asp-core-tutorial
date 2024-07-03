using GameStore.Data;
using GameStore.DTOs;
using GameStore.Entities;

namespace GameStore.Endpoints;

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

        var group = app.MapGroup("games")       //      games/...
                    .WithParameterValidation();

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
        group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            // create entity
            Game game = new(){
                Name = newGame.Name,
                Genre = dbContext.Genres.Find(newGame.GenreId),
                GenreId = newGame.GenreId,
                Price = newGame.Price,
                ReleaseDate = newGame.ReleaseDate,
            };

            dbContext.Games.Add(game);  // add entity into Games Table
            dbContext.SaveChanges();    // save into DB

            GameDto gameDto = new(
                game.Id,
                game.Name,
                game.Genre!.Name,
                game.Price,
                game.ReleaseDate
            );
            return Results.CreatedAtRoute(GetGameEndPointName, new { id = game.Id }, gameDto);
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
