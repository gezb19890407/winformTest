namespace 美圣短信发送
{
    partial class Form1
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
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtParam1 = new System.Windows.Forms.TextBox();
            this.txtParam3 = new System.Windows.Forms.TextBox();
            this.txtParam4 = new System.Windows.Forms.TextBox();
            this.txtParam5 = new System.Windows.Forms.TextBox();
            this.txtParam2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTypeValue = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtBackValue = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtContentValue = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(406, 379);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "发送";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(123, 45);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(153, 21);
            this.txtPhone.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "手机号码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "短信模版";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(71, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "参数1";
            // 
            // cmbType
            // 
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(123, 80);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(153, 20);
            this.cmbType.TabIndex = 6;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(297, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "参数2";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(71, 200);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "参数3";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(297, 200);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "参数4";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(71, 237);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "参数5";
            // 
            // txtParam1
            // 
            this.txtParam1.Location = new System.Drawing.Point(123, 164);
            this.txtParam1.Name = "txtParam1";
            this.txtParam1.Size = new System.Drawing.Size(153, 21);
            this.txtParam1.TabIndex = 11;
            // 
            // txtParam3
            // 
            this.txtParam3.Location = new System.Drawing.Point(123, 197);
            this.txtParam3.Name = "txtParam3";
            this.txtParam3.Size = new System.Drawing.Size(153, 21);
            this.txtParam3.TabIndex = 12;
            // 
            // txtParam4
            // 
            this.txtParam4.Location = new System.Drawing.Point(338, 197);
            this.txtParam4.Name = "txtParam4";
            this.txtParam4.Size = new System.Drawing.Size(153, 21);
            this.txtParam4.TabIndex = 13;
            // 
            // txtParam5
            // 
            this.txtParam5.Location = new System.Drawing.Point(123, 234);
            this.txtParam5.Name = "txtParam5";
            this.txtParam5.Size = new System.Drawing.Size(153, 21);
            this.txtParam5.TabIndex = 14;
            // 
            // txtParam2
            // 
            this.txtParam2.Location = new System.Drawing.Point(338, 164);
            this.txtParam2.Name = "txtParam2";
            this.txtParam2.Size = new System.Drawing.Size(153, 21);
            this.txtParam2.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(53, 125);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 16;
            this.label8.Text = "模版内容";
            // 
            // txtTypeValue
            // 
            this.txtTypeValue.Location = new System.Drawing.Point(123, 122);
            this.txtTypeValue.Name = "txtTypeValue";
            this.txtTypeValue.Size = new System.Drawing.Size(358, 21);
            this.txtTypeValue.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(71, 411);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 18;
            this.label9.Text = "返回值";
            // 
            // txtBackValue
            // 
            this.txtBackValue.Location = new System.Drawing.Point(123, 408);
            this.txtBackValue.Name = "txtBackValue";
            this.txtBackValue.Size = new System.Drawing.Size(358, 21);
            this.txtBackValue.TabIndex = 19;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(53, 287);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 20;
            this.label10.Text = "内容预览";
            // 
            // txtContentValue
            // 
            this.txtContentValue.Location = new System.Drawing.Point(123, 284);
            this.txtContentValue.Multiline = true;
            this.txtContentValue.Name = "txtContentValue";
            this.txtContentValue.Size = new System.Drawing.Size(358, 89);
            this.txtContentValue.TabIndex = 21;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(406, 232);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 22;
            this.button2.Text = "预览";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 468);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtContentValue);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtBackValue);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtTypeValue);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtParam2);
            this.Controls.Add(this.txtParam5);
            this.Controls.Add(this.txtParam4);
            this.Controls.Add(this.txtParam3);
            this.Controls.Add(this.txtParam1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtParam1;
        private System.Windows.Forms.TextBox txtParam3;
        private System.Windows.Forms.TextBox txtParam4;
        private System.Windows.Forms.TextBox txtParam5;
        private System.Windows.Forms.TextBox txtParam2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTypeValue;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtBackValue;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtContentValue;
        private System.Windows.Forms.Button button2;
    }
}

