namespace Sudoku
{
    partial class frmOptions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblInfo = new System.Windows.Forms.Label();
            this.radPlay = new System.Windows.Forms.RadioButton();
            this.radSolve = new System.Windows.Forms.RadioButton();
            this.grpType = new System.Windows.Forms.GroupBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnRandom = new System.Windows.Forms.Button();
            this.ofdLoad = new System.Windows.Forms.OpenFileDialog();
            this.sfdSave = new System.Windows.Forms.SaveFileDialog();
            this.btnGenerateCandidates = new System.Windows.Forms.Button();
            this.btnRemoveCandidates = new System.Windows.Forms.Button();
            this.lblRandom = new System.Windows.Forms.Label();
            this.updMin = new System.Windows.Forms.NumericUpDown();
            this.updMax = new System.Windows.Forms.NumericUpDown();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnSample = new System.Windows.Forms.Button();
            this.grpType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.updMax)).BeginInit();
            this.SuspendLayout();
            // 
            // lblInfo
            // 
            this.lblInfo.Location = new System.Drawing.Point(12, 9);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(177, 53);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "Appuyer sur la clé espace pour afficher ce message";
            // 
            // radPlay
            // 
            this.radPlay.AutoSize = true;
            this.radPlay.Location = new System.Drawing.Point(10, 42);
            this.radPlay.Name = "radPlay";
            this.radPlay.Size = new System.Drawing.Size(123, 17);
            this.radPlay.TabIndex = 3;
            this.radPlay.TabStop = true;
            this.radPlay.Text = "Résoudre sois même";
            this.radPlay.UseVisualStyleBackColor = true;
            // 
            // radSolve
            // 
            this.radSolve.AutoSize = true;
            this.radSolve.Checked = true;
            this.radSolve.Location = new System.Drawing.Point(10, 19);
            this.radSolve.Name = "radSolve";
            this.radSolve.Size = new System.Drawing.Size(164, 17);
            this.radSolve.TabIndex = 2;
            this.radSolve.TabStop = true;
            this.radSolve.Text = "Résoudre avec le programme";
            this.radSolve.UseVisualStyleBackColor = true;
            // 
            // grpType
            // 
            this.grpType.Controls.Add(this.radPlay);
            this.grpType.Controls.Add(this.radSolve);
            this.grpType.Location = new System.Drawing.Point(12, 43);
            this.grpType.Name = "grpType";
            this.grpType.Size = new System.Drawing.Size(188, 67);
            this.grpType.TabIndex = 2;
            this.grpType.TabStop = false;
            this.grpType.Text = "Methode de sudoku";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 116);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(188, 20);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Jouer";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(301, 90);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(83, 45);
            this.btnReset.TabIndex = 9;
            this.btnReset.Text = "Réinitialiser (Annuler)";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(212, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(83, 22);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Enregistrer";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(301, 12);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(83, 22);
            this.btnLoad.TabIndex = 5;
            this.btnLoad.Text = "Ouvrir";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnRandom
            // 
            this.btnRandom.Location = new System.Drawing.Point(390, 12);
            this.btnRandom.Name = "btnRandom";
            this.btnRandom.Size = new System.Drawing.Size(83, 22);
            this.btnRandom.TabIndex = 10;
            this.btnRandom.Text = "Randomizer";
            this.btnRandom.UseVisualStyleBackColor = true;
            this.btnRandom.Click += new System.EventHandler(this.btnRandom_Click);
            // 
            // ofdLoad
            // 
            this.ofdLoad.DefaultExt = "txt";
            this.ofdLoad.FileName = "sudoku.txt";
            this.ofdLoad.Filter = "Text Documents|*.txt";
            this.ofdLoad.RestoreDirectory = true;
            this.ofdLoad.Title = "Load Sudoku Puzzle";
            // 
            // sfdSave
            // 
            this.sfdSave.DefaultExt = "txt";
            this.sfdSave.FileName = "sudoku.txt";
            this.sfdSave.Filter = "Text Documents|*.txt";
            this.sfdSave.RestoreDirectory = true;
            this.sfdSave.Title = "Save Sudoku Puzzle";
            // 
            // btnGenerateCandidates
            // 
            this.btnGenerateCandidates.Location = new System.Drawing.Point(212, 39);
            this.btnGenerateCandidates.Name = "btnGenerateCandidates";
            this.btnGenerateCandidates.Size = new System.Drawing.Size(83, 45);
            this.btnGenerateCandidates.TabIndex = 6;
            this.btnGenerateCandidates.Text = "Trouver les candidats";
            this.btnGenerateCandidates.UseVisualStyleBackColor = true;
            this.btnGenerateCandidates.Click += new System.EventHandler(this.btnGenerateCandidates_Click);
            // 
            // btnRemoveCandidates
            // 
            this.btnRemoveCandidates.Location = new System.Drawing.Point(301, 39);
            this.btnRemoveCandidates.Name = "btnRemoveCandidates";
            this.btnRemoveCandidates.Size = new System.Drawing.Size(83, 45);
            this.btnRemoveCandidates.TabIndex = 7;
            this.btnRemoveCandidates.Text = "Enlever les candidats";
            this.btnRemoveCandidates.UseVisualStyleBackColor = true;
            this.btnRemoveCandidates.Click += new System.EventHandler(this.btnRemoveCandidates_Click);
            // 
            // lblRandom
            // 
            this.lblRandom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRandom.Location = new System.Drawing.Point(390, 39);
            this.lblRandom.Name = "lblRandom";
            this.lblRandom.Size = new System.Drawing.Size(83, 45);
            this.lblRandom.TabIndex = 11;
            this.lblRandom.Text = "Marge de randomisation (min à max)";
            // 
            // updMin
            // 
            this.updMin.Location = new System.Drawing.Point(393, 90);
            this.updMin.Maximum = new decimal(new int[] {
            81,
            0,
            0,
            0});
            this.updMin.Name = "updMin";
            this.updMin.Size = new System.Drawing.Size(80, 20);
            this.updMin.TabIndex = 11;
            this.updMin.ValueChanged += new System.EventHandler(this.upd_ValueChanged);
            // 
            // updMax
            // 
            this.updMax.Location = new System.Drawing.Point(393, 115);
            this.updMax.Maximum = new decimal(new int[] {
            81,
            0,
            0,
            0});
            this.updMax.Name = "updMax";
            this.updMax.Size = new System.Drawing.Size(80, 20);
            this.updMax.TabIndex = 12;
            this.updMax.ValueChanged += new System.EventHandler(this.upd_ValueChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnSample
            // 
            this.btnSample.Location = new System.Drawing.Point(212, 88);
            this.btnSample.Name = "btnSample";
            this.btnSample.Size = new System.Drawing.Size(83, 45);
            this.btnSample.TabIndex = 8;
            this.btnSample.Text = "Prendre d\'un échantillion";
            this.btnSample.UseVisualStyleBackColor = true;
            this.btnSample.Click += new System.EventHandler(this.btnSample_Click);
            // 
            // frmOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 142);
            this.Controls.Add(this.btnSample);
            this.Controls.Add(this.updMax);
            this.Controls.Add(this.updMin);
            this.Controls.Add(this.lblRandom);
            this.Controls.Add(this.btnRemoveCandidates);
            this.Controls.Add(this.btnGenerateCandidates);
            this.Controls.Add(this.btnRandom);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.grpType);
            this.Controls.Add(this.lblInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOptions";
            this.Text = "Options";
            this.Deactivate += new System.EventHandler(this.FrmOptions_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmOptions_FormClosing);
            this.Shown += new System.EventHandler(this.frmOptions_Shown);
            this.grpType.ResumeLayout(false);
            this.grpType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.updMax)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.RadioButton radPlay;
        private System.Windows.Forms.RadioButton radSolve;
        private System.Windows.Forms.GroupBox grpType;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnRandom;
        private System.Windows.Forms.OpenFileDialog ofdLoad;
        private System.Windows.Forms.SaveFileDialog sfdSave;
        private System.Windows.Forms.Button btnGenerateCandidates;
        private System.Windows.Forms.Button btnRemoveCandidates;
        private System.Windows.Forms.Label lblRandom;
        private System.Windows.Forms.NumericUpDown updMin;
        private System.Windows.Forms.NumericUpDown updMax;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnSample;
    }
}