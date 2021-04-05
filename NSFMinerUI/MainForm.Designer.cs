
namespace NSFMinerUI
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.TbMiner = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.BtnStopMonitor = new System.Windows.Forms.Button();
            this.BtnStartMonitor = new System.Windows.Forms.Button();
            this.TbMonitor = new System.Windows.Forms.RichTextBox();
            this.BtnReset = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.NumMemory = new System.Windows.Forms.NumericUpDown();
            this.TBMemory = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnApply = new System.Windows.Forms.Button();
            this.NumPower = new System.Windows.Forms.NumericUpDown();
            this.TBPower = new System.Windows.Forms.TrackBar();
            this.CBGpuList = new System.Windows.Forms.ComboBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumMemory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TBMemory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumPower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TBPower)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1039, 450);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.TbMiner);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1031, 422);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Miner";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // TbMiner
            // 
            this.TbMiner.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TbMiner.Location = new System.Drawing.Point(3, 3);
            this.TbMiner.Name = "TbMiner";
            this.TbMiner.ReadOnly = true;
            this.TbMiner.Size = new System.Drawing.Size(762, 178);
            this.TbMiner.TabIndex = 0;
            this.TbMiner.TabStop = false;
            this.TbMiner.Text = "";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.BtnStopMonitor);
            this.tabPage2.Controls.Add(this.BtnStartMonitor);
            this.tabPage2.Controls.Add(this.TbMonitor);
            this.tabPage2.Controls.Add(this.BtnReset);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.NumMemory);
            this.tabPage2.Controls.Add(this.TBMemory);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.BtnApply);
            this.tabPage2.Controls.Add(this.NumPower);
            this.tabPage2.Controls.Add(this.TBPower);
            this.tabPage2.Controls.Add(this.CBGpuList);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1031, 422);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Overclock";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // BtnStopMonitor
            // 
            this.BtnStopMonitor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnStopMonitor.Location = new System.Drawing.Point(867, 391);
            this.BtnStopMonitor.Name = "BtnStopMonitor";
            this.BtnStopMonitor.Size = new System.Drawing.Size(75, 23);
            this.BtnStopMonitor.TabIndex = 7;
            this.BtnStopMonitor.Text = "Stop";
            this.BtnStopMonitor.UseVisualStyleBackColor = true;
            this.BtnStopMonitor.Click += new System.EventHandler(this.BtnStopMonitor_Click);
            // 
            // BtnStartMonitor
            // 
            this.BtnStartMonitor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnStartMonitor.Location = new System.Drawing.Point(948, 391);
            this.BtnStartMonitor.Name = "BtnStartMonitor";
            this.BtnStartMonitor.Size = new System.Drawing.Size(75, 23);
            this.BtnStartMonitor.TabIndex = 8;
            this.BtnStartMonitor.Text = "Start";
            this.BtnStartMonitor.UseVisualStyleBackColor = true;
            this.BtnStartMonitor.Click += new System.EventHandler(this.BtnStartMonitor_Click);
            // 
            // TbMonitor
            // 
            this.TbMonitor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TbMonitor.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TbMonitor.Location = new System.Drawing.Point(407, 4);
            this.TbMonitor.Name = "TbMonitor";
            this.TbMonitor.ReadOnly = true;
            this.TbMonitor.Size = new System.Drawing.Size(616, 381);
            this.TbMonitor.TabIndex = 9;
            this.TbMonitor.TabStop = false;
            this.TbMonitor.Text = "";
            // 
            // BtnReset
            // 
            this.BtnReset.Location = new System.Drawing.Point(191, 195);
            this.BtnReset.Name = "BtnReset";
            this.BtnReset.Size = new System.Drawing.Size(94, 29);
            this.BtnReset.TabIndex = 5;
            this.BtnReset.Text = "Reset";
            this.BtnReset.UseVisualStyleBackColor = true;
            this.BtnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "Memory Clock in MHz";
            // 
            // NumMemory
            // 
            this.NumMemory.Location = new System.Drawing.Point(291, 152);
            this.NumMemory.Name = "NumMemory";
            this.NumMemory.Size = new System.Drawing.Size(94, 23);
            this.NumMemory.TabIndex = 4;
            this.NumMemory.ValueChanged += new System.EventHandler(this.NumMemory_ValueChanged);
            // 
            // TBMemory
            // 
            this.TBMemory.LargeChange = 100;
            this.TBMemory.Location = new System.Drawing.Point(8, 144);
            this.TBMemory.Maximum = 1500;
            this.TBMemory.Minimum = -500;
            this.TBMemory.Name = "TBMemory";
            this.TBMemory.Size = new System.Drawing.Size(277, 45);
            this.TBMemory.SmallChange = 50;
            this.TBMemory.TabIndex = 3;
            this.TBMemory.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.TBMemory.Scroll += new System.EventHandler(this.TBMemory_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Powerlimit in %";
            // 
            // BtnApply
            // 
            this.BtnApply.Location = new System.Drawing.Point(291, 195);
            this.BtnApply.Name = "BtnApply";
            this.BtnApply.Size = new System.Drawing.Size(94, 29);
            this.BtnApply.TabIndex = 6;
            this.BtnApply.Text = "Apply";
            this.BtnApply.UseVisualStyleBackColor = true;
            this.BtnApply.Click += new System.EventHandler(this.BtnApply_Click);
            // 
            // NumPower
            // 
            this.NumPower.Location = new System.Drawing.Point(291, 77);
            this.NumPower.Name = "NumPower";
            this.NumPower.Size = new System.Drawing.Size(94, 23);
            this.NumPower.TabIndex = 2;
            this.NumPower.ValueChanged += new System.EventHandler(this.NumPower_ValueChanged);
            // 
            // TBPower
            // 
            this.TBPower.Location = new System.Drawing.Point(8, 69);
            this.TBPower.Maximum = 120;
            this.TBPower.Minimum = 40;
            this.TBPower.Name = "TBPower";
            this.TBPower.Size = new System.Drawing.Size(277, 45);
            this.TBPower.TabIndex = 1;
            this.TBPower.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.TBPower.Value = 40;
            this.TBPower.Scroll += new System.EventHandler(this.TBPower_Scroll);
            // 
            // CBGpuList
            // 
            this.CBGpuList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBGpuList.FormattingEnabled = true;
            this.CBGpuList.Location = new System.Drawing.Point(8, 13);
            this.CBGpuList.Name = "CBGpuList";
            this.CBGpuList.Size = new System.Drawing.Size(182, 23);
            this.CBGpuList.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1039, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumMemory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TBMemory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumPower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TBPower)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.RichTextBox TbMiner;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox CBGpuList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnApply;
        private System.Windows.Forms.NumericUpDown NumPower;
        private System.Windows.Forms.TrackBar TBPower;
        private System.Windows.Forms.Button BtnReset;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown NumMemory;
        private System.Windows.Forms.TrackBar TBMemory;
        private System.Windows.Forms.Button BtnStopMonitor;
        private System.Windows.Forms.Button BtnStartMonitor;
        private System.Windows.Forms.RichTextBox TbMonitor;
    }
}