﻿namespace GenAlgorithm
{
    partial class GenAlgorithmView
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
            this.startPopulBox = new System.Windows.Forms.ComboBox();
            this.initialPopulationLabel = new System.Windows.Forms.Label();
            this.run = new System.Windows.Forms.Button();
            this.InitialPopulation = new System.Windows.Forms.TextBox();
            this.crossoverBox = new System.Windows.Forms.ComboBox();
            this.crossoverLabel = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.numberOfPopulationBox = new System.Windows.Forms.TextBox();
            this.populationCountLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.mutationBox = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.selectionBox = new System.Windows.Forms.ComboBox();
            this.selectionLabel = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.numberOfIterationBox = new System.Windows.Forms.TextBox();
            this.iterationCountLabel = new System.Windows.Forms.Label();
            this.bettaBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.maxCostLabel = new System.Windows.Forms.Label();
            this.report = new System.Windows.Forms.Button();
            this.startsNumberBox = new System.Windows.Forms.TextBox();
            this.reportLabel = new System.Windows.Forms.Label();
            this.dataInstancesBox = new System.Windows.Forms.ComboBox();
            this.dataInstancesLabel = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.mutationLabel = new System.Windows.Forms.Label();
            this.InitialPopulLabel2 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DataLabel = new System.Windows.Forms.Label();
            this.MaxCost = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // startPopulBox
            // 
            this.startPopulBox.FormattingEnabled = true;
            this.startPopulBox.Items.AddRange(new object[] {
            "Danzig algorithm",
            "Random algorithm"});
            this.startPopulBox.Location = new System.Drawing.Point(151, 74);
            this.startPopulBox.Name = "startPopulBox";
            this.startPopulBox.Size = new System.Drawing.Size(159, 21);
            this.startPopulBox.TabIndex = 0;
            // 
            // initialPopulationLabel
            // 
            this.initialPopulationLabel.AutoSize = true;
            this.initialPopulationLabel.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.initialPopulationLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.initialPopulationLabel.Location = new System.Drawing.Point(27, 77);
            this.initialPopulationLabel.Name = "initialPopulationLabel";
            this.initialPopulationLabel.Size = new System.Drawing.Size(105, 18);
            this.initialPopulationLabel.TabIndex = 1;
            this.initialPopulationLabel.Text = "Initial population:";
            // 
            // run
            // 
            this.run.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.run.Location = new System.Drawing.Point(578, 104);
            this.run.Name = "run";
            this.run.Size = new System.Drawing.Size(100, 49);
            this.run.TabIndex = 3;
            this.run.Text = "Run ";
            this.run.UseVisualStyleBackColor = true;
            this.run.Click += new System.EventHandler(this.runClick);
            // 
            // InitialPopulation
            // 
            this.InitialPopulation.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.InitialPopulation.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.InitialPopulation.Location = new System.Drawing.Point(30, 322);
            this.InitialPopulation.Multiline = true;
            this.InitialPopulation.Name = "InitialPopulation";
            this.InitialPopulation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.InitialPopulation.Size = new System.Drawing.Size(225, 410);
            this.InitialPopulation.TabIndex = 4;
            // 
            // crossoverBox
            // 
            this.crossoverBox.FormattingEnabled = true;
            this.crossoverBox.Items.AddRange(new object[] {
            "Single-point crossover",
            "Two-point crossover",
            "Uniform crossover"});
            this.crossoverBox.Location = new System.Drawing.Point(151, 101);
            this.crossoverBox.Name = "crossoverBox";
            this.crossoverBox.Size = new System.Drawing.Size(159, 21);
            this.crossoverBox.TabIndex = 5;
            // 
            // crossoverLabel
            // 
            this.crossoverLabel.AutoSize = true;
            this.crossoverLabel.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.crossoverLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.crossoverLabel.Location = new System.Drawing.Point(27, 104);
            this.crossoverLabel.Name = "crossoverLabel";
            this.crossoverLabel.Size = new System.Drawing.Size(71, 18);
            this.crossoverLabel.TabIndex = 6;
            this.crossoverLabel.Text = "Crossover:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(0, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            // 
            // numberOfPopulationBox
            // 
            this.numberOfPopulationBox.Location = new System.Drawing.Point(578, 32);
            this.numberOfPopulationBox.Name = "numberOfPopulationBox";
            this.numberOfPopulationBox.Size = new System.Drawing.Size(100, 20);
            this.numberOfPopulationBox.TabIndex = 9;
            this.numberOfPopulationBox.Text = "5";
            // 
            // populationCountLabel
            // 
            this.populationCountLabel.AutoSize = true;
            this.populationCountLabel.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.populationCountLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.populationCountLabel.Location = new System.Drawing.Point(431, 35);
            this.populationCountLabel.Name = "populationCountLabel";
            this.populationCountLabel.Size = new System.Drawing.Size(109, 18);
            this.populationCountLabel.TabIndex = 10;
            this.populationCountLabel.Text = "Population count:";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 23);
            this.label5.TabIndex = 38;
            // 
            // mutationBox
            // 
            this.mutationBox.FormattingEnabled = true;
            this.mutationBox.Items.AddRange(new object[] {
            "Point mutation",
            "Inversion",
            "Translocation",
            "Saltation"});
            this.mutationBox.Location = new System.Drawing.Point(151, 129);
            this.mutationBox.Name = "mutationBox";
            this.mutationBox.Size = new System.Drawing.Size(159, 21);
            this.mutationBox.TabIndex = 14;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(0, 0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 0;
            // 
            // selectionBox
            // 
            this.selectionBox.FormattingEnabled = true;
            this.selectionBox.Items.AddRange(new object[] {
            "Betta-Tournament",
            "Linear-rank"});
            this.selectionBox.Location = new System.Drawing.Point(151, 156);
            this.selectionBox.Name = "selectionBox";
            this.selectionBox.Size = new System.Drawing.Size(159, 21);
            this.selectionBox.TabIndex = 19;
            // 
            // selectionLabel
            // 
            this.selectionLabel.AutoSize = true;
            this.selectionLabel.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.selectionLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.selectionLabel.Location = new System.Drawing.Point(27, 159);
            this.selectionLabel.Name = "selectionLabel";
            this.selectionLabel.Size = new System.Drawing.Size(66, 18);
            this.selectionLabel.TabIndex = 20;
            this.selectionLabel.Text = "Selection:";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(0, 0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 0;
            // 
            // numberOfIterationBox
            // 
            this.numberOfIterationBox.Location = new System.Drawing.Point(578, 59);
            this.numberOfIterationBox.Name = "numberOfIterationBox";
            this.numberOfIterationBox.Size = new System.Drawing.Size(100, 20);
            this.numberOfIterationBox.TabIndex = 23;
            this.numberOfIterationBox.Text = "5";
            // 
            // iterationCountLabel
            // 
            this.iterationCountLabel.AutoSize = true;
            this.iterationCountLabel.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.iterationCountLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.iterationCountLabel.Location = new System.Drawing.Point(431, 59);
            this.iterationCountLabel.Name = "iterationCountLabel";
            this.iterationCountLabel.Size = new System.Drawing.Size(94, 18);
            this.iterationCountLabel.TabIndex = 24;
            this.iterationCountLabel.Text = "Iteration count:";
            // 
            // bettaBox
            // 
            this.bettaBox.Location = new System.Drawing.Point(346, 157);
            this.bettaBox.Name = "bettaBox";
            this.bettaBox.Size = new System.Drawing.Size(73, 20);
            this.bettaBox.TabIndex = 25;
            this.bettaBox.Text = "2";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label9.Location = new System.Drawing.Point(317, 159);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 18);
            this.label9.TabIndex = 26;
            this.label9.Text = "β :";
            // 
            // maxCostLabel
            // 
            this.maxCostLabel.AutoSize = true;
            this.maxCostLabel.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.maxCostLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.maxCostLabel.Location = new System.Drawing.Point(188, 296);
            this.maxCostLabel.Name = "maxCostLabel";
            this.maxCostLabel.Size = new System.Drawing.Size(199, 23);
            this.maxCostLabel.TabIndex = 30;
            this.maxCostLabel.Text = "The highest cost in a generation:";
            this.maxCostLabel.UseCompatibleTextRendering = true;
            // 
            // report
            // 
            this.report.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.report.Location = new System.Drawing.Point(579, 322);
            this.report.Name = "report";
            this.report.Size = new System.Drawing.Size(100, 60);
            this.report.TabIndex = 31;
            this.report.Text = "Create reports";
            this.report.UseVisualStyleBackColor = true;
            this.report.Click += new System.EventHandler(this.reportClick);
            // 
            // startsNumberBox
            // 
            this.startsNumberBox.Location = new System.Drawing.Point(578, 250);
            this.startsNumberBox.Name = "startsNumberBox";
            this.startsNumberBox.Size = new System.Drawing.Size(100, 20);
            this.startsNumberBox.TabIndex = 32;
            this.startsNumberBox.Text = "2";
            // 
            // reportLabel
            // 
            this.reportLabel.AllowDrop = true;
            this.reportLabel.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.reportLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.reportLabel.Location = new System.Drawing.Point(576, 180);
            this.reportLabel.Name = "reportLabel";
            this.reportLabel.Size = new System.Drawing.Size(102, 45);
            this.reportLabel.TabIndex = 34;
            this.reportLabel.Text = "Number of runs of the algorithm:";
            // 
            // dataInstancesBox
            // 
            this.dataInstancesBox.FormattingEnabled = true;
            this.dataInstancesBox.Items.AddRange(new object[] {
            "Test",
            "No correlation",
            "The weak correlation",
            "The strong correlation",
            "Subtotals"});
            this.dataInstancesBox.Location = new System.Drawing.Point(151, 32);
            this.dataInstancesBox.Name = "dataInstancesBox";
            this.dataInstancesBox.Size = new System.Drawing.Size(159, 21);
            this.dataInstancesBox.TabIndex = 35;
            // 
            // dataInstancesLabel
            // 
            this.dataInstancesLabel.AutoSize = true;
            this.dataInstancesLabel.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataInstancesLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.dataInstancesLabel.Location = new System.Drawing.Point(27, 35);
            this.dataInstancesLabel.Name = "dataInstancesLabel";
            this.dataInstancesLabel.Size = new System.Drawing.Size(99, 18);
            this.dataInstancesLabel.TabIndex = 36;
            this.dataInstancesLabel.Text = "Data instances:";
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox4.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox4.Location = new System.Drawing.Point(30, 213);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(494, 57);
            this.textBox4.TabIndex = 37;
            // 
            // mutationLabel
            // 
            this.mutationLabel.AutoSize = true;
            this.mutationLabel.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mutationLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.mutationLabel.Location = new System.Drawing.Point(27, 132);
            this.mutationLabel.Name = "mutationLabel";
            this.mutationLabel.Size = new System.Drawing.Size(62, 18);
            this.mutationLabel.TabIndex = 39;
            this.mutationLabel.Text = "Mutation:";
            // 
            // InitialPopulLabel2
            // 
            this.InitialPopulLabel2.AutoSize = true;
            this.InitialPopulLabel2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.InitialPopulLabel2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.InitialPopulLabel2.Location = new System.Drawing.Point(27, 296);
            this.InitialPopulLabel2.Name = "InitialPopulLabel2";
            this.InitialPopulLabel2.Size = new System.Drawing.Size(105, 18);
            this.InitialPopulLabel2.TabIndex = 40;
            this.InitialPopulLabel2.Text = "Initial population:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 0;
            // 
            // DataLabel
            // 
            this.DataLabel.AutoSize = true;
            this.DataLabel.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DataLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.DataLabel.Location = new System.Drawing.Point(27, 192);
            this.DataLabel.Name = "DataLabel";
            this.DataLabel.Size = new System.Drawing.Size(39, 18);
            this.DataLabel.TabIndex = 42;
            this.DataLabel.Text = "Data:";
            // 
            // MaxCost
            // 
            this.MaxCost.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.MaxCost.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MaxCost.Location = new System.Drawing.Point(296, 322);
            this.MaxCost.Multiline = true;
            this.MaxCost.Name = "MaxCost";
            this.MaxCost.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MaxCost.Size = new System.Drawing.Size(91, 410);
            this.MaxCost.TabIndex = 43;
            // 
            // GenAlgorithmView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(707, 744);
            this.Controls.Add(this.MaxCost);
            this.Controls.Add(this.DataLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.InitialPopulLabel2);
            this.Controls.Add(this.mutationLabel);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.dataInstancesLabel);
            this.Controls.Add(this.dataInstancesBox);
            this.Controls.Add(this.reportLabel);
            this.Controls.Add(this.startsNumberBox);
            this.Controls.Add(this.report);
            this.Controls.Add(this.maxCostLabel);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.bettaBox);
            this.Controls.Add(this.iterationCountLabel);
            this.Controls.Add(this.numberOfIterationBox);
            this.Controls.Add(this.selectionLabel);
            this.Controls.Add(this.selectionBox);
            this.Controls.Add(this.mutationBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.populationCountLabel);
            this.Controls.Add(this.numberOfPopulationBox);
            this.Controls.Add(this.crossoverLabel);
            this.Controls.Add(this.crossoverBox);
            this.Controls.Add(this.InitialPopulation);
            this.Controls.Add(this.run);
            this.Controls.Add(this.initialPopulationLabel);
            this.Controls.Add(this.startPopulBox);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "GenAlgorithmView";
            this.Text = "Genetic algorithm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox startPopulBox;
        private System.Windows.Forms.Label initialPopulationLabel;
        private System.Windows.Forms.Button run;
        private System.Windows.Forms.TextBox InitialPopulation;
        private System.Windows.Forms.ComboBox crossoverBox;
        private System.Windows.Forms.Label crossoverLabel;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox numberOfPopulationBox;
        private System.Windows.Forms.Label populationCountLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox mutationBox;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox selectionBox;
        private System.Windows.Forms.Label selectionLabel;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox numberOfIterationBox;
        private System.Windows.Forms.Label iterationCountLabel;
        private System.Windows.Forms.TextBox bettaBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label maxCostLabel;
        private System.Windows.Forms.Button report;
        private System.Windows.Forms.TextBox startsNumberBox;
        private System.Windows.Forms.Label reportLabel;
        private System.Windows.Forms.ComboBox dataInstancesBox;
        private System.Windows.Forms.Label dataInstancesLabel;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label mutationLabel;
        private System.Windows.Forms.Label InitialPopulLabel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label DataLabel;
        private System.Windows.Forms.TextBox MaxCost;

 
    }
}

