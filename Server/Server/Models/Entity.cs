using System.Numerics;

namespace Server.Models
{
    public abstract class Entity
    {
        public Guid ID { get; protected set; }
        public ICollider Collider { get; protected set; }
        public Vector2 Position { get; protected set; }
        public float MoveSpeed { get; protected set; }
        public Entity(Vector2 Position, ICollider Collider, float MoveSpeed)
        {
            ID = Guid.NewGuid();
            if (Collider != null)
                this.Collider = Collider;
            else
                throw new Exception("Collider is null");
            this.Position = Position;
            this.MoveSpeed = MoveSpeed;
        }

        public virtual void Move(Vector2 Move)
        {
            Position += Move;
            Collider?.Move(Move);
        }
    }
}
