namespace NarcoFeeder
{
    partial class frmMain
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
            this.feedDelayText = new System.Windows.Forms.TextBox();
            this.feedDelayLabel = new System.Windows.Forms.Label();
            this.selectorLabel = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.typeSelector = new System.Windows.Forms.ComboBox();
            this.typeSelector1 = new System.Windows.Forms.ComboBox();
            this.feedDelay1 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // feedDelayText
            // 
            this.feedDelayText.Location = new System.Drawing.Point(160, 26);
            this.feedDelayText.Name = "feedDelayText";
            this.feedDelayText.Size = new System.Drawing.Size(100, 20);
            this.feedDelayText.TabIndex = 1;
            this.feedDelayText.TextChanged += new System.EventHandler(this.feedDelayText_TextChanged);
            // 
            // feedDelayLabel
            // 
            this.feedDelayLabel.AutoSize = true;
            this.feedDelayLabel.Location = new System.Drawing.Point(179, 9);
            this.feedDelayLabel.Name = "feedDelayLabel";
            this.feedDelayLabel.Size = new System.Drawing.Size(56, 13);
            this.feedDelayLabel.TabIndex = 2;
            this.feedDelayLabel.Text = "Period(ms)";
            // 
            // selectorLabel
            // 
            this.selectorLabel.AutoSize = true;
            this.selectorLabel.Location = new System.Drawing.Point(55, 9);
            this.selectorLabel.Name = "selectorLabel";
            this.selectorLabel.Size = new System.Drawing.Size(31, 13);
            this.selectorLabel.TabIndex = 5;
            this.selectorLabel.Text = "Type";
            // 
            // statusLabel
            // 
            this.statusLabel.Location = new System.Drawing.Point(12, 97);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(260, 19);
            this.statusLabel.TabIndex = 6;
            this.statusLabel.Text = "Feeder switched off.";
            // 
            // typeSelector
            // 
            this.typeSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeSelector.FormattingEnabled = true;
            this.typeSelector.Items.AddRange(new object[] {
            "Idle",
            "Feed Narco",
            "LeftClick",
            "LeftDoubleClick",
            "PressTButton",
            "Press0Button",
            "PressEButton",
            "PressoButton",
            "RightClick"});
            this.typeSelector.Location = new System.Drawing.Point(15, 25);
            this.typeSelector.Name = "typeSelector";
            this.typeSelector.Size = new System.Drawing.Size(139, 21);
            this.typeSelector.TabIndex = 7;
            // 
            // typeSelector1
            // 
            this.typeSelector1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeSelector1.FormattingEnabled = true;
            this.typeSelector1.Items.AddRange(new object[] {
            "Idle",
            "Press"});
            this.typeSelector1.Location = new System.Drawing.Point(15, 52);
            this.typeSelector1.Name = "typeSelector1";
            this.typeSelector1.Size = new System.Drawing.Size(80, 21);
            this.typeSelector1.TabIndex = 8;
            // 
            // feedDelay1
            // 
            this.feedDelay1.Location = new System.Drawing.Point(160, 52);
            this.feedDelay1.Name = "feedDelay1";
            this.feedDelay1.Size = new System.Drawing.Size(100, 20);
            this.feedDelay1.TabIndex = 9;
            this.feedDelay1.TextChanged += new System.EventHandler(this.feedDelay1_TextChanged);

            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(101, 53);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(53, 20);
            this.textBox1.TabIndex = 10;
            this.textBox1.Text = "0";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 116);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.feedDelay1);
            this.Controls.Add(this.typeSelector1);
            this.Controls.Add(this.typeSelector);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.selectorLabel);
            this.Controls.Add(this.feedDelayLabel);
            this.Controls.Add(this.feedDelayText);
            this.Name = "frmMain";
            this.Text = "NarcoFeeder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox feedDelayText;
        private System.Windows.Forms.Label feedDelayLabel;
        private System.Windows.Forms.Label selectorLabel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.ComboBox typeSelector;
        private System.Windows.Forms.ComboBox typeSelector1;
        private System.Windows.Forms.TextBox feedDelay1;
        private System.Windows.Forms.TextBox textBox1;
    }
}

