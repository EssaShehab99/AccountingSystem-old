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
    public partial class Found_Operation : Form
    {
        //messagw 110-124
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-OQGE058\MSSQLSERVER,1433; Initial Catalog=accountingsystem;Integrated Security=false; User ID=sa;password=Essa771838957;");
        public Found_Operation()
        {
            InitializeComponent();

        }

        private void Found_Operation_Load(object sender, EventArgs e)
        {
            comboBoxEx3.Items.Add("سحب");
            comboBoxEx3.Items.Add("إيداع");
            comboBoxEx3.Items.Add("تحويل بين حسابين");
            comboBoxEx3.Items.Add("مصارفة عملات");

            comboBoxEx1.Items.Add("رقم العملية");
            comboBoxEx1.Items.Add("وصف العملية/ملاحظة");
            comboBoxEx1.Items.Add("مدة");

            comboBoxEx3.Text = "إيداع";
            comboBoxEx1.Text = "رقم العملية";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
            timer1.Start();

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {



            switch (comboBoxEx3.Text)
            {
                //1m
                case "إيداع":

                    switch (comboBoxEx1.Text)
                    {
                        case "رقم العملية":
                            try
                            {
                                SqlDataAdapter da = new SqlDataAdapter("select d.operation_id as'رقم العملية',d.amount المبلغ,d.details as 'التفاصيل',m.account_name as 'اسم الحساب',u.user_name المستخدم,d.number_edit as'عدد التعديلات',d.operation_date التاريخ from deposit_operation d join users u on(d.user_id =u.user_id) join main_accounts m on(d.account_id=m.account_id) where operation_id=" + textBoxX2.Text + "", con);
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                dataGridViewX1.DataSource = dt;
                                try
                                {
                                    SqlCommand cmd = new SqlCommand("select sum(d.amount) from deposit_operation d join users u on(d.user_id =u.user_id) join main_accounts m on(d.account_id=m.account_id) where operation_id=" + textBoxX2.Text + "", con);
                                    con.Open();
                                    textBoxX1.Text = cmd.ExecuteScalar().ToString();
                                    con.Close();
                                }
                                catch
                                {
                                    MessageBox.Show("مشكلة في جمع المبالغ ", "110", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                }
                            }
                            catch
                            {
                                MessageBox.Show("مشكلة في عرض العمليات ", "111", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            }
                            break;
                        case "وصف العملية/ملاحظة":
                            try
                            {
                                SqlDataAdapter da = new SqlDataAdapter("select d.operation_id as'رقم العملية',d.amount المبلغ,d.details as 'التفاصيل',m.account_name as 'اسم الحساب',u.user_name المستخدم,d.number_edit as'عدد التعديلات',d.operation_date التاريخ from deposit_operation d join users u on(d.user_id =u.user_id) join main_accounts m on(d.account_id=m.account_id)  where d.details like N'%" + textBoxX2.Text + "%'", con);
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                dataGridViewX1.DataSource = dt;
                                try
                                {
                                    SqlCommand cmd = new SqlCommand("select sum(d.amount) from deposit_operation d join users u on(d.user_id =u.user_id) join main_accounts m on(d.account_id=m.account_id) where d.details like N'%" + textBoxX2.Text + "%'", con);
                                    con.Open();
                                    textBoxX1.Text = cmd.ExecuteScalar().ToString();
                                    con.Close();
                                }
                                catch
                                {
                                    MessageBox.Show("مشكلة في جمع المبالغ ", "112", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                }
                            }
                            catch
                            {
                                MessageBox.Show("مشكلة في عرض العمليات ", "113", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            }
                            break;
                        case "مدة":

                            try
                            {
                                SqlDataAdapter da = new SqlDataAdapter("select d.operation_id as'رقم العملية',d.amount المبلغ,d.details as 'غرض العملية',m.account_name as 'اسم الحساب',u.user_name المستخدم,d.number_edit as'عدد التعديلات',d.operation_date التاريخ from deposit_operation d join users u on(d.user_id =u.user_id) join main_accounts m on(d.account_id=m.account_id) where d.operation_date BETWEEN cast('" + dateTimePicker2.Value + "'  as datetime)AND cast('" + dateTimePicker1.Value + "' as datetime)", con);
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                dataGridViewX1.DataSource = dt;
                                try
                                {
                                    SqlCommand cmd = new SqlCommand("select sum(d.amount) from deposit_operation d join users u on(d.user_id =u.user_id) join main_accounts m on(d.account_id=m.account_id) where d.operation_date BETWEEN cast('" + dateTimePicker2.Value + "'  as datetime)AND cast('" + dateTimePicker1.Value + "' as datetime)", con);
                                    con.Open();
                                    textBoxX1.Text = cmd.ExecuteScalar().ToString();
                                    con.Close();
                                }
                                catch
                                {
                                    MessageBox.Show("مشكلة في جمع المبالغ ", "114", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                }
                            }
                            catch
                            {
                                MessageBox.Show("مشكلة في عرض العمليات ", "115", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            }

                            break;


                    }
                    break;
                case "سحب":

                    switch (comboBoxEx1.Text)
                    {
                        case "رقم العملية":
                            try
                            {
                                SqlDataAdapter da = new SqlDataAdapter("select d.operation_id as'رقم العملية',d.amount المبلغ,d.details as 'التفاصيل',m.account_name as 'اسم الحساب',u.user_name المستخدم,d.number_edit as'عدد التعديلات',d.operation_date التاريخ from pull_operation d join users u on(d.user_id =u.user_id) join main_accounts m on(d.account_id=m.account_id) where operation_id=" + textBoxX2.Text + "", con);
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                dataGridViewX1.DataSource = dt;
                                try
                                {
                                    SqlCommand cmd = new SqlCommand("select sum(d.amount) from pull_operation d join users u on(d.user_id =u.user_id) join main_accounts m on(d.account_id=m.account_id) where operation_id=" + textBoxX2.Text + "", con);
                                    con.Open();
                                    textBoxX1.Text = cmd.ExecuteScalar().ToString();
                                    con.Close();
                                }
                                catch
                                {
                                    MessageBox.Show("مشكلة في جمع المبالغ ", "110", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                }
                            }
                            catch
                            {
                                MessageBox.Show("مشكلة في عرض العمليات ", "111", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            }
                            break;
                        case "وصف العملية/ملاحظة":
                            try
                            {
                                SqlDataAdapter da = new SqlDataAdapter("select d.operation_id as'رقم العملية',d.amount المبلغ,d.details as 'التفاصيل',m.account_name as 'اسم الحساب',u.user_name المستخدم,d.number_edit as'عدد التعديلات',d.operation_date التاريخ from pull_operation d join users u on(d.user_id =u.user_id) join main_accounts m on(d.account_id=m.account_id)  where d.details like N'%" + textBoxX2.Text + "%'", con);
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                dataGridViewX1.DataSource = dt;
                                try
                                {
                                    SqlCommand cmd = new SqlCommand("select sum(d.amount) from pull_operation d join users u on(d.user_id =u.user_id) join main_accounts m on(d.account_id=m.account_id) where d.details like N'%" + textBoxX2.Text + "%'", con);
                                    con.Open();
                                    textBoxX1.Text = cmd.ExecuteScalar().ToString();
                                    con.Close();
                                }
                                catch
                                {
                                    MessageBox.Show("مشكلة في جمع المبالغ ", "112", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                }
                            }
                            catch
                            {
                                MessageBox.Show("مشكلة في عرض العمليات ", "113", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            }
                            break;
                        case "مدة":

                            try
                            {
                                SqlDataAdapter da = new SqlDataAdapter("select d.operation_id as'رقم العملية',d.amount المبلغ,d.details as 'غرض العملية',m.account_name as 'اسم الحساب',u.user_name المستخدم,d.number_edit as'عدد التعديلات',d.operation_date التاريخ from pull_operation d join users u on(d.user_id =u.user_id) join main_accounts m on(d.account_id=m.account_id) where d.operation_date BETWEEN cast('" + dateTimePicker2.Value + "'  as datetime)AND cast('" + dateTimePicker1.Value + "' as datetime)", con);
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                dataGridViewX1.DataSource = dt;
                                try
                                {
                                    SqlCommand cmd = new SqlCommand("select sum(d.amount) from pull_operation d join users u on(d.user_id =u.user_id) join main_accounts m on(d.account_id=m.account_id) where d.operation_date BETWEEN cast('" + dateTimePicker2.Value + "'  as datetime)AND cast('" + dateTimePicker1.Value + "' as datetime)", con);
                                    con.Open();
                                    textBoxX1.Text = cmd.ExecuteScalar().ToString();
                                    con.Close();
                                }
                                catch
                                {
                                    MessageBox.Show("مشكلة في جمع المبالغ ", "114", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                }
                            }
                            catch
                            {
                                MessageBox.Show("مشكلة في عرض العمليات ", "115", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            }

                            break;


                    }
                    break;
                //2m
                case "تحويل بين حسابين":
                    switch (comboBoxEx1.Text)
                    {
                        case "رقم العملية":
                            try
                            {
                                SqlDataAdapter da = new SqlDataAdapter("select e.operation_id as'رقم العملية',e.amount as'المبلغ المحول',m.account_name as'الحساب المحول',m2.account_name as'الحساب المستلم',e.details as'تفاصيل عملية التحويل',u.user_name as'اسم المستخدم',e.number_edit as 'عدد التعديلات',e.operation_date as'تاريخ العملية'  from exchange_same_currency e join main_accounts m on(e.account_id_from=m.account_id) join main_accounts m2 on(e.account_id_to=m2.account_id) join users u on(e.user_id=u.user_id) where e.operation_id=" + textBoxX2.Text + "", con);
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                dataGridViewX1.DataSource = dt;
                                try
                                {
                                    SqlCommand cmd = new SqlCommand("select sum(e.amount) from exchange_same_currency e join main_accounts m on(e.account_id_from=m.account_id) join main_accounts m2 on(e.account_id_to=m2.account_id) join users u on(e.user_id=u.user_id) where e.operation_id=" + textBoxX2.Text + "", con);
                                    con.Open();
                                    textBoxX1.Text = cmd.ExecuteScalar().ToString();
                                    con.Close();
                                }
                                catch
                                {
                                    MessageBox.Show("مشكلة في جمع المبالغ ", "116", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                }
                            }
                            catch
                            {
                                MessageBox.Show("مشكلة في عرض العمليات ", "117", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            }
                            break;
                        case "وصف العملية/ملاحظة":
                            try
                            {
                                SqlDataAdapter da = new SqlDataAdapter("select e.operation_id as'رقم العملية',e.amount as'المبلغ المحول',m.account_name as'الحساب المحول',m2.account_name as'الحساب المستلم',e.details as'تفاصيل عملية التحويل',u.user_name as'اسم المستخدم',e.number_edit as 'عدد التعديلات',e.operation_date as'تاريخ العملية'  from exchange_same_currency e join main_accounts m on(e.account_id_from=m.account_id) join main_accounts m2 on(e.account_id_to=m2.account_id) join users u on(e.user_id=u.user_id) where e.details like N'%" + textBoxX2.Text + "%'", con);
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                dataGridViewX1.DataSource = dt;
                                try
                                {
                                    SqlCommand cmd = new SqlCommand("select sum(e.amount) from exchange_same_currency e join main_accounts m on(e.account_id_from=m.account_id) join main_accounts m2 on(e.account_id_to=m2.account_id) join users u on(e.user_id=u.user_id) where e.details like N'%" + textBoxX2.Text + "%'", con);
                                    con.Open();
                                    textBoxX1.Text = cmd.ExecuteScalar().ToString();
                                    con.Close();
                                }
                                catch
                                {
                                    MessageBox.Show("مشكلة في جمع المبالغ ", "118", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                }
                            }
                            catch
                            {
                                MessageBox.Show("مشكلة في عرض العمليات ", "119", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            }
                            break;
                        case "مدة":

                            try
                            {
                                SqlDataAdapter da = new SqlDataAdapter("select e.operation_id as'رقم العملية',e.amount as'المبلغ المحول',m.account_name as'الحساب المحول',m2.account_name as'الحساب المستلم',e.details as'تفاصيل عملية التحويل',u.user_name as'اسم المستخدم',e.number_edit as 'عدد التعديلات',e.operation_date as'تاريخ العملية'  from exchange_same_currency e join main_accounts m on(e.account_id_from=m.account_id) join main_accounts m2 on(e.account_id_to=m2.account_id) join users u on(e.user_id=u.user_id) where e.operation_date BETWEEN cast('" + dateTimePicker2.Value + "'  as datetime)AND cast('" + dateTimePicker1.Value + "' as datetime)", con);
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                dataGridViewX1.DataSource = dt;
                                try
                                {
                                    SqlCommand cmd = new SqlCommand("select sum(e.amount) from exchange_same_currency e join main_accounts m on(e.account_id_from=m.account_id) join main_accounts m2 on(e.account_id_to=m2.account_id) join users u on(e.user_id=u.user_id) where e.operation_date BETWEEN cast('" + dateTimePicker2.Value + "'  as datetime)AND cast('" + dateTimePicker1.Value + "' as datetime)", con);
                                    con.Open();
                                    textBoxX1.Text = cmd.ExecuteScalar().ToString();
                                    con.Close();
                                }
                                catch
                                {
                                    MessageBox.Show("مشكلة في جمع المبالغ ", "120", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                }
                            }
                            catch
                            {
                                MessageBox.Show("مشكلة في عرض العمليات ", "121", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            }

                            break;


                    }
                    break;
                //3m
                case "مصارفة عملات":
                    switch (comboBoxEx1.Text)
                    {
                        case "رقم العملية":
                            try
                            {
                                SqlDataAdapter da = new SqlDataAdapter("select e.operation_id as'رقم العملية',e.amount as'المبلغ',c.currency_name as'العملة',e.exchange_price as'سعر الصرف',e.exchange_rate as'المبلغ المقابل',m.account_name as'الحساب المحول',m2.account_name as'الحساب المستلم',e.details as'تفاصيل عملية المصارفة',u.user_name as'اسم المستخدم',e.number_edit as 'عدد التعديلات',e.operation_date as'تاريخ العملية'  from exchange_currency e join main_accounts m on(e.account_id_from=m.account_id) join main_accounts m2 on(e.account_id_to=m2.account_id) join accounts_currency c on(m.amount_creditor=c.currency_id) join users u on(e.user_id=u.user_id) where e.operation_id=" + textBoxX2.Text + "", con);
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                dataGridViewX1.DataSource = dt;
                            }
                            catch
                            {
                                MessageBox.Show("مشكلة في عرض العمليات ", "122", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            }
                            break;
                        case "وصف العملية/ملاحظة":
                            try
                            {
                                SqlDataAdapter da = new SqlDataAdapter("select e.operation_id as'رقم العملية',e.amount as'المبلغ',e.exchange_price as'سعر الصرف',e.exchange_rate as'المبلغ المقابل',m.account_name as'الحساب المحول',m2.account_name as'الحساب المستلم',e.details as'تفاصيل عملية المصارفة',u.user_name as'اسم المستخدم',e.number_edit as 'عدد التعديلات',e.operation_date as'تاريخ العملية'  from exchange_currency e join main_accounts m on(e.account_id_from=m.account_id) join main_accounts m2 on(e.account_id_to=m2.account_id) join users u on(e.user_id=u.user_id) where e.details like N'%" + textBoxX2.Text + "%'", con);
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                dataGridViewX1.DataSource = dt;

                            }
                            catch
                            {
                                MessageBox.Show("مشكلة في عرض العمليات ", "123", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            }
                            break;
                        case "مدة":

                            try
                            {
                                SqlDataAdapter da = new SqlDataAdapter("select e.operation_id as'رقم العملية',e.amount as'المبلغ',c.currency_name as'العملة',e.exchange_price as'سعر الصرف',e.exchange_rate as'المبلغ المقابل',m.account_name as'الحساب المحول',m2.account_name as'الحساب المستلم',e.details as'وصف عملية المصارفة',u.user_name as'اسم المستخدم',e.number_edit as 'عدد التعديلات',e.operation_date as'تاريخ العملية'  from exchange_currency e join main_accounts m on(e.account_id_from=m.account_id) join main_accounts m2 on(e.account_id_to=m2.account_id) join accounts_currency c on(m.amount_creditor=c.currency_id) join users u on(e.user_id=u.user_id) where e.operation_date BETWEEN cast('" + dateTimePicker2.Value + "'  as datetime)AND cast('" + dateTimePicker1.Value + "' as datetime)", con);
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                dataGridViewX1.DataSource = dt;
                            }
                            catch
                            {
                                MessageBox.Show("مشكلة في عرض العمليات ", "124", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            }

                            break;


                    }

                    break;


            }




        }

        private void labelX1_Click(object sender, EventArgs e)
        {

        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            textBoxX1.Clear();
            textBoxX2.Clear();
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEx1.Text == "مدة")
            {
                dateTimePicker2.Enabled = true;
                dateTimePicker2.Enabled = true;
                dateTimePicker1.Enabled = true;
                textBoxX2.Enabled = false;
                textBoxX2.Clear();
            }
            else
            {
                dateTimePicker2.Enabled = false;
                dateTimePicker1.Enabled = false;
                textBoxX2.Enabled = true;
                textBoxX2.Clear();

            }
        }

        private void dataGridViewX1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridViewX1.Rows[e.RowIndex].Cells["N"].Value = (e.RowIndex + 1).ToString();
        }

        private void comboBoxEx3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBoxX2_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}