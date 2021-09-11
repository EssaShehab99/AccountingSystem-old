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
    public partial class Full_Accounts : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-OQGE058\MSSQLSERVER,1433; Initial Catalog=accountingsystem;Integrated Security=false; User ID=sa;password=Essa771838957;");
        int user_id, testclick = 0;
        public Full_Accounts(int user_id)
        {
            InitializeComponent();
            this.user_id = user_id;
        }
        private void backdata()
        {
            try
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("select isnull(amount_debit,0) as'amountdebit' from main_accounts where account_name=N'" + dataGridViewX1.SelectedRows[0].Cells[2].Value.ToString() + "'", con);
                SqlCommand cmd3 = new SqlCommand("select currency_id  from accounts_currency c join accounts_levels l on(c.level_id=l.level_id) where c.currency_name+'/' + l.level_name=N'" + dataGridViewX1.SelectedRows[0].Cells[3].Value.ToString() + "'", con);

                SqlCommand cmd2 = new SqlCommand("select isnull(c.amount_creditor,0) as'amountdebit' from accounts_currency c where currency_id=" + Convert.ToInt16(cmd3.ExecuteScalar()) + "", con);

                decimal amountbackdebit = decimal.Parse(cmd1.ExecuteScalar().ToString()) - decimal.Parse(dataGridViewX1.SelectedRows[0].Cells[1].Value.ToString());
                decimal amountbackcreditor = decimal.Parse(cmd2.ExecuteScalar().ToString()) - decimal.Parse(dataGridViewX1.SelectedRows[0].Cells[1].Value.ToString());

                SqlCommand cmd4 = new SqlCommand("update main_accounts set amount_debit=" + amountbackdebit + " where account_name=N'" + dataGridViewX1.SelectedRows[0].Cells[2].Value.ToString() + "'", con);

                SqlCommand cmd10 = new SqlCommand("update accounts_currency set amount_creditor=" + amountbackcreditor + " where currency_id=" + Convert.ToInt16(cmd3.ExecuteScalar()) + "", con);

                cmd10.ExecuteNonQuery();
                cmd4.ExecuteNonQuery();

            }
            catch
            {
                MessageBox.Show("مشكلة في إرجاع المبالغ للحسابات", "52", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            finally
            {
                con.Close();
            }
        }

        private void updateamount()
        {
            try
            {
                SqlCommand cmd1 = new SqlCommand("select m.account_id,c.currency_name,isnull(m.amount_creditor,0)-isnull(m.amount_debit,0) as'amount'  from main_accounts m join accounts_currency c on(m.currency_id=c.currency_id) where account_name=N'" + comboBoxEx.Text + "'", con);
                SqlCommand cmd2 = new SqlCommand("select c.currency_id,c.currency_name,isnull(c.amount_creditor,0)-isnull(c.amount_debit,0) as'amount'  from accounts_currency c JOIN accounts_levels l on(c.level_id=l.level_id) where c.currency_name+'/' + l.level_name=N'" + comboBoxEx1.Text + "'", con);

                con.Open();
                SqlDataReader dr1 = cmd1.ExecuteReader();
                if (dr1.Read())
                {
                    textBoxX3.Text = dr1["account_id"].ToString();
                    textBoxX2.Text = dr1["amount"].ToString();
                    textBoxX1.Text = dr1["currency_name"].ToString();
                    dr1.Close();
                }
                con.Close();
                con.Open();
                SqlDataReader dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {
                    textBoxX8.Text = dr2["currency_id"].ToString();
                    textBoxX6.Text = dr2["currency_name"].ToString();
                    textBoxX7.Text = dr2["amount"].ToString();
                    dr2.Close();
                }

            }
            catch
            {
                MessageBox.Show("مشكلة في عرض البيانات", "50", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            finally
            {
                con.Close();
            }

        }

        private void inserttodataGridView()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select f.operation_id'رقم العملية',f.amount'المبلغ',m.account_name 'الحساب المحول',l.level_name+'/'+c.currency_name 'الحساب		',f.details'التفاصيل',u.user_name'اسم المستخدم',f.operation_date'التاريخ' from full_accounts f join users u on(f.user_id=u.user_id) join main_accounts m on(f.account_id_from=m.account_id) join accounts_currency c on(f.account_id_to=c.currency_id)  join accounts_levels l on(c.level_id=l.level_id)", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridViewX1.DataSource = dt;
            }
            catch
            {
                MessageBox.Show("مشكلة في عرض العمليات السابقة", "51", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void Full_Accounts_Load(object sender, EventArgs e)
        {
            inserttodataGridView();
            try
            {
                SqlCommand cmd;
                SqlDataReader dr;
                con.Open();
                cmd = new SqlCommand("select account_name from main_accounts where account_status!=N'متوقف'", con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBoxEx.Items.Add(dr[0]).ToString();
                }
            }
            catch
            {
                MessageBox.Show("مشكلة في جلب بيانات الحسابات", "53", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            finally
            {
                con.Close();
            }
            try
            {
                SqlCommand cmd;
                SqlDataReader dr;
                con.Open();
                cmd = new SqlCommand("select l.level_name+'/' +c.currency_name 'currency_name' from accounts_currency c JOIN accounts_levels l on(c.level_id=l.level_id)", con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBoxEx1.Items.Add(dr[0]).ToString();
                }
            }
            catch
            {
                MessageBox.Show("مشكلة في جلب بيانات الحسابات", "53", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            finally
            {
                con.Close();
            }

        }

        private void comboBoxEx_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("select m.account_id,c.currency_name,isnull(m.amount_creditor,0)-isnull(m.amount_debit,0) as'amount'  from main_accounts m join accounts_currency c on(m.currency_id=c.currency_id) where account_name=N'" + comboBoxEx.Text + "'", con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    textBoxX3.Text = dr["account_id"].ToString();
                    textBoxX2.Text = dr["amount"].ToString();
                    textBoxX1.Text = dr["currency_name"].ToString();
                }
            }
            catch
            {
                MessageBox.Show("مشكلة في عرض البيانات", "54", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            finally
            {
                con.Close();
            }
        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("select c.currency_id,c.currency_name,isnull(c.amount_creditor,0)-isnull(c.amount_debit,0) as'amount'  from accounts_currency c JOIN accounts_levels l on(c.level_id=l.level_id) where  l.level_name+'/' +c.currency_name=N'" + comboBoxEx1.Text + "'", con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    textBoxX8.Text = dr["currency_id"].ToString();
                    textBoxX6.Text = dr["currency_name"].ToString();
                    textBoxX7.Text = dr["amount"].ToString();
                }
            }
            catch
            {
                MessageBox.Show("مشكلة في عرض البيانات", "54", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            finally
            {
                con.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString("dd/M/yyyy,  hh:mm:ss");
            timer1.Start();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {

            if (textBoxX1.Text == textBoxX6.Text)
            {
                try
                {
                    testclick = 0;
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("insert into full_accounts(account_id_from,amount,details,account_id_to,user_id,operation_date) values(" + textBoxX3.Text + "," + textBoxX4.Text + ",N'" + textBoxX5.Text + "'," + textBoxX8.Text + "," + user_id + ",'" + dateTimePicker1.Value + "')", con);
                    SqlCommand cmd2 = new SqlCommand("select isnull(amount_debit,0) as'amountdebit' from main_accounts where account_id=" + textBoxX3.Text + "", con);
                    SqlCommand cmd3 = new SqlCommand("select isnull(amount_creditor,0) as'amountcreditor' from accounts_currency where currency_id=" + textBoxX8.Text + "", con);


                    decimal amountdebit = Convert.ToDecimal(cmd2.ExecuteScalar()) + decimal.Parse(textBoxX4.Text);
                    decimal amountcreditor = Convert.ToDecimal(cmd3.ExecuteScalar()) + decimal.Parse(textBoxX4.Text);

                    SqlCommand cmd4 = new SqlCommand("update main_accounts set amount_debit=" + amountdebit + " where account_id=" + textBoxX3.Text + "", con);
                    SqlCommand cmd10 = new SqlCommand("update accounts_currency set amount_creditor=" + amountcreditor + " where currency_id="+textBoxX8.Text+"", con);
                    DialogResult result = MessageBox.Show("سيتم تحويل مبلغ وقدره " + textBoxX4.Text + ", من حساب: " + comboBoxEx.Text + ", إلى حساب: " + comboBoxEx1.Text + ", هل تريد الاستمرار؟", "57", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {

                        cmd1.ExecuteNonQuery();
                        cmd4.ExecuteNonQuery();
                        cmd10.ExecuteNonQuery();
                        MessageBox.Show("تمت عملية التحويل بنجاح", "58", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        inserttodataGridView();
                        con.Close();
                        updateamount();
                    }
                }
                catch
                {
                    MessageBox.Show("لم تتم عملية التحويل", "59", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {
                MessageBox.Show("غير مسموح التحويل بين حسابين من عملات مختلفة", "60", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

                
          

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

        private void buttonX2_Click(object sender, EventArgs e)
        {
            testclick = 0;
            textBoxX1.Text = "عملة الحساب";
            textBoxX2.Text = "رصيد الحساب";
            textBoxX3.Text = "رقم الحساب";
            textBoxX6.Text = "عملة الحساب";
            textBoxX7.Text = "رصيد الحساب";
            textBoxX8.Text = "رقم الحساب";
            textBoxX4.Clear();
            textBoxX5.Clear();
            comboBoxEx.Text = "";
            comboBoxEx1.Text = "";
            testclick = 0;
            timer2.Start();

        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            inserttodataGridView();
            updateamount();
            timer2.Start();
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {

        }
    }
}
