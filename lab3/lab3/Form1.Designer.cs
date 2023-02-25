namespace Lab3
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.сделатьСнимокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.остановитьПоискToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.maxProcCntTextBox = new System.Windows.Forms.TextBox();
            this.StateLabel = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.processesListBox = new System.Windows.Forms.ListBox();
            this.answerListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.maxHeapsCntTextBox = new System.Windows.Forms.TextBox();
            this.maxBlocksCntTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сделатьСнимокToolStripMenuItem,
            this.остановитьПоискToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1000, 29);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // сделатьСнимокToolStripMenuItem
            // 
            this.сделатьСнимокToolStripMenuItem.Name = "сделатьСнимокToolStripMenuItem";
            this.сделатьСнимокToolStripMenuItem.Size = new System.Drawing.Size(140, 25);
            this.сделатьСнимокToolStripMenuItem.Text = "Запустить поиск";
            this.сделатьСнимокToolStripMenuItem.Click += new System.EventHandler(this.сделатьСнимокToolStripMenuItem_Click);
            // 
            // остановитьПоискToolStripMenuItem
            // 
            this.остановитьПоискToolStripMenuItem.Name = "остановитьПоискToolStripMenuItem";
            this.остановитьПоискToolStripMenuItem.Size = new System.Drawing.Size(152, 25);
            this.остановитьПоискToolStripMenuItem.Text = "Остановить поиск";
            this.остановитьПоискToolStripMenuItem.Click += new System.EventHandler(this.остановитьПоискToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 29);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.maxBlocksCntTextBox);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.maxHeapsCntTextBox);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.maxProcCntTextBox);
            this.splitContainer1.Panel1.Controls.Add(this.StateLabel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1000, 467);
            this.splitContainer1.SplitterDistance = 56;
            this.splitContainer1.TabIndex = 1;
            // 
            // maxProcCntTextBox
            // 
            this.maxProcCntTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.maxProcCntTextBox.Location = new System.Drawing.Point(388, 9);
            this.maxProcCntTextBox.Name = "maxProcCntTextBox";
            this.maxProcCntTextBox.Size = new System.Drawing.Size(71, 26);
            this.maxProcCntTextBox.TabIndex = 4;
            this.maxProcCntTextBox.Text = "1000";
            // 
            // StateLabel
            // 
            this.StateLabel.AutoSize = true;
            this.StateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.StateLabel.Location = new System.Drawing.Point(12, 15);
            this.StateLabel.Name = "StateLabel";
            this.StateLabel.Size = new System.Drawing.Size(145, 20);
            this.StateLabel.TabIndex = 3;
            this.StateLabel.Text = "Поиск не запущен";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.processesListBox);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.answerListBox);
            this.splitContainer2.Size = new System.Drawing.Size(1000, 407);
            this.splitContainer2.SplitterDistance = 446;
            this.splitContainer2.TabIndex = 0;
            // 
            // processesListBox
            // 
            this.processesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.processesListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.processesListBox.FormattingEnabled = true;
            this.processesListBox.ItemHeight = 20;
            this.processesListBox.Location = new System.Drawing.Point(0, 0);
            this.processesListBox.Name = "processesListBox";
            this.processesListBox.Size = new System.Drawing.Size(446, 407);
            this.processesListBox.TabIndex = 1;
            // 
            // answerListBox
            // 
            this.answerListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.answerListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.answerListBox.FormattingEnabled = true;
            this.answerListBox.ItemHeight = 20;
            this.answerListBox.Location = new System.Drawing.Point(0, 0);
            this.answerListBox.Name = "answerListBox";
            this.answerListBox.Size = new System.Drawing.Size(550, 407);
            this.answerListBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(465, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Макс кол-во куч";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(198, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(184, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Макс кол-во процессов";
            // 
            // maxHeapsCntTextBox
            // 
            this.maxHeapsCntTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.maxHeapsCntTextBox.Location = new System.Drawing.Point(600, 9);
            this.maxHeapsCntTextBox.Name = "maxHeapsCntTextBox";
            this.maxHeapsCntTextBox.Size = new System.Drawing.Size(71, 26);
            this.maxHeapsCntTextBox.TabIndex = 8;
            this.maxHeapsCntTextBox.Text = "50";
            // 
            // maxBlocksCntTextBox
            // 
            this.maxBlocksCntTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.maxBlocksCntTextBox.Location = new System.Drawing.Point(842, 9);
            this.maxBlocksCntTextBox.Name = "maxBlocksCntTextBox";
            this.maxBlocksCntTextBox.Size = new System.Drawing.Size(71, 26);
            this.maxBlocksCntTextBox.TabIndex = 10;
            this.maxBlocksCntTextBox.Text = "100";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label3.Location = new System.Drawing.Point(677, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(159, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Макс кол-во блоков";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 496);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem сделатьСнимокToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox maxProcCntTextBox;
        private System.Windows.Forms.Label StateLabel;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListBox processesListBox;
        private System.Windows.Forms.ListBox answerListBox;
        private System.Windows.Forms.ToolStripMenuItem остановитьПоискToolStripMenuItem;
        private System.Windows.Forms.TextBox maxBlocksCntTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox maxHeapsCntTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

