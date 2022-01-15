namespace GigaSplitter
{
    partial class frmCountandSplit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCountandSplit));
            this.txt_SplitClaimCount = new System.Windows.Forms.TextBox();
            this.lbl_SplitClaimCount = new System.Windows.Forms.Label();
            this.lbl_TotalClaimCountValue = new System.Windows.Forms.Label();
            this.lbl_TotalClaimCount = new System.Windows.Forms.Label();
            this.btn_SplitClaims = new System.Windows.Forms.Button();
            this.btnSourceFile = new System.Windows.Forms.Button();
            this.txtSourceFile = new System.Windows.Forms.TextBox();
            this.txtDestinationFolder = new System.Windows.Forms.TextBox();
            this.ofdSourceFile = new System.Windows.Forms.OpenFileDialog();
            this.sfdDestinationFolder = new System.Windows.Forms.SaveFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.ss1 = new System.Windows.Forms.StatusStrip();
            this.ss1Lbl1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ss1Pb1 = new System.Windows.Forms.ToolStripProgressBar();
            this.ss1Lbl2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ss2 = new System.Windows.Forms.StatusStrip();
            this.ss2Lbl1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.ss1.SuspendLayout();
            this.ss2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_SplitClaimCount
            // 
            this.txt_SplitClaimCount.Location = new System.Drawing.Point(146, 148);
            this.txt_SplitClaimCount.Name = "txt_SplitClaimCount";
            this.txt_SplitClaimCount.Size = new System.Drawing.Size(144, 20);
            this.txt_SplitClaimCount.TabIndex = 7;
            // 
            // lbl_SplitClaimCount
            // 
            this.lbl_SplitClaimCount.AutoSize = true;
            this.lbl_SplitClaimCount.Location = new System.Drawing.Point(56, 151);
            this.lbl_SplitClaimCount.Name = "lbl_SplitClaimCount";
            this.lbl_SplitClaimCount.Size = new System.Drawing.Size(84, 13);
            this.lbl_SplitClaimCount.TabIndex = 6;
            this.lbl_SplitClaimCount.Text = "Spli&t Line Count:";
            // 
            // lbl_TotalClaimCountValue
            // 
            this.lbl_TotalClaimCountValue.AutoSize = true;
            this.lbl_TotalClaimCountValue.Location = new System.Drawing.Point(146, 123);
            this.lbl_TotalClaimCountValue.Name = "lbl_TotalClaimCountValue";
            this.lbl_TotalClaimCountValue.Size = new System.Drawing.Size(143, 13);
            this.lbl_TotalClaimCountValue.TabIndex = 5;
            this.lbl_TotalClaimCountValue.Text = "<Total Line Count - Number>";
            // 
            // lbl_TotalClaimCount
            // 
            this.lbl_TotalClaimCount.AutoSize = true;
            this.lbl_TotalClaimCount.Location = new System.Drawing.Point(52, 123);
            this.lbl_TotalClaimCount.Name = "lbl_TotalClaimCount";
            this.lbl_TotalClaimCount.Size = new System.Drawing.Size(88, 13);
            this.lbl_TotalClaimCount.TabIndex = 4;
            this.lbl_TotalClaimCount.Text = "Total Line Count:";
            // 
            // btn_SplitClaims
            // 
            this.btn_SplitClaims.Location = new System.Drawing.Point(235, 198);
            this.btn_SplitClaims.Name = "btn_SplitClaims";
            this.btn_SplitClaims.Size = new System.Drawing.Size(282, 38);
            this.btn_SplitClaims.TabIndex = 8;
            this.btn_SplitClaims.Text = "&Split Lines";
            this.btn_SplitClaims.UseVisualStyleBackColor = true;
            this.btn_SplitClaims.Click += new System.EventHandler(this.btn_SplitClaims_Click);
            // 
            // btnSourceFile
            // 
            this.btnSourceFile.Location = new System.Drawing.Point(31, 40);
            this.btnSourceFile.Name = "btnSourceFile";
            this.btnSourceFile.Size = new System.Drawing.Size(104, 23);
            this.btnSourceFile.TabIndex = 0;
            this.btnSourceFile.Text = "Source &File";
            this.btnSourceFile.UseVisualStyleBackColor = true;
            this.btnSourceFile.Click += new System.EventHandler(this.btnSourceFile_Click);
            // 
            // txtSourceFile
            // 
            this.txtSourceFile.Location = new System.Drawing.Point(146, 42);
            this.txtSourceFile.Name = "txtSourceFile";
            this.txtSourceFile.Size = new System.Drawing.Size(526, 20);
            this.txtSourceFile.TabIndex = 1;
            // 
            // txtDestinationFolder
            // 
            this.txtDestinationFolder.Location = new System.Drawing.Point(146, 77);
            this.txtDestinationFolder.Name = "txtDestinationFolder";
            this.txtDestinationFolder.Size = new System.Drawing.Size(526, 20);
            this.txtDestinationFolder.TabIndex = 3;
            // 
            // ofdSourceFile
            // 
            this.ofdSourceFile.Filter = "Text Files (*.txt)|*.txt|XML files (*.xml)|*.xml";
            this.ofdSourceFile.RestoreDirectory = true;
            this.ofdSourceFile.Title = "Source File";
            // 
            // sfdDestinationFolder
            // 
            this.sfdDestinationFolder.FileName = "Destination Folder";
            this.sfdDestinationFolder.Filter = "All files|*.*";
            this.sfdDestinationFolder.RestoreDirectory = true;
            this.sfdDestinationFolder.Title = "Destination Folder";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "&Destination Folder:";
            // 
            // ss1
            // 
            this.ss1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ss1Lbl1,
            this.ss1Pb1,
            this.ss1Lbl2});
            this.ss1.Location = new System.Drawing.Point(0, 286);
            this.ss1.Name = "ss1";
            this.ss1.Size = new System.Drawing.Size(700, 22);
            this.ss1.SizingGrip = false;
            this.ss1.TabIndex = 10;
            // 
            // ss1Lbl1
            // 
            this.ss1Lbl1.AutoSize = false;
            this.ss1Lbl1.Name = "ss1Lbl1";
            this.ss1Lbl1.Size = new System.Drawing.Size(50, 17);
            this.ss1Lbl1.Text = "0%";
            // 
            // ss1Pb1
            // 
            this.ss1Pb1.AutoSize = false;
            this.ss1Pb1.Name = "ss1Pb1";
            this.ss1Pb1.Size = new System.Drawing.Size(600, 16);
            // 
            // ss1Lbl2
            // 
            this.ss1Lbl2.AutoSize = false;
            this.ss1Lbl2.Name = "ss1Lbl2";
            this.ss1Lbl2.Size = new System.Drawing.Size(50, 17);
            this.ss1Lbl2.Text = "100%";
            // 
            // ss2
            // 
            this.ss2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ss2Lbl1});
            this.ss2.Location = new System.Drawing.Point(0, 264);
            this.ss2.Name = "ss2";
            this.ss2.Size = new System.Drawing.Size(700, 22);
            this.ss2.SizingGrip = false;
            this.ss2.TabIndex = 11;
            // 
            // ss2Lbl1
            // 
            this.ss2Lbl1.Name = "ss2Lbl1";
            this.ss2Lbl1.Size = new System.Drawing.Size(74, 17);
            this.ss2Lbl1.Text = "<SS2 LBL-1>";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(700, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAbout,
            this.mnuApplication});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44, 20);
            this.mnuHelp.Text = "&Help";
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(152, 22);
            this.mnuAbout.Text = "&About";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // mnuApplication
            // 
            this.mnuApplication.Name = "mnuApplication";
            this.mnuApplication.Size = new System.Drawing.Size(152, 22);
            this.mnuApplication.Text = "Applicatio&n";
            this.mnuApplication.Click += new System.EventHandler(this.mnuApplication_Click);
            // 
            // frmCountandSplit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 308);
            this.Controls.Add(this.ss2);
            this.Controls.Add(this.ss1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDestinationFolder);
            this.Controls.Add(this.txtSourceFile);
            this.Controls.Add(this.btnSourceFile);
            this.Controls.Add(this.txt_SplitClaimCount);
            this.Controls.Add(this.lbl_SplitClaimCount);
            this.Controls.Add(this.lbl_TotalClaimCountValue);
            this.Controls.Add(this.lbl_TotalClaimCount);
            this.Controls.Add(this.btn_SplitClaims);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "frmCountandSplit";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "GigaSplitter";
            this.Load += new System.EventHandler(this.CountandSplit_Load);
            this.ss1.ResumeLayout(false);
            this.ss1.PerformLayout();
            this.ss2.ResumeLayout(false);
            this.ss2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_SplitClaimCount;
        private System.Windows.Forms.Label lbl_SplitClaimCount;
        private System.Windows.Forms.Label lbl_TotalClaimCountValue;
        private System.Windows.Forms.Label lbl_TotalClaimCount;
        private System.Windows.Forms.Button btn_SplitClaims;
        private System.Windows.Forms.Button btnSourceFile;
        private System.Windows.Forms.TextBox txtSourceFile;
        private System.Windows.Forms.TextBox txtDestinationFolder;
        private System.Windows.Forms.OpenFileDialog ofdSourceFile;
        private System.Windows.Forms.SaveFileDialog sfdDestinationFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip ss1;
        private System.Windows.Forms.ToolStripStatusLabel ss1Lbl1;
        private System.Windows.Forms.ToolStripProgressBar ss1Pb1;
        private System.Windows.Forms.ToolStripStatusLabel ss1Lbl2;
        private System.Windows.Forms.StatusStrip ss2;
        private System.Windows.Forms.ToolStripStatusLabel ss2Lbl1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.ToolStripMenuItem mnuApplication;
    }
}