namespace SMSmanage
{
    partial class FrmSet
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
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtCentreNum = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProtrocalType = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtHoldTime = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtIpPort = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtListenPort = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtServeIp = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "短信群发器端口";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(132, 21);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(148, 21);
            this.txtPort.TabIndex = 1;
            // 
            // txtCentreNum
            // 
            this.txtCentreNum.Location = new System.Drawing.Point(132, 55);
            this.txtCentreNum.Name = "txtCentreNum";
            this.txtCentreNum.Size = new System.Drawing.Size(148, 21);
            this.txtCentreNum.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "短信中心号码";
            // 
            // txtProtrocalType
            // 
            this.txtProtrocalType.Location = new System.Drawing.Point(132, 89);
            this.txtProtrocalType.Name = "txtProtrocalType";
            this.txtProtrocalType.Size = new System.Drawing.Size(148, 21);
            this.txtProtrocalType.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "通信协议";
            // 
            // txtHoldTime
            // 
            this.txtHoldTime.Location = new System.Drawing.Point(132, 123);
            this.txtHoldTime.Name = "txtHoldTime";
            this.txtHoldTime.Size = new System.Drawing.Size(148, 21);
            this.txtHoldTime.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "群发延时";
            // 
            // txtIpPort
            // 
            this.txtIpPort.Location = new System.Drawing.Point(132, 157);
            this.txtIpPort.Name = "txtIpPort";
            this.txtIpPort.Size = new System.Drawing.Size(148, 21);
            this.txtIpPort.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "监听IPAddress";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(205, 356);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "保 存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtListenPort
            // 
            this.txtListenPort.Location = new System.Drawing.Point(132, 191);
            this.txtListenPort.Name = "txtListenPort";
            this.txtListenPort.Size = new System.Drawing.Size(148, 21);
            this.txtListenPort.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(50, 194);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "监听端口";
            // 
            // txtServeIp
            // 
            this.txtServeIp.Location = new System.Drawing.Point(132, 225);
            this.txtServeIp.Name = "txtServeIp";
            this.txtServeIp.Size = new System.Drawing.Size(148, 21);
            this.txtServeIp.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(50, 228);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 13;
            this.label7.Text = "服务器IP";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(37, 265);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 15;
            this.label8.Text = "默认发送短信";
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(132, 262);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(148, 88);
            this.txtMessage.TabIndex = 16;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(37, 361);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(155, 12);
            this.label9.TabIndex = 17;
            this.label9.Text = "如需收取急件时间可用#代替";
            // 
            // FrmSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 391);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtServeIp);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtListenPort);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtIpPort);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtHoldTime);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtProtrocalType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCentreNum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "FrmSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统设置";
            this.Load += new System.EventHandler(this.FrmSet_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtCentreNum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProtrocalType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtHoldTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtIpPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtListenPort;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtServeIp;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Label label9;
    }
}