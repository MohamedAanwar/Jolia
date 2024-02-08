using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using s.dataa;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace s
{
    public partial class update : Form
    {
        string username;
        string usertype;
        public update(string usernamee, string usertypee)
        {
            InitializeComponent();
            username = usernamee;
            usertype = usertypee;
            textBox3.MaxLength = 15;

        }
        int index;
        StoresContext context = new StoresContext();
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                index = e.RowIndex;
                DataGridViewRow dvg = dataGridView2.Rows[index];


                textBox1.Text = dvg.Cells[0].Value.ToString();
                label4.Text = textBox4.Text = dvg.Cells[1].Value.ToString();
                textBox5.Text = dvg.Cells[2].Value.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show("اضغط علي الخانة", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void update_Load(object sender, EventArgs e)
        {
            dataGridView2.DataSource = context.Catogries.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (usertype == "Admin")
            {


                if (textBox1.Text != "")
                {
                    bool f = int.TryParse(textBox1.Text, out int d);
                    if (f)
                    {
                        var add = context.Catogries.SingleOrDefault(a => a.CatId == int.Parse(textBox1.Text));
                        var bef = add.CatNum;
                        if (add != null)
                        {
                            bool c = decimal.TryParse(textBox2.Text, out decimal a);
                            bool q = decimal.TryParse(textBox6.Text, out decimal z);
                            if (c && q)
                            {
                                if (decimal.Parse(textBox2.Text) != 0 && decimal.Parse(textBox2.Text) > 0)
                                {
                                    add.CatNum += decimal.Parse(textBox2.Text);
                                    add.CatNumbef += decimal.Parse(textBox2.Text);
                                    context.Update(add);
                                    context.SaveChanges();
                                    if (textBox3.Text == "ادخل اسم الصنف")
                                    {
                                        dataGridView2.DataSource = context.Catogries.ToList();
                                    }
                                    else
                                    {
                                        dataGridView2.DataSource = context.Catogries.Where(a => a.CatName.ToLower().Contains(textBox3.Text.ToLower())).ToList();
                                        
                                    }
                                    MessageBox.Show("تم تزويد " + label4.Text + " بمقدار " + textBox2.Text+" - "+"قبل: "+bef+" -- بعد:"+add.CatNum, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    textBox1.Text = textBox4.Text = textBox5.Text = "";
                                    textBox2.Text = textBox6.Text = "0";
                                }
                                else
                                {
                                    MessageBox.Show("لا يمكن ادخال مبلغ اقل من الصفر", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    textBox2.Text = textBox6.Text = "0";
                                }
                            }
                            else
                            {
                                MessageBox.Show("ادخل الرقم الصجيح", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                textBox1.Text = textBox4.Text = textBox5.Text = "";
                                textBox2.Text = textBox6.Text = "0";
                            }

                        }
                        else
                        {
                            MessageBox.Show("الصنف غير متوفر", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            textBox1.Text = textBox4.Text = textBox5.Text = "";
                            textBox2.Text = textBox6.Text = "0";
                        }

                    }
                    else
                    {
                        MessageBox.Show("ادخل الرقم الصجيح", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBox1.Text = textBox4.Text = textBox5.Text = "";
                        textBox2.Text = textBox6.Text = "0";
                    }

                }
                else
                {
                    MessageBox.Show("يجب ادخال البيانات!", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox2.Text = textBox6.Text = "0";
                }

            }
            else
            {
                MessageBox.Show("لا يوجد صلاحيه ", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Text = textBox4.Text = textBox5.Text = "";
                textBox2.Text = textBox6.Text = "0";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                bool f = int.TryParse(textBox1.Text, out int d);
                if (f)
                {
                    var add = context.Catogries.SingleOrDefault(a => a.CatId == int.Parse(textBox1.Text));
                    var bef = add.CatNum;
                    var bef1 = add.CatNumbef;
                    if (add != null)
                    {
                        bool c = decimal.TryParse(textBox2.Text, out decimal a);
                        bool q = decimal.TryParse(textBox6.Text, out decimal z);
                        if (c && q)
                        {

                            if (decimal.Parse(textBox2.Text) >= 0)
                            {
                                if (int.Parse(textBox2.Text) == 0)
                                {
                                    add.CatNum -= decimal.Parse(textBox6.Text);
                                    context.Update(add);
                                    context.SaveChanges();
                                    if (textBox3.Text == "ادخل اسم الصنف")
                                    {
                                        dataGridView2.DataSource = context.Catogries.ToList();
                                    }
                                    else
                                    {
                                        dataGridView2.DataSource = context.Catogries.Where(a => a.CatName.ToLower().Contains(textBox3.Text.ToLower())).ToList();

                                    }
                                    MessageBox.Show("تم نقص " + label4.Text + " بمقدار " + textBox6.Text + " - " + "قبل: " + bef + " -- بعد:" + add.CatNum, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    if (add.CatNum < 5 && add.CatNum != 0)
                                    {
                                        MessageBox.Show("احذر رصيد الصنف " + " - " + add.CatName + " - " + " قليل ", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                    }
                                    else if (Convert.ToInt64(add.CatNum) == 0)
                                    {
                                        MessageBox.Show("احذر نفذ الصنف " + " - " + add.CatName, "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                    var con = context.Consumables.SingleOrDefault(a => a.Catname == add.CatName);
                                    if (con != null)
                                    {
                                        con.Catnum += decimal.Parse(textBox6.Text);
                                        context.SaveChanges();
                                    }
                                    else
                                    {
                                        Consumable consum = new Consumable();
                                        consum.Catname = add.CatName;
                                        consum.Catnum = decimal.Parse(textBox6.Text);
                                        consum.Date = DateTime.Now;
                                        consum.User = username;
                                        context.Add(consum);
                                        context.SaveChanges();
                                    }
                                    textBox1.Text = textBox4.Text = textBox5.Text = "";
                                    textBox2.Text = textBox6.Text = "0";
                                }
                                else
                                {
                                    if (usertype == "Admin")
                                    {

                                        add.CatNum -= decimal.Parse(textBox2.Text);
                                        add.CatNumbef -= decimal.Parse(textBox2.Text);
                                        context.Update(add);
                                        context.SaveChanges();
                                        if (textBox3.Text == "ادخل اسم الصنف")
                                        {
                                            dataGridView2.DataSource = context.Catogries.ToList();
                                        }
                                        else
                                        {
                                            dataGridView2.DataSource = context.Catogries.Where(a => a.CatName.ToLower().Contains(textBox3.Text.ToLower())).ToList();

                                        }
                                        MessageBox.Show("تم نقص " + label4.Text + " بمقدار " + textBox2.Text + " - " + "قبل: " + bef1 + " -- بعد:" + add.CatNumbef, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        if (add.CatNum < 5 && add.CatNum != 0)
                                        {
                                            MessageBox.Show("احذر رصيد الصنف " + " - " + add.CatName + " - " + " قليل ", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                        }
                                        else if (Convert.ToInt64(add.CatNum) == 0)
                                        {
                                            MessageBox.Show("احذر نفذ الصنف " + " - " + add.CatName, "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }

                                        textBox1.Text = textBox4.Text = textBox5.Text = "";
                                        textBox2.Text = textBox6.Text = "0";
                                    }
                                    else
                                    {
                                        MessageBox.Show("لا يوجد صلاحيه ", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        textBox1.Text = textBox4.Text = textBox5.Text = "";
                                        textBox2.Text = textBox6.Text = "0";
                                    }

                                }




                            }
                            else
                            {
                                MessageBox.Show("لا يمكن ادخال مبلغ اقل من الصفر", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                textBox2.Text = textBox6.Text = "0";
                            }


                        }
                        else
                        {
                            MessageBox.Show("ادخل الرقم الصجيح", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            textBox1.Text = textBox4.Text = textBox5.Text = "";
                            textBox2.Text = textBox6.Text = "0";
                        }

                    }
                    else
                    {
                        MessageBox.Show("الصنف غير متوفر", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBox1.Text = textBox4.Text = textBox5.Text = "";
                        textBox2.Text = textBox6.Text = "0";
                    }

                }
                else
                {
                    MessageBox.Show("ادخل الرقم الصجيح", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox1.Text = textBox4.Text = textBox5.Text = "";
                    textBox2.Text = textBox6.Text = "0";
                }

            }
            else
            {
                MessageBox.Show("يجب ادخال البيانات!", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Text = textBox6.Text = "0";
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                dataGridView2.DataSource = context.Catogries.Where(a => a.CatName.ToLower().Contains(textBox3.Text.ToLower())).ToList();
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
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

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)

        {
            if (usertype == "Admin")
            {
                if (textBox4.Text != "" && textBox1.Text != "")
                {

                    bool q = decimal.TryParse(textBox5.Text, out decimal z);
                    if (q)
                    {
                        if (decimal.Parse(textBox5.Text) != 0 && decimal.Parse(textBox5.Text) > 0)
                        {
                            var upd = context.Catogries.SingleOrDefault(s => s.CatId == int.Parse(textBox1.Text));
                            if (upd != null)
                            {
                                upd.CatName = textBox4.Text;
                                upd.CatPrice = decimal.Parse(textBox5.Text);

                                context.Update(upd);
                                context.SaveChanges();
                                if (textBox3.Text == "ادخل اسم الصنف")
                                {
                                    dataGridView2.DataSource = context.Catogries.ToList();
                                }
                                else
                                {
                                    dataGridView2.DataSource = context.Catogries.Where(a => a.CatName.ToLower().Contains(textBox3.Text.ToLower())).ToList();

                                }
                                textBox1.Text = textBox4.Text = textBox5.Text = "";
                                MessageBox.Show("تم تعديل البيانات", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("خطا في رقم الصنف", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                textBox1.Text = textBox4.Text = textBox5.Text = "";
                            }

                        }
                        else
                        {
                            MessageBox.Show("لا يمكن ادخال مبلغ اقل من الصفر", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            textBox5.Text = "";
                        }
                    }
                    else
                    {
                        MessageBox.Show("ادخل الرقم الصجيح", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBox5.Text = "";
                        textBox2.Text = textBox6.Text = "0";
                    }

                }
                else
                {
                    MessageBox.Show("يجب ادخال البيانات!", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox1.Text = textBox4.Text = "";
                }
            }
            else
            {
                MessageBox.Show("لا يوجد صلاحيه ", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Text = textBox4.Text = "";
            }

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                index = e.RowIndex;
                DataGridViewRow dvg = dataGridView2.Rows[index];


                textBox1.Text = dvg.Cells[0].Value.ToString();
                label4.Text = textBox4.Text = dvg.Cells[1].Value.ToString();
                textBox5.Text = dvg.Cells[2].Value.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show("اضغط علي الخانة", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2.PerformClick();
            }
        }

        private void dataGridView2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "0")
            {
                textBox2.Text = "";

            }
        }

        private void textBox6_Enter(object sender, EventArgs e)
        {
            if (textBox6.Text == "0")
            {
                textBox6.Text = "";

            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
