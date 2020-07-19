namespace App
{
    partial class Register
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Register));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tk_bx = new System.Windows.Forms.TextBox();
            this.mk_2_bx = new System.Windows.Forms.TextBox();
            this.mk_bx = new System.Windows.Forms.TextBox();
            this.ma_bx = new System.Windows.Forms.TextBox();
            this.bt_dky = new System.Windows.Forms.Button();
            this.bt_thoat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(137, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "ĐĂNG KÝ";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(75, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "TÀI KHOẢN:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(75, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "MẬT KHẨU:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 202);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(167, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "NHẬP LẠI MẬT KHẨU:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(52, 246);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "MÃ XÁC NHẬN:";
            // 
            // tk_bx
            // 
            this.tk_bx.Location = new System.Drawing.Point(181, 106);
            this.tk_bx.Name = "tk_bx";
            this.tk_bx.Size = new System.Drawing.Size(183, 22);
            this.tk_bx.TabIndex = 5;
            this.tk_bx.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // mk_2_bx
            // 
            this.mk_2_bx.Location = new System.Drawing.Point(181, 197);
            this.mk_2_bx.Name = "mk_2_bx";
            this.mk_2_bx.PasswordChar = '*';
            this.mk_2_bx.Size = new System.Drawing.Size(183, 22);
            this.mk_2_bx.TabIndex = 6;
            // 
            // mk_bx
            // 
            this.mk_bx.Location = new System.Drawing.Point(181, 151);
            this.mk_bx.Name = "mk_bx";
            this.mk_bx.PasswordChar = '*';
            this.mk_bx.Size = new System.Drawing.Size(183, 22);
            this.mk_bx.TabIndex = 7;
            // 
            // ma_bx
            // 
            this.ma_bx.Location = new System.Drawing.Point(181, 241);
            this.ma_bx.Name = "ma_bx";
            this.ma_bx.PasswordChar = '*';
            this.ma_bx.Size = new System.Drawing.Size(183, 22);
            this.ma_bx.TabIndex = 8;
            // 
            // bt_dky
            // 
            this.bt_dky.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.bt_dky.Location = new System.Drawing.Point(97, 293);
            this.bt_dky.Name = "bt_dky";
            this.bt_dky.Size = new System.Drawing.Size(93, 39);
            this.bt_dky.TabIndex = 9;
            this.bt_dky.Text = "Đăng ký";
            this.bt_dky.UseVisualStyleBackColor = false;
            this.bt_dky.Click += new System.EventHandler(this.bt_dky_Click);
            // 
            // bt_thoat
            // 
            this.bt_thoat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.bt_thoat.Location = new System.Drawing.Point(287, 293);
            this.bt_thoat.Name = "bt_thoat";
            this.bt_thoat.Size = new System.Drawing.Size(93, 39);
            this.bt_thoat.TabIndex = 10;
            this.bt_thoat.Text = "Thoát";
            this.bt_thoat.UseVisualStyleBackColor = false;
            this.bt_thoat.Click += new System.EventHandler(this.bt_thoat_Click);
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(435, 359);
            this.Controls.Add(this.bt_thoat);
            this.Controls.Add(this.bt_dky);
            this.Controls.Add(this.ma_bx);
            this.Controls.Add(this.mk_bx);
            this.Controls.Add(this.mk_2_bx);
            this.Controls.Add(this.tk_bx);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Register";
            this.Text = "Register";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tk_bx;
        private System.Windows.Forms.TextBox mk_2_bx;
        private System.Windows.Forms.TextBox mk_bx;
        private System.Windows.Forms.TextBox ma_bx;
        private System.Windows.Forms.Button bt_dky;
        private System.Windows.Forms.Button bt_thoat;
    }
}