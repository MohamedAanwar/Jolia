using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace s
{
    public partial class Form3 : Form
    {
        string usertype;
        string username;
        public Form3(string user, string usernamee)
        {
            InitializeComponent();
            usertype = user;
            username = usernamee;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            this.Hide();
            form.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (usertype == "Admin")
            {
                openchildform(new add());
            }
            else
            {
                MessageBox.Show("لا يوجد صلاحيه ", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openchildform(new update(username, usertype));
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
        private Form activeform = null;
        private void openchildform(Form childform)
        {
            if (activeform != null)
                activeform.Close();
            activeform = childform;
            childform.TopLevel = false;
            childform.FormBorderStyle = FormBorderStyle.None;
            childform.Dock = DockStyle.Fill;
            panelchildform.Controls.Add(childform);
            panelchildform.Tag = childform;
            childform.BringToFront();
            childform.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (usertype == "Admin")
            {
                openchildform(new delete());
            }
            else
            {
                MessageBox.Show("لا يوجد صلاحيه ", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (usertype == "Admin")
            {
                openchildform(new supliers());
            }
            else
            {
                MessageBox.Show("لا يوجد صلاحيه ", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (usertype == "Admin")
            {
                openchildform(new users());
            }
            else
            {
                MessageBox.Show("لا يوجد صلاحيه ", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (usertype == "Admin")
            {
                openchildform(new add());
            }
            else
            {
                MessageBox.Show("لا يوجد صلاحيه ", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            openchildform(new update(username, usertype));
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (usertype == "Admin")
            {
                openchildform(new delete());
            }
            else
            {
                MessageBox.Show("لا يوجد صلاحيه ", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (usertype == "Admin")
            {
                openchildform(new supliers());
            }
            else
            {
                MessageBox.Show("لا يوجد صلاحيه ", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            if (usertype == "Admin")
            {
                openchildform(new users());
            }
            else
            {
                MessageBox.Show("لا يوجد صلاحيه ", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            this.Hide();
            form.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (usertype == "Admin")
            {
                openchildform(new bins());
            }
            else
            {
                MessageBox.Show("لا يوجد صلاحيه ", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            if (usertype == "Admin")
            {
                openchildform(new bins());
            }
            else
            {
                MessageBox.Show("لا يوجد صلاحيه ", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (usertype == "Admin")
            {
                openchildform(new ooutt());
            }
            else
            {
                MessageBox.Show("لا يوجد صلاحيه ", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            if (usertype == "Admin")
            {
                openchildform(new ooutt());
            }
            else
            {
                MessageBox.Show("لا يوجد صلاحيه ", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (usertype == "Admin")
            {
                openchildform(new statics());
            }
            else
            {
                MessageBox.Show("لا يوجد صلاحيه ", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            if (usertype == "Admin")
            {
                openchildform(new statics());
            }
            else
            {
                MessageBox.Show("لا يوجد صلاحيه ", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
