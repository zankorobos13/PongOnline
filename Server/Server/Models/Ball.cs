using System.Numerics;

namespace Server.Models
{
    public class Ball : Entity
    {
        private const float MOVE_SPEED = 3.0f;
        public Vector2 Movement { get; set;  }

        public static List<LineCollider> LineColliders = new List<LineCollider>();

        public Ball(Vector2 Position, ICollider Collider) : base(Position, Collider, MOVE_SPEED)
        {
            Random rand = new Random();
            Movement = Vector2.Normalize(new Vector2((float)rand.NextDouble(), (float)rand.NextDouble()));
        }

        public override void Move(Vector2 Move)
        {
            foreach (var LineCollider in LineColliders)
            {
                if (Collider.CheckCollision(LineCollider))
                {
                    Move = Reflect(Move, LineCollider);
                    while (Collider.CheckCollision(LineCollider)){
                        base.Move(Move);
                    }
                    break;
                }
            }
            Movement = Move;
            base.Move(Move);
        }

        private Vector2 Reflect(Vector2 Move, LineCollider LineCollider)
        {
            return new Vector2((float)(Move.X * Math.Cos(2 * LineCollider.Angle) + Move.Y * Math.Sin(2 * LineCollider.Angle)), (float)(Move.X * Math.Sin(2 * LineCollider.Angle) - Move.Y * Math.Cos(2 * LineCollider.Angle)));
        }
    }
}
