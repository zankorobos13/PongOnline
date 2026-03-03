using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using System.Numerics;

namespace Server.Models
{
    public class Game
    {
        public Guid GameID { get; private set; }
        public Guid Player1ID {  get; private set; }
        public Guid Player2ID { get; private set; }
        public Board Board1 { get; private set;  }
        public Board Board2 { get;  private set; }
        public LineCollider TopBorder { get; private set;  }
        public LineCollider BottomBorder { get; private set; }
        public Ball Ball { get; private set;  }

        private DateTime InitTime { get; set; }
        private DateTime RecentTime { get; set; }
        private DateTime NewTime { get; set; }

        public Game(Guid Player1ID, Guid Player2ID)
        {
            GameID = Guid.Parse("afb4ce76-5b17-4038-83fb-e9993ee6fe95");//Guid.NewGuid();
            Console.WriteLine(GameID);
            InitTime = DateTime.Now;
            RecentTime = InitTime;
            NewTime = InitTime;
            this.Player1ID = Player1ID;
            this.Player2ID = Player2ID;

            TopBorder = new LineCollider(new Vector2(-11.5f, 5), new Vector2(11.5f, 5));
            BottomBorder = new LineCollider(new Vector2(-11.5f, -5), new Vector2(11.5f, -5));
            new LineCollider(new Vector2(-11.5f, -5), new Vector2(-11.5f, 5));
            new LineCollider(new Vector2(11.5f, -5), new Vector2(11.5f, 5));

            //Board1 = new Board(new Vector2(-10.5f, 0), new BoxCollider(
            //    new Vector2(-11, 1), new Vector2(-10, 1), new Vector2(-11, -1), new Vector2(-10, -1)
            //    ), TopBorder, BottomBorder);
            Board1 = new Board(new Vector2(-10.5f, 0), new BoxCollider(
                new LineCollider(new Vector2(-10.9f, 1), new Vector2(-10.1f, 1)),
                new LineCollider(new Vector2(-10, 1), new Vector2(-10, -1)),
                new LineCollider(new Vector2(-10.1f, -1), new Vector2(-10.9f, -1)),
                new LineCollider(new Vector2(-11, -1), new Vector2(-11, 1))
                ), TopBorder, BottomBorder);
            //Board2 = new Board(new Vector2(10.5f, 0), new BoxCollider(
            //    new Vector2(10, 1), new Vector2(11, 1), new Vector2(10, -1), new Vector2(11, -1)
            //    ), TopBorder, BottomBorder);
            Board2 = new Board(new Vector2(10.5f, 0), new BoxCollider(
                new LineCollider(new Vector2(10.9f, 1), new Vector2(10.1f, 1)),
                new LineCollider(new Vector2(10, 1), new Vector2(10, -1)),
                new LineCollider(new Vector2(10.1f, -1), new Vector2(10.9f, -1)),
                new LineCollider(new Vector2(11, -1), new Vector2(11, 1))
                ), TopBorder, BottomBorder);
            Ball = new Ball(new Vector2(0, 0), new CircleCollider(new Vector2(0, 0), 0.5f));
        }

        public void Tick() // Подумать над BallMove
        {
            NewTime = DateTime.Now;
            float DeltaTimeSeconds = (float)(NewTime - RecentTime).TotalSeconds;
            
            Board1.Move(Board1.Direction * Board1.MoveSpeed * DeltaTimeSeconds);
            Board2.Move(Board2.Direction * Board2.MoveSpeed * DeltaTimeSeconds);
            
            Ball.Move(Ball.MoveSpeed * DeltaTimeSeconds);

            //Console.WriteLine(  Ball.Position.X.ToString() + " " + Ball.Position.Y.ToString() + " " + DeltaTimeSeconds.ToString() );
            //Console.WriteLine(  Ball.Direction.X.ToString()+ " " + Ball.Direction.Y.ToString() + Ball.MoveSpeed);
            RecentTime = NewTime;
            
        }


    }
}
