using System.Numerics;

namespace Server.Models
{
    public class Ball : Entity
    {
        private const double MOVE_SPEED = 3.0;

        public static List<LineCollider> LineColliders = new List<LineCollider>();

        public Ball(Vector2 Position, ICollider Collider) : base(Position, Collider)
        {

        }

        public override void Move(Vector2 Move)
        {
            foreach (var LineCollider in LineColliders)
            {
                if (Collider.CheckCollision(LineCollider))
                {
                    Move = Reflect(Move, LineCollider);
                    break;
                }
            }

        }

        private Vector2 Reflect(Vector2 Move, LineCollider LineCollider)
        {
            return new Vector2((float)(Move.X * Math.Cos(2 * LineCollider.Angle) + Move.Y * Math.Sin(2 * LineCollider.Angle)), (float)(Move.X * Math.Sin(2 * LineCollider.Angle) - Move.Y * Math.Cos(2 * LineCollider.Angle)));
        }
    }
}
