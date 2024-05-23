namespace BulletHell;

internal class View
{
    private readonly Player player = new(Assets1.ship);
    public static int InvalidateTimerInterval { get; } = 1;
    private Attack[] Attacks => Model.GetInstance().ActiveAttacks.Select(x => x.Item2).ToArray();
    public int ScreenWidth { get; private set; }
    public int ScreenHeight { get; private set; }

    #region Singlton
    private static View instance;

    private View() { }

    public static View GetInstance()
    {
        instance ??= new View();

        return instance;
    }
    #endregion

    public void Init(int width, int height)
    {
        ScreenWidth = width;
        ScreenHeight = height;
    }

    public void OnPaint(object? sender, PaintEventArgs e)
    {
        var g = e.Graphics;
        g.DrawImage(player.Sprite, new RectangleF(new Point((int)player.Position.X, (int)player.Position.Y), player.Size));
        foreach (var Attack in Attacks)
        {
            if (Attack is OrbAttack orbAttack)
            {
                foreach (var orb in orbAttack.Orbs)
                {
                    g.DrawImage(orb.Sprite, new RectangleF(new Point((int)orb.Position.X, (int)orb.Position.Y), orb.Size));
                }
            }
            else if (Attack is RectangleAttack rectangleAttack)
            {
                g.FillRectangle(Brushes.Green, rectangleAttack.Rectangle);
            }
        }
    }        
}
