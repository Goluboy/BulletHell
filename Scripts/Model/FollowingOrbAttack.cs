namespace BulletHell;

class FollowingOrbAttack : OrbAttack
{
    private VectorV playerPos;
    private FollowingOrbAttack(float orbsMovementSpeed,
        VectorV startPos, Func<float, VectorV> offsetFunction, float startTime, float duration)
        : base(1, orbsMovementSpeed, startPos, offsetFunction, startTime, duration)
    {

    }

    public FollowingOrbAttack(float orbsMovementSpeed, VectorV startPos, float startTime, float duration)
        : base(1, orbsMovementSpeed, startPos, null, startTime, duration)
    {
        playerPos = new VectorV(-1, -1);
        offsetFunction = new Func<float, VectorV>(x => new VectorV(playerPos.X - startPos.X, playerPos.Y - startPos.Y));

        Orbs[0] = new Orb(startPos, Assets1.refresher);
    }

    public override void Move()
    {
        if (playerPos == new VectorV(-1, -1))
        {
            playerPos = Model.GetInstance().PlayerPosition;
            offsetFunction = new Func<float, VectorV>(x => new VectorV(playerPos.X - startPos.X, playerPos.Y - startPos.Y));
        }

        orbsMovementSum += orbsMovementSpeed;

        for (int i = 0; i < Orbs.Length; i++)
        {
            Orbs[i].LocalPosition += offsetFunction(0).Normalize() * orbsMovementSpeed;
        }
    }
}
