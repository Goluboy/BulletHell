using System.ComponentModel;
using System.Diagnostics;
using Timer = System.Windows.Forms.Timer;

namespace BulletHell;

internal class Model
{
    public event Action GameOver;
    public List<Attack> InactiveAttacks { get; private set; }
    public List<(float, Attack)> ActiveAttacks { get; private set; } = new(64);
    public VectorV PlayerPosition { get; private set; } = new VectorV(960,540);
    public Timer GameLogicTimer { get; private set; }
    public Stopwatch Stopwatch { get; private set; }
    public bool IsCollide => ActiveAttacks.Any(x => x.Item2.IsCollide(PlayerPosition));

    private int width => View.GetInstance().ScreenWidth;
    private int height => View.GetInstance().ScreenHeight;
    private VectorV playerDir => Controller.GetInstance().PlayerDir;

    private readonly float playerSpeed = 8f;

    private MeshOrbAttackFactory meshOrbAttackFactory;
    private FollowingOrbAttackFactory followingOrbAttackFactory;
    private RectangleAttackFactory rectangleAttackFactory;
    private int GameLogicTimerInterval = 16;

    #region Singlton
    private static Model instance;

    private Model() { }

    public static Model GetInstance()
    {
        instance ??= new Model();

        return instance;
    }
    #endregion

    public void Init()
    {
        InactiveAttacks = [new CircleOrbAttack(10, VectorV.Zero, 10,50, (float f) => new VectorV(f,f),(float f) => 100, 8600, 1000)];
        GameOver += OnGameOver;
        Stopwatch = new Stopwatch();
        GameLogicTimer = new Timer() { Interval = GameLogicTimerInterval };
        GameLogicTimer.Tick += new EventHandler(Update);
        Stopwatch.Start();
        GameLogicTimer.Start();

        meshOrbAttackFactory = new MeshOrbAttackFactory(150, 
            (6, new VectorV(0,700), (float f) => new VectorV(0,-f), 20,3,10),
            (100, new VectorV(1000, 500), (float f) => new VectorV(-f, 0), 2, 2, 6000),
            (100, new VectorV(0, 100), (float f) => new VectorV(f, 0), 2, 2, 7300),
            (25, new VectorV(0, 700), (float f) => new VectorV(0, -f), 20,3, 7300),
            (100, new VectorV(1000, 500), (float f) => new VectorV(-f, 0), 2, 2, 8600),
            (100, new VectorV(0, 100), (float f) => new VectorV(f, 0), 2, 2, 8600)
            );

        followingOrbAttackFactory = new FollowingOrbAttackFactory(
            (4,50, new VectorV(width, height), 6000),
            (4, 50, new VectorV(0,0), 7300),
            (4, 50, new VectorV(width, height), 8600),
            (1000,50, new VectorV(0, 0), 8600));

        rectangleAttackFactory = new RectangleAttackFactory(
            (400, 400, 15, new VectorV(0, -400), (float f) => new VectorV(0, f), 6000, 1050),
            (400, 400, 15, new VectorV(width - 400, -400), (float f) => new VectorV(0, f), 6000, 1050),
            (400, 400, 15, new VectorV(width - 400, height - 400), (float f) => new VectorV(0, -f), 7300, 1050),
            (400, 400, 15, new VectorV(0, height + 100), (float f) => new VectorV(0, -f), 7300, 1050),
            (400, 400, 15, new VectorV(0, -400), (float f) => new VectorV(0, f), 8600, 1050),
            (400, 400, 15, new VectorV(width - 400, -400), (float f) => new VectorV(0, f), 8600, 1050),
            (400, 400, 15, new VectorV(width - 400, height + 400), (float f) => new VectorV(0, -f), 8600, 1050),
            (400, 400, 15, new VectorV(0, height + 100), (float f) => new VectorV(0, -f), 8600, 1050));


        InactiveAttacks.AddRange(followingOrbAttackFactory.CreateAttacks());
        InactiveAttacks.AddRange(meshOrbAttackFactory.CreateAttacks());
        InactiveAttacks.AddRange(rectangleAttackFactory.CreateAttacks());
        InactiveAttacks.Sort();
    }

    private void Update(object? sender, EventArgs args)
    {
        CheckAttackTimers();
        MoveOrbs();
        MovePlayer();
    }

    public void MoveOrbs()
    {
        foreach(var orbAttack in ActiveAttacks)
        {
            orbAttack.Item2.Move();
        }
    }

    public void CheckAttackTimers()
    {
        if (InactiveAttacks.Count != 0 
            && InactiveAttacks[0].startTime <= Stopwatch.ElapsedMilliseconds)
        {
            ActiveAttacks.Add((InactiveAttacks[0].startTime + InactiveAttacks[0].Duration, InactiveAttacks[0]));
            InactiveAttacks.RemoveAt(0);
            ActiveAttacks.Sort();
        }

        if (ActiveAttacks.Count != 0
            && ActiveAttacks[0].Item2.startTime + ActiveAttacks[0].Item2.Duration <= Stopwatch.ElapsedMilliseconds)
        {
            ActiveAttacks.RemoveAt(0);
        }
    }

    public void MovePlayer()
    {
        VectorV nextPos;

        if (playerDir == VectorV.Down || playerDir == VectorV.Right
            || playerDir == VectorV.Up || playerDir == VectorV.Left)
        {
            nextPos = PlayerPosition + playerDir * playerSpeed;
        }
        else
        {
            nextPos = PlayerPosition + playerDir * playerSpeed / MathF.Sqrt(2);
        }

        PlayerPosition = nextPos;
        
        if (IsCollide)
        {
            GameOver.Invoke();
        }
    }

    private void OnGameOver()
    {
        Stopwatch.Stop();
        GameLogicTimer.Stop();
    }
}
