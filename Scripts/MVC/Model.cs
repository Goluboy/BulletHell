using System.ComponentModel;
using System.Diagnostics;
using Timer = System.Windows.Forms.Timer;

namespace BulletHell;

internal class Model
{
    public event Action GameOver;
    public List<Attack> InactiveAttacks { get; private set; } = new(64);
    public List<(float, Attack)> ActiveAttacks { get; private set; } = new(64);
    public VectorV PlayerPosition { get; private set; } = new VectorV(960,540);
    public Timer GameLogicTimer { get; private set; }
    public Stopwatch Stopwatch { get; private set; }

    private VectorV playerDir => Controller.GetInstance().PlayerDir;
    public bool IsCollide => ActiveAttacks.Any(x => x.Item2.IsCollide(PlayerPosition));

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
            (50, new VectorV(1920, 1080), 6000),
            (50, new VectorV(1920, 1080), 6010),
            (50, new VectorV(1920, 1080), 6020),
            (50, new VectorV(1920, 1080), 6030),
            (50, new VectorV(0,0), 7300),
            (50, new VectorV(0, 0), 7310),
            (50, new VectorV(0, 0), 7320),
            (50, new VectorV(0, 0), 7330),
            (50, new VectorV(1920, 1080), 8600),
            (50, new VectorV(1920, 1080), 8610),
            (50, new VectorV(1920, 1080), 8620),
            (50, new VectorV(1920, 1080), 8630),
            (50, new VectorV(0, 0), 8600),
            (50, new VectorV(0, 0), 8610),
            (50, new VectorV(0, 0), 8620),
            (50, new VectorV(0, 0), 8630));

        rectangleAttackFactory = new RectangleAttackFactory(
            (400, 400, 15, new VectorV(0, -400), (float f) => new VectorV(0, f), 6000, 1050),
            (400, 400, 15, new VectorV(1520, -400), (float f) => new VectorV(0, f), 6000, 1050),
            (400, 400, 15, new VectorV(1520, 1580), (float f) => new VectorV(0, -f), 7300, 1050),
            (400, 400, 15, new VectorV(0, 1180), (float f) => new VectorV(0, -f), 7300, 1050),
            (400, 400, 15, new VectorV(0, -400), (float f) => new VectorV(0, f), 8600, 1050),
            (400, 400, 15, new VectorV(1520, -400), (float f) => new VectorV(0, f), 8600, 1050),
            (400, 400, 15, new VectorV(1520, 1580), (float f) => new VectorV(0, -f), 8600, 1050),
            (400, 400, 15, new VectorV(0, 1180), (float f) => new VectorV(0, -f), 8600, 1050));


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
