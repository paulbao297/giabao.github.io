namespace App
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dang_nhapbx = new System.Windows.Forms.TextBox();
            this.mat_khaubx = new System.Windows.Forms.TextBox();
            this.bt_dang_nhap = new System.Windows.Forms.Button();
            this.bt_dang_ky = new System.Windows.Forms.Button();
            this.bt_thoat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(80, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "ĐĂNG NHẬP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(3, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "TÀI KHOẢN:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(6, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "MẬT KHẨU:";
            // 
            // dang_nhapbx
            // 
            this.dang_nhapbx.Location = new System.Drawing.Point(109, 100);
            this.dang_nhapbx.Name = "dang_nhapbx";
            this.dang_nhapbx.Size = new System.Drawing.Size(183, 22);
            this.dang_nhapbx.TabIndex = 3;
            // 
            // mat_khaubx
            // 
            this.mat_khaubx.Location = new System.Drawing.Point(109, 165);
            this.mat_khaubx.Name = "mat_khaubx";
            this.mat_khaubx.PasswordChar = '*';
            this.mat_khaubx.Size = new System.Drawing.Size(183, 22);
            this.mat_khaubx.TabIndex = 4;
            // 
            // bt_dang_nhap
            // 
            this.bt_dang_nhap.BackColor = System.Drawing.Color.RoyalBlue;
            this.bt_dang_nhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_dang_nhap.ForeColor = System.Drawing.Color.White;
            this.bt_dang_nhap.Location = new System.Drawing.Point(12, 218);
            this.bt_dang_nhap.Name = "bt_dang_nhap";
            this.bt_dang_nhap.Size = new System.Drawing.Size(347, 48);
            this.bt_dang_nhap.TabIndex = 5;
            this.bt_dang_nhap.Text = "Đăng nhập";
            this.bt_dang_nhap.UseVisualStyleBackColor = false;
            this.bt_dang_nhap.Click += new System.EventHandler(this.bt_dang_nhap_Click);
            // 
            // bt_dang_ky
            // 
            this.bt_dang_ky.BackColor = System.Drawing.Color.DodgerBlue;
            this.bt_dang_ky.ForeColor = System.Drawing.Color.White;
            this.bt_dang_ky.Location = new System.Drawing.Point(34, 272);
            this.bt_dang_ky.Name = "bt_dang_ky";
            this.bt_dang_ky.Size = new System.Drawing.Size(98, 36);
            this.bt_dang_ky.TabIndex = 6;
            this.bt_dang_ky.Text = "Đăng ký";
            this.bt_dang_ky.UseVisualStyleBackColor = false;
            this.bt_dang_ky.Click += new System.EventHandler(this.bt_dang_ky_Click);
            // 
            // bt_thoat
            // 
            this.bt_thoat.Location = new System.Drawing.Point(223, 272);
            this.bt_thoat.Name = "bt_thoat";
            this.bt_thoat.Size = new System.Drawing.Size(98, 36);
            this.bt_thoat.TabIndex = 7;
            this.bt_thoat.Text = "Thoát";
            this.bt_thoat.UseVisualStyleBackColor = true;
            this.bt_thoat.Click += new System.EventHandler(this.bt_thoat_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(371, 320);
            this.Controls.Add(this.bt_thoat);
            this.Controls.Add(this.bt_dang_ky);
            this.Controls.Add(this.bt_dang_nhap);
            this.Controls.Add(this.mat_khaubx);
            this.Controls.Add(this.dang_nhapbx);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "Login";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox dang_nhapbx;
        private System.Windows.Forms.TextBox mat_khaubx;
        private System.Windows.Forms.Button bt_dang_nhap;
        private System.Windows.Forms.Button bt_dang_ky;
        private System.Windows.Forms.Button bt_thoat;
    }
}