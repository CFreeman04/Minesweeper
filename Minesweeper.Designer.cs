using Minesweeper.Properties;

namespace WinFormsApp1
{
    partial class Minesweeper
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
            btnResetGame = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            txtTime = new TextBox();
            txtFlags = new TextBox();
            menuStrip1 = new MenuStrip();
            menuToolStripMenuItem = new ToolStripMenuItem();
            mnuBeginner = new ToolStripMenuItem();
            mnuIntermediate = new ToolStripMenuItem();
            mnuExpert = new ToolStripMenuItem();
            mnuSeparator = new ToolStripSeparator();
            mnuExit = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // btnResetGame
            // 
            btnResetGame.BackgroundImageLayout = ImageLayout.Stretch;
            btnResetGame.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            btnResetGame.Image = Resources.Yellow_Smiley;
            btnResetGame.Location = new Point(224, 53);
            btnResetGame.Name = "btnResetGame";
            btnResetGame.Size = new Size(75, 75);
            btnResetGame.TabIndex = 0;
            btnResetGame.UseVisualStyleBackColor = true;
            btnResetGame.Click += ResetGame_Click;
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // txtTime
            // 
            txtTime.BackColor = SystemColors.Control;
            txtTime.Font = new Font("Stencil", 19.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtTime.ForeColor = Color.Red;
            txtTime.Location = new Point(378, 53);
            txtTime.Name = "txtTime";
            txtTime.ReadOnly = true;
            txtTime.Size = new Size(117, 70);
            txtTime.TabIndex = 0;
            txtTime.TabStop = false;
            txtTime.Text = "000";
            txtTime.TextAlign = HorizontalAlignment.Center;
            // 
            // txtFlags
            // 
            txtFlags.BackColor = SystemColors.Control;
            txtFlags.Font = new Font("Stencil", 19.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtFlags.ForeColor = Color.Red;
            txtFlags.Location = new Point(43, 53);
            txtFlags.Name = "txtFlags";
            txtFlags.ReadOnly = true;
            txtFlags.Size = new Size(117, 70);
            txtFlags.TabIndex = 0;
            txtFlags.TabStop = false;
            txtFlags.TextAlign = HorizontalAlignment.Right;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(32, 32);
            menuStrip1.Items.AddRange(new ToolStripItem[] { menuToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(541, 42);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            menuToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { mnuBeginner, mnuIntermediate, mnuExpert, mnuSeparator, mnuExit });
            menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            menuToolStripMenuItem.Size = new Size(97, 38);
            menuToolStripMenuItem.Text = "Menu";
            // 
            // mnuBeginner
            // 
            mnuBeginner.Checked = true;
            mnuBeginner.CheckState = CheckState.Checked;
            mnuBeginner.Name = "mnuBeginner";
            mnuBeginner.Size = new Size(410, 44);
            mnuBeginner.Text = "Beginner (10 bombs)";
            // 
            // mnuIntermediate
            // 
            mnuIntermediate.Enabled = false;
            mnuIntermediate.Name = "mnuIntermediate";
            mnuIntermediate.Size = new Size(410, 44);
            mnuIntermediate.Text = "Intermediate (40 bombs)";
            // 
            // mnuExpert
            // 
            mnuExpert.Enabled = false;
            mnuExpert.Name = "mnuExpert";
            mnuExpert.Size = new Size(410, 44);
            mnuExpert.Text = "Expert (99 bombs)";
            // 
            // mnuSeparator
            // 
            mnuSeparator.Name = "mnuSeparator";
            mnuSeparator.Size = new Size(407, 6);
            // 
            // mnuExit
            // 
            mnuExit.Name = "mnuExit";
            mnuExit.Size = new Size(410, 44);
            mnuExit.Text = "Exit";
            mnuExit.Click += mnuExit_Click;
            // 
            // Minesweeper
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(541, 705);
            Controls.Add(txtFlags);
            Controls.Add(txtTime);
            Controls.Add(btnResetGame);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Minesweeper";
            Text = "My Minesweeper";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnResetGame;
        private System.Windows.Forms.Timer timer1;
        private TextBox txtTime;
        private TextBox txtFlags;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem menuToolStripMenuItem;
        private ToolStripMenuItem mnuBeginner;
        private ToolStripMenuItem mnuIntermediate;
        private ToolStripMenuItem mnuExpert;
        private ToolStripSeparator mnuSeparator;
        private ToolStripMenuItem mnuExit;
    }
}
