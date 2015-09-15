using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Amaze.RK_Downloader
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" && textBox2.Text == "")
            {
                MessageBox.Show("Successfully signed in! Welcome to the A.RK");
                this.Hide();
                Form1 Main = new Form1();
                Main.Show();
            }
            else if (t1.Text + t2.Text + t3.Text + t4.Text == "1234")
            {
                MessageBox.Show("Successfully signed in! Welcome to the A.RK");
                this.Hide();
                Form1 Main = new Form1();
                Main.Show();
            }
            else
            {
                MessageBox.Show("Incorrect Username or Password. Are you sure you're a member?");
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void t1_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void t2_TextChanged(object sender, EventArgs e)
        {
            this.ActiveControl = t3;
        }

        private void t1_TextChanged(object sender, EventArgs e)
        {
            this.ActiveControl = t2;
        }

        private void t3_TextChanged(object sender, EventArgs e)
        {
            this.ActiveControl = t4;
        }

        private void t4_TextChanged(object sender, EventArgs e)
        {
            this.ActiveControl = button1;
        }
    }
}
