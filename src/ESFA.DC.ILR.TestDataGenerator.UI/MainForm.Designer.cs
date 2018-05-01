namespace ILRTestDataGenerator
{
    partial class MainForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.uiUKPRN = new System.Windows.Forms.TextBox();
            this.uiYear = new System.Windows.Forms.TextBox();
            this.uiOuputFile = new System.Windows.Forms.Button();
            this.uiParameters = new System.Windows.Forms.DataGridView();
            this.RuleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Valid = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Active = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.BaseLearner = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uiRuleData = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.uiMultiplication = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.fileNamespace = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.uiParameters)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "UKPRN";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(208, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Year";
            // 
            // uiUKPRN
            // 
            this.uiUKPRN.Location = new System.Drawing.Point(102, 10);
            this.uiUKPRN.Name = "uiUKPRN";
            this.uiUKPRN.Size = new System.Drawing.Size(100, 20);
            this.uiUKPRN.TabIndex = 2;
            this.uiUKPRN.Text = "90000064";
            this.uiUKPRN.TextChanged += new System.EventHandler(this.uiUKPRN_TextChanged);
            // 
            // uiYear
            // 
            this.uiYear.Location = new System.Drawing.Point(249, 10);
            this.uiYear.Name = "uiYear";
            this.uiYear.Size = new System.Drawing.Size(100, 20);
            this.uiYear.TabIndex = 3;
            this.uiYear.Text = "1718";
            // 
            // uiOuputFile
            // 
            this.uiOuputFile.Location = new System.Drawing.Point(494, 8);
            this.uiOuputFile.Name = "uiOuputFile";
            this.uiOuputFile.Size = new System.Drawing.Size(75, 23);
            this.uiOuputFile.TabIndex = 4;
            this.uiOuputFile.Text = "Output";
            this.uiOuputFile.UseVisualStyleBackColor = true;
            this.uiOuputFile.Click += new System.EventHandler(this.uiOuputFile_Click);
            // 
            // uiParameters
            // 
            this.uiParameters.AllowUserToAddRows = false;
            this.uiParameters.AllowUserToDeleteRows = false;
            this.uiParameters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.uiParameters.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RuleName,
            this.Valid,
            this.Active,
            this.BaseLearner});
            this.uiParameters.Location = new System.Drawing.Point(1, 67);
            this.uiParameters.Name = "uiParameters";
            this.uiParameters.Size = new System.Drawing.Size(959, 556);
            this.uiParameters.TabIndex = 5;
            // 
            // RuleName
            // 
            this.RuleName.HeaderText = "Rule Name";
            this.RuleName.Name = "RuleName";
            this.RuleName.Width = 150;
            // 
            // Valid
            // 
            this.Valid.HeaderText = "Valid";
            this.Valid.Name = "Valid";
            this.Valid.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Valid.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Active
            // 
            this.Active.HeaderText = "Active";
            this.Active.Name = "Active";
            this.Active.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Active.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // BaseLearner
            // 
            this.BaseLearner.HeaderText = "Learner";
            this.BaseLearner.Name = "BaseLearner";
            this.BaseLearner.Width = 300;
            // 
            // uiRuleData
            // 
            this.uiRuleData.AutoSize = true;
            this.uiRuleData.Location = new System.Drawing.Point(586, 10);
            this.uiRuleData.Name = "uiRuleData";
            this.uiRuleData.Size = new System.Drawing.Size(35, 13);
            this.uiRuleData.TabIndex = 6;
            this.uiRuleData.Text = "label3";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(352, 10);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Multiplication";
            // 
            // uiMultiplication
            // 
            this.uiMultiplication.Location = new System.Drawing.Point(430, 9);
            this.uiMultiplication.Margin = new System.Windows.Forms.Padding(2);
            this.uiMultiplication.Name = "uiMultiplication";
            this.uiMultiplication.Size = new System.Drawing.Size(52, 20);
            this.uiMultiplication.TabIndex = 8;
            this.uiMultiplication.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "namespace";
            // 
            // fileNamespace
            // 
            this.fileNamespace.FormattingEnabled = true;
            this.fileNamespace.Items.AddRange(new object[] {
            "ESFA/ILR/2018-19",
            "SFA/ILR/2017-18"});
            this.fileNamespace.Location = new System.Drawing.Point(81, 36);
            this.fileNamespace.Name = "fileNamespace";
            this.fileNamespace.Size = new System.Drawing.Size(121, 21);
            this.fileNamespace.TabIndex = 10;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(249, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Transform XDS";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 593);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.fileNamespace);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.uiMultiplication);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.uiRuleData);
            this.Controls.Add(this.uiParameters);
            this.Controls.Add(this.uiOuputFile);
            this.Controls.Add(this.uiYear);
            this.Controls.Add(this.uiUKPRN);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "ILR Test Data Generator";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uiParameters)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox uiUKPRN;
        private System.Windows.Forms.TextBox uiYear;
        private System.Windows.Forms.Button uiOuputFile;
        private System.Windows.Forms.DataGridView uiParameters;
        private System.Windows.Forms.DataGridViewTextBoxColumn RuleName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Valid;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Active;
        private System.Windows.Forms.DataGridViewTextBoxColumn BaseLearner;
        private System.Windows.Forms.Label uiRuleData;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox uiMultiplication;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox fileNamespace;
        private System.Windows.Forms.Button button1;
    }
}

