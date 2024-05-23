namespace BulletHell;

internal abstract class Entity
{
    public VectorV Position
    {
        get { return position; }
        set
        {
            position = value;
            localPosition = value - OriginalPosition;
        }
    }

    public VectorV LocalPosition
    {
        get { return localPosition; }
        set
        {
            localPosition = value;
            position = OriginalPosition + value;
        }
    }

    public Entity(Image sprite)
    {
        Size = sprite.Size;
        Sprite = sprite;
        OriginalPosition = new VectorV(50, 50);
    }

    public Entity(VectorV position, Image sprite) : this(sprite)
    {
        Position = position;
        OriginalPosition = position;
        LocalPosition = new VectorV(0, 0);
    }

    public Size Size;
    public Image Sprite;
    public readonly VectorV OriginalPosition;

    private VectorV position;
    private VectorV localPosition;
}
