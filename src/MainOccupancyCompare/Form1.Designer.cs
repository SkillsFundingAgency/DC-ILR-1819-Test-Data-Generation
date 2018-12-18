namespace MainOccupancyCompare
{
    partial class Form1
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.uiClipboard = new System.Windows.Forms.Button();
            this.uiCompare = new System.Windows.Forms.Button();
            this.uiBrowse2 = new System.Windows.Forms.Button();
            this.uiBrowse1 = new System.Windows.Forms.Button();
            this.uiFilepath2 = new System.Windows.Forms.TextBox();
            this.uiFilepath1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.uiFindingsList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.uiProgress = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.uiProgress);
            this.splitContainer1.Panel1.Controls.Add(this.uiClipboard);
            this.splitContainer1.Panel1.Controls.Add(this.uiCompare);
            this.splitContainer1.Panel1.Controls.Add(this.uiBrowse2);
            this.splitContainer1.Panel1.Controls.Add(this.uiBrowse1);
            this.splitContainer1.Panel1.Controls.Add(this.uiFilepath2);
            this.splitContainer1.Panel1.Controls.Add(this.uiFilepath1);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.uiFindingsList);
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 68;
            this.splitContainer1.TabIndex = 0;
            // 
            // uiClipboard
            // 
            this.uiClipboard.Location = new System.Drawing.Point(713, 33);
            this.uiClipboard.Name = "uiClipboard";
            this.uiClipboard.Size = new System.Drawing.Size(75, 23);
            this.uiClipboard.TabIndex = 7;
            this.uiClipboard.Text = "Clipboard";
            this.uiClipboard.UseVisualStyleBackColor = true;
            this.uiClipboard.Click += new System.EventHandler(this.uiClipboard_Click);
            // 
            // uiCompare
            // 
            this.uiCompare.Location = new System.Drawing.Point(713, 8);
            this.uiCompare.Name = "uiCompare";
            this.uiCompare.Size = new System.Drawing.Size(75, 23);
            this.uiCompare.TabIndex = 6;
            this.uiCompare.Text = "Compare";
            this.uiCompare.UseVisualStyleBackColor = true;
            this.uiCompare.Click += new System.EventHandler(this.uiCompare_Click);
            // 
            // uiBrowse2
            // 
            this.uiBrowse2.Location = new System.Drawing.Point(464, 35);
            this.uiBrowse2.Name = "uiBrowse2";
            this.uiBrowse2.Size = new System.Drawing.Size(75, 23);
            this.uiBrowse2.TabIndex = 5;
            this.uiBrowse2.Text = "Browse";
            this.uiBrowse2.UseVisualStyleBackColor = true;
            this.uiBrowse2.Click += new System.EventHandler(this.uiBrowse2_Click);
            // 
            // uiBrowse1
            // 
            this.uiBrowse1.Location = new System.Drawing.Point(464, 8);
            this.uiBrowse1.Name = "uiBrowse1";
            this.uiBrowse1.Size = new System.Drawing.Size(75, 23);
            this.uiBrowse1.TabIndex = 4;
            this.uiBrowse1.Text = "Browse";
            this.uiBrowse1.UseVisualStyleBackColor = true;
            this.uiBrowse1.Click += new System.EventHandler(this.uiBrowse1_Click);
            // 
            // uiFilepath2
            // 
            this.uiFilepath2.Location = new System.Drawing.Point(92, 38);
            this.uiFilepath2.Name = "uiFilepath2";
            this.uiFilepath2.Size = new System.Drawing.Size(366, 20);
            this.uiFilepath2.TabIndex = 3;
            // 
            // uiFilepath1
            // 
            this.uiFilepath1.Location = new System.Drawing.Point(92, 13);
            this.uiFilepath1.Name = "uiFilepath1";
            this.uiFilepath1.Size = new System.Drawing.Size(366, 20);
            this.uiFilepath1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Report 2 Path";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Report 1 Path";
            // 
            // uiFindingsList
            // 
            this.uiFindingsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader6,
            this.columnHeader3,
            this.columnHeader7,
            this.columnHeader4,
            this.columnHeader5});
            this.uiFindingsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiFindingsList.FullRowSelect = true;
            this.uiFindingsList.Location = new System.Drawing.Point(0, 0);
            this.uiFindingsList.Name = "uiFindingsList";
            this.uiFindingsList.Size = new System.Drawing.Size(800, 378);
            this.uiFindingsList.TabIndex = 0;
            this.uiFindingsList.UseCompatibleStateImageBehavior = false;
            this.uiFindingsList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Difference";
            this.columnHeader1.Width = 93;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Source";
            this.columnHeader2.Width = 119;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Section";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "SourceKey";
            this.columnHeader3.Width = 132;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Column";
            this.columnHeader7.Width = 95;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "ValueSource";
            this.columnHeader4.Width = 248;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "ValueOther";
            this.columnHeader5.Width = 170;
            // 
            // uiProgress
            // 
            this.uiProgress.Location = new System.Drawing.Point(545, 9);
            this.uiProgress.Name = "uiProgress";
            this.uiProgress.Size = new System.Drawing.Size(162, 23);
            this.uiProgress.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button uiCompare;
        private System.Windows.Forms.Button uiBrowse2;
        private System.Windows.Forms.Button uiBrowse1;
        private System.Windows.Forms.TextBox uiFilepath2;
        private System.Windows.Forms.TextBox uiFilepath1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView uiFindingsList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Button uiClipboard;
        private System.Windows.Forms.ProgressBar uiProgress;
    }
}

