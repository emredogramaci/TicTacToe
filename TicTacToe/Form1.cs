namespace TicTacToe
{
    public partial class Form1 : Form
    {
        bool playerTurn = true; //Player X - true
        int turnCount = 0;
        int winStreak = 0;
        public Form1()
        {
            InitializeComponent();
            updateWinStreak("");
        }

        private void checkWinner()
        {
            bool theWinner = false;

            //Horizontal condition
            if ((A1.Text == A2.Text) && (A2.Text == A3.Text) && (!A2.Enabled)) { theWinner = true; }
            else if ((B1.Text == B2.Text) && (B2.Text == B3.Text) && (!B3.Enabled)) { theWinner = true; }
            else if ((C1.Text == C2.Text) && (C2.Text == C3.Text) && (!C2.Enabled)) { theWinner = true; }

            //Vertical condition
            else if ((A1.Text == B1.Text) && (B1.Text == C1.Text) && (!B1.Enabled)) { theWinner = true; }
            else if ((A2.Text == B2.Text) && (B2.Text == C2.Text) && (!B2.Enabled)) { theWinner = true; }
            else if ((A3.Text == B3.Text) && (B3.Text == C3.Text) && (!B3.Enabled)) { theWinner = true; }

            //Cross condition
            else if ((A1.Text == B2.Text) && (B2.Text == C3.Text) && (!B2.Enabled)) { theWinner = true; }
            else if ((A3.Text == B2.Text) && (B2.Text == C1.Text) && (!B2.Enabled)) { theWinner = true; }

            if (theWinner)
            {
                string winner = "";
                if (playerTurn)
                    winner = "O";
                else
                    winner = "X";
                updateWinStreak(winner);

                MessageBox.Show(winner + " Wins!","The Winner!", MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                autoNewGame();
            }
            else
            {
                if(turnCount == 0)
                {
                    MessageBox.Show("There is no winner!", "Draw", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    autoNewGame();
                }
            }
        }

        private void autoNewGame()
        {
            playerTurn = true;
            turnCount = 0;

            try
            {
                foreach (Control b in Controls)
                {
                    if (b is Button)
                    {
                        Button? button = b as Button;
                        button.Enabled = true;
                        (b as Button).Text = "";
                    }
                }
            }catch { }
        }

        private void updateWinStreak(string winner)
        {
            if (winner == "X")
            {
                if (winStreak < 0)
                    winStreak = 0;
                winStreak++;
            }
            else if (winner == "O")
            {
                if (winStreak > 0)
                    winStreak = 0;
                winStreak--;
            }
            else
            {
                winStreak = 0;
            }

            txtWinStreak.Visible = (winStreak != 0);
            txtWinStreak.Text = String.Format("{0} is on win streak of {1}!", winner, Math.Abs(winStreak));
        }

        private void txtWinStreak_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to reset the win streak?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                updateWinStreak("");
            }
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            autoNewGame();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit(); //Close the app
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("TicTacToe by Emre Dogramaci", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnClick(object sender, EventArgs e)
        {
            Button btnPlay = (Button)sender;

            if (playerTurn)
            {
                btnPlay.Text = "X";
                btnPlay.Enabled = false;
            }
            else
            {
                btnPlay.Text = "O";
                btnPlay.Enabled = false;
            }

            turnCount++;
            playerTurn = !playerTurn;
            checkWinner();
        }
    }
}