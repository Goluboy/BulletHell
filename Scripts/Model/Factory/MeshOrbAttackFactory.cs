namespace BulletHell;
class MeshOrbAttackFactory(float distance, params (float speed, VectorV startPos, Func<float, VectorV> offsetFunction, int orbsInRow, int numberOfRows, float startTime)[] args) : Factory<MeshOrbAttack>
{
    private (float speed, VectorV startPos, Func<float, VectorV> offsetFunction, int orbsInRow, int numberOfRows, float startTime)[] args = args;
    private float distance = distance;
    public override MeshOrbAttack[] CreateAttacks()
    {
        var array = new MeshOrbAttack[args.Length];
        for (int i = 0; i < args.Length; i++)
        {
            var arg = args[i];
            array[i] = new MeshOrbAttack(arg.orbsInRow, arg.numberOfRows, distance, 
                arg.speed, arg.startPos, arg.offsetFunction, arg.startTime, 10000);
        }
        return array;
    }
}