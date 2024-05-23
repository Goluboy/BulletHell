namespace BulletHell;

class FollowingOrbAttackFactory(params (float orbsMovementSpeed, VectorV startPos, float startTime)[] args) : Factory<FollowingOrbAttack>
{
    (float orbsMovementSpeed, VectorV startPos, float startTime)[] args = args;
    public override FollowingOrbAttack[] CreateAttacks()
    {
        var array = new FollowingOrbAttack[args.Length];
        for (int i = 0; i < args.Length; i++)
        {
            var arg = args[i];
            array[i] = new FollowingOrbAttack(arg.orbsMovementSpeed, arg.startPos, arg.startTime, 10000);
        }
        return array;
    }
}
