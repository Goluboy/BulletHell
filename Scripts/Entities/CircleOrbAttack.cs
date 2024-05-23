namespace BulletHell;

internal class CircleOrbAttack : OrbAttack
{
    private Func<float, float> increasingRadiusFunction;
    public CircleOrbAttack(int orbsCount, VectorV startPos, float orbsMovementSpeed, Func<float, VectorV> offsetFunction, Func<float, float> inflateRadiusFunction, float startTime, float duration)
        : base(orbsCount, orbsMovementSpeed, startPos, offsetFunction, startTime, duration)
    {
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
        AttackMovementSum += 1;

        for (int i = 0; i < Orbs.Length; i++)
        {
            Orbs[i].LocalPosition = CircleFunction(orbsMovementSum + i * MathF.Tau / Orbs.Length) + offsetFunction(AttackMovementSum);
        }
    }
}

