using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Security.Cryptography;


namespace App
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void bt_dang_nhap_Click(object sender, EventArgs e)
        {
            int key=0;
            try
            {
                
                MongoDB new_mongo = new MongoDB();
                new_mongo.Connect();
                dang_nhap new_dang_nhap = new_mongo.Dang_nhap(dang_nhapbx.Text);
                //hash password
                string hmat_khau = ComputeHash(mat_khaubx.Text, new SHA256CryptoServiceProvider());
                if (hmat_khau == new_dang_nhap.Pass)
                {
                    key = 1;              
                }
                else
                {
                    key = 0;
                }
            }
            catch
            {
                MessageBox.Show("WRONG ACCOUNT !!!");
                dang_nhapbx.Text = "";
                mat_khaubx.Text = "";
                dang_nhapbx.Focus();
            }
            if (key == 1)
            {
                this.Hide();
                Form1 fm = new Form1();
                fm.Closed += (s, args) => this.Close();
                fm.ShowDialog();
                this.Close();
            }
            if (key == 0)
            {
                MessageBox.Show("WRONG PASS !!!");
                dang_nhapbx.Text = "";
                mat_khaubx.Text = "";
                dang_nhapbx.Focus();
            }
            /*MongoDB new_mongo = new MongoDB();
            new_mongo.Connect();
            dang_nhap new_dang_nhap = new dang_nhap();
            new_dang_nhap.Name = dang_nhapbx.Text;
            //hash password
            new_dang_nhap.Pass = ComputeHash(mat_khaubx.Text, new SHA256CryptoServiceProvider());
            var Bson = JsonConvert.SerializeObject(new_dang_nhap);
            var tmp = BsonSerializer.Deserialize<BsonDocument>(Bson);
            new_mongo.Insert(tmp);*/
        }

        private void bt_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public string ComputeHash(string input, HashAlgorithm algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }

        private void bt_dang_ky_Click(object sender, EventArgs e)
        {
            Register nw = new Register();
            nw.ShowDialog();
        }
    }
}
