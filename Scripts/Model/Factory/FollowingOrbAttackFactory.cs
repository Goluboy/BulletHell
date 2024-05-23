namespace BulletHell;

class FollowingOrbAttackFactory(params (int number, float orbsMovementSpeed, VectorV startPos, float startTime)[] args) : Factory<FollowingOrbAttack>
{
    (int number, float orbsMovementSpeed, VectorV startPos, float startTime)[] args = args;
    public override FollowingOrbAttack[] CreateAttacks()
    {
        var list = new List<FollowingOrbAttack>();
        for (int i = 0; i < args.Length; i++)
        {
            var arg = args[i];
            for (int j = 0; j < arg.number; j++)
                list.Add(new FollowingOrbAttack(arg.orbsMovementSpeed, arg.startPos, arg.startTime + 10 * j, 10000));
        }
        return list.ToArray();
    }
}
