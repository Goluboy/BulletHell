namespace BulletHell
{
    class RectangleAttackFactory(params (float width, float height, float speed, VectorV startPos, 
        Func<float, VectorV> offsetFunction, float startTime, float duration)[] args) : Factory<RectangleAttack>
    {
        private (float width, float height, float speed, VectorV startPos,
        Func<float, VectorV> offsetFunction, float startTime, float duration)[] args = args;
        public override RectangleAttack[] CreateAttacks()
        {
            var array = new RectangleAttack[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                var arg = args[i];
                array[i] = new RectangleAttack(arg.width, arg.height, arg.speed, arg.startPos, arg.offsetFunction, arg.startTime, arg.duration);
            }
            return array;
        }
    }
}
