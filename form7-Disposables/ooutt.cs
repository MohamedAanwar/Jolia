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
    public partial class ooutt : Form
    {
        StoresContext context = new StoresContext();
        public ooutt()
        {
            InitializeComponent();
        }

        private void ooutt_Load(object sender, EventArgs e)
        {
            dataGridView2.DataSource = context.Consumables.Select(a => new { a.Catname, a.Catnum, a.Date, a.User }).ToList();
            comboBox3.DataSource=context.Consumables.ToList();
            comboBox3.DisplayMember = "catname";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل تريد حذف المستهلكات ؟", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var c = context.Consumables.ToList();
                context.RemoveRange(c);
                context.SaveChanges();
                dataGridView2.DataSource = context.Consumables.ToList();
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
            xlsh.Name = "المستهلك";
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
            save.FileName = "المستهلك";
            save.DefaultExt = ".xlsx";
            if (save.ShowDialog() == DialogResult.OK)
            {
                xlwb.SaveAs(save.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            }
            xlapp.Quit();
            MessageBox.Show("لقد تم تصدير البيانات الي برنامج اكسيل بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = context.Consumables.Where(a => a.Catname.ToLower().Contains(comboBox3.Text.ToLower())).ToList();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

