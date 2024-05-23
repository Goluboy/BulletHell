namespace BulletHell;

abstract class Attack(float movementSpeed, VectorV startPos, Func<float, VectorV> offsetFunction, 
    float startTime, float duration) : IComparable<Attack>
{
    public readonly float startTime = startTime;
    public readonly float Duration = duration;
    protected float movementSpeed = movementSpeed;
    protected float movementSum;
    protected VectorV offset;
    protected VectorV startPos = startPos;
    protected Func<float, VectorV> offsetFunction = offsetFunction;

    public virtual void Move()
    { }

    public virtual bool IsCollide(VectorV playerPosition) 
    { return false; }

    public int CompareTo(Attack? other)
    {
        return startTime.CompareTo(other.startTime);
    }
}

