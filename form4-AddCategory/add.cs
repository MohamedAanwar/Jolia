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
    public partial class add : Form
    {
        StoresContext context = new StoresContext();
        public add()
        {
            InitializeComponent();
            dataGridView2.DataSource = context.Catogries.ToList();
            textBox3.MaxLength = 15;
        }

        private void add_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                var s = context.Catogries.SingleOrDefault(a => a.CatName == textBox1.Text);
                if (s != null)
                {
                    MessageBox.Show("لا يمكن تكرار الصنف", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox2.Text = textBox1.Text = textBox4.Text = "";
                }
                else
                {
                    Catogry catogry = new Catogry();
                    if (textBox1.Text != "" && textBox2.Text != "")
                    {
                        catogry.CatName = textBox1.Text;
                        bool t = decimal.TryParse(textBox2.Text, out decimal id);
                        bool a = decimal.TryParse(textBox4.Text, out decimal idd);
                        if (t & a)
                        {
                            if (decimal.Parse(textBox2.Text) != 0 && decimal.Parse(textBox2.Text) > 0)
                            {
                                catogry.CatNum = decimal.Parse(textBox2.Text);
                                catogry.CatNumbef = decimal.Parse(textBox2.Text);
                                catogry.CatPrice = decimal.Parse(textBox4.Text);
                                context.Add(catogry);
                                context.SaveChanges();
                                dataGridView2.DataSource = context.Catogries.ToList();
                                MessageBox.Show("تم اضافة صنف جديد", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                textBox1.Text = textBox2.Text = textBox4.Text = "";
                            }
                            else
                            {
                                MessageBox.Show("لا يمكن ادخال عدد اقل من الصفر", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                textBox2.Text = "";
                            }
                        }
                        else
                        {
                            MessageBox.Show("يجب ادخال ارقام فقط!", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            textBox2.Text = textBox4.Text = "";
                        }

                    }
                }


            }
            else
            {
                MessageBox.Show("يجب ادخال البيانات", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }





        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

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

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (textBox3.Text != "ادخل اسم الصنف")
            {
                dataGridView2.DataSource = context.Catogries.Where(a => a.CatName.Contains(textBox3.Text)).ToList();
            }
            else
            {
                MessageBox.Show("يجب ادخال البيانات", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2.PerformClick();
            }
        }
        int index;
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                index = e.RowIndex;
                DataGridViewRow dvg = dataGridView2.Rows[index];
                textBox1.Text = dvg.Cells[1].Value.ToString();
                textBox2.Text = dvg.Cells[3].Value.ToString();
                textBox4.Text = dvg.Cells[2].Value.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show("اضغط علي الخانة", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            DataObject cd = dataGridView2.GetClipboardContent();
            if (cd != null)
            {
                Clipboard.SetDataObject(cd);
            }
            Microsoft.Office.Interop.Excel.Application xlapp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlwb = xlapp.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet xlsh = null;
            xlsh = xlwb.Sheets["Sheet1"];
            xlsh = xlwb.ActiveSheet;
            xlsh.Name = "الاصناف";
            for (int i = 1; i < dataGridView2.Columns.Count + 1; i++)
            {
                xlsh.Cells[1, i] = dataGridView2.Columns[i - 1].HeaderText;
            }
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView2.Columns.Count; j++)
                {
                    xlsh.Cells[i + 2, j + 1] = dataGridView2.Rows[i].Cells[j].Value.ToString();
                }
            }

            var save = new SaveFileDialog();
            save.FileName = "الاصناف";
            save.DefaultExt = ".xlsx";
            if (save.ShowDialog() == DialogResult.OK)
            {
                xlwb.SaveAs(save.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            }
            xlapp.Quit();
            MessageBox.Show("لقد تم تصدير البيانات الي برنامج اكسيل بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}