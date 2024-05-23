namespace BulletHell;

class MeshOrbAttack : OrbAttack
{
    public readonly float Width;
    public readonly float Height;
    public MeshOrbAttack(int orbsInRow, int numberOfRows, float distance, float orbsMovementSpeed, VectorV startPos,
        Func<float, VectorV> offsetFunction, float startTime, float duration)
        : base(numberOfRows * orbsInRow, orbsMovementSpeed, startPos, offsetFunction, startTime, duration)
    {
        Orbs = OrderOrbs(numberOfRows, orbsInRow, distance);
    }

    public override void Move()
    {
        orbsMovementSum += orbsMovementSpeed;
        var a = offsetFunction(orbsMovementSum);

        for (int i = 0; i < Orbs.Length; i++)
        {
            Orbs[i].LocalPosition = startPos + a;
        }
    }

    private Orb[] OrderOrbs(int numberOfRows, int orbsInRow, float distance)
    {
        List<Orb> list = new List<Orb>();

        for (int j = 0; j < orbsInRow; j++)
        {
            for (int i = 0; i < numberOfRows; i++)
            {
                list.Add(new Orb(startPos + new VectorV((j + ((i+1) % 2) * 0.5f) * distance * MathF.Sqrt(3) / 2, - i * distance), Assets1.refresher));
            }
        }

        return list.ToArray();
    }
}
