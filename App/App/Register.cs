using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;


namespace App
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void bt_dky_Click(object sender, EventArgs e)
        {
            MongoDB new_mongo_regis = new MongoDB();
            new_mongo_regis.Connect();
            dang_ky dky = new dang_ky();
            dky.Name = tk_bx.Text;
            dky.Pass = ComputeHash(mk_bx.Text, new SHA256CryptoServiceProvider());
            string code = "982907";
            if (mk_2_bx.Text != mk_bx.Text)
            {
                MessageBox.Show("Mat khau nhap lai khong giong");
                mk_2_bx.Text = "";
                ma_bx.Text = "";
                mk_2_bx.Focus();
            }
            else if (ma_bx.Text == code)
            {
                var Bson = JsonConvert.SerializeObject(dky);
                var tmp = BsonSerializer.Deserialize<BsonDocument>(Bson);
                new_mongo_regis.Insert(tmp);
                this.Close();
            }
            else
            {
                MessageBox.Show("Sai ma xac nhan");
                ma_bx.Text = "";
                ma_bx.Focus();

            }
        }
        public string ComputeHash(string input, HashAlgorithm algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }

        private void bt_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
