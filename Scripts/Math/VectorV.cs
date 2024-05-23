namespace BulletHell;

struct VectorV
{
    public VectorV(float X, float Y)
    {
        this.X = X; 
        this.Y = Y;
    }
    public float X { get; set; }
    public float Y { get; set; }

    public float Magnitude => MathF.Sqrt(X * X + Y * Y);
    public static VectorV Zero => new VectorV(0);
    public static VectorV Up => new VectorV(0, -1);
    public static VectorV Down => new VectorV(0, 1);
    public static VectorV Right => new VectorV(1, 0);
    public static VectorV Left => new VectorV(-1, 0);

    public VectorV(float value) : this(value, value)
    { 
    }

    public static VectorV operator +(VectorV left, VectorV right)
    {
        return new VectorV(left.X + right.X, left.Y + right.Y);
    }

    public static VectorV operator -(VectorV left, VectorV right)
    {
        return new VectorV(left.X - right.X, left.Y - right.Y);
    }

    public static VectorV operator -(VectorV left)
    {
        return new VectorV( -left.X, -left.Y);
    }

    public static VectorV operator *(float factor, VectorV right)
    {
        return new VectorV(factor * right.X, factor * right.Y);
    }

    public static VectorV operator *(VectorV left, float factor)
    {
        return new VectorV(left.X * factor, left.Y * factor);
    }

    public static VectorV operator /(VectorV left, float inversedFactor)
    {
        return new VectorV(left.X / inversedFactor, left.Y / inversedFactor);
    }

    public static bool operator ==(VectorV left, VectorV right) 
    {
        return left.Equals(right);
    }

    public static bool operator !=(VectorV left, VectorV right)
    {
        return !left.Equals(right);
    }

    public float Distance(VectorV other)
    {
        var x = (X - other.X);
        var y = (Y - other.Y);
        return MathF.Sqrt(x * x + y * y);
    }

    public VectorV Normalize()
    {
        return new VectorV(X, Y) / Magnitude;
    }

    public override bool Equals(object obj)
    {
        return X == ((VectorV)obj).X && Y == ((VectorV)obj).Y;
    }

    public override int GetHashCode()
    {
        return (int)(X * 16777619 + Y);
    }
}
