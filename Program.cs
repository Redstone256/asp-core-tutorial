using GameStore.Endpoints;
using GameStore.Data;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var connString = builder.Configuration.GetConnectionString("GameStore");

builder.Services.AddSqlite<GameStoreContext>(connString);

app.MapGamesEndpoints();


app.Run();
