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

namespace s
{
    public partial class supliers : Form
    {
        public supliers()
        {
            InitializeComponent();
            textBox3.MaxLength = 15;
        }
        StoresContext context = new StoresContext();
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void supliers_Load(object sender, EventArgs e)
        {
            dataGridView2.DataSource = context.Sups.ToList();
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "ادخل اسم المورد")
            {
                textBox3.Text = "";
                textBox3.ForeColor = Color.Black;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (textBox3.Text != "")
            {
                dataGridView2.DataSource = context.Sups.Where(a => a.Sname.ToLower().Contains(textBox3.Text.ToLower())).Select(a => new { a.SId, a.Sname, a.Smoney }).ToList();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                var s = context.Sups.SingleOrDefault(a => a.Sname == textBox2.Text);
                if (s != null)
                {
                    bool a = decimal.TryParse(textBox1.Text, out decimal de);
                    if (a)
                    {
                        if (decimal.Parse(textBox1.Text) != 0 && decimal.Parse(textBox1.Text) > 0)
                        {
                            s.Smoney -= decimal.Parse(textBox1.Text);
                            context.Update(s);
                            context.SaveChanges();
                            dataGridView2.DataSource = context.Sups.ToList();
                            MessageBox.Show("تم دفع المبلغ " + textBox1.Text + " ج.م " + " للمورد " + textBox2.Text, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            textBox1.Text = textBox2.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("لا يمكن ادخال مبلغ اقل من الصفر", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            textBox1.Text = "";
                        }

                    }
                    else
                    {
                        MessageBox.Show("ادخل الرقم الصجيح", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBox1.Text = textBox2.Text = "";
                    }

                }
                else
                {

                    MessageBox.Show("المورد غير موجود", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox2.Text = textBox1.Text = "";
                }
            }
            else
            {
                MessageBox.Show("يجب ادخال البيانات!", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Text = textBox1.Text = "";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                var s = context.Sups.SingleOrDefault(a => a.Sname == textBox2.Text);
                if (s != null)
                {
                    MessageBox.Show("لا يمكن تكرار المورد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox2.Text = "";
                }
                else
                {
                    var ns = new Sup();
                    ns.Sname = textBox2.Text;
                    ns.Smoney = 0;
                    context.Add(ns);
                    context.SaveChanges();
                    dataGridView2.DataSource = context.Sups.ToList();
                    MessageBox.Show("تم اضافة المورد " + textBox2.Text, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox2.Text = "";


                }
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "ادخل اسم المورد")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
            }
        }
        int index;
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                index = e.RowIndex;
                DataGridViewRow dvg = dataGridView2.Rows[index];
                textBox2.Text = dvg.Cells[1].Value.ToString();
                textBox2.ForeColor = Color.Black;

            }
            catch (Exception ex)
            {
                MessageBox.Show("اضغط علي الخانة", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                var s = context.Sups.SingleOrDefault(a => a.Sname == textBox2.Text);
                if (s != null)
                {
                    if (MessageBox.Show("هل تريد حذف المورد ؟", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        if (s.Smoney != 0)
                        {
                            MessageBox.Show("المورد له فواتير يجب دفعها اولا", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            textBox2.Text = "";
                        }
                        else
                        {
                            context.Remove(s);
                            context.SaveChanges();
                            dataGridView2.DataSource = context.Sups.ToList();

                            textBox2.Text = "";
                        }
                    }

                }
                else
                {

                    MessageBox.Show("المورد غير موجود", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox2.Text = "";
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "ادخل المبلغ")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                var s = context.Sups.SingleOrDefault(a => a.Sname == textBox2.Text);
                if (s != null)
                {
                    bool a = decimal.TryParse(textBox1.Text, out decimal de);
                    if (a)
                    {
                        if (decimal.Parse(textBox1.Text) != 0 && decimal.Parse(textBox1.Text) > 0)
                        {
                            var bin = new Bin();
                            bin.Binsup = textBox2.Text;
                            bin.Binmon = decimal.Parse(textBox1.Text);
                            bin.Bindate = DateTime.Now;
                            context.Add(bin);
                            s.Smoney += decimal.Parse(textBox1.Text);
                            context.Update(s);
                            context.SaveChanges();
                            dataGridView2.DataSource = context.Sups.ToList();
                            MessageBox.Show("تم استلام فاتوره بمبلغ " + textBox1.Text + " ج.م " + " من المورد " + textBox2.Text, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            textBox1.Text = textBox2.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("لا يمكن ادخال مبلغ اقل من الصفر", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            textBox1.Text = "";
                        }

                    }
                    else
                    {
                        MessageBox.Show("ادخل الرقم الصجيح", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBox1.Text = textBox2.Text = "";
                    }

                }
                else
                {

                    MessageBox.Show("المورد غير موجود", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox2.Text = textBox1.Text = "";
                }
            }
            else
            {
                MessageBox.Show("يجب ادخال البيانات!", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Text = textBox1.Text = "";
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            bins b = new bins();
            this.Close();
            b.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {

            bins b = new bins();
            this.Close();
            b.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {


        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
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
            xlsh.Name = "باقي حساب الموردين";
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
            save.FileName = "باقي حساب الموردين";
            save.DefaultExt = ".xlsx";
            if (save.ShowDialog() == DialogResult.OK)
            {
                xlwb.SaveAs(save.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            }
            xlapp.Quit();
            MessageBox.Show("لقد تم تصدير البيانات الي برنامج اكسيل بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2.PerformClick();
            }
        }
    }
}
