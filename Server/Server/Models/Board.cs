using System.Numerics;

namespace Server.Models
{
    public class Board : Entity
    {
        private const float MOVE_SPEED = 1.0f;
        private LineCollider TopBorder { get; set; }
        private LineCollider BottomBorder { get; set; }

        public Board(Vector2 Position, BoxCollider Collider, LineCollider TopBorder, LineCollider BottomBorder) : base(Position, Collider, MOVE_SPEED)
        {
            this.TopBorder = TopBorder;
            this.BottomBorder = BottomBorder;
        }

        public override void Move(Vector2 Move)
        {
            if ((Move.Y < 0 && (Collider.CheckCollision(BottomBorder)))|| (Move.Y > 0 && Collider.CheckCollision(TopBorder)))
            {
                base.Move(Move);
            }
        }
    }
}
