using Minesweeper.Properties;

namespace WinFormsApp1
{
    public partial class Minesweeper : Form
    {
        // Form Information
        private static Button[,] btn = new Button[16, 30];
        private static Label[,] lbl = new Label[16, 30];
        private static int startX = 50;
        private static int startY = 200;
        private static int buttonSize = 50;

        // Board Information
        public static int height = 9;
        public static int width = 9;
        public static int bombsTotal = 10;
        public static int[,] board = new int[width, height];
        public static int flags;

        // Time
        int seconds = 0;

        // GamePlay
        public bool gameOver;
        public bool firstPlay;
        static Point coord = new Point();
        public bool m_left = false;
        public bool m_right = false;

        // Points that are around
        public static int[] delta_x = { 1, 0, -1, 0, 1, -1, -1, 1 };
        public static int[] delta_y = { 0, 1, 0, -1, 1, -1, 1, -1 };
        public Minesweeper()
        {
            InitializeComponent();
            CreateFormItems();
            NewGame();
        }
        void CreateFormItems()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    btn[i, j] = new Button();
                    btn[i, j].SetBounds(startX + j * buttonSize, startY + i * buttonSize, buttonSize, buttonSize);
                    btn[i, j].TabIndex = 0;
                    btn[i, j].UseVisualStyleBackColor = false;
                    btn[i, j].BackColor = SystemColors.ActiveBorder;
                    btn[i, j].MouseDown += new MouseEventHandler(Button_Click);
                    Controls.Add(btn[i, j]);


                    lbl[i, j] = new Label();
                    lbl[i, j].SetBounds(startX + j * buttonSize, startY + i * buttonSize, buttonSize, buttonSize);
                    lbl[i, j].TabIndex = 0;
                    lbl[i, j].BorderStyle = BorderStyle.FixedSingle;
                    lbl[i, j].TextAlign = ContentAlignment.MiddleCenter;
                    lbl[i, j].Font = new Font("Consolas", 13.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    lbl[i, j].MouseDown += new MouseEventHandler(Label_Click);
                    lbl[i, j].MouseUp += new MouseEventHandler(Label_MouseUpClick);
                    Controls.Add(lbl[i, j]);
                }
            }
        }

        void NewGame()
        {
            gameOver = false;
            firstPlay = true;

            SetupBombs(width, height, bombsTotal);
            SetupBoardNumbers();
        }
        void SetupBombs(int boardWidth, int boardHeight, int maxBombs)
        {
            Random rnd = new Random();
            HashSet<string> bombs = new HashSet<string>();

            while (bombs.Count < bombsTotal)
            {
                int x = rnd.Next(0, boardWidth);  // Random Numbers for x-coord
                int y = rnd.Next(0, boardHeight); // Random Numbers for y-coord

                if (!bombs.Contains($"{x}{y}"))
                { // Need to find 10 differnt bomb coords
                    bombs.Add($"{x}{y}");
                    board[x, y] = -1;
                }
            }

            flags = bombsTotal;
            txtFlags.Text = $"{flags}";
        }
        void SetupBoardNumbers()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (board[i, j] != -1)
                    { // Non-bomb locations
                        board[i, j] = MinesAround(i, j);

                        lbl[i, j].Text = $"{MinesAround(i, j)}";
                        FormatLabelColors(lbl[i, j]);
                    }
                    else
                    {
                        lbl[i, j].Image = Resources.Bomb1; // Bomb location
                        
                        lbl[i, j].Text = $"{-1}";
                    }
                }
            }
        }
        void FormatLabelColors(Label label)
        {
            switch (label.Text)
            {
                case "0": label.Text = ""; break;
                case "1": label.ForeColor = Color.Blue; break;
                case "2": label.ForeColor = Color.Green; break;
                case "3": label.ForeColor = Color.Red; break;
                case "4": label.ForeColor = Color.Navy; break;
                case "5": label.ForeColor = Color.Firebrick; break;
                case "6": label.ForeColor = Color.Turquoise; break;
                case "7": label.ForeColor = Color.Purple; break;
                case "8": label.ForeColor = Color.DarkGray; break;
                default: break;
            }
        }
        bool IsPointOnBoard(int x, int y)
        {
            if (x < 0 || x > width - 1 || y < 0 || y > height - 1) { return false; }
            else return true;
        }
        int MinesAround(int x, int y)
        {
            int count = 0;

            for (int i = 0; i < 8; i++)
            { // Maximum of 8 cells around given coord
                int xCoord = x + delta_x[i];
                int yCoord = y + delta_y[i];

                if (IsPointOnBoard(xCoord, yCoord) && board[xCoord, yCoord] == -1) { count++; }
            }
            return count;
        }
        private void Button_Click(object? sender, MouseEventArgs e)
        {
            if (firstPlay)
            {
                timer1.Start();
                firstPlay = false;
            }

            if (gameOver) return;

            coord = ((Button)sender).Location;

            int j = (coord.X - startX) / buttonSize; // Columns are left/right
            int i = (coord.Y - startY) / buttonSize; // Rows are up/down

            if (e.Button == MouseButtons.Left && btn[i, j].Image == null)
            {
                btn[i, j].Visible = false;

                if (lbl[i, j].Text == "") EmptySpace(i, j); // See if more empty spaces are around

                if (lbl[i, j].Image != null)
                { // Selected a bomb location...Game Over
                    lbl[i, j].Image = Resources.Bomb2;
                    GameOver();
                }
                // Check if button pressed was final play
                LeftClickWin();
            }
            else if (e.Button == MouseButtons.Right)
            { // Toggle Flag Image
                if (btn[i, j].Image == null)
                {
                    btn[i, j].Image = Resources.Flag;
                    flags--;
                }
                else
                {
                    btn[i, j].Image = null;
                    flags++;
                }

                txtFlags.Text = $"{flags}";

                // Check is last marked flag was final bomb location
                FlagClickWin();
            }
        }
        private void Label_Click(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) m_left = true;
            if (e.Button == MouseButtons.Right) m_right = true;
        }
        private void Label_MouseUpClick(object? sender, MouseEventArgs e)
        {
            coord = ((Label)sender).Location;

            int j = (coord.X - startX) / buttonSize;
            int i = (coord.Y - startY) / buttonSize;

            if (m_left && m_right) // Both buttons clicked
            {
                if (FlagsAround(i, j) == Int32.Parse(lbl[i, j].Text))
                    ClearAround(i, j);
            }

            m_left = false;
            m_right = false;
        }
        void EmptySpace(int x, int y)
        {
            for (int i = 0; i < 8; i++)
            {
                int xCoord = x + delta_x[i];
                int yCoord = y + delta_y[i];

                if (IsPointOnBoard(xCoord, yCoord) && board[xCoord, yCoord] != -1 && btn[xCoord, yCoord].Visible != false &&
                    btn[xCoord, yCoord].Image == null)
                {
                    btn[xCoord, yCoord].Visible = false;

                    if (board[xCoord, yCoord] == 0) { EmptySpace(xCoord, yCoord); } // Recursive call to show all blank spaces around                    
                }
            }
        }
        void GameOver()
        {
            gameOver = true;
            timer1.Stop();
            btnResetGame.Image = Resources.Red_Smiley;
            DisplayBombLocations();
            
        }
        void DisplayBombLocations()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    btn[i, j].Enabled = false; // Disable all buttons

                    if (lbl[i, j].Text == "-1" && btn[i, j].Image != null)
                    { // Bomb Location w/ Flag
                        btn[i, j].Enabled = true; // Enable flagged buttons to prevent dimming
                    }
                    else if (lbl[i, j].Text == "-1")
                    { // Bomb Location 
                        lbl[i, j].Text = "";
                        btn[i, j].Visible = false;
                    }
                    else if (lbl[i, j].Text != "-1" && btn[i, j].Image != null)
                    { // Incorrect Flag Location
                        btn[i, j].Image = null;
                        btn[i, j].Visible = false;
                        lbl[i, j].Text = "";
                        lbl[i, j].Image = Resources.Bomb3;
                    }
                }
            }
        }
        private void ResetGame_Click(object sender, EventArgs e)
        {
            btnResetGame.Image = Resources.Yellow_Smiley;
            timer1.Stop();
            ResetGame();            
            firstPlay = true;            
            seconds = 0;
            txtTime.Text = seconds.ToString().PadLeft(3, '0');
        }
        void ResetGame()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    board[i, j] = 0;

                    lbl[i, j].Text = "";
                    lbl[i, j].Image = null;

                    btn[i, j].Image = null;
                    btn[i, j].Visible = true;
                    btn[i, j].Enabled = true;
                }
            }
            NewGame();
        }
        void LeftClickWin()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (btn[i, j].Visible == true && lbl[i, j].Text != "-1") return;
                }
            }
            gameOver = true;
            timer1.Stop();
            PlaceFlags();
            btnResetGame.Image = Resources.Green_Smiley;
            //MessageBox.Show("YOU WIN!", "Congratulations", MessageBoxButtons.OK);
        }
        void FlagClickWin()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (lbl[i, j].Text == "-1" && btn[i, j].Image == null) return;
                }
            }
            gameOver = true;
            timer1.Stop();
            RevealBoard();
            btnResetGame.Image = Resources.Green_Smiley;
            //MessageBox.Show("YOU WIN!", "Congratulations", MessageBoxButtons.OK);
        }
        void RevealBoard()
        {
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    if (btn[i, j].Visible == true && btn[i, j].Image == null)
                        btn[i, j].Visible = false;
        }
        void PlaceFlags()
        {
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    if (btn[i, j].Visible == true && btn[i, j].Image == null)
                        btn[i, j].Image = Resources.Flag;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            seconds++;

            if (seconds <= 999)
                txtTime.Text = seconds.ToString().PadLeft(3, '0');
        }
        private void ClearAround(int x, int y)
        {
            for (int i = 0; i < 8; i++)
            {
                int xCoord = x + delta_x[i];
                int yCoord = y + delta_y[i];

                if (IsPointOnBoard(xCoord, yCoord))
                {
                    if (btn[xCoord, yCoord].Image == null)
                    { // Show labels that are unflagged
                        btn[xCoord, yCoord].Visible = false;

                        if (lbl[xCoord, yCoord].Text == $"{-1}")
                        {
                            GameOver();
                            return;
                        }

                        if (lbl[xCoord, yCoord].Text == "") EmptySpace(xCoord, yCoord);
                    }
                }
            }
        }
        private int FlagsAround(int x, int y)
        {
            int count = 0;
            for (int i = 0; i < 8; i++)
            {
                int xCoord = x + delta_x[i];
                int yCoord = y + delta_y[i];

                if (IsPointOnBoard(xCoord, yCoord))
                {
                    if (btn[xCoord, yCoord].Visible == true && btn[xCoord, yCoord].Image != null) count++;
                }
            }
            return count;
        }
        private void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
