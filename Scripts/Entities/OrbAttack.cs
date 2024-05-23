namespace BulletHell;

abstract internal class OrbAttack : Attack
{

    public Orb[] Orbs { get; protected set; }
    protected float orbsMovementSpeed;
    protected float AttackMovementSum;
    protected float orbsMovementSum;
    public OrbAttack(int orbsCount, float orbsMovementSpeed,
    VectorV startPos, Func<float, VectorV> offsetFunction, float startTime, float duration)
        : base(orbsMovementSpeed, startPos, offsetFunction, startTime, duration)
    {
         Orbs = new Orb[orbsCount];
         this.orbsMovementSpeed = orbsMovementSpeed;
    }
    public Orb this[int index]
    {
        get { return Orbs[index]; }
    }
    public override bool IsCollide(VectorV playerPosition)
    {
        foreach (var orb in Orbs)
        {
            if (RectangleF.Intersect(new RectangleF(new PointF(playerPosition.X, playerPosition.Y), new SizeF(28, 28)),
                new RectangleF(new PointF(orb.Position.X, orb.Position.Y), new SizeF(25, 25))) != RectangleF.Empty)
                return true;
        }
        return false;
    }

    public int CompareTo(OrbAttack? other)
    {
        return startTime.CompareTo(other.startTime);
    }
}
