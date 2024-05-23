namespace BulletHell
{
    internal class CircleOrbAttackFactory(params (int orbsCount, VectorV startPos, float orbsMovementSpeed, 
        Func<float, VectorV> offsetFunction, Func<float, float> inflateRadiusFunction, float startTime)[] args) : Factory<CircleOrbAttack>
    {
        (int orbsCount, VectorV startPos, float orbsMovementSpeed,
        Func<float, VectorV> offsetFunction, Func<float, float> inflateRadiusFunction, float startTime)[] args = args;

        public override CircleOrbAttack[] CreateAttacks()
        {
            var array = new CircleOrbAttack[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                var arg = args[i];
                array[i] = new CircleOrbAttack(arg.orbsCount, arg.startPos, arg.orbsMovementSpeed,
                    arg.offsetFunction, arg.inflateRadiusFunction, arg.startTime, 10000);
            }
            return array;
        }
    }
}
