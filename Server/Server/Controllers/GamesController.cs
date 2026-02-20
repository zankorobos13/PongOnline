using Server.Models;
using System.Numerics;

namespace Server.Controllers
{
    public static class GamesController
    {
        private static List<Game> Games = new List<Game>();

        public static void NewGame(Guid PlayerID1, Guid PlayerID2)
        {
            Game NewGame = new Game(PlayerID1, PlayerID2);
            Games.Add(NewGame);
        }

        public static void Tick()
        {
            foreach (var Game in Games)
            {
                Game.Tick(new Vector2(0f,0f), new Vector2(0f, 0f));
            } 
        }

        public static Game GetGameByID(Guid id)
        {
            Game Game = Games.Where(game => game.GameID == id).FirstOrDefault() ?? throw new Exception("There is no game with this id");
            return Game;
        }
    }
}
