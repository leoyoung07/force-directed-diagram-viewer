namespace ForceDirectedDiagramViewer
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
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.labelSeed = new System.Windows.Forms.Label();
            this.panelNodesDiagram = new System.Windows.Forms.Panel();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.panelNodePairsDiagram = new System.Windows.Forms.Panel();
            this.labelNodes = new System.Windows.Forms.Label();
            this.labelNodePairs = new System.Windows.Forms.Label();
            this.labelSpringLength = new System.Windows.Forms.Label();
            this.labelConnectionLengthValue = new System.Windows.Forms.Label();
            this.comboBoxChoice = new System.Windows.Forms.ComboBox();
            this.labelSample = new System.Windows.Forms.Label();
            this.numericUpDownMinimumDisplacement = new System.Windows.Forms.NumericUpDown();
            this.labelMinimumDisplacement = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinimumDisplacement)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(1133, 659);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(75, 23);
            this.buttonGenerate.TabIndex = 0;
            this.buttonGenerate.Text = "Generate";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelSeed
            // 
            this.labelSeed.AutoSize = true;
            this.labelSeed.Location = new System.Drawing.Point(613, 664);
            this.labelSeed.Name = "labelSeed";
            this.labelSeed.Size = new System.Drawing.Size(35, 13);
            this.labelSeed.TabIndex = 2;
            this.labelSeed.Text = "Seed:";
            // 
            // panelNodesDiagram
            // 
            this.panelNodesDiagram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNodesDiagram.Location = new System.Drawing.Point(12, 27);
            this.panelNodesDiagram.Name = "panelNodesDiagram";
            this.panelNodesDiagram.Size = new System.Drawing.Size(595, 619);
            this.panelNodesDiagram.TabIndex = 7;
            this.panelNodesDiagram.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(94, 659);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(513, 45);
            this.trackBar1.TabIndex = 9;
            this.trackBar1.Value = 5;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // panelNodePairsDiagram
            // 
            this.panelNodePairsDiagram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNodePairsDiagram.Location = new System.Drawing.Point(613, 27);
            this.panelNodePairsDiagram.Name = "panelNodePairsDiagram";
            this.panelNodePairsDiagram.Size = new System.Drawing.Size(595, 619);
            this.panelNodePairsDiagram.TabIndex = 8;
            this.panelNodePairsDiagram.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // labelNodes
            // 
            this.labelNodes.AutoSize = true;
            this.labelNodes.Location = new System.Drawing.Point(12, 11);
            this.labelNodes.Name = "labelNodes";
            this.labelNodes.Size = new System.Drawing.Size(84, 13);
            this.labelNodes.TabIndex = 11;
            this.labelNodes.Text = "Nodes Algorithm";
            // 
            // labelNodePairs
            // 
            this.labelNodePairs.AutoSize = true;
            this.labelNodePairs.Location = new System.Drawing.Point(613, 11);
            this.labelNodePairs.Name = "labelNodePairs";
            this.labelNodePairs.Size = new System.Drawing.Size(105, 13);
            this.labelNodePairs.TabIndex = 13;
            this.labelNodePairs.Text = "Node Pairs Algorithm";
            // 
            // labelSpringLength
            // 
            this.labelSpringLength.AutoSize = true;
            this.labelSpringLength.Location = new System.Drawing.Point(12, 664);
            this.labelSpringLength.Name = "labelSpringLength";
            this.labelSpringLength.Size = new System.Drawing.Size(76, 13);
            this.labelSpringLength.TabIndex = 15;
            this.labelSpringLength.Text = "Spring Length:";
            // 
            // labelConnectionLengthValue
            // 
            this.labelConnectionLengthValue.AutoSize = true;
            this.labelConnectionLengthValue.Location = new System.Drawing.Point(354, 678);
            this.labelConnectionLengthValue.Name = "labelConnectionLengthValue";
            this.labelConnectionLengthValue.Size = new System.Drawing.Size(13, 13);
            this.labelConnectionLengthValue.TabIndex = 16;
            this.labelConnectionLengthValue.Text = "5";
            this.labelConnectionLengthValue.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // comboBoxChoice
            // 
            this.comboBoxChoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxChoice.FormattingEnabled = true;
            this.comboBoxChoice.Items.AddRange(new object[] {
            "Peers",
            "Snowflake",
            "Structure"});
            this.comboBoxChoice.Location = new System.Drawing.Point(980, 660);
            this.comboBoxChoice.Name = "comboBoxChoice";
            this.comboBoxChoice.Size = new System.Drawing.Size(121, 21);
            this.comboBoxChoice.TabIndex = 17;
            // 
            // labelSample
            // 
            this.labelSample.AutoSize = true;
            this.labelSample.Location = new System.Drawing.Point(929, 664);
            this.labelSample.Name = "labelSample";
            this.labelSample.Size = new System.Drawing.Size(45, 13);
            this.labelSample.TabIndex = 20;
            this.labelSample.Text = "Sample:";
            // 
            // numericUpDownMinimumDisplacement
            // 
            this.numericUpDownMinimumDisplacement.Location = new System.Drawing.Point(863, 661);
            this.numericUpDownMinimumDisplacement.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numericUpDownMinimumDisplacement.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMinimumDisplacement.Name = "numericUpDownMinimumDisplacement";
            this.numericUpDownMinimumDisplacement.ReadOnly = true;
            this.numericUpDownMinimumDisplacement.Size = new System.Drawing.Size(41, 20);
            this.numericUpDownMinimumDisplacement.TabIndex = 21;
            this.numericUpDownMinimumDisplacement.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownMinimumDisplacement.ValueChanged += new System.EventHandler(this.numericUpDownMinimumDisplacement_ValueChanged);
            // 
            // labelMinimumDisplacement
            // 
            this.labelMinimumDisplacement.AutoSize = true;
            this.labelMinimumDisplacement.Location = new System.Drawing.Point(739, 663);
            this.labelMinimumDisplacement.Name = "labelMinimumDisplacement";
            this.labelMinimumDisplacement.Size = new System.Drawing.Size(118, 13);
            this.labelMinimumDisplacement.TabIndex = 22;
            this.labelMinimumDisplacement.Text = "Minimum Displacement:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1221, 697);
            this.Controls.Add(this.labelMinimumDisplacement);
            this.Controls.Add(this.numericUpDownMinimumDisplacement);
            this.Controls.Add(this.labelSample);
            this.Controls.Add(this.comboBoxChoice);
            this.Controls.Add(this.labelNodePairs);
            this.Controls.Add(this.labelNodes);
            this.Controls.Add(this.labelConnectionLengthValue);
            this.Controls.Add(this.labelSpringLength);
            this.Controls.Add(this.panelNodePairsDiagram);
            this.Controls.Add(this.panelNodesDiagram);
            this.Controls.Add(this.labelSeed);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.trackBar1);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Force Directed Diagram Viewer";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinimumDisplacement)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.Label labelSeed;
        private System.Windows.Forms.Panel panelNodesDiagram;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Panel panelNodePairsDiagram;
        private System.Windows.Forms.Label labelNodes;
        private System.Windows.Forms.Label labelNodePairs;
        private System.Windows.Forms.Label labelSpringLength;
        private System.Windows.Forms.Label labelConnectionLengthValue;
        private System.Windows.Forms.ComboBox comboBoxChoice;
        private System.Windows.Forms.Label labelSample;
        private System.Windows.Forms.NumericUpDown numericUpDownMinimumDisplacement;
        private System.Windows.Forms.Label labelMinimumDisplacement;
    }
}

