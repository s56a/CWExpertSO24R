namespace CWExpert
{
    partial class CWExpert190519
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
            Audio.callback_return = 2;
            Audio.StopAudio();

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
            this.txtCall = new System.Windows.Forms.TextBox();
            this.btnSendCall = new System.Windows.Forms.Button();
            this.btnF1 = new System.Windows.Forms.Button();
            this.btnF2 = new System.Windows.Forms.Button();
            this.btnF3 = new System.Windows.Forms.Button();
            this.btnF4 = new System.Windows.Forms.Button();
            this.btnF5 = new System.Windows.Forms.Button();
            this.btnF6 = new System.Windows.Forms.Button();
            this.btnF8 = new System.Windows.Forms.Button();
            this.btnF7 = new System.Windows.Forms.Button();
            this.btnStartMR = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.setupMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSendRST = new System.Windows.Forms.Button();
            this.txtRST = new System.Windows.Forms.TextBox();
            this.btnSendNr = new System.Windows.Forms.Button();
            this.txtNr = new System.Windows.Forms.TextBox();
            this.lblRST = new System.Windows.Forms.Label();
            this.lblNr = new System.Windows.Forms.Label();
            this.btnStopMR = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtrNbr = new System.Windows.Forms.TextBox();
            this.txtrRST = new System.Windows.Forms.TextBox();
            this.txtrCall = new System.Windows.Forms.TextBox();
            this.btnF11 = new System.Windows.Forms.Button();
            this.btnF10 = new System.Windows.Forms.Button();
            this.btnF9 = new System.Windows.Forms.Button();
            this.rxa = new System.Windows.Forms.TextBox();
            this.txa = new System.Windows.Forms.TextBox();
            this.txb = new System.Windows.Forms.TextBox();
            this.rxb = new System.Windows.Forms.TextBox();
            this.btnF12 = new System.Windows.Forms.Button();
            this.btncall = new System.Windows.Forms.Button();
            this.btnrst = new System.Windows.Forms.Button();
            this.btnnbr = new System.Windows.Forms.Button();
            this.butleft = new System.Windows.Forms.Button();
            this.butright = new System.Windows.Forms.Button();
            this.butesc = new System.Windows.Forms.Button();
            this.butdva = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCall
            // 
            this.txtCall.Location = new System.Drawing.Point(11, 111);
            this.txtCall.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCall.MaxLength = 16;
            this.txtCall.Name = "txtCall";
            this.txtCall.Size = new System.Drawing.Size(148, 22);
            this.txtCall.TabIndex = 0;
            this.txtCall.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCall_KeyUp);
            // 
            // btnSendCall
            // 
            this.btnSendCall.Location = new System.Drawing.Point(200, 108);
            this.btnSendCall.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSendCall.Name = "btnSendCall";
            this.btnSendCall.Size = new System.Drawing.Size(53, 28);
            this.btnSendCall.TabIndex = 1;
            this.btnSendCall.Text = "<Call";
            this.btnSendCall.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSendCall.UseVisualStyleBackColor = true;
            this.btnSendCall.Click += new System.EventHandler(this.btnSendCall_Click);
            // 
            // btnF1
            // 
            this.btnF1.Location = new System.Drawing.Point(75, 225);
            this.btnF1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnF1.Name = "btnF1";
            this.btnF1.Size = new System.Drawing.Size(85, 28);
            this.btnF1.TabIndex = 2;
            this.btnF1.Text = "F1";
            this.btnF1.UseVisualStyleBackColor = true;
            this.btnF1.Click += new System.EventHandler(this.btnF1_Click);
            // 
            // btnF2
            // 
            this.btnF2.Location = new System.Drawing.Point(165, 225);
            this.btnF2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnF2.Name = "btnF2";
            this.btnF2.Size = new System.Drawing.Size(85, 28);
            this.btnF2.TabIndex = 3;
            this.btnF2.Text = "F2";
            this.btnF2.UseVisualStyleBackColor = true;
            this.btnF2.Click += new System.EventHandler(this.btnF2_Click);
            // 
            // btnF3
            // 
            this.btnF3.Location = new System.Drawing.Point(256, 225);
            this.btnF3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnF3.Name = "btnF3";
            this.btnF3.Size = new System.Drawing.Size(85, 28);
            this.btnF3.TabIndex = 4;
            this.btnF3.Text = "F3";
            this.btnF3.UseVisualStyleBackColor = true;
            this.btnF3.Click += new System.EventHandler(this.btnF3_Click);
            // 
            // btnF4
            // 
            this.btnF4.Location = new System.Drawing.Point(347, 225);
            this.btnF4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnF4.Name = "btnF4";
            this.btnF4.Size = new System.Drawing.Size(85, 28);
            this.btnF4.TabIndex = 5;
            this.btnF4.Text = "F4";
            this.btnF4.UseVisualStyleBackColor = true;
            this.btnF4.Click += new System.EventHandler(this.btnF4_Click);
            // 
            // btnF5
            // 
            this.btnF5.Location = new System.Drawing.Point(75, 277);
            this.btnF5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnF5.Name = "btnF5";
            this.btnF5.Size = new System.Drawing.Size(85, 28);
            this.btnF5.TabIndex = 6;
            this.btnF5.Text = "F5";
            this.btnF5.UseVisualStyleBackColor = true;
            this.btnF5.Click += new System.EventHandler(this.btnF5_Click);
            // 
            // btnF6
            // 
            this.btnF6.Location = new System.Drawing.Point(165, 277);
            this.btnF6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnF6.Name = "btnF6";
            this.btnF6.Size = new System.Drawing.Size(85, 28);
            this.btnF6.TabIndex = 7;
            this.btnF6.Text = "F6";
            this.btnF6.UseVisualStyleBackColor = true;
            this.btnF6.Click += new System.EventHandler(this.btnF6_Click);
            // 
            // btnF8
            // 
            this.btnF8.Location = new System.Drawing.Point(347, 277);
            this.btnF8.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnF8.Name = "btnF8";
            this.btnF8.Size = new System.Drawing.Size(85, 28);
            this.btnF8.TabIndex = 9;
            this.btnF8.Text = "F8";
            this.btnF8.UseVisualStyleBackColor = true;
            this.btnF8.Click += new System.EventHandler(this.btnF8_Click);
            // 
            // btnF7
            // 
            this.btnF7.Location = new System.Drawing.Point(256, 277);
            this.btnF7.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnF7.Name = "btnF7";
            this.btnF7.Size = new System.Drawing.Size(85, 28);
            this.btnF7.TabIndex = 8;
            this.btnF7.Text = "F7";
            this.btnF7.UseVisualStyleBackColor = true;
            this.btnF7.Click += new System.EventHandler(this.btnF7_Click);
            // 
            // btnStartMR
            // 
            this.btnStartMR.BackColor = System.Drawing.SystemColors.Control;
            this.btnStartMR.Location = new System.Drawing.Point(11, 329);
            this.btnStartMR.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnStartMR.Name = "btnStartMR";
            this.btnStartMR.Size = new System.Drawing.Size(53, 28);
            this.btnStartMR.TabIndex = 10;
            this.btnStartMR.Text = "Start";
            this.btnStartMR.UseVisualStyleBackColor = false;
            this.btnStartMR.Click += new System.EventHandler(this.btnStartMR_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setupMenuItem,
            this.exitToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Margin = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(509, 28);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // setupMenuItem
            // 
            this.setupMenuItem.Name = "setupMenuItem";
            this.setupMenuItem.Size = new System.Drawing.Size(61, 24);
            this.setupMenuItem.Text = "Setup";
            this.setupMenuItem.Click += new System.EventHandler(this.setupMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(47, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // btnSendRST
            // 
            this.btnSendRST.Location = new System.Drawing.Point(200, 143);
            this.btnSendRST.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSendRST.Name = "btnSendRST";
            this.btnSendRST.Size = new System.Drawing.Size(53, 28);
            this.btnSendRST.TabIndex = 14;
            this.btnSendRST.Text = "< Se";
            this.btnSendRST.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSendRST.UseVisualStyleBackColor = true;
            this.btnSendRST.Click += new System.EventHandler(this.btnSendRST_Click);
            // 
            // txtRST
            // 
            this.txtRST.Location = new System.Drawing.Point(83, 145);
            this.txtRST.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtRST.MaxLength = 7;
            this.txtRST.Name = "txtRST";
            this.txtRST.Size = new System.Drawing.Size(76, 22);
            this.txtRST.TabIndex = 13;
            this.txtRST.TextChanged += new System.EventHandler(this.txtRST_TextChanged);
            this.txtRST.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtRST_KeyUp);
            // 
            // btnSendNr
            // 
            this.btnSendNr.Location = new System.Drawing.Point(200, 178);
            this.btnSendNr.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSendNr.Name = "btnSendNr";
            this.btnSendNr.Size = new System.Drawing.Size(53, 28);
            this.btnSendNr.TabIndex = 16;
            this.btnSendNr.Text = "< Se";
            this.btnSendNr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSendNr.UseVisualStyleBackColor = true;
            this.btnSendNr.Click += new System.EventHandler(this.btnSendNr_Click);
            // 
            // txtNr
            // 
            this.txtNr.Location = new System.Drawing.Point(83, 181);
            this.txtNr.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtNr.MaxLength = 5;
            this.txtNr.Name = "txtNr";
            this.txtNr.Size = new System.Drawing.Size(76, 22);
            this.txtNr.TabIndex = 15;
            this.txtNr.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNr_KeyUp);
            // 
            // lblRST
            // 
            this.lblRST.AutoSize = true;
            this.lblRST.Location = new System.Drawing.Point(25, 149);
            this.lblRST.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRST.Name = "lblRST";
            this.lblRST.Size = new System.Drawing.Size(35, 16);
            this.lblRST.TabIndex = 18;
            this.lblRST.Text = "RST";
            // 
            // lblNr
            // 
            this.lblNr.AutoSize = true;
            this.lblNr.Location = new System.Drawing.Point(24, 185);
            this.lblNr.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNr.Name = "lblNr";
            this.lblNr.Size = new System.Drawing.Size(36, 16);
            this.lblNr.TabIndex = 19;
            this.lblNr.Text = "NBR";
            // 
            // btnStopMR
            // 
            this.btnStopMR.Location = new System.Drawing.Point(455, 327);
            this.btnStopMR.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnStopMR.Name = "btnStopMR";
            this.btnStopMR.Size = new System.Drawing.Size(53, 28);
            this.btnStopMR.TabIndex = 20;
            this.btnStopMR.Text = "Stop";
            this.btnStopMR.UseVisualStyleBackColor = true;
            this.btnStopMR.Click += new System.EventHandler(this.btnStopMR_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(455, 186);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 16);
            this.label7.TabIndex = 59;
            this.label7.Text = "NBR";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(457, 149);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 16);
            this.label8.TabIndex = 58;
            this.label8.Text = "RST";
            // 
            // txtrNbr
            // 
            this.txtrNbr.Location = new System.Drawing.Point(347, 182);
            this.txtrNbr.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtrNbr.MaxLength = 5;
            this.txtrNbr.Name = "txtrNbr";
            this.txtrNbr.Size = new System.Drawing.Size(76, 22);
            this.txtrNbr.TabIndex = 55;
            this.txtrNbr.TextChanged += new System.EventHandler(this.txtrNbr_TextChanged);
            // 
            // txtrRST
            // 
            this.txtrRST.Location = new System.Drawing.Point(347, 145);
            this.txtrRST.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtrRST.MaxLength = 3;
            this.txtrRST.Name = "txtrRST";
            this.txtrRST.Size = new System.Drawing.Size(76, 22);
            this.txtrRST.TabIndex = 53;
            this.txtrRST.TextChanged += new System.EventHandler(this.txtrRST_TextChanged);
            // 
            // txtrCall
            // 
            this.txtrCall.Location = new System.Drawing.Point(347, 111);
            this.txtrCall.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtrCall.MaxLength = 16;
            this.txtrCall.Name = "txtrCall";
            this.txtrCall.Size = new System.Drawing.Size(148, 22);
            this.txtrCall.TabIndex = 42;
            this.txtrCall.TextChanged += new System.EventHandler(this.txtrCall_TextChanged);
            // 
            // btnF11
            // 
            this.btnF11.Location = new System.Drawing.Point(256, 329);
            this.btnF11.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnF11.Name = "btnF11";
            this.btnF11.Size = new System.Drawing.Size(85, 28);
            this.btnF11.TabIndex = 64;
            this.btnF11.Text = "F11";
            this.btnF11.UseVisualStyleBackColor = true;
            this.btnF11.Click += new System.EventHandler(this.btnF11_Click);
            // 
            // btnF10
            // 
            this.btnF10.Location = new System.Drawing.Point(165, 329);
            this.btnF10.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnF10.Name = "btnF10";
            this.btnF10.Size = new System.Drawing.Size(85, 28);
            this.btnF10.TabIndex = 63;
            this.btnF10.Text = "F10";
            this.btnF10.UseVisualStyleBackColor = true;
            this.btnF10.Click += new System.EventHandler(this.btnF10_Click);
            // 
            // btnF9
            // 
            this.btnF9.Location = new System.Drawing.Point(75, 329);
            this.btnF9.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnF9.Name = "btnF9";
            this.btnF9.Size = new System.Drawing.Size(85, 28);
            this.btnF9.TabIndex = 62;
            this.btnF9.Text = "F9";
            this.btnF9.UseVisualStyleBackColor = true;
            this.btnF9.Click += new System.EventHandler(this.btnF9_Click);
            // 
            // rxa
            // 
            this.rxa.Location = new System.Drawing.Point(11, 265);
            this.rxa.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rxa.Name = "rxa";
            this.rxa.Size = new System.Drawing.Size(52, 22);
            this.rxa.TabIndex = 66;
            this.rxa.Text = "RX A";
            // 
            // txa
            // 
            this.txa.Location = new System.Drawing.Point(11, 297);
            this.txa.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txa.Name = "txa";
            this.txa.Size = new System.Drawing.Size(52, 22);
            this.txa.TabIndex = 67;
            this.txa.Text = "TX A";
            // 
            // txb
            // 
            this.txb.Location = new System.Drawing.Point(455, 295);
            this.txb.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txb.Name = "txb";
            this.txb.Size = new System.Drawing.Size(52, 22);
            this.txb.TabIndex = 68;
            this.txb.Text = "TX B";
            // 
            // rxb
            // 
            this.rxb.Location = new System.Drawing.Point(455, 263);
            this.rxb.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rxb.Name = "rxb";
            this.rxb.Size = new System.Drawing.Size(52, 22);
            this.rxb.TabIndex = 69;
            this.rxb.Text = "RX B";
            // 
            // btnF12
            // 
            this.btnF12.Location = new System.Drawing.Point(347, 327);
            this.btnF12.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnF12.Name = "btnF12";
            this.btnF12.Size = new System.Drawing.Size(83, 28);
            this.btnF12.TabIndex = 70;
            this.btnF12.Text = "F12";
            this.btnF12.UseVisualStyleBackColor = true;
            this.btnF12.Click += new System.EventHandler(this.btnF12_Click);
            // 
            // btncall
            // 
            this.btncall.Location = new System.Drawing.Point(256, 108);
            this.btncall.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btncall.Name = "btncall";
            this.btncall.Size = new System.Drawing.Size(53, 28);
            this.btncall.TabIndex = 71;
            this.btncall.Text = "sign>";
            this.btncall.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btncall.UseVisualStyleBackColor = true;
            this.btncall.Click += new System.EventHandler(this.btncall_Click);
            // 
            // btnrst
            // 
            this.btnrst.Location = new System.Drawing.Point(256, 143);
            this.btnrst.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnrst.Name = "btnrst";
            this.btnrst.Size = new System.Drawing.Size(53, 28);
            this.btnrst.TabIndex = 72;
            this.btnrst.Text = "nd >";
            this.btnrst.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnrst.UseVisualStyleBackColor = true;
            this.btnrst.Click += new System.EventHandler(this.btnrst_Click);
            // 
            // btnnbr
            // 
            this.btnnbr.Location = new System.Drawing.Point(256, 177);
            this.btnnbr.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnnbr.Name = "btnnbr";
            this.btnnbr.Size = new System.Drawing.Size(53, 28);
            this.btnnbr.TabIndex = 73;
            this.btnnbr.Text = "nd >";
            this.btnnbr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnnbr.UseVisualStyleBackColor = true;
            this.btnnbr.Click += new System.EventHandler(this.btnnbr_Click);
            // 
            // butleft
            // 
            this.butleft.BackColor = System.Drawing.Color.LightPink;
            this.butleft.Location = new System.Drawing.Point(187, 58);
            this.butleft.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.butleft.Name = "butleft";
            this.butleft.Size = new System.Drawing.Size(67, 28);
            this.butleft.TabIndex = 81;
            this.butleft.Text = "< Left";
            this.butleft.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butleft.UseVisualStyleBackColor = false;
            this.butleft.Click += new System.EventHandler(this.btnleft_Click);
            // 
            // butright
            // 
            this.butright.BackColor = System.Drawing.Color.LightGreen;
            this.butright.Location = new System.Drawing.Point(256, 58);
            this.butright.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.butright.Name = "butright";
            this.butright.Size = new System.Drawing.Size(67, 28);
            this.butright.TabIndex = 82;
            this.butright.Text = "Right >";
            this.butright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butright.UseVisualStyleBackColor = false;
            this.butright.Click += new System.EventHandler(this.btnright_Click);
            // 
            // butesc
            // 
            this.butesc.Location = new System.Drawing.Point(11, 225);
            this.butesc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.butesc.Name = "butesc";
            this.butesc.Size = new System.Drawing.Size(53, 28);
            this.butesc.TabIndex = 83;
            this.butesc.Text = "Esc";
            this.butesc.UseVisualStyleBackColor = true;
            this.butesc.Click += new System.EventHandler(this.butesc_Click);
            // 
            // butdva
            // 
            this.butdva.Location = new System.Drawing.Point(455, 225);
            this.butdva.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.butdva.Name = "butdva";
            this.butdva.Size = new System.Drawing.Size(53, 28);
            this.butdva.TabIndex = 84;
            this.butdva.Text = "2R";
            this.butdva.UseVisualStyleBackColor = true;
            this.butdva.Click += new System.EventHandler(this.butdva_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(328, 6);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 16);
            this.label2.TabIndex = 85;
            this.label2.Text = "S56A - YT7PWR - DL7NX ";
            this.label2.Click += new System.EventHandler(this.Label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(264, 6);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 16);
            this.label3.TabIndex = 86;
            this.label3.Text = "Radio 2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(183, 6);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 16);
            this.label4.TabIndex = 87;
            this.label4.Text = "Radio 1";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(11, 85);
            this.textBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox3.MaxLength = 16;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(148, 22);
            this.textBox3.TabIndex = 88;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(11, 59);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox2.MaxLength = 16;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(148, 22);
            this.textBox2.TabIndex = 89;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(11, 33);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox1.MaxLength = 16;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(148, 22);
            this.textBox1.TabIndex = 90;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(347, 33);
            this.textBox4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox4.MaxLength = 16;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(148, 22);
            this.textBox4.TabIndex = 91;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(347, 59);
            this.textBox5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox5.MaxLength = 16;
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(148, 22);
            this.textBox5.TabIndex = 92;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(347, 85);
            this.textBox6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox6.MaxLength = 16;
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(148, 22);
            this.textBox6.TabIndex = 93;
            // 
            // CWExpert190519
            // 
            this.AccessibleName = "";
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 361);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.butdva);
            this.Controls.Add(this.butesc);
            this.Controls.Add(this.butright);
            this.Controls.Add(this.butleft);
            this.Controls.Add(this.btnnbr);
            this.Controls.Add(this.btnrst);
            this.Controls.Add(this.btncall);
            this.Controls.Add(this.btnF12);
            this.Controls.Add(this.rxb);
            this.Controls.Add(this.txb);
            this.Controls.Add(this.txa);
            this.Controls.Add(this.rxa);
            this.Controls.Add(this.btnF11);
            this.Controls.Add(this.btnF10);
            this.Controls.Add(this.btnF9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtrNbr);
            this.Controls.Add(this.txtrRST);
            this.Controls.Add(this.txtrCall);
            this.Controls.Add(this.btnStopMR);
            this.Controls.Add(this.lblNr);
            this.Controls.Add(this.lblRST);
            this.Controls.Add(this.btnSendNr);
            this.Controls.Add(this.txtNr);
            this.Controls.Add(this.btnSendRST);
            this.Controls.Add(this.txtRST);
            this.Controls.Add(this.btnStartMR);
            this.Controls.Add(this.btnF8);
            this.Controls.Add(this.btnF7);
            this.Controls.Add(this.btnF6);
            this.Controls.Add(this.btnF5);
            this.Controls.Add(this.btnF4);
            this.Controls.Add(this.btnF3);
            this.Controls.Add(this.btnF2);
            this.Controls.Add(this.btnF1);
            this.Controls.Add(this.btnSendCall);
            this.Controls.Add(this.txtCall);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(527, 408);
            this.MinimumSize = new System.Drawing.Size(527, 383);
            this.Name = "CWExpert190519";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "CWExpert  v. 21.8.23.";
            this.Load += new System.EventHandler(this.CWExpert_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CWExpert_KeyUp);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txtCall;
        private System.Windows.Forms.Button btnSendCall;
        private System.Windows.Forms.Button btnF1;
        private System.Windows.Forms.Button btnF2;
        private System.Windows.Forms.Button btnF3;
        private System.Windows.Forms.Button btnF4;
        private System.Windows.Forms.Button btnF5;
        private System.Windows.Forms.Button btnF6;
        private System.Windows.Forms.Button btnF8;
        private System.Windows.Forms.Button btnF7;
        private System.Windows.Forms.Button btnStartMR;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem setupMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Button btnSendRST;
        private System.Windows.Forms.TextBox txtRST;
        private System.Windows.Forms.Button btnSendNr;
        private System.Windows.Forms.TextBox txtNr;
        private System.Windows.Forms.Label lblRST;
        private System.Windows.Forms.Label lblNr;
        private System.Windows.Forms.Button btnStopMR;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtrNbr;
        private System.Windows.Forms.TextBox txtrRST;
        public System.Windows.Forms.TextBox txtrCall;
        private System.Windows.Forms.Button btnF11;
        private System.Windows.Forms.Button btnF10;
        private System.Windows.Forms.Button btnF9;
        private System.Windows.Forms.TextBox rxa;
        private System.Windows.Forms.TextBox txa;
        private System.Windows.Forms.TextBox txb;
        private System.Windows.Forms.TextBox rxb;
        private System.Windows.Forms.Button btnF12;
        private System.Windows.Forms.Button btncall;
        private System.Windows.Forms.Button btnrst;
        private System.Windows.Forms.Button btnnbr;
        private System.Windows.Forms.Button butleft;
        private System.Windows.Forms.Button butright;
        private System.Windows.Forms.Button butesc;
        private System.Windows.Forms.Button butdva;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox textBox3;
        public System.Windows.Forms.TextBox textBox2;
        public System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.TextBox textBox4;
        public System.Windows.Forms.TextBox textBox5;
        public System.Windows.Forms.TextBox textBox6;
    }
}

