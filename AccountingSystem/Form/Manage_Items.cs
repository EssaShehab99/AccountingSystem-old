using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AccountingSystem
{
    public partial class Manage_Items : Form
    {
        //messagw 147-160
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-OQGE058\MSSQLSERVER,1433; Initial Catalog=accountingsystem;Integrated Security=false; User ID=sa;password=Essa771838957;");
        int testclick = 0;
        public Manage_Items(string user_type)
        {
            InitializeComponent();
            if (user_type == "user")
            {
                buttonX5.Enabled = false;
                buttonX4.Enabled = false;
                dateTimePicker1.Enabled = false;
            }
        }
        private void inserttodataGridView()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select item_id as 'رقم الصنف',item_name as 'اسم الصنف',item_amount as 'سعر الصنف',amount_currency'العملة',item_description as 'وصف الصنف',item_date as 'تاريخ الصنف' from items", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridViewX1.DataSource = dt;
            }
            catch
            {
                MessageBox.Show("مشكلة في عرض المواد ", "147", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
        private void Manage_Items_Load(object sender, EventArgs e)
        {
            inserttodataGridView();
            comboBoxEx.Items.Add("ريال يمني");
            comboBoxEx.Items.Add("ريال سعودي");
            comboBoxEx.Items.Add("دولار أمريكي");
            comboBoxEx.Items.Add("درهم إماراتي");
            comboBoxEx.Items.Add("ريال قطري");
            comboBoxEx.Text = "ريال يمني";
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            testclick = 0;
            SqlCommand cmd = new SqlCommand("insert into items(item_name,item_amount,amount_currency,item_description,item_date)values(N'" + textBoxX6.Text + "'," + textBoxX1.Text + ",N'" + comboBoxEx.Text + "',N'" + textBoxX5.Text + "','" + dateTimePicker1.Value + "')", con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("تم إضافة الصنف بنجاح", "148", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch
            {
                MessageBox.Show("لم تتم العملية بنجاح", "149", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
                inserttodataGridView();
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            testclick = 0;
            try
            {
                if (textBoxX6.Text != "" && textBoxX5.Text == "")
                {
                    SqlDataAdapter da = new SqlDataAdapter("select item_id as 'رقم الصنف',item_name as 'اسم الصنف',item_amount as 'سعر الصنف',amount_currency'العملة',item_description as 'وصف الصنف',item_date as 'تاريخ الصنف' from items where item_name like N'%" + textBoxX6.Text + "%'", con);

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridViewX1.DataSource = dt;
                }
                else if (textBoxX5.Text != "" && textBoxX6.Text == "")
                {
                    SqlDataAdapter da = new SqlDataAdapter("select item_id as 'رقم الصنف',item_name as 'اسم الصنف',item_amount as 'سعر الصنف',amount_currency'العملة',item_description as 'وصف الصنف',item_date as 'تاريخ الصنف' from items where item_description like N'%" + textBoxX5.Text + "%'", con);

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridViewX1.DataSource = dt;
                }
                else if (textBoxX5.Text != "" && textBoxX6.Text != "")
                {
                    SqlDataAdapter da = new SqlDataAdapter("select item_id as 'رقم الصنف',item_name as 'اسم الصنف',item_amount as 'سعر الصنف',amount_currency'العملة',item_description as 'وصف الصنف',item_date as 'تاريخ الصنف' from items where item_description like N'%" + textBoxX5.Text + "%' and item_name like N'%" + textBoxX6.Text + "%'", con);

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridViewX1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("الرجاء تحديد اسم الصنف أو وصفه", "150", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }

            }
            catch
            {
                MessageBox.Show("مشكلة في عرض المواد ", "151", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void dataGridViewX1_MouseClick(object sender, MouseEventArgs e)
        {
            testclick = 1;
            try
            {
                textBoxX6.Text = dataGridViewX1.SelectedRows[0].Cells[1].Value.ToString();
                textBoxX1.Text = dataGridViewX1.SelectedRows[0].Cells[2].Value.ToString();
                comboBoxEx.Text = dataGridViewX1.SelectedRows[0].Cells[3].Value.ToString();
                textBoxX5.Text = dataGridViewX1.SelectedRows[0].Cells[4].Value.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dataGridViewX1.SelectedRows[0].Cells[5].Value.ToString());

                con.Close();
            }
            catch
            {
                MessageBox.Show("مشكلة في عرض بيانات العملية", "152", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            finally
            {
                con.Close();
            }

        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            inserttodataGridView();
            timer2.Start();

        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            if (testclick == 1)
            {
                DialogResult result = MessageBox.Show("هل تريد الاستمرار في عملية التعديل", "153", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    testclick = 0;
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Update items set item_name=N'" + textBoxX6.Text + "',item_amount=" + textBoxX1.Text + ",amount_currency=N'" + comboBoxEx.Text + "',item_description=N'" + textBoxX5.Text + "',item_date='" + dateTimePicker1.Value + "' where item_id=" + dataGridViewX1.SelectedRows[0].Cells[0].Value.ToString() + "", con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("تم تعديل بيانات الصنف بنجاح", "154", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        MessageBox.Show("لم تتم العملية بنجاح", "155", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        con.Close();
                        inserttodataGridView();
                    }

                }
            }
            else
            {
                MessageBox.Show("الرجاء تحديد العملية المراد تعديلها", "156", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString("dd/M/yyyy,  hh:mm:ss");
            timer1.Start();

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            dateTimePicker1.Value = Convert.ToDateTime(DateTime.Now.ToString());
            timer2.Start();

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            timer2.Stop();

        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            if (testclick == 1)
            {
                testclick = 0;

                DialogResult result = MessageBox.Show("هل تريد الاستمرار في عملية الحذف", "157", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd1 = new SqlCommand("delete items where item_id=" + dataGridViewX1.SelectedRows[0].Cells[0].Value + "", con);
                        cmd1.ExecuteNonQuery();
                        MessageBox.Show("تم حذف الصنف بنجاح", "158", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    catch
                    {
                        MessageBox.Show("لم تتم العملية بنجاح", "159", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        con.Close();
                        inserttodataGridView();
                    }

                }
            }
            else
            {
                MessageBox.Show("الرجاء تحديد العملية المراد حذفها", "160", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}