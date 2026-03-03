using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Server.Controllers;
using Server.Models;
using System.Data;
using System.Numerics;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);

Guid p1 = Guid.Parse("aaaaaaaa-5b17-4038-83fb-e9993ee6fe95");//Guid.NewGuid();
Guid p2 = Guid.Parse("bbbbbbbb-5b17-4038-83fb-e9993ee6fe95");//Guid.NewGuid();
GamesController.NewGame(p1, p2);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHostedService<BackGroundGameTicks>();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.IncludeFields = true;
    options.SerializerOptions.PropertyNameCaseInsensitive = true;
});

var app = builder.Build();

app.MapGet("/Games/{id}", (Guid id) =>
{
    Game Game = GamesController.GetGameByID(id);
    //Game.Tick(new System.Numerics.Vector2(0, 0), new System.Numerics.Vector2(0, 0));
    //Console.WriteLine("get");
    return new Dictionary<string, float>
    {
        { "Board1_X", Game.Board1.Position.X },
        { "Board1_Y", Game.Board1.Position.Y },
        { "Board2_X", Game.Board2.Position.X },
        { "Board2_Y", Game.Board2.Position.Y },
        { "Ball_X", Game.Ball.Position.X },
        { "Ball_Y", Game.Ball.Position.Y }
    };
});



app.MapPost("Games/", async (HttpRequest request) =>{
    request.EnableBuffering();
    var body = await new StreamReader(request.Body).ReadToEndAsync();
    request.Body.Position = 0;

    //Console.WriteLine($"Получен запрос: {body}");

    try
    {
        PostGameDataStruct data = await request.ReadFromJsonAsync<PostGameDataStruct>();
        
        //Console.WriteLine("game_id: " + data.game_id);
        Game Game = GamesController.GetGameByID(Guid.Parse(data.game_id));
        Board Board;
        if (Game.Player1ID == Guid.Parse(data.player_id))
            Board = Game.Board1;
        else
            Board = Game.Board2;
        Board.Direction = new Vector2(data.move_x, data.move_y);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ошибка: {ex.Message}");
        throw;
    }
});

app.Run();
