using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku
{
    public partial class frmMain : Form
    {
        //Initialiser les variables
        public int[,] board = new int[9, 9];
        public int[,] startingBoard = new int[9, 9];
        public bool playerInput = false;
        public Dictionary<Point, List<int>> candidates = new Dictionary<Point, List<int>>();
        public int[,] superCandidates = new int[9,9];
        private Point startWrong = new Point(-1, -1);
        private Point endWrong = new Point(-1, -1);
        private Point wrongNumA = new Point(-1, -1);
        private Point wrongNumB = new Point(-1, -1);
        private List<int> illegal = new List<int>();
        private List<int> illegalType = new List<int>();
        private Point selectedIndex = new Point(-1, -1);
        private bool solved;
        private bool testing;
        frmOptions optionsForm = new frmOptions();

        public frmMain()
        {
            InitializeComponent();
        }

        //Fonction qui affiche à l'utilisateur
        private void picMain_Paint(object sender, PaintEventArgs e)
        {
            //Dessine des boites grises de 3x3
            for (int i = 0; i < 3; i++)
            {
                for (int k = 0; k < 3; k++)
                {
                    if ((i + k) % 2 == 0)
                    {
                        e.Graphics.FillRectangle(Brushes.LightGray, i * 201, k * 201, 201, 201);
                    }
                }
            }
            if (bgwSolver.IsBusy == false || testing == true)
            {
                e.Graphics.FillRectangle(Brushes.LightCoral, startWrong.X * 67, 
                    startWrong.Y * 67, Math.Abs(startWrong.X - endWrong.X) * 67 + 67, Math.Abs(startWrong.Y - endWrong.Y) * 67 + 67);
                e.Graphics.FillRectangle(Brushes.LightBlue, selectedIndex.X * 67, selectedIndex.Y * 67, 67, 67);
            }
            //Dessine les lignes de chaques case (9x9) de largeur 67x67 px
            for (int i = 0; i <= 603; i += 67)
            {
                e.Graphics.DrawLine(Pens.Gray, 0, i, 603, i);
                e.Graphics.DrawLine(Pens.Gray, i, 0, i, 603);
            }
            //Dessine les nombres de chaque case
            for (int i = 0; i < 9; i++)
            {
                for (int k = 0; k < 9; k++)
                {
                    if (startingBoard[i, k] != 0)
                    {
                        e.Graphics.DrawString(startingBoard[i, k].ToString(),
                            new Font(new FontFamily("Arial"), 67, FontStyle.Regular, GraphicsUnit.Pixel),
                            new SolidBrush(Color.FromArgb(14, 73, 113)), i * 67, k * 67);
                    }
                    else if (board[i, k] != 0)
                    {
                        e.Graphics.DrawString(board[i, k].ToString(),
                            new Font(new FontFamily("Arial"), 67, FontStyle.Regular, GraphicsUnit.Pixel),
                            Brushes.Black, i * 67, k * 67);
                    }
                    else if (candidates[new Point(i, k)].Count() > 0 && (bgwSolver.IsBusy == false || testing == true))
                    {
                        foreach (int n in candidates[new Point(i, k)])
                        {
                            Point subLocation = new Point(((n - 1) % 3) * 22, ((n - 1) / 3) * 22);
                            if (superCandidates[i, k] == n)
                            {
                                e.Graphics.DrawString(n.ToString(),
                                    new Font(new FontFamily("Arial"), 22, FontStyle.Regular, GraphicsUnit.Pixel),
                                    Brushes.Blue, i * 67 + subLocation.X, k * 67 + subLocation.Y);
                            } else
                            {
                                e.Graphics.DrawString(n.ToString(),
                                    new Font(new FontFamily("Arial"), 22, FontStyle.Regular, GraphicsUnit.Pixel),
                                    Brushes.Black, i * 67 + subLocation.X, k * 67 + subLocation.Y);
                            }
                        }
                    }
                    if (wrongNumA == new Point(i, k) || wrongNumB == new Point(i, k))
                        e.Graphics.DrawString(board[i, k].ToString(),
                            new Font(new FontFamily("Arial"), 67, FontStyle.Regular, GraphicsUnit.Pixel),
                            Brushes.Red, i * 67, k * 67);
                }
            }
        }

        //Fonction qui traite le cliquet de l'utilisateur
        private void picMain_Click(object sender, EventArgs e)
        {
            if (bgwSolver.IsBusy == false || testing == true)
            {
                //Trouve le case sur le tableau de 9x9 qui a été appuyé
                int x = (Cursor.Position.X - this.DesktopLocation.X - 8);
                int y = (Cursor.Position.Y - this.DesktopLocation.Y - 30);
                Point clicked = new Point(Convert.ToInt16(Math.Floor(x / 67.0)), Convert.ToInt16(Math.Floor(y / 67.0)));
                if (selectedIndex.X == clicked.X && selectedIndex.Y == clicked.Y && playerInput == true &&
                    board[selectedIndex.X, selectedIndex.Y] == 0 && startingBoard[selectedIndex.X, selectedIndex.Y] == 0)
                {
                    Point subClicked = new Point((x - clicked.X * 67) / 22, (y - clicked.Y * 67) / 22);
                    int n = subClicked.Y * 3 + subClicked.X + 1;
                    if (candidates[clicked].Contains(n))
                    {
                        candidates[clicked].Remove(n);
                    }
                    else
                    {
                        candidates[clicked].Add(n);
                    }
                }
                selectedIndex = clicked;
                picMain.Refresh();
            }
        }

        //Fonction qui initialise le tableau
        private void frmMain_Load(object sender, EventArgs e)
        {
            //Initialise chaque case avec une valeur de 0
            for (int i = 0; i < 9; i++)
            {
                for (int k = 0; k < 9; k++)
                {
                    candidates[new Point(i, k)] = new List<int>();
                    superCandidates[i, k] = 0;
                    board[i, k] = 0;
                }
            }
            optionsForm = new frmOptions();
            optionsForm.validated = true;
            optionsForm.Show();
            optionsForm.Activate();
        }

        //Fonction qui traite les appuis de clé
        private void frmMain_KeyUp(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            if (bgwSolver.IsBusy == false || testing == true)
            {
                if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Escape)
                {
                    if (Application.OpenForms.OfType<frmOptions>().Count() <= 0)
                        optionsForm = new frmOptions();
                    optionsForm.Show();
                    //Si c'est la clé d'espace et le tableau est légale, résoudre le tableau
                }
                else if (e.KeyCode == Keys.Back)
                {
                    if (playerInput == false)
                        startingBoard[selectedIndex.X, selectedIndex.Y] = 0;
                    board[selectedIndex.X, selectedIndex.Y] = 0;
                    selectedIndex.X--;
                    if (selectedIndex.X < 0)
                    {
                        selectedIndex.Y--;
                        selectedIndex.X = 8;
                        if (selectedIndex.Y < 0)
                            selectedIndex.Y = 8;
                    }
                    isLegal(board);
                }
                else
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Left:
                            selectedIndex.X--;
                            break;
                        case Keys.Right:
                            selectedIndex.X++;
                            break;
                        case Keys.Up:
                            selectedIndex.Y--;
                            break;
                        case Keys.Down:
                            selectedIndex.Y++;
                            break;
                    }
                    if (selectedIndex.X < 0)
                    {
                        selectedIndex.X = 8;
                        selectedIndex.Y--;
                    }
                    else if (selectedIndex.X > 8)
                    {
                        selectedIndex.X = 0;
                        selectedIndex.Y++;
                    }
                    if (selectedIndex.Y < 0)
                        selectedIndex.Y = 8;
                    else if (selectedIndex.Y > 8)
                        selectedIndex.Y = 0;
                    KeysConverter kc = new KeysConverter();
                    string s = kc.ConvertToString(e.KeyCode);
                    char c = s.Substring(s.Length - 1).ToCharArray()[0];
                    int ascii = (int)c;
                    try
                    {
                        if (ascii > 47 && ascii < 58)
                        {
                            if (playerInput == false)
                            {
                                startingBoard[selectedIndex.X, selectedIndex.Y] = Convert.ToInt32(c.ToString());
                                selectedIndex.X++;
                                if (selectedIndex.X > 8)
                                {
                                    selectedIndex.X = 0;
                                    selectedIndex.Y++;
                                    if (selectedIndex.Y > 8)
                                        selectedIndex.Y = 0;
                                }
                            }
                            else if (startingBoard[selectedIndex.X, selectedIndex.Y] == 0)
                            {
                                board[selectedIndex.X, selectedIndex.Y] = Convert.ToInt32(c.ToString());
                                if (isLegal(board) == true && board.Cast<int>().Contains<int>(0) == false)
                                {
                                    picMain.Refresh();
                                    MessageBox.Show("Complété!");
                                    if (Application.OpenForms.OfType<frmOptions>().Count() <= 0)
                                        optionsForm = new frmOptions();
                                    optionsForm.Show();
                                }
                                selectedIndex.X++;
                                if (selectedIndex.X > 8)
                                {
                                    selectedIndex.X = 0;
                                    selectedIndex.Y++;
                                    if (selectedIndex.Y > 8)
                                        selectedIndex.Y = 0;
                                }
                            }
                            isLegal(board);
                        }
                    }
                    catch (Exception ex) { }
                }
                picMain.Refresh();
                if (playerInput == true)
                    resetBoard();
            }
        }

        //Fonction qui sois détermine si le tableau actuelle est légale, ou si le placement d'un nombre est légale
        public bool isLegal(int[,] boardUsed, int x = -1, int y = -1, int num = 0)
        {
            int[][] currentTest = new int[3][];
            for (int i = 0; i < 3; i++)
                currentTest[i] = new int[9];
            if (x < 0 || y < 0)
            {
                resetBoard();
                if (wrongNumA != new Point(-1, -1) || wrongNumB != new Point(-1, -1))
                {
                    if (boardUsed[wrongNumA.X, wrongNumA.Y] != boardUsed[wrongNumB.X, wrongNumB.Y])
                    {
                        wrongNumA = new Point(-1, -1);
                        wrongNumB = new Point(-1, -1);
                        startWrong = new Point(-1, -1);
                        endWrong = new Point(-1, -1);
                    }
                }
                for (int i = 0; i < 9; i++)
                {
                    for (int k = 0; k < 9; k++)
                    {
                        currentTest[0][k] = boardUsed[i, k];
                        currentTest[1][k] = boardUsed[k, i];
                        currentTest[2][k] = boardUsed[(i % 3) * 3 + (k % 3), (i / 3) * 3 + (k / 3)];
                    }
                    for (int k = 1; k < 10; k++)
                    {
                        if (currentTest[0].Count(n => n == k) > 1)
                        {
                            if (wrongNumA == new Point(-1, -1) && wrongNumB == new Point(-1, -1))
                            {
                                wrongNumA = new Point(i, String.Join("", currentTest[0]).IndexOf(k.ToString()));
                                wrongNumB = new Point(i, String.Join("", currentTest[0]).LastIndexOf(k.ToString()));
                                startWrong = new Point(i, 0);
                                endWrong = new Point(i, 8);
                            }
                            return false;
                        } else if (currentTest[1].Count(n => n == k) > 1)
                        {
                            if (wrongNumA == new Point(-1, -1) && wrongNumB == new Point(-1, -1))
                            {
                                wrongNumA = new Point(String.Join("", currentTest[1]).IndexOf(k.ToString()), i);
                                wrongNumB = new Point(String.Join("", currentTest[1]).LastIndexOf(k.ToString()), i);
                                startWrong = new Point(0, i);
                                endWrong = new Point(8, i);
                            }
                            return false;
                        } else if (currentTest[2].Count(n => n == k) > 1)
                        {
                            if (wrongNumA == new Point(-1, -1) && wrongNumB == new Point(-1, -1))
                            {
                                int index;
                                index = String.Join("", currentTest[2]).IndexOf(k.ToString());
                                wrongNumA = new Point((i % 3) * 3 + (index % 3), (i / 3) * 3 + (index / 3));
                                index = String.Join("", currentTest[2]).LastIndexOf(k.ToString());
                                wrongNumB = new Point((i % 3) * 3 + (index % 3), (i / 3) * 3 + (index / 3));

                                startWrong = new Point((i % 3) * 3, (i / 3) * 3);
                                endWrong = new Point((i % 3) * 3 + 2, (i / 3) * 3 + 2);
                            }
                            return false;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < 9; i++)
                {
                    currentTest[0][i] = boardUsed[x, i];
                    currentTest[1][i] = boardUsed[i, y];
                    currentTest[2][i] = boardUsed[(x / 3) * 3 + (i % 3), (y / 3) * 3 + (i / 3)];
                }
                if (currentTest[0].Contains(num) || currentTest[1].Contains(num) || currentTest[2].Contains(num))
                    return false;
            }
            return true;
        }

        //Fonction qui trouve une solution eventuelle
        private void solveCell(int x, int y, int[,] boardUsed)
        {
            if (solved == false)
            {
                if (testing == false)
                {
                    board = boardUsed;
                    picMain.Invalidate();
                }
                //this.BeginInvoke(new MethodInvoker(picMain.Refresh));
                if (startingBoard[x, y] == 0)
                {
                    for (int i = 1; i <= 9 && solved == false; i++)
                    {
                        if (startingBoard[x, y] != 0)
                            boardUsed[x, y] = startingBoard[x, y];
                        if (isLegal(boardUsed, x, y, i) == true)
                        {
                            boardUsed[x, y] = i;
                            if (x >= 8 && y >= 8)
                            {
                                solved = true;
                            }
                            int nextX = x + 1;
                            int nextY = y;
                            if (nextX > 8)
                            {
                                nextX = 0;
                                nextY++;
                            }
                            solveCell(nextX, nextY, boardUsed);
                        }
                    }
                    if (solved == false)
                        boardUsed[x, y] = startingBoard[x, y];
                }
                else
                {
                    if (x >= 8 && y >= 8)
                    {
                        solved = true;
                    }
                    else
                    {
                        int nextX = x + 1;
                        int nextY = y;
                        if (nextX > 8)
                        {
                            nextX = 0;
                            nextY++;
                        }
                        solveCell(nextX, nextY, boardUsed);
                    }
                }
            }
            if (x == 0 && y == 0 && solved == false)
                MessageBox.Show("Il n'y as pas de solution pour ce sudoku");
        }

        public void resetBoard()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int k = 0; k < 9; k++)
                {
                    if (startingBoard[i, k] != 0)
                        board[i, k] = startingBoard[i, k];
                }
            }
        }

        public void findCandidates()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int k = 0; k < 9; k++)
                {
                    if (board[i, k] == 0)
                    {
                        candidates[new Point(i, k)] = new List<int>(); ;
                        for (int n = 1; n < 10; n++)
                        {
                            if (isLegal(board, i, k, n) == true)
                                candidates[new Point(i, k)].Add(n);
                        }
                        picMain.Refresh();
                    }
                }
            }
        }

        public void findSuperCandidates()
        {
            findCandidates();
            int[][][] currentTest = new int[3][][];
            int[] count = new int[3];
            for (int i = 0; i < 3; i++)
            {
                currentTest[i] = new int[9][];
                for (int k = 0; k < 3; k++)
                    currentTest[i][k] = new int[9];
            }
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    superCandidates[x, y] = 0;
                    if (candidates[new Point(x, y)].Count() == 1)
                    {
                        superCandidates[x, y] = candidates[new Point(x, y)][0];
                    }
                    else
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            currentTest[0][i] = candidates[new Point(x, i)].ToArray();
                            currentTest[1][i] = candidates[new Point(i, y)].ToArray();
                            currentTest[2][i] = candidates[new Point((x / 3) * 3 + (i % 3), (y / 3) * 3 + (i / 3))].ToArray();
                        }
                        for (int i = 1; i < 10; i++)
                        {
                            count[0] = 0; count[1] = 0; count[2] = 0;
                            for (int k = 0; k < 9; k++)
                            {
                                for (int j = 0; j < 3; j++)
                                {
                                    if (currentTest[j][k].Contains(i))
                                        count[j]++;
                                }

                            }
                            if (count[0] == 1 && count[1] == 1 && count[2] == 1)
                                superCandidates[x, y] = i;
                        }
                    }
                }
            }
            picMain.Refresh();
        }

        public void resolveSuperCandidates()
        {
            findSuperCandidates();
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    if (superCandidates[x, y] != 0)
                    {
                        board[x, y] = superCandidates[x, y];
                        superCandidates[x, y] = 0;
                    }
                }
            }
        }

        //Fonction qui débute la résolution
        public void solveSudoku(bool test)
        {
            testing = test;
            bgwSolver.RunWorkerAsync();
        }

        public void refreshPicture()
        {
            picMain.Refresh();
        }

        private void bgwSolver_DoWork(object sender, DoWorkEventArgs e)
        {
            solved = false;
            resetBoard();
            if (testing == true)
            {
                int[,] currentBoard = new int[9, 9];
                for (int i = 0; i < 9; i++)
                {
                    for (int k = 0; k < 9; k++)
                    {
                        currentBoard[i, k] = board[i, k];
                    }
                }
                solveCell(0, 0, currentBoard);
            } else
            {
                resetBoard();
                solveCell(0, 0, board);
                picMain.Invalidate();
            }
        }

        private void BgwSolver_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (solved == false)
                if (Application.OpenForms.OfType<frmOptions>().Count() <= 0)
                    optionsForm = new frmOptions();
                optionsForm.Show();
        }
    }
}
