namespace BulletHell;

internal class Controller
{
    private VectorV playerDir;
    public VectorV PlayerDir { get => playerDir;  }

    #region Singlton
    private static Controller instance;

    private Controller() { }

    public static Controller GetInstance()
    {
        instance ??= new Controller();

        return instance;
    }
    #endregion

    public void Init()
    {
        playerDir = new VectorV(0, 0);
    }

    public void OnKeyDown(object? sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.W:
                playerDir.Y = -1;
                break;
            case Keys.S:
                playerDir.Y = 1;
                break;
            case Keys.A:
                playerDir.X = -1;
                break;
            case Keys.D:
                playerDir.X = 1;
                break;
        }
    }

    public void OnKeyUp(object? sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.W:
                playerDir.Y = playerDir.Y == 1 ? 1 : 0;
                break;
            case Keys.S:
                playerDir.Y = playerDir.Y == -1 ? -1 : 0;
                break;
            case Keys.A:
                playerDir.X = playerDir.X == 1 ? 1 : 0;
                break;
            case Keys.D:
                playerDir.X = playerDir.X == -1 ? -1 : 0;
                break;
        }
    }
}
