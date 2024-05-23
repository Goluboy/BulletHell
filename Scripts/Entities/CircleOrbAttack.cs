namespace BulletHell;

internal class CircleOrbAttack : OrbAttack
{
    private Func<float, float> increasingRadiusFunction;
    private float attackMovementSpeed;
    public CircleOrbAttack(int orbsCount, VectorV startPos, float orbsMovementSpeed,
        float attackMovementSpeed, Func<float, VectorV> offsetFunction, Func<float, float> inflateRadiusFunction, float startTime, float duration)
        : base(orbsCount, orbsMovementSpeed, startPos, offsetFunction, startTime, duration)
    {
        this.attackMovementSpeed = attackMovementSpeed;
        this.increasingRadiusFunction = inflateRadiusFunction;
        for (int i = 0; i < orbsCount; i++)
        {
            Orbs[i] = new Orb(new VectorV(100, 100), Assets1.refresher);
        }
    }

    private VectorV CircleFunction(float x)
    {
        return new VectorV(startPos.X + increasingRadiusFunction(Model.GetInstance().Stopwatch.ElapsedMilliseconds - startTime) * MathF.Cos(x),
            startPos.Y + increasingRadiusFunction(Model.GetInstance().Stopwatch.ElapsedMilliseconds - startTime) * MathF.Sin(x));
    }

    public override void Move()
    {
        orbsMovementSum += orbsMovementSpeed;
        AttackMovementSum += attackMovementSpeed;

        for (int i = 0; i < Orbs.Length; i++)
        {
            Orbs[i].LocalPosition = CircleFunction(orbsMovementSum + i * MathF.Tau / Orbs.Length) + offsetFunction(AttackMovementSum);
        }
    }
}

