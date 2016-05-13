namespace GenAlgorithm
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
            this.label1 = new System.Windows.Forms.Label();
            this.run = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.crossoverBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.numberOfPopulationBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.mutationBox = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.selectionBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.numberOfIterationBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.bettaBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.maxWeightBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.report = new System.Windows.Forms.Button();
            this.startsNumber = new System.Windows.Forms.TextBox();
            this.reportLabel = new System.Windows.Forms.Label();
            this.dataInstancesBox = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // startPopulBox
            // 
            this.startPopulBox.FormattingEnabled = true;
            this.startPopulBox.Items.AddRange(new object[] {
            "Алгоритм Данцига",
            "Жадный алгоритм",
            "Случайный алгоритм"});
            this.startPopulBox.Location = new System.Drawing.Point(151, 25);
            this.startPopulBox.Name = "startPopulBox";
            this.startPopulBox.Size = new System.Drawing.Size(159, 21);
            this.startPopulBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Начальная популяция";
            // 
            // run
            // 
            this.run.Location = new System.Drawing.Point(665, 233);
            this.run.Name = "run";
            this.run.Size = new System.Drawing.Size(100, 75);
            this.run.TabIndex = 3;
            this.run.Text = "Запустить генетический алгоритм";
            this.run.UseVisualStyleBackColor = true;
            this.run.Click += new System.EventHandler(this.runClick);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(21, 153);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(243, 349);
            this.textBox1.TabIndex = 4;
            // 
            // crossoverBox
            // 
            this.crossoverBox.FormattingEnabled = true;
            this.crossoverBox.Items.AddRange(new object[] {
            "Одноточечный кроссовер",
            "Двуточечный кроссовер",
            "Однородный кроссовер"});
            this.crossoverBox.Location = new System.Drawing.Point(151, 53);
            this.crossoverBox.Name = "crossoverBox";
            this.crossoverBox.Size = new System.Drawing.Size(159, 21);
            this.crossoverBox.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Кроссовер";
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
            this.numberOfPopulationBox.Location = new System.Drawing.Point(664, 28);
            this.numberOfPopulationBox.Name = "numberOfPopulationBox";
            this.numberOfPopulationBox.Size = new System.Drawing.Size(100, 20);
            this.numberOfPopulationBox.TabIndex = 9;
            this.numberOfPopulationBox.Text = "5";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(477, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(175, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Введите численность популяции:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Мутация";
            // 
            // mutationBox
            // 
            this.mutationBox.FormattingEnabled = true;
            this.mutationBox.Items.AddRange(new object[] {
            "Точечная мутация",
            "Инверсия",
            "Сальтация",
            "Транслакация"});
            this.mutationBox.Location = new System.Drawing.Point(151, 80);
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
            "Бетта-Турнир",
            "Линейная-ранговая"});
            this.selectionBox.Location = new System.Drawing.Point(151, 107);
            this.selectionBox.Name = "selectionBox";
            this.selectionBox.Size = new System.Drawing.Size(159, 21);
            this.selectionBox.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(33, 115);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Селекция";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(0, 0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 0;
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox7.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox7.Location = new System.Drawing.Point(270, 153);
            this.textBox7.Multiline = true;
            this.textBox7.Name = "textBox7";
            this.textBox7.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox7.Size = new System.Drawing.Size(245, 349);
            this.textBox7.TabIndex = 22;
            // 
            // numberOfIterationBox
            // 
            this.numberOfIterationBox.Location = new System.Drawing.Point(664, 57);
            this.numberOfIterationBox.Name = "numberOfIterationBox";
            this.numberOfIterationBox.Size = new System.Drawing.Size(100, 20);
            this.numberOfIterationBox.TabIndex = 23;
            this.numberOfIterationBox.Text = "5";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(488, 60);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(163, 13);
            this.label8.TabIndex = 24;
            this.label8.Text = "Введите количество итераций:";
            // 
            // bettaBox
            // 
            this.bettaBox.Location = new System.Drawing.Point(341, 108);
            this.bettaBox.Name = "bettaBox";
            this.bettaBox.Size = new System.Drawing.Size(73, 20);
            this.bettaBox.TabIndex = 25;
            this.bettaBox.Text = "2";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(316, 115);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(19, 13);
            this.label9.TabIndex = 26;
            this.label9.Text = "β :";
            // 
            // maxWeightBox
            // 
            this.maxWeightBox.Location = new System.Drawing.Point(664, 83);
            this.maxWeightBox.Name = "maxWeightBox";
            this.maxWeightBox.Size = new System.Drawing.Size(100, 20);
            this.maxWeightBox.TabIndex = 27;
            this.maxWeightBox.Text = "80";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(496, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(155, 13);
            this.label4.TabIndex = 28;
            this.label4.Text = "Введите ограничение на вес:";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(536, 153);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(56, 349);
            this.richTextBox1.TabIndex = 29;
            this.richTextBox1.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(405, 131);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(180, 13);
            this.label6.TabIndex = 30;
            this.label6.Text = "Максимальная цена в поколении:";
            // 
            // report
            // 
            this.report.Location = new System.Drawing.Point(664, 428);
            this.report.Name = "report";
            this.report.Size = new System.Drawing.Size(100, 60);
            this.report.TabIndex = 31;
            this.report.Text = "создать отчет";
            this.report.UseVisualStyleBackColor = true;
            this.report.Click += new System.EventHandler(this.report_Click);
            // 
            // startsNumber
            // 
            this.startsNumber.Location = new System.Drawing.Point(664, 390);
            this.startsNumber.Name = "startsNumber";
            this.startsNumber.Size = new System.Drawing.Size(100, 20);
            this.startsNumber.TabIndex = 32;
            this.startsNumber.Text = "2";
            // 
            // reportLabel
            // 
            this.reportLabel.AllowDrop = true;
            this.reportLabel.Location = new System.Drawing.Point(662, 331);
            this.reportLabel.Name = "reportLabel";
            this.reportLabel.Size = new System.Drawing.Size(102, 43);
            this.reportLabel.TabIndex = 34;
            this.reportLabel.Text = "Колличество запусков генного алгоритма:";
            // 
            // dataInstancesBox
            // 
            this.dataInstancesBox.FormattingEnabled = true;
            this.dataInstancesBox.Items.AddRange(new object[] {
            "Без корреляции",
            "Слабая корреляция",
            "Сильная корреляция",
            "Подсуммы"});
            this.dataInstancesBox.Location = new System.Drawing.Point(665, 169);
            this.dataInstancesBox.Name = "dataInstancesBox";
            this.dataInstancesBox.Size = new System.Drawing.Size(131, 21);
            this.dataInstancesBox.TabIndex = 35;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(662, 153);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 13);
            this.label10.TabIndex = 36;
            this.label10.Text = "Класс задач:";
            // 
            // GenAlgorithmView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(807, 536);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.dataInstancesBox);
            this.Controls.Add(this.reportLabel);
            this.Controls.Add(this.startsNumber);
            this.Controls.Add(this.report);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.maxWeightBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.bettaBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.numberOfIterationBox);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.selectionBox);
            this.Controls.Add(this.mutationBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numberOfPopulationBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.crossoverBox);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.run);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.startPopulBox);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "GenAlgorithmView";
            this.Text = "Генетический алгоритм";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox startPopulBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button run;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox crossoverBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox numberOfPopulationBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox mutationBox;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox selectionBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox numberOfIterationBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox bettaBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox maxWeightBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button report;
        private System.Windows.Forms.TextBox startsNumber;
        private System.Windows.Forms.Label reportLabel;
        private System.Windows.Forms.ComboBox dataInstancesBox;
        private System.Windows.Forms.Label label10;

 
    }
}

