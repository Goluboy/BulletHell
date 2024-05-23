namespace BulletHell;

internal class Player : Entity
{
    public new VectorV Position => Model.GetInstance().PlayerPosition;

    public Player(Image sprite) : base(sprite)
    {
    }
}
