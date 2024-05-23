namespace BulletHell;

internal class Orb : Entity
{
    public VectorV Dir { get; set; }
    public Orb(Image sprite) : base(sprite)
    {
    }
    public Orb(VectorV position, Image sprite) : base(position, sprite)
    {
    }
}
