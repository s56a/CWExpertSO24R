namespace CWExpert
{
    partial class Setup
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.comboAudioBuffer = new System.Windows.Forms.ComboBox();
            this.comboAudioOutput = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblAudioDriver = new System.Windows.Forms.Label();
            this.lblAudioLatency = new System.Windows.Forms.Label();
            this.comboAudioDriver = new System.Windows.Forms.ComboBox();
            this.udLatency = new System.Windows.Forms.NumericUpDown();
            this.comboAudioInput = new System.Windows.Forms.ComboBox();
            this.lblAudioSampleRate = new System.Windows.Forms.Label();
            this.lblAudioInput = new System.Windows.Forms.Label();
            this.comboAudioSampleRate = new System.Windows.Forms.ComboBox();
            this.tbSetup = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblCallSign = new System.Windows.Forms.Label();
            this.txtCALL = new System.Windows.Forms.TextBox();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udLatency)).BeginInit();
            this.tbSetup.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(101, 340);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(228, 340);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.comboAudioBuffer);
            this.tabPage1.Controls.Add(this.comboAudioOutput);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.lblAudioDriver);
            this.tabPage1.Controls.Add(this.lblAudioLatency);
            this.tabPage1.Controls.Add(this.comboAudioDriver);
            this.tabPage1.Controls.Add(this.udLatency);
            this.tabPage1.Controls.Add(this.comboAudioInput);
            this.tabPage1.Controls.Add(this.lblAudioSampleRate);
            this.tabPage1.Controls.Add(this.lblAudioInput);
            this.tabPage1.Controls.Add(this.comboAudioSampleRate);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(347, 266);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Audio";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 183);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Buffer size";
            // 
            // comboAudioBuffer
            // 
            this.comboAudioBuffer.FormattingEnabled = true;
            this.comboAudioBuffer.Items.AddRange(new object[] {
            "64",
            "128",
            "256",
            "512",
            "1024",
            "2048"});
            this.comboAudioBuffer.Location = new System.Drawing.Point(122, 180);
            this.comboAudioBuffer.Name = "comboAudioBuffer";
            this.comboAudioBuffer.Size = new System.Drawing.Size(121, 21);
            this.comboAudioBuffer.TabIndex = 12;
            this.comboAudioBuffer.SelectedIndexChanged += new System.EventHandler(this.comboAudioBuffer_SelectedIndexChanged);
            // 
            // comboAudioOutput
            // 
            this.comboAudioOutput.FormattingEnabled = true;
            this.comboAudioOutput.Location = new System.Drawing.Point(122, 98);
            this.comboAudioOutput.Name = "comboAudioOutput";
            this.comboAudioOutput.Size = new System.Drawing.Size(167, 21);
            this.comboAudioOutput.TabIndex = 10;
            this.comboAudioOutput.SelectedIndexChanged += new System.EventHandler(this.comboAudioOutput_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Output";
            // 
            // lblAudioDriver
            // 
            this.lblAudioDriver.AutoSize = true;
            this.lblAudioDriver.Location = new System.Drawing.Point(41, 19);
            this.lblAudioDriver.Name = "lblAudioDriver";
            this.lblAudioDriver.Size = new System.Drawing.Size(62, 13);
            this.lblAudioDriver.TabIndex = 4;
            this.lblAudioDriver.Text = "AudioDriver";
            // 
            // lblAudioLatency
            // 
            this.lblAudioLatency.AutoSize = true;
            this.lblAudioLatency.Location = new System.Drawing.Point(41, 223);
            this.lblAudioLatency.Name = "lblAudioLatency";
            this.lblAudioLatency.Size = new System.Drawing.Size(45, 13);
            this.lblAudioLatency.TabIndex = 9;
            this.lblAudioLatency.Text = "Latency";
            // 
            // comboAudioDriver
            // 
            this.comboAudioDriver.FormattingEnabled = true;
            this.comboAudioDriver.Location = new System.Drawing.Point(122, 16);
            this.comboAudioDriver.Name = "comboAudioDriver";
            this.comboAudioDriver.Size = new System.Drawing.Size(167, 21);
            this.comboAudioDriver.TabIndex = 2;
            this.comboAudioDriver.SelectedIndexChanged += new System.EventHandler(this.comboAudioDriver_SelectedIndexChanged);
            // 
            // udLatency
            // 
            this.udLatency.Location = new System.Drawing.Point(151, 221);
            this.udLatency.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.udLatency.Name = "udLatency";
            this.udLatency.Size = new System.Drawing.Size(56, 20);
            this.udLatency.TabIndex = 8;
            this.udLatency.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udLatency.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.udLatency.ValueChanged += new System.EventHandler(this.udLatency_ValueChanged);
            // 
            // comboAudioInput
            // 
            this.comboAudioInput.FormattingEnabled = true;
            this.comboAudioInput.Location = new System.Drawing.Point(122, 57);
            this.comboAudioInput.Name = "comboAudioInput";
            this.comboAudioInput.Size = new System.Drawing.Size(167, 21);
            this.comboAudioInput.TabIndex = 3;
            this.comboAudioInput.SelectedIndexChanged += new System.EventHandler(this.comboAudioInput_SelectedIndexChanged);
            // 
            // lblAudioSampleRate
            // 
            this.lblAudioSampleRate.AutoSize = true;
            this.lblAudioSampleRate.Location = new System.Drawing.Point(41, 142);
            this.lblAudioSampleRate.Name = "lblAudioSampleRate";
            this.lblAudioSampleRate.Size = new System.Drawing.Size(63, 13);
            this.lblAudioSampleRate.TabIndex = 7;
            this.lblAudioSampleRate.Text = "Sample rate";
            // 
            // lblAudioInput
            // 
            this.lblAudioInput.AutoSize = true;
            this.lblAudioInput.Location = new System.Drawing.Point(41, 60);
            this.lblAudioInput.Name = "lblAudioInput";
            this.lblAudioInput.Size = new System.Drawing.Size(31, 13);
            this.lblAudioInput.TabIndex = 5;
            this.lblAudioInput.Text = "Input";
            // 
            // comboAudioSampleRate
            // 
            this.comboAudioSampleRate.FormattingEnabled = true;
            this.comboAudioSampleRate.Items.AddRange(new object[] {
            "6000",
            "8000",
            "11025",
            "12000",
            "22050",
            "24000",
            "44100",
            "48000",
            "96000",
            "192000"});
            this.comboAudioSampleRate.Location = new System.Drawing.Point(122, 139);
            this.comboAudioSampleRate.Name = "comboAudioSampleRate";
            this.comboAudioSampleRate.Size = new System.Drawing.Size(121, 21);
            this.comboAudioSampleRate.TabIndex = 6;
            this.comboAudioSampleRate.SelectedIndexChanged += new System.EventHandler(this.comboAudioSampleRate_SelectedIndexChanged);
            // 
            // tbSetup
            // 
            this.tbSetup.Controls.Add(this.tabPage1);
            this.tbSetup.Controls.Add(this.tabPage2);
            this.tbSetup.Location = new System.Drawing.Point(25, 12);
            this.tbSetup.Name = "tbSetup";
            this.tbSetup.SelectedIndex = 0;
            this.tbSetup.Size = new System.Drawing.Size(355, 292);
            this.tbSetup.TabIndex = 10;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.lblCallSign);
            this.tabPage2.Controls.Add(this.txtCALL);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(347, 266);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Misc";
            // 
            // lblCallSign
            // 
            this.lblCallSign.AutoSize = true;
            this.lblCallSign.Location = new System.Drawing.Point(75, 40);
            this.lblCallSign.Name = "lblCallSign";
            this.lblCallSign.Size = new System.Drawing.Size(46, 13);
            this.lblCallSign.TabIndex = 1;
            this.lblCallSign.Text = "Call sign";
            // 
            // txtCALL
            // 
            this.txtCALL.Location = new System.Drawing.Point(171, 37);
            this.txtCALL.MaxLength = 10;
            this.txtCALL.Name = "txtCALL";
            this.txtCALL.Size = new System.Drawing.Size(100, 20);
            this.txtCALL.TabIndex = 0;
            this.txtCALL.TextChanged += new System.EventHandler(this.txtCALL_TextChanged);
            // 
            // Setup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 392);
            this.Controls.Add(this.tbSetup);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnOK);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(413, 426);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(413, 426);
            this.Name = "Setup";
            this.Text = "CWExpert Setup";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Setup_Closing);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udLatency)).EndInit();
            this.tbSetup.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label lblAudioDriver;
        private System.Windows.Forms.Label lblAudioLatency;
        private System.Windows.Forms.ComboBox comboAudioDriver;
        private System.Windows.Forms.NumericUpDown udLatency;
        private System.Windows.Forms.ComboBox comboAudioInput;
        private System.Windows.Forms.Label lblAudioSampleRate;
        private System.Windows.Forms.Label lblAudioInput;
        private System.Windows.Forms.ComboBox comboAudioSampleRate;
        private System.Windows.Forms.TabControl tbSetup;
        private System.Windows.Forms.ComboBox comboAudioOutput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboAudioBuffer;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lblCallSign;
        public System.Windows.Forms.TextBox txtCALL;
    }
}