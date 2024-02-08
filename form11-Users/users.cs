using s.dataa;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace s
{
    public partial class users : Form
    {
        public users()
        {
            InitializeComponent();
            textBox2.MaxLength = textBox4.MaxLength = 10;
            textBox1.MaxLength = 15;

        }
        StoresContext context = new StoresContext();
        private void users_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = context.Logins.ToList();
            comboBox2.DataSource = context.Logins.ToList();
            comboBox1.DisplayMember = "username";
            comboBox2.DisplayMember = "username";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                var s = new Login();
                if (comboBox3.Text != "Admin" && comboBox3.Text != "User")
                {
                    MessageBox.Show("خطا في نوع المستخدم", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox1.Text = textBox2.Text = comboBox3.Text = "";
                }
                else
                {
                    s.UserType = comboBox3.Text;
                    s.Username = textBox1.Text;
                    var sha = SHA256.Create();
                    var asbyarr = Encoding.Default.GetBytes(textBox2.Text);
                    var hash = sha.ComputeHash(asbyarr);

                    s.Pass = Convert.ToBase64String(hash);
                    context.Add(s);
                    context.SaveChanges();
                    comboBox1.DataSource = context.Logins.ToList();
                    comboBox2.DataSource = context.Logins.ToList();

                    textBox1.Text = textBox2.Text = comboBox3.Text = "";
                    MessageBox.Show("تم اضافه المستخدم", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }
            else
            {
                MessageBox.Show("يجب ادخال البيانات!", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {

                var s = context.Logins.SingleOrDefault(a => a.Username == comboBox1.Text);
                if (s != null)
                {
                    var sha = SHA256.Create();
                    var asbyarr = Encoding.Default.GetBytes(textBox4.Text);
                    var hash = sha.ComputeHash(asbyarr);
                    s.Pass = Convert.ToBase64String(hash);
                    context.Update(s);
                    context.SaveChanges();
                    textBox4.Text = "";
                    MessageBox.Show("تم تغير كلمة المرور", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("خطا في اسم المستخدم", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    comboBox1.Text = "";
                    textBox4.Text = "";
                }
            }
            else
            {
                MessageBox.Show("يجب ادخال البيانات!", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }



        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text != "")
            {
                if (MessageBox.Show("هل تريد ازالة المستخدم ؟", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var s = context.Logins.SingleOrDefault(a => a.Username == comboBox2.Text);
                    if (s != null)
                    {
                        context.Remove(s);
                        context.SaveChanges();
                        comboBox2.DataSource = context.Logins.ToList();
                        comboBox1.DataSource = context.Logins.ToList();
                        MessageBox.Show("تم ازالة المستخدم", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("خطا في اسم المستخدم", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        comboBox2.Text = "";
                    }

                }

            }
            else
            {
                MessageBox.Show("يجب ادخال البيانات!", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = checkBox1.Checked ? '\0' : '*';
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            textBox4.PasswordChar = checkBox2.Checked ? '\0' : '*';
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
