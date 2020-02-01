using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace Sudoku
{
    public partial class frmOptions : Form
    {
        frmMain principalForm;
        public bool validated;
        private DateTime lastClick;
        private int clickCount;
        public frmOptions()
        {
            InitializeComponent();
        }

        private static readonly RNGCryptoServiceProvider _generator = new RNGCryptoServiceProvider();

        public static int ri(int minimumValue, int maximumValue)
        {
            byte[] randomNumber = new byte[1];

            _generator.GetBytes(randomNumber);

            double asciiValueOfRandomCharacter = Convert.ToDouble(randomNumber[0]);

            // We are using Math.Max, and substracting 0.00000000001, 
            // to ensure "multiplier" will always be between 0.0 and .99999999999
            // Otherwise, it's possible for it to be "1", which causes problems in our rounding.
            double multiplier = Math.Max(0, (asciiValueOfRandomCharacter / 255d) - 0.00000000001d);

            // We need to add one to the range, to allow for the rounding done with Math.Floor
            int range = maximumValue - minimumValue + 1;

            double randomValueInRange = Math.Floor(multiplier * range);

            return (int)(minimumValue + randomValueInRange);
        }

        private void frmOptions_Shown(object sender, EventArgs e)
        {
            principalForm = Application.OpenForms.OfType<frmMain>().Single();
            if (principalForm.playerInput == true)
            {
                btnGenerateCandidates.Enabled = true;
                btnRemoveCandidates.Enabled = true;
            } else
            {
                btnGenerateCandidates.Enabled = false;
                btnRemoveCandidates.Enabled = false;
            }
            updMin.Value = 10;
            updMax.Value = 30;
            validated = false;
            lastClick = DateTime.Now;
            clickCount = 0;
            this.Activate();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (principalForm.isLegal(principalForm.board) && radSolve.Checked)
            {
                if (principalForm.board.Cast<int>().SequenceEqual(principalForm.startingBoard.Cast<int>()) == false)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        for (int k = 0; k < 9; k++)
                        {
                            principalForm.board[i, k] = 0;
                        }
                    }
                }
                principalForm.playerInput = false;
                btnGenerateCandidates.Enabled = false;
                btnRemoveCandidates.Enabled = false;
                principalForm.solveSudoku(false);
                validated = true;
            }
            else if (principalForm.isLegal(principalForm.board) && radPlay.Checked)
            {
                principalForm.playerInput = true;
                btnGenerateCandidates.Enabled = true;
                btnRemoveCandidates.Enabled = true;
                principalForm.solveSudoku(true);
                validated = true;
            }
            else
            {
                MessageBox.Show("Le sudoku entrée n'est pas valide", "Erreur");
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (principalForm.board.Cast<int>().SequenceEqual(principalForm.startingBoard.Cast<int>()))
            {
                btnRemoveCandidates.PerformClick();
                for (int i = 0; i < 9; i++)
                {
                    for (int k = 0; k < 9; k++)
                    {
                        principalForm.board[i, k] = 0;
                        principalForm.startingBoard[i, k] = 0;
                    }
                }
                principalForm.playerInput = false;
            }
            else
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int k = 0; k < 9; k++)
                    {
                        principalForm.board[i, k] = principalForm.startingBoard[i, k];
                    }
                }
            }
            principalForm.refreshPicture();
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int k = 0; k < 9; k++)
                {
                    principalForm.board[i, k] = 0;
                    principalForm.startingBoard[i, k] = 0;
                    principalForm.candidates[new Point(i, k)] = new List<int>();
                }
            }
            int count = ri((int)updMin.Value, (int)updMax.Value);
            int x, y, n;
            for (int i = 0; i < count; i++)
            {
                x = ri(0, 8);
                y = ri(0, 8);
                n = ri(1, 9);
                if (principalForm.board[x, y] == 0 && principalForm.isLegal(principalForm.board, x, y, n) == true)
                {
                    principalForm.board[x, y] = n;
                    principalForm.startingBoard[x, y] = n;
                }
                else
                {
                    i--;
                }
            }
            principalForm.refreshPicture();
        }

        private void upd_ValueChanged(object sender, EventArgs e)
        {
            if (sender.Equals(updMin))
            {
                updMax.Minimum = updMin.Value;
            }
            else
            {
                updMin.Maximum = updMax.Value;
            }
            lblRandom.Text = string.Format("Marge de randomisation ({0} à {1})", updMin.Value.ToString(), updMax.Value.ToString());
        }

        private void btnGenerateCandidates_Click(object sender, EventArgs e)
        {
            if (clickCount == 0 || (DateTime.Now.Ticks - lastClick.Ticks) > 20000000)
            {
                principalForm.findCandidates();
                lastClick = DateTime.Now;
                clickCount = 1;
            } else
            {
                if (clickCount > 1)
                {
                    principalForm.resolveSuperCandidates();
                    clickCount = 0;
                }
                else
                    principalForm.findSuperCandidates();
                {
                    clickCount = 2;
                }
                lastClick = DateTime.Now;
            }
        }

        private void btnRemoveCandidates_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int k = 0; k < 9; k++)
                {
                    principalForm.candidates[new Point(i, k)] = new List<int>(); ;
                    principalForm.superCandidates[i, k] = 0;
                    principalForm.refreshPicture();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            validated = true;
            sfdSave.InitialDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            if (sfdSave.ShowDialog() == DialogResult.OK)
            {
                principalForm.resetBoard();
                using (StreamWriter sw = new StreamWriter(sfdSave.FileName))
                {
                    for (int i = 0; i < 9; i++)
                    {
                        for (int k = 0; k < 9; k++)
                        {
                            sw.Write(principalForm.startingBoard[i, k].ToString());
                        }
                        sw.WriteLine();
                    }
                    for (int i = 0; i < 9; i++)
                    {
                        for (int k = 0; k < 9; k++)
                        {
                            sw.Write(principalForm.board[i, k].ToString());
                        }
                        sw.WriteLine();
                    }
                    string s;
                    for (int i = 0; i < 9; i++)
                    {
                        s = "";
                        for (int k = 0; k < 9; k++)
                        {
                            s += "0";
                            foreach (int n in principalForm.candidates[new Point(i, k)])
                            {
                                s += n.ToString();
                            }
                            s += ",";
                        }
                        sw.WriteLine(s.Substring(0, s.Length - 1));
                    }
                }
            }
            validated = false;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            validated = true;
            ofdLoad.InitialDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            if (ofdLoad.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = new StreamReader(ofdLoad.FileName))
                {
                    try
                    {
                        string currentLine = "";
                        for (int i = 0; i < 9; i++)
                        {
                            currentLine = sr.ReadLine();
                            for (int k = 0; k < 9; k++)
                            {
                                principalForm.startingBoard[i, k] = Convert.ToInt16(currentLine.Substring(k, 1));
                            }
                        }
                        for (int i = 0; i < 9; i++)
                        {
                            currentLine = sr.ReadLine();
                            for (int k = 0; k < 9; k++)
                            {
                                principalForm.board[i, k] = Convert.ToInt16(currentLine.Substring(k, 1));
                            }
                        }
                        string[] lineArray;
                        int parsedChar;
                        for (int i = 0; i < 9; i++)
                        {
                            lineArray = sr.ReadLine().Split(',');
                            for (int k = 0; k < 9; k++)
                            {
                                foreach (char c in lineArray[k].ToCharArray())
                                {
                                    parsedChar = Convert.ToInt32(c.ToString());
                                    if (parsedChar != 0)
                                        principalForm.candidates[new Point(i, k)].Add(parsedChar);
                                }
                            }
                        }
                        btnRemoveCandidates.PerformClick();
                        principalForm.refreshPicture();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Invalid file loaded", "Error");
                        for (int i = 0; i < 9; i++)
                        {
                            for (int k = 0; k < 9; k++)
                            {
                                principalForm.board[i, k] = 0;
                                principalForm.startingBoard[i, k] = 0;
                            }
                        }
                    }
                }
            }
            validated = false;
        }

        private void btnSample_Click(object sender, EventArgs e)
        {
            int randResult = ri(0, 49);
            string resourceName = "Sudoku.Resources.sudoku sample.txt";
            using (StreamReader sr = new StreamReader(
                System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName)))
            {
                string currentLine = "";
                for (int i = 0; i < (randResult + 1) * 10; i++)
                {
                    currentLine = sr.ReadLine();
                    if (i > randResult * 10)
                    {
                        for (int k = 0; k < 9; k++)
                        {
                            principalForm.startingBoard[i - randResult * 10 - 1, k] = Convert.ToInt16(currentLine.Substring(k, 1));
                            principalForm.board[i - randResult * 10 - 1, k] = principalForm.startingBoard[i - randResult * 10 - 1, k];
                        }
                    }
                }
            }
            btnRemoveCandidates.PerformClick();
            principalForm.refreshPicture();
        }

        private void FrmOptions_Deactivate(object sender, EventArgs e)
        {
            if (validated == false)
            {
                DialogResult result = MessageBox.Show("Vous n'avez pas commencer à jouer! Voulez vous retourner à la forme d'options?", "Avis", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    this.Activate();
                } else
                {
                    validated = true;
                }
            }
        }

        private void FrmOptions_FormClosing(object sender, FormClosingEventArgs e)
        {
            validated = true;
        }
    }
}
