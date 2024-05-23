namespace BulletHell
{
    partial class BulletHell
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            InvalidateTimer = new System.Windows.Forms.Timer(components);
            PlayButton = new Button();
            ExitButton = new Button();
            ShowControlsTimer = new System.Windows.Forms.Timer(components);
            ControlsLegend = new PictureBox();
            DiedLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)ControlsLegend).BeginInit();
            SuspendLayout();
            // 
            // InvalidateTimer
            // 
            InvalidateTimer.Tick += Redraw;
            // 
            // PlayButton
            // 
            PlayButton.BackgroundImage = Assets1.New_Game__col_Button;
            PlayButton.BackgroundImageLayout = ImageLayout.Stretch;
            PlayButton.Location = new Point(61, 277);
            PlayButton.Name = "PlayButton";
            PlayButton.Size = new Size(261, 86);
            PlayButton.TabIndex = 1;
            PlayButton.UseVisualStyleBackColor = true;
            PlayButton.Click += ShowControls;
            // 
            // ExitButton
            // 
            ExitButton.BackgroundImage = Assets1.Exit__col_Button;
            ExitButton.BackgroundImageLayout = ImageLayout.Stretch;
            ExitButton.Location = new Point(61, 420);
            ExitButton.Name = "ExitButton";
            ExitButton.Size = new Size(261, 86);
            ExitButton.TabIndex = 2;
            ExitButton.UseVisualStyleBackColor = true;
            ExitButton.Click += ExitButtonOnClick;
            // 
            // ShowControlsTimer
            // 
            ShowControlsTimer.Interval = 1000;
            ShowControlsTimer.Tick += WaitControls;
            // 
            // ControlsLegend
            // 
            ControlsLegend.BackgroundImageLayout = ImageLayout.None;
            ControlsLegend.Image = Assets1.WASD;
            ControlsLegend.Location = new Point(567, 67);
            ControlsLegend.Name = "ControlsLegend";
            ControlsLegend.Size = new Size(737, 737);
            ControlsLegend.SizeMode = PictureBoxSizeMode.StretchImage;
            ControlsLegend.TabIndex = 3;
            ControlsLegend.TabStop = false;
            ControlsLegend.Visible = false;
            // 
            // DiedLabel
            // 
            DiedLabel.AutoSize = true;
            DiedLabel.BackColor = Color.Transparent;
            DiedLabel.Font = new Font("Showcard Gothic", 72F, FontStyle.Regular, GraphicsUnit.Point, 0);
            DiedLabel.Location = new Point(839, 277);
            DiedLabel.Name = "DiedLabel";
            DiedLabel.Size = new Size(274, 119);
            DiedLabel.TabIndex = 4;
            DiedLabel.Text = "Died";
            DiedLabel.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1920, 1061);
            Controls.Add(DiedLabel);
            Controls.Add(ControlsLegend);
            Controls.Add(ExitButton);
            Controls.Add(PlayButton);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            KeyPreview = true;
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            KeyDown += Form1OnKeyDown;
            ((System.ComponentModel.ISupportInitialize)ControlsLegend).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer InvalidateTimer;
        private Button PlayButton;
        private Button ExitButton;
        private System.Windows.Forms.Timer ShowControlsTimer;
        private PictureBox ControlsLegend;
        private Label DiedLabel;
    }
}
