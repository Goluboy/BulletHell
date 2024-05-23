namespace BulletHell;

internal class RectangleAttack : Attack
{
    public VectorV Position { get; private set; }
    public RectangleF Rectangle { get; private set; }
    public RectangleAttack(float width, float height, float movementSpeed, VectorV startPos, Func<float, VectorV> offsetFunction,
    float startTime, float duration)
        : base(movementSpeed, startPos, offsetFunction, startTime, duration)
    {
        Position = startPos;
        Rectangle = new RectangleF(Position.X, Position.Y, width, height);
    }

    public override bool IsCollide(VectorV playerPosition)
    {
        return RectangleF.Intersect(Rectangle, new RectangleF(playerPosition.X, playerPosition.Y, 1, 1)) != RectangleF.Empty;
    }

    public override void Move()
    {
        Rectangle = new RectangleF(Position.X, Position.Y, Rectangle.Width, Rectangle.Height);
        movementSum += movementSpeed;
        Position += offsetFunction(movementSum);
    }
}
