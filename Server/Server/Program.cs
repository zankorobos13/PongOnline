using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Server.Controllers;
using Server.Models;

var builder = WebApplication.CreateBuilder(args);

Guid p1 = new Guid();
Guid p2 = new Guid();
Console.WriteLine(p1);
Console.WriteLine(p2);
GamesController.NewGame(p1, p2);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.MapGet("/Games/{id}", (Guid id) =>
{
    Game Game = GamesController.GetGameByID(id);
    Game.Tick(new System.Numerics.Vector2(0,0), new System.Numerics.Vector2(0,0));
    Console.WriteLine("get");
    return new List<object>
    {
        Game.Board1.Position.X.ToString() + " " + Game.Board1.Position.Y,
        Game.Board2.Position.X.ToString() + " " + Game.Board2.Position.Y,
        Game.Ball.Position.X.ToString() + " " + Game.Ball.Position.Y,
    };
}

);

app.Run();
