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
    public partial class Cash_Deposit : Form
    {
        //message 16-32
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-OQGE058\MSSQLSERVER,1433; Initial Catalog=accountingsystem;Integrated Security=false; User ID=sa;password=Essa771838957;");
        int user_id, testclick = 0;
        public Cash_Deposit(int user_id, string user_type)
        {
            InitializeComponent();
            this.user_id = user_id;
            if (user_type != "admin")
            {
                buttonX4.Enabled = false;
                buttonX5.Enabled = false;
                dateTimePicker1.Enabled = false;
            }
        }

        private void updateamount()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select account_id,isnull(amount_creditor,0)-isnull(amount_debit,0) as'amount' from main_accounts where account_name=N'" + comboBoxEx.Text + "'", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    textBoxX3.Text = dr["account_id"].ToString();
                    textBoxX2.Text = dr["amount"].ToString();
                    dr.Close();
                }
            }
            catch
            {
                MessageBox.Show("مشكلة في عرض البيانات", "16", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
                SqlDataAdapter da = new SqlDataAdapter("select d.operation_id as'رقم العملية',d.amount المبلغ,d.details as 'التفاصيل',m.account_name as 'اسم الحساب',u.user_name المستخدم,d.number_edit as'عدد التعديلات',d.operation_date التاريخ from deposit_operation d join users u on(d.user_id =u.user_id) join main_accounts m on(d.account_id=m.account_id) where m.account_status!=N'متوقف'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridViewX1.DataSource = dt;
            }
            catch
            {
                MessageBox.Show("مشكلة في عرض العمليات السابقة", "17", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
        private void backdata()
        {
            try
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("select isnull(amount_creditor,0) as'amountcreditor' from main_accounts where account_name=N'" + dataGridViewX1.SelectedRows[0].Cells[3].Value.ToString() + "'", con);
                decimal amountcreditor = Convert.ToDecimal(cmd1.ExecuteScalar()) - Convert.ToDecimal(dataGridViewX1.SelectedRows[0].Cells[1].Value.ToString());
                SqlCommand cmd2 = new SqlCommand("update main_accounts set amount_creditor=" + amountcreditor + "where account_name=N'" + dataGridViewX1.SelectedRows[0].Cells[3].Value.ToString() + "'", con);

                SqlCommand cmd0 = new SqlCommand("select currency_id from main_accounts where account_name=N'" + dataGridViewX1.SelectedRows[0].Cells[3].Value.ToString() + "'", con);
                SqlCommand cmd4 = new SqlCommand("select isnull(amount_debit,0) from accounts_currency where currency_id=" + Convert.ToInt16(cmd0.ExecuteScalar()) + "", con);
                decimal amountdebit = Convert.ToDecimal(cmd4.ExecuteScalar()) - Convert.ToDecimal(textBoxX4.Text);
                SqlCommand cmd3 = new SqlCommand("update accounts_currency set amount_debit=" + amountdebit + " where currency_id=" + cmd0.ExecuteScalar().ToString() + "", con);

                cmd3.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();

            }
            catch
            {
                MessageBox.Show("مشكلة في إرجاع المبلغ للحساب", "18", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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

        private void Cash_Deposit_Load(object sender, EventArgs e)
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
                MessageBox.Show("مشكلة في جلب بيانات الحسابات", "19", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
                SqlCommand cmd = new SqlCommand("select account_id ,isnull(amount_creditor,0)-isnull(amount_debit,0) as'amount' from main_accounts where account_name=N'" + comboBoxEx.Text + "'", con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    textBoxX3.Text = dr["account_id"].ToString();
                    textBoxX2.Text = dr["amount"].ToString();
                }
            }
            catch
            {
                MessageBox.Show("مشكلة في عرض البيانات", "20", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            finally
            {
                con.Close();
            }
        }

        private void textBoxX4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {

            try
            {
                con.Open();
                SqlCommand cmd3 = new SqlCommand("select account_status from main_accounts where account_id=" + textBoxX3.Text + "", con);
                if (cmd3.ExecuteScalar().ToString() == "متوقف")
                {
                    MessageBox.Show("الحساب متوقف حالياً", "20a", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    con.Close();

                    testclick = 0;
                    decimal amnt = 0.0M;
                    SqlCommand cmd = new SqlCommand("insert into deposit_operation(account_id,amount,details,user_id,operation_date)values(" + textBoxX3.Text + "," + textBoxX4.Text + ",N'" + textBoxX5.Text + "'," + user_id + ",'" + dateTimePicker1.Value + "')", con);
                    SqlCommand cmd1 = new SqlCommand("select isnull(amount_creditor,0) as'amount' from main_accounts where account_id=" + textBoxX3.Text + "", con);
                    SqlDataReader dr1;
                    try
                    {
                        con.Open();
                        dr1 = cmd1.ExecuteReader();
                        if (dr1.Read())
                        {
                            amnt = decimal.Parse(dr1.GetValue(0).ToString()) + decimal.Parse(textBoxX4.Text);
                        }
                        con.Close();
                        SqlCommand cmd2 = new SqlCommand("update main_accounts set amount_creditor=" + amnt + " where account_id=" + textBoxX3.Text + "", con);
                        DialogResult result = MessageBox.Show("سيتم إيداع مبلغ وقدره: " + textBoxX4.Text + ", إلى حساب: " + comboBoxEx.Text + ", هل تريد الاستمرار في العملية؟", "21", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            con.Open();

                            decimal amountdebit;
                            SqlCommand cmd0 = new SqlCommand("select currency_id from main_accounts where account_name=N'" + comboBoxEx.Text + "'", con);
                            SqlCommand cmd4 = new SqlCommand("select isnull(amount_debit,0) from accounts_currency where currency_id=" + Convert.ToInt16(cmd0.ExecuteScalar()) + "", con);
                            amountdebit = Convert.ToDecimal(cmd4.ExecuteScalar()) + Convert.ToDecimal(textBoxX4.Text);
                            SqlCommand cmd5 = new SqlCommand("update accounts_currency set amount_debit=" + amountdebit + " where currency_id=" + cmd0.ExecuteScalar().ToString() + "", con);
                            cmd5.ExecuteNonQuery();

                            cmd.ExecuteNonQuery();
                            cmd2.ExecuteNonQuery();
                            MessageBox.Show("تمت عملية الإيداع بنجاح", "22", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            inserttodataGridView();
                            con.Close();
                            updateamount();
                        }

                    }
                    catch
                    {
                        MessageBox.Show("لم تتم عملية الإيداع", "23", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            catch
            {
                MessageBox.Show("لم يتم تحديد حالة الحساب", "20b", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                con.Close();

            }
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            testclick = 0;
            inserttodataGridView();
            updateamount();
            timer2.Start();


        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            testclick = 0;
            textBoxX2.Text = "رصيد الحساب";
            textBoxX3.Text = "رقم الحساب";
            textBoxX4.Clear();
            textBoxX5.Clear();
            comboBoxEx.Text = "";
            testclick = 0;
            timer2.Start();

        }

        private void dataGridViewX1_MouseClick(object sender, MouseEventArgs e)
        {
            testclick = 1;
            try
            {
                textBoxX4.Text = dataGridViewX1.SelectedRows[0].Cells[1].Value.ToString();
                textBoxX5.Text = dataGridViewX1.SelectedRows[0].Cells[2].Value.ToString();
                comboBoxEx.Text = dataGridViewX1.SelectedRows[0].Cells[3].Value.ToString();
                con.Close();
                updateamount();
            }
            catch
            {
                MessageBox.Show("مشكلة في عرض بيانات العملية", "24", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            finally
            {
                con.Close();
            }
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            testclick = 0;
            this.Close();
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
                if (testclick == 1)
                {
                    DialogResult result = MessageBox.Show("عملية رقم " + dataGridViewX1.SelectedRows[0].Cells[0].Value.ToString() + " هل تريد الاستمرار في عملية الحذف؟", "25", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        backdata();
                        SqlCommand cmd1 = new SqlCommand("delete from deposit_operation where operation_id=" + dataGridViewX1.SelectedRows[0].Cells[0].Value.ToString() + "", con);
                        try
                        {
                            con.Open();
                            cmd1.ExecuteNonQuery();
                            MessageBox.Show("تمت عملية الحذف بنجاح", "26", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            inserttodataGridView();
                            con.Close();
                            updateamount();
                        }
                        catch
                        {
                            MessageBox.Show("لم تتم عملية الحذف بنجاح", "17", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            con.Close();
                            testclick = 0;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("الرجاء تحديد العملية المراد حذفها", "28", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {

                    if (testclick == 1)
                    {
                        DialogResult result = MessageBox.Show("هل تريد الاستمرار في عملية التعديل؟", "29", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            backdata();
                            decimal amountcreditor = 0.0M;
                            SqlCommand cmd1 = new SqlCommand("select isnull(amount_creditor,0) as'amountnew' from main_accounts where account_id=" + textBoxX3.Text + "", con);
                            SqlCommand cmd4 = new SqlCommand("select isnull(number_edit,0) from deposit_operation where operation_id=" + dataGridViewX1.SelectedRows[0].Cells[0].Value + "", con);
                            try
                            {
                                con.Open();
                                SqlCommand cmd2 = new SqlCommand("update deposit_operation set account_id= " + textBoxX3.Text + ",amount=" + textBoxX4.Text + ",details=N'" + textBoxX5.Text + "',operation_date='" + dateTimePicker1.Value + "',number_edit=" + (Convert.ToInt16(cmd4.ExecuteScalar()) + 1) + " where operation_id=" + dataGridViewX1.SelectedRows[0].Cells[0].Value + "", con);
                                amountcreditor = Convert.ToDecimal(cmd1.ExecuteScalar()) + Convert.ToDecimal(textBoxX4.Text);
                                SqlCommand cmd3 = new SqlCommand("update main_accounts set amount_creditor=" + amountcreditor + "where account_id=" + textBoxX3.Text + "", con);

                                SqlCommand cmd0 = new SqlCommand("select currency_id from main_accounts where account_id=N'" + textBoxX3.Text + "'", con);
                                SqlCommand cmd6 = new SqlCommand("select isnull(amount_debit,0) from accounts_currency where currency_id=" + Convert.ToInt16(cmd0.ExecuteScalar()) + "", con);
                                decimal amountdebit = Convert.ToDecimal(cmd6.ExecuteScalar()) + Convert.ToDecimal(textBoxX4.Text);
                                SqlCommand cmd5 = new SqlCommand("update accounts_currency set amount_debit=" + amountdebit + " where currency_id=" + cmd0.ExecuteScalar().ToString() + "", con);
                                cmd5.ExecuteNonQuery();


                                cmd2.ExecuteNonQuery();
                                cmd3.ExecuteNonQuery();
                                MessageBox.Show("تمت عملية التعديل بنجاح", "30", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                inserttodataGridView();
                                con.Close();
                                updateamount();
                            }
                            catch
                            {
                                MessageBox.Show("لم تتم عملية التعديل بنجاح", "31", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            finally
                            {
                                con.Close();
                                testclick = 0;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("الرجاء تحديد العملية المراد تعديلها", "32", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


    }
}