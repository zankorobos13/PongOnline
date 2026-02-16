using System.Numerics;

namespace Server.Models
{
    public class Board : Entity
    {
        private static LineCollider TopBorder { get; set; }
        private static LineCollider BottomBorder { get; set; }

        public Board(Vector2 Position, BoxCollider Collider) : base(Position, Collider)
        {

        }

        public Board(Vector2 Position, BoxCollider Collider, LineCollider TopBorder, LineCollider BottomBorder) : base(Position, Collider)
        {
            Board.TopBorder = TopBorder ?? new LineCollider(new Vector2(), new Vector2());
            Board.BottomBorder = BottomBorder;
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
