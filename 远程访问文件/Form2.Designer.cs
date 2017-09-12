namespace 远程访问文件
{
    partial class Form2
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtDataBase = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtLocalPath = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtLocalDataBase = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(346, 311);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "复制";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(88, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "地址";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(76, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "用户名";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(88, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "密码";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 184);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "数据库字符串";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(64, 257);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "本地路径";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(143, 65);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(278, 21);
            this.txtAddress.TabIndex = 6;
            this.txtAddress.Text = "10.32.6.197";
            // 
            // txtDataBase
            // 
            this.txtDataBase.Location = new System.Drawing.Point(143, 181);
            this.txtDataBase.Name = "txtDataBase";
            this.txtDataBase.PasswordChar = '*';
            this.txtDataBase.Size = new System.Drawing.Size(278, 21);
            this.txtDataBase.TabIndex = 7;
            this.txtDataBase.Text = "Data Source=10.32.255.1\\sql2005;Database=NTHB_tongzhouXF;User ID=shencai;Password" +
    "=shencai12369";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(143, 146);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(278, 21);
            this.txtPassword.TabIndex = 8;
            this.txtPassword.Text = "nthbj";
            // 
            // txtLocalPath
            // 
            this.txtLocalPath.Location = new System.Drawing.Point(143, 254);
            this.txtLocalPath.Name = "txtLocalPath";
            this.txtLocalPath.Size = new System.Drawing.Size(278, 21);
            this.txtLocalPath.TabIndex = 9;
            this.txtLocalPath.Text = "D:\\通州区信访\\真实系统\\EvdienceFile\\SpeechSound";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(143, 101);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(278, 21);
            this.txtUserName.TabIndex = 10;
            this.txtUserName.Text = "nthbj";
            // 
            // txtLocalDataBase
            // 
            this.txtLocalDataBase.Location = new System.Drawing.Point(143, 215);
            this.txtLocalDataBase.Name = "txtLocalDataBase";
            this.txtLocalDataBase.PasswordChar = '*';
            this.txtLocalDataBase.Size = new System.Drawing.Size(278, 21);
            this.txtLocalDataBase.TabIndex = 12;
            this.txtLocalDataBase.Text = "Data Source=10.132.22.191;Database=DB_YGXF_TZ;User ID=sa;Password=tzhb_sckj2014";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(52, 218);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "本地数据库";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 383);
            this.Controls.Add(this.txtLocalDataBase);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.txtLocalPath);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtDataBase);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtDataBase;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtLocalPath;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtLocalDataBase;
        private System.Windows.Forms.Label label6;
    }
}

