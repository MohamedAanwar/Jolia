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
    public partial class bins : Form
    {
        public bins()
        {
            InitializeComponent();

        }
        StoresContext context = new StoresContext();
        private void bins_Load(object sender, EventArgs e)
        {
            dataGridView2.DataSource = context.Bins.ToList();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = context.Bins.Where(a => a.Binsup.ToLower().Contains(textBox3.Text.ToLower())).ToList();
        }
        int index;
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                index = e.RowIndex;
                DataGridViewRow dvg = dataGridView2.Rows[index];
                label2.Text = dvg.Cells[0].Value.ToString();
                label3.Text = dvg.Cells[1].Value.ToString();
                label4.Text = dvg.Cells[2].Value.ToString();



            }
            catch (Exception ex)
            {
                MessageBox.Show("اضغط علي الخانة", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (label2.Text == "حذف")
            {
                MessageBox.Show("اضغط علي الفاتورة", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                if (MessageBox.Show("هل تريد حذف الفاتورة ؟", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var b = context.Bins.SingleOrDefault(a => a.BinId == int.Parse(label2.Text));
                    var supname = context.Sups.SingleOrDefault(a => a.Sname == label3.Text);
                    supname.Smoney -= decimal.Parse(label4.Text);
                    context.Update(supname);
                    context.Remove(b);
                    context.SaveChanges();
                    dataGridView2.DataSource = context.Bins.ToList();
                }


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
            xlsh.Name = "فواتير الموردين";
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
            save.FileName = "فواتير الموردين";
            save.DefaultExt = ".xlsx";
            if (save.ShowDialog() == DialogResult.OK)
            {
                xlwb.SaveAs(save.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            }
            xlapp.Quit();
            MessageBox.Show("لقد تم تصدير البيانات الي برنامج اكسيل بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "ادخل اسم المورد")
            {
                textBox3.Text = "";
                textBox3.ForeColor = Color.Black;
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2.PerformClick();
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {

        }
    }
}
