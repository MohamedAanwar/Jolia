using s.dataa;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Cryptography;

namespace s
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            textBox2.MaxLength = 10;
        }
        StoresContext context = new StoresContext();
        private void Form2_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = context.Logins.ToList();
            comboBox1.DisplayMember = "username";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "" && textBox2.Text != "")
            {
                var user = context.Logins.SingleOrDefault(a => a.Username == comboBox1.Text);
                if (user != null)
                {
                    var sha = SHA256.Create();
                    var asbyarr = Encoding.Default.GetBytes(textBox2.Text);
                    var hash = sha.ComputeHash(asbyarr);
                    if (!user.Pass.Equals(Convert.ToBase64String(hash)))
                    {
                        MessageBox.Show("كلمه مرور غير صحيحه", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        comboBox1.Text = textBox2.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("تم تسجيل الدخول");
                        Form3 f = new Form3(user.UserType, user.Username);
                        this.Hide();
                        f.Show();
                    }
                }
                else
                {
                    MessageBox.Show("خطا في اسم المستخدم او كلمه المرور", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    comboBox1.Text = textBox2.Text = "";
                }
            }
            else
            {
                MessageBox.Show("يجب ادخال البيانات!", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBox1.Text = textBox2.Text = "";
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = checkBox1.Checked ? '\0' : '*';
        }
    }
}
