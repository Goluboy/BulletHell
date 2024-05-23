using System.Media;

namespace BulletHell;

public partial class BulletHell : Form
{
    private readonly Model model = Model.GetInstance();
    private readonly View view = View.GetInstance();
    private readonly Controller controller = Controller.GetInstance();
    private float controlsTime;
    Label a = new Label();
    private SoundPlayer soundPlayer = new SoundPlayer(Assets1._467339_At_the_Speed_of_Light_FINA);


    public BulletHell()
    {
        InitializeComponent();
        model.GameOver += OnGameOver;
        foreach (Button button in Controls.OfType<Button>())
        {
            button.Click += HideAllControls;
        }

        Bounds = Screen.PrimaryScreen.Bounds;
    }

    private void ShowControls(object sender, EventArgs e)
    {
        ControlsLegend.Visible = true;
        ShowControlsTimer.Start();
    }

    private void WaitControls(object? sender, EventArgs e)
    {
        controlsTime += ShowControlsTimer.Interval;
        if (controlsTime > ShowControlsTimer.Interval - 1e-5)
        {
            ShowControlsTimer.Tick -= WaitControls;
            StartGame();
            ControlsLegend.Visible = false;
        }

    }

    private void StartGame()
    {
        soundPlayer.Play();
        KeyDown += controller.OnKeyDown;
        KeyUp += controller.OnKeyUp;
        Paint += view.OnPaint;

        controller.Init();
        model.Init();
        view.Init(Width, Height); 

        InvalidateTimer.Interval = View.InvalidateTimerInterval;
        InvalidateTimer.Start();
    }

    private void HideAllControls(object? sender, EventArgs e)
    {
        foreach (Control control in Controls)
        {
            if (control is PictureBox pictureBox && pictureBox == ControlsLegend)
                continue;
            control.Hide();
        }
    }

    public void Redraw(object? sender, EventArgs e)
    {
        Invalidate();
    }

    private void PlayButtonOnClick(object sender, EventArgs e)
    {
        ShowControls(this, null);
    }

    private void ExitButtonOnClick(object sender, EventArgs e)
    {
        Application.Exit();
    }

    private void Form1OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
            Application.Exit();
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {      
        if (MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo) == DialogResult.No)
        {
            e.Cancel = true;
        }

        if (model.GameLogicTimer != null)
            model.GameLogicTimer.Start();
    }

    public void OnGameOver()
    {
        soundPlayer.Stop();
        DiedLabel.Show();
        var continueButton = new Button()
        {
            BackgroundImage = Assets1.Continue__col_Button,
            BackgroundImageLayout = ImageLayout.Stretch,
            Location = new Point(Width / 2 - 130, 3 * Height / 4),
            Name = "MainMenuButton",
            Size = new Size(261, 86)
        };

        continueButton.Click += OnReset;

        Controls.Add(continueButton);
    }

    private void OnReset(object? sender, EventArgs e)
    {
        FormClosing -= Form1_FormClosing;
        Application.Restart();
    }
}
