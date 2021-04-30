namespace NamedPipeSerialProxy.UI
{
    partial class NamedPipeSerialProxyDlg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose ();
            }
            base.Dispose (disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataBitsComboBox = new System.Windows.Forms.ComboBox();
            this.stopBitsComboBox = new System.Windows.Forms.ComboBox();
            this.parityComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.baudRateComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.serialPortComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.namedPipeComboBox = new System.Windows.Forms.ComboBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnWriteConfig = new System.Windows.Forms.Button();
            this.richTextLog = new System.Windows.Forms.RichTextBox();
            this.btnTestService = new System.Windows.Forms.Button();
            this.btnWriteDefaultConfig = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdoCritical = new System.Windows.Forms.RadioButton();
            this.rdoError = new System.Windows.Forms.RadioButton();
            this.rdoWarn = new System.Windows.Forms.RadioButton();
            this.rdoInfo = new System.Windows.Forms.RadioButton();
            this.rdoVerbose = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataBitsComboBox);
            this.groupBox1.Controls.Add(this.stopBitsComboBox);
            this.groupBox1.Controls.Add(this.parityComboBox);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.baudRateComboBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.serialPortComboBox);
            this.groupBox1.Location = new System.Drawing.Point(288, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(190, 204);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Serial Port:";
            // 
            // dataBitsComboBox
            // 
            this.dataBitsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dataBitsComboBox.FormattingEnabled = true;
            this.dataBitsComboBox.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.dataBitsComboBox.Location = new System.Drawing.Point(67, 100);
            this.dataBitsComboBox.Name = "dataBitsComboBox";
            this.dataBitsComboBox.Size = new System.Drawing.Size(110, 21);
            this.dataBitsComboBox.TabIndex = 8;
            // 
            // stopBitsComboBox
            // 
            this.stopBitsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stopBitsComboBox.FormattingEnabled = true;
            this.stopBitsComboBox.Location = new System.Drawing.Point(67, 127);
            this.stopBitsComboBox.Name = "stopBitsComboBox";
            this.stopBitsComboBox.Size = new System.Drawing.Size(110, 21);
            this.stopBitsComboBox.TabIndex = 9;
            // 
            // parityComboBox
            // 
            this.parityComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.parityComboBox.FormattingEnabled = true;
            this.parityComboBox.Location = new System.Drawing.Point(67, 73);
            this.parityComboBox.Name = "parityComboBox";
            this.parityComboBox.Size = new System.Drawing.Size(110, 21);
            this.parityComboBox.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 130);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Stop Bits:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Data Bits:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Parity:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Baud Rate:";
            // 
            // baudRateComboBox
            // 
            this.baudRateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.baudRateComboBox.FormattingEnabled = true;
            this.baudRateComboBox.Items.AddRange(new object[] {
            "110",
            "300",
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "28800",
            "38400",
            "56000",
            "57600",
            "115200",
            "128000",
            "153600",
            "230400",
            "256000",
            "460800",
            "921600"});
            this.baudRateComboBox.Location = new System.Drawing.Point(67, 46);
            this.baudRateComboBox.Name = "baudRateComboBox";
            this.baudRateComboBox.Size = new System.Drawing.Size(110, 21);
            this.baudRateComboBox.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Port:";
            // 
            // serialPortComboBox
            // 
            this.serialPortComboBox.FormattingEnabled = true;
            this.serialPortComboBox.Location = new System.Drawing.Point(67, 19);
            this.serialPortComboBox.Name = "serialPortComboBox";
            this.serialPortComboBox.Size = new System.Drawing.Size(110, 21);
            this.serialPortComboBox.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.namedPipeComboBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(270, 94);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Named Pipe:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 73);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "and Asynchronous";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(257, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Named pipe clients are configured to be BiDirectional";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name:";
            // 
            // namedPipeComboBox
            // 
            this.namedPipeComboBox.FormattingEnabled = true;
            this.namedPipeComboBox.Location = new System.Drawing.Point(50, 19);
            this.namedPipeComboBox.Name = "namedPipeComboBox";
            this.namedPipeComboBox.Size = new System.Drawing.Size(213, 21);
            this.namedPipeComboBox.TabIndex = 2;
            // 
            // btnTest
            // 
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTest.Location = new System.Drawing.Point(487, 18);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(119, 23);
            this.btnTest.TabIndex = 0;
            this.btnTest.Text = "Start";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnWriteConfig
            // 
            this.btnWriteConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWriteConfig.Location = new System.Drawing.Point(487, 76);
            this.btnWriteConfig.Name = "btnWriteConfig";
            this.btnWriteConfig.Size = new System.Drawing.Size(119, 23);
            this.btnWriteConfig.TabIndex = 1;
            this.btnWriteConfig.Text = "Write Config File";
            this.btnWriteConfig.UseVisualStyleBackColor = true;
            this.btnWriteConfig.Click += new System.EventHandler(this.btnWriteConfig_Click);
            // 
            // richTextLog
            // 
            this.richTextLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextLog.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextLog.Location = new System.Drawing.Point(12, 224);
            this.richTextLog.Name = "richTextLog";
            this.richTextLog.Size = new System.Drawing.Size(594, 425);
            this.richTextLog.TabIndex = 4;
            this.richTextLog.Text = "";
            // 
            // btnTestService
            // 
            this.btnTestService.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTestService.Location = new System.Drawing.Point(487, 47);
            this.btnTestService.Name = "btnTestService";
            this.btnTestService.Size = new System.Drawing.Size(119, 23);
            this.btnTestService.TabIndex = 5;
            this.btnTestService.Text = "Test Service";
            this.btnTestService.UseVisualStyleBackColor = true;
            this.btnTestService.Click += new System.EventHandler(this.btnTestService_Click);
            // 
            // btnWriteDefaultConfig
            // 
            this.btnWriteDefaultConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWriteDefaultConfig.Location = new System.Drawing.Point(488, 106);
            this.btnWriteDefaultConfig.Name = "btnWriteDefaultConfig";
            this.btnWriteDefaultConfig.Size = new System.Drawing.Size(118, 27);
            this.btnWriteDefaultConfig.TabIndex = 6;
            this.btnWriteDefaultConfig.Text = "Write Default Config";
            this.btnWriteDefaultConfig.UseVisualStyleBackColor = true;
            this.btnWriteDefaultConfig.Click += new System.EventHandler(this.btnWriteDefaultConfig_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdoCritical);
            this.groupBox3.Controls.Add(this.rdoError);
            this.groupBox3.Controls.Add(this.rdoWarn);
            this.groupBox3.Controls.Add(this.rdoInfo);
            this.groupBox3.Controls.Add(this.rdoVerbose);
            this.groupBox3.Location = new System.Drawing.Point(12, 113);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(270, 103);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Log Level";
            // 
            // rdoCritical
            // 
            this.rdoCritical.AutoSize = true;
            this.rdoCritical.Location = new System.Drawing.Point(99, 43);
            this.rdoCritical.Name = "rdoCritical";
            this.rdoCritical.Size = new System.Drawing.Size(56, 17);
            this.rdoCritical.TabIndex = 4;
            this.rdoCritical.Text = "Critical";
            this.rdoCritical.UseVisualStyleBackColor = true;
            // 
            // rdoError
            // 
            this.rdoError.AutoSize = true;
            this.rdoError.Location = new System.Drawing.Point(98, 19);
            this.rdoError.Name = "rdoError";
            this.rdoError.Size = new System.Drawing.Size(47, 17);
            this.rdoError.TabIndex = 3;
            this.rdoError.Text = "Error";
            this.rdoError.UseVisualStyleBackColor = true;
            // 
            // rdoWarn
            // 
            this.rdoWarn.AutoSize = true;
            this.rdoWarn.Location = new System.Drawing.Point(7, 68);
            this.rdoWarn.Name = "rdoWarn";
            this.rdoWarn.Size = new System.Drawing.Size(65, 17);
            this.rdoWarn.TabIndex = 2;
            this.rdoWarn.Text = "Warning";
            this.rdoWarn.UseVisualStyleBackColor = true;
            // 
            // rdoInfo
            // 
            this.rdoInfo.AutoSize = true;
            this.rdoInfo.Location = new System.Drawing.Point(7, 44);
            this.rdoInfo.Name = "rdoInfo";
            this.rdoInfo.Size = new System.Drawing.Size(43, 17);
            this.rdoInfo.TabIndex = 1;
            this.rdoInfo.Text = "Info";
            this.rdoInfo.UseVisualStyleBackColor = true;
            this.rdoInfo.CheckedChanged += new System.EventHandler(this.LogLevelRadio_CheckedChanged);
            // 
            // rdoVerbose
            // 
            this.rdoVerbose.AutoSize = true;
            this.rdoVerbose.Checked = true;
            this.rdoVerbose.Location = new System.Drawing.Point(7, 20);
            this.rdoVerbose.Name = "rdoVerbose";
            this.rdoVerbose.Size = new System.Drawing.Size(64, 17);
            this.rdoVerbose.TabIndex = 0;
            this.rdoVerbose.TabStop = true;
            this.rdoVerbose.Text = "Verbose";
            this.rdoVerbose.UseVisualStyleBackColor = true;
            this.rdoVerbose.CheckedChanged += new System.EventHandler(this.LogLevelRadio_CheckedChanged);
            // 
            // NamedPipeSerialProxyDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 661);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnWriteDefaultConfig);
            this.Controls.Add(this.btnTestService);
            this.Controls.Add(this.richTextLog);
            this.Controls.Add(this.btnWriteConfig);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(634, 300);
            this.Name = "NamedPipeSerialProxyDlg";
            this.ShowIcon = false;
            this.Text = "Named Pipe Serial Proxy";
            this.Load += new System.EventHandler(this.NamedPipeSerialProxyDlg_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnWriteConfig;
        private System.Windows.Forms.ComboBox serialPortComboBox;
        private System.Windows.Forms.ComboBox namedPipeComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox baudRateComboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox parityComboBox;
        private System.Windows.Forms.ComboBox stopBitsComboBox;
        private System.Windows.Forms.ComboBox dataBitsComboBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RichTextBox richTextLog;
        private System.Windows.Forms.Button btnTestService;
        private System.Windows.Forms.Button btnWriteDefaultConfig;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdoCritical;
        private System.Windows.Forms.RadioButton rdoError;
        private System.Windows.Forms.RadioButton rdoWarn;
        private System.Windows.Forms.RadioButton rdoInfo;
        private System.Windows.Forms.RadioButton rdoVerbose;
    }
}

