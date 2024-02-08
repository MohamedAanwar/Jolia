
using s.dataa;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace s
{
    public partial class statics : Form
    {
        public statics()
        {
            InitializeComponent();
        }
        PrintPreviewDialog pd = new PrintPreviewDialog();
        PrintDocument printdoc = new PrintDocument();
        DateTime datetwo;
        StoresContext context = new StoresContext();
        private void statics_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var y = context.Catogries.Sum(a => a.CatPrice * a.CatNumbef).Value.ToString();
            var a = context.Catogries.Sum(a => (a.CatNumbef - a.CatNum) * a.CatPrice).Value.ToString();
            var b = context.Catogries.Sum(a => (a.CatNum * a.CatPrice)).Value.ToString();
            label4.Text = y;
            label6.Text = a;
            label7.Text = b;
            datetwo = DateTime.Now;
            label12.Text = context.Dates.SingleOrDefault(a => a.User == "mo").D.ToString();
            label13.Text = datetwo.ToString();
            timer1.Start();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل تريد الانهاء ؟", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var s = context.Dates.SingleOrDefault(a => a.User == "mo");
                s.D = DateTime.Now;
                context.Update(s);
                context.SaveChanges();
                context.Catogries.ToList().ForEach(a => a.CatNumbef = a.CatNum);


                context.SaveChanges();
                this.Hide();

            }
        }
        int startpoint = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            startpoint += 20;
            label15.Text = startpoint + " %".ToString();

            if (startpoint == 100)
            {

                timer1.Stop();
                panel4.Visible = false;
                label15.Visible = false;


            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            var y = context.Catogries.Sum(a => a.CatPrice * a.CatNumbef).Value.ToString();
            var a = context.Catogries.Sum(a => (a.CatNumbef - a.CatNum) * a.CatPrice).Value.ToString();
            var b = context.Catogries.Sum(a => (a.CatNum * a.CatPrice)).Value.ToString();
            label4.Text = y;
            label6.Text = a;
            label7.Text = b;
            datetwo = DateTime.Now;
            label12.Text = context.Dates.SingleOrDefault(a => a.User == "mo").D.ToString();
            label13.Text = datetwo.ToString();
            timer1.Start();
        }
        Bitmap mem;

        private void pictureBox1_Click(object sender, EventArgs e)
        {


        }
        public void printdoc_printpage(object sender, PrintPageEventArgs e)
        {
            Rectangle pagearea = new Rectangle();
            pagearea.Width = panel10.Width;
            pagearea.Height = panel10.Height;
            pagearea.Size = new Size(panel10.Width, panel10.Height);

            e.Graphics.DrawImage(mem, (pagearea.Width / 12) - (panel10.Width / 12), panel10.Location.Y);
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pictureBox3.Visible = false;
            PrinterSettings printerSettings = new PrinterSettings();
            mem = new Bitmap(panel10.Width, panel10.Height);
            panel10.DrawToBitmap(mem, new Rectangle(0, 0, panel10.Width, panel10.Height));
            pd.Document = printdoc;
            printdoc.PrintPage += new PrintPageEventHandler(printdoc_printpage);
            pd.ShowDialog();

        }
    }
}
