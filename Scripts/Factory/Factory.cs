namespace BulletHell;

abstract class Factory<T> where T : Attack
{
    public abstract T[] CreateAttacks();
}

