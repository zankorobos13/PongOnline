using System.Numerics;

namespace Server.Models
{
    public class Ball : Entity
    {
        private const float MOVE_SPEED = 3f;
        public Vector2 Direction { get; set;  }

        public static List<LineCollider> LineColliders = new List<LineCollider>();

        public Ball(Vector2 Position, ICollider Collider) : base(Position, Collider, MOVE_SPEED)
        {
            Random rand = new Random();
            Direction = Vector2.Normalize(new Vector2((float)rand.NextDouble(), (float)rand.NextDouble()));
        }

        public override void Move(float DeltaTime)
        {
            List<LineCollider> CollidedLineCollliderList = new List<LineCollider>();
            foreach (var LineCollider in LineColliders)
            {
                if (Collider.CheckCollision(LineCollider))
                {
                    CollidedLineCollliderList.Add(LineCollider);
                }
            }
            if (CollidedLineCollliderList.Count == 1)
            {
                Console.WriteLine("collision");
                Direction = Reflect(Direction, CollidedLineCollliderList[0]);
                while (Collider.CheckCollision(CollidedLineCollliderList[0]))
                {
                    base.Move(Direction * MoveSpeed * DeltaTime);
                }
            }
            else if (CollidedLineCollliderList.Count == 2)
            {
                Console.WriteLine("collision");
                Direction = Reflect(Direction); // Временно работает только для 90 градусов
                while (Collider.CheckCollision(CollidedLineCollliderList[0]))
                {
                    base.Move(Direction * MoveSpeed * DeltaTime);
                }
            }
            base.Move(Direction * MoveSpeed * DeltaTime);
        }

        private Vector2 Reflect(Vector2 Move, LineCollider LineCollider)
        {
            return Vector2.Normalize(new Vector2((float)(Move.X * Math.Cos(2 * LineCollider.Angle) + Move.Y * Math.Sin(2 * LineCollider.Angle)), (float)(Move.X * Math.Sin(2 * LineCollider.Angle) - Move.Y * Math.Cos(2 * LineCollider.Angle))));
        }

        private Vector2 Reflect(Vector2 Move) // Временно работает только для 90 градусов
        {
            return Vector2.Normalize(new Vector2(-Move.X, -Move.Y));
        }
    }
}
