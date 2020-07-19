using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();

        }
        private int dem = 0;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dem++;
            if (dem == 3)
            {
                timer1.Stop();
                this.Hide();
                Login dn = new Login();
                dn.ShowDialog();
                this.Close();
            }
        }

        private void Start_Load(object sender, EventArgs e)
        {
            timer1.Start();

        }
    }
}
