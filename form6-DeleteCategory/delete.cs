using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace s
{
    public partial class delete : Form
    {
        public delete()
        {
            InitializeComponent();
            textBox3.MaxLength = 15;
        }
        StoresContext context = new StoresContext();
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (MessageBox.Show("هل تريد حذف الصنف ؟", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var s = context.Catogries.SingleOrDefault(a => a.CatName == textBox1.Text);
                    if (s != null)
                    {
                        context.Catogries.Remove(s);
                        context.SaveChanges();
                        dataGridView2.DataSource = context.Catogries.ToList();
                        textBox1.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("الصنف غير متوفر", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBox1.Text = "";
                    }

                }

            }
            else
            {
                MessageBox.Show("يجب ادخال البيانات", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        int index;
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {





        }

        private void delete_Load(object sender, EventArgs e)
        {
            dataGridView2.DataSource = context.Catogries.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                dataGridView2.DataSource = context.Catogries.Where(a => a.CatName.ToLower().Contains(textBox3.Text.ToLower())).ToList();
            }
            else
            {
                MessageBox.Show("يجب ادخال البيانات", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "ادخل اسم الصنف")
            {
                textBox3.Text = "";
                textBox3.ForeColor = Color.Black;
            }

        }

        private void textBox3_Leave(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2.PerformClick();
            }
        }

        private void dataGridView2_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                index = e.RowIndex;
                DataGridViewRow dvg = dataGridView2.Rows[index];
                textBox1.Text = dvg.Cells[1].Value.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show("اضغط علي الخانة", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
