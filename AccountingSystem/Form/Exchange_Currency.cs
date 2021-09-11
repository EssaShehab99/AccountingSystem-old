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
    public partial class Exchange_Currency : Form
    {
        //messagw 71-91
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-OQGE058\MSSQLSERVER,1433; Initial Catalog=accountingsystem;Integrated Security=false; User ID=sa;password=Essa771838957;");
        int user_id, testclick = 0;
        public Exchange_Currency(int user_id, string user_type)
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
                SqlCommand cmd1 = new SqlCommand("select m.account_id,c.currency_name,isnull(m.amount_creditor,0)-isnull(m.amount_debit,0) as'amount'  from main_accounts m join accounts_currency c on(m.currency_id=c.currency_id) where account_name=N'" + comboBoxEx.Text + "'", con);
                SqlCommand cmd2 = new SqlCommand("select m.account_id,c.currency_name,isnull(m.amount_creditor,0)-isnull(m.amount_debit,0) as'amount'  from main_accounts m join accounts_currency c on(m.currency_id=c.currency_id) where account_name=N'" + comboBoxEx1.Text + "'", con);

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
                    textBoxX8.Text = dr2["account_id"].ToString();
                    textBoxX7.Text = dr2["amount"].ToString();
                    textBoxX6.Text = dr2["currency_name"].ToString();
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
                SqlDataAdapter da = new SqlDataAdapter("select e.operation_id as'رقم العملية',e.amount as'المبلغ',e.exchange_price as'سعر الصرف',e.exchange_rate as'المبلغ المقابل',m.account_name as'الحساب المحول',m2.account_name as'الحساب المستلم',e.details as'التفاصيل',u.user_name as'اسم المستخدم',e.number_edit as 'عدد التعديلات',e.operation_date as'تاريخ العملية'  from exchange_currency e join main_accounts m on(e.account_id_from=m.account_id) join main_accounts m2 on(e.account_id_to=m2.account_id) join users u on(e.user_id=u.user_id)", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridViewX1.DataSource = dt;
            }
            catch
            {
                MessageBox.Show("مشكلة في عرض العمليات السابقة", "51", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
        private void backdata()
        {
            try
            {

                con.Open();
                SqlCommand cmd1 = new SqlCommand("select isnull(amount_debit,0) as'amountdebit' from main_accounts where account_name=N'" + dataGridViewX1.SelectedRows[0].Cells[4].Value.ToString() + "'", con);
                SqlCommand cmd2 = new SqlCommand("select isnull(amount_creditor,0) as'amountcreditor' from main_accounts where account_name=N'" + dataGridViewX1.SelectedRows[0].Cells[5].Value.ToString() + "'", con);
                SqlCommand cmd8 = new SqlCommand("select currency_id from main_accounts where account_name=N'" + dataGridViewX1.SelectedRows[0].Cells[4].Value.ToString() + "'", con);
                SqlCommand cmd9 = new SqlCommand("select currency_id from main_accounts where account_name=N'" + dataGridViewX1.SelectedRows[0].Cells[5].Value.ToString() + "'", con);
                SqlCommand cmd6 = new SqlCommand("select isnull(amount_creditor,0) as'amountdebit' from accounts_currency where currency_id=" + Convert.ToInt16(cmd8.ExecuteScalar()) + "", con);
                SqlCommand cmd7 = new SqlCommand("select isnull(amount_debit,0) as'amountcreditor' from accounts_currency where currency_id=" + Convert.ToInt16(cmd9.ExecuteScalar()) + "", con);

                decimal amountbackdebit = decimal.Parse(cmd1.ExecuteScalar().ToString()) - decimal.Parse(dataGridViewX1.SelectedRows[0].Cells[1].Value.ToString());
                decimal amountbackcreditor = decimal.Parse(cmd2.ExecuteScalar().ToString()) - decimal.Parse(dataGridViewX1.SelectedRows[0].Cells[3].Value.ToString());
                decimal amountcreditor2 = Convert.ToDecimal(cmd6.ExecuteScalar()) - Convert.ToDecimal(dataGridViewX1.SelectedRows[0].Cells[1].Value.ToString());
                decimal amountdebit2 = Convert.ToDecimal(cmd7.ExecuteScalar()) - Convert.ToDecimal(dataGridViewX1.SelectedRows[0].Cells[3].Value.ToString());

                SqlCommand cmd3 = new SqlCommand("update main_accounts set amount_debit=" + amountbackdebit + " where account_name=N'" + dataGridViewX1.SelectedRows[0].Cells[4].Value.ToString() + "'", con);
                SqlCommand cmd4 = new SqlCommand("update main_accounts set amount_creditor=" + amountbackcreditor + " where account_name=N'" + dataGridViewX1.SelectedRows[0].Cells[5].Value.ToString() + "'", con);

                SqlCommand cmd10 = new SqlCommand("update accounts_currency set amount_creditor=" + amountcreditor2 + " where currency_id=" + Convert.ToInt16(cmd8.ExecuteScalar()) + "", con);
                SqlCommand cmd11 = new SqlCommand("update accounts_currency set amount_debit=" + amountdebit2 + " where currency_id=" + Convert.ToInt16(cmd9.ExecuteScalar()) + "", con);

                cmd10.ExecuteNonQuery();
                cmd11.ExecuteNonQuery();

                cmd3.ExecuteNonQuery();
                cmd4.ExecuteNonQuery();


            }
            catch
            {
                MessageBox.Show("مشكلة في إرجاع المبالغ للحسابات", "73", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            finally
            {
                con.Close();
            }
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            if (textBoxX1.Text != textBoxX6.Text)
            {
                if (testclick == 1)
                {
                    DialogResult result = MessageBox.Show("سيتم تعديل عملية رقم " + dataGridViewX1.SelectedRows[0].Cells[0].Value.ToString() + ", من حساب: " + comboBoxEx.Text + ", إلى حساب: " + comboBoxEx1.Text + ", هل تريد الاستمرار؟", "74", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        testclick = 0;
                        SqlCommand cmd2 = new SqlCommand("select isnull(amount_debit,0) as'amountdebit' from main_accounts where account_id=" + textBoxX3.Text + "", con);
                        SqlCommand cmd3 = new SqlCommand("select isnull(amount_creditor,0) as'amountcreditor' from main_accounts where account_id=" + textBoxX8.Text + "", con);
                        SqlCommand cmd6 = new SqlCommand("select isnull(number_edit,0) from exchange_currency where operation_id=" + dataGridViewX1.SelectedRows[0].Cells[0].Value + "", con);

                        try
                        {
                            backdata();
                            con.Open();
                            SqlCommand cmd8 = new SqlCommand("select currency_id from main_accounts where account_name=N'" + comboBoxEx.Text + "'", con);
                            SqlCommand cmd9 = new SqlCommand("select currency_id from main_accounts where account_name=N'" + comboBoxEx1.Text + "'", con);
                            SqlCommand cmd10 = new SqlCommand("select isnull(amount_creditor,0) as'amountdebit' from accounts_currency where currency_id=" + Convert.ToInt16(cmd8.ExecuteScalar()) + "", con);
                            SqlCommand cmd7 = new SqlCommand("select isnull(amount_debit,0) as'amountcreditor' from accounts_currency where currency_id=" + Convert.ToInt16(cmd9.ExecuteScalar()) + "", con);
                            SqlCommand cmd1 = new SqlCommand("update exchange_currency set account_id_from =" + textBoxX3.Text + ", amount=" + textBoxX4.Text + ",exchange_price=" + textBoxX9.Text + ",exchange_rate=" + textBoxX10.Text + ",details=N'" + textBoxX5.Text + "',account_id_to=" + textBoxX8.Text + ",user_id=" + user_id + ",number_edit=" + (Convert.ToInt16(cmd6.ExecuteScalar()) + 1) + ",operation_date='" + dateTimePicker1.Value + "' where operation_id=" + dataGridViewX1.SelectedRows[0].Cells[0].Value.ToString() + "", con);
                            decimal amountdebit = Convert.ToDecimal(cmd2.ExecuteScalar()) + Convert.ToDecimal(textBoxX4.Text);
                            decimal amountcreditor = Convert.ToDecimal(cmd3.ExecuteScalar()) + Convert.ToDecimal(textBoxX10.Text);
                            decimal amountcreditor2 = Convert.ToDecimal(cmd10.ExecuteScalar()) + decimal.Parse(textBoxX4.Text);
                            decimal amountdebit2 = Convert.ToDecimal(cmd7.ExecuteScalar()) + decimal.Parse(textBoxX10.Text);

                            SqlCommand cmd4 = new SqlCommand("update main_accounts set amount_debit=" + amountdebit + " where account_id=" + textBoxX3.Text + "", con);
                            SqlCommand cmd5 = new SqlCommand("update main_accounts set amount_creditor=" + amountcreditor + " where account_id=" + textBoxX8.Text + "", con);
                            SqlCommand cmd11 = new SqlCommand("update accounts_currency set amount_creditor=" + amountcreditor2 + " where currency_id=" + Convert.ToInt16(cmd8.ExecuteScalar()) + "", con);
                            SqlCommand cmd12 = new SqlCommand("update accounts_currency set amount_debit=" + amountdebit2 + " where currency_id=" + Convert.ToInt16(cmd9.ExecuteScalar()) + "", con);

                            cmd1.ExecuteNonQuery();
                            cmd4.ExecuteNonQuery();
                            cmd5.ExecuteNonQuery();
                            cmd11.ExecuteNonQuery();
                            cmd12.ExecuteNonQuery();
                            MessageBox.Show("تمت عملية التعديل بنجاح", "75", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            inserttodataGridView();
                            con.Close();
                            updateamount();
                        }
                        catch
                        {
                            MessageBox.Show("لم تتم عملية التعديل", "76", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("الرجاء تحديد العملية المراد تعديلها", "77", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("غير مسموح المصارفة بين حسابين من نفس العملة", "78", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void textBoxX4_TextChanged(object sender, EventArgs e)
        {
            if (textBoxX9.Text != "")
            {


                if (textBoxX4.Text == ".")
                {
                    textBoxX4.Text = "";
                }

                if (textBoxX4.Text != "")
                {
                    string x = Convert.ToString(Convert.ToDecimal(textBoxX4.Text) * Convert.ToDecimal(textBoxX9.Text));
                    textBoxX10.Text = x;

                }
            }

        }

        private void textBoxX9_TextChanged(object sender, EventArgs e)
        {
            if (textBoxX4.Text != "")
            {
                if (textBoxX9.Text == ".")
                {
                    textBoxX9.Text = "";
                }
                if (textBoxX9.Text != "")
                {
                    string x = Convert.ToString(Convert.ToDecimal(textBoxX4.Text) * Convert.ToDecimal(textBoxX9.Text));
                    textBoxX10.Text = x;
                }
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

        private void textBoxX9_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }

        }

        private void textBoxX10_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }

        }

        private void Exchange_Currency_Load(object sender, EventArgs e)
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
                    comboBoxEx1.Items.Add(dr[0]).ToString();
                }
            }
            catch
            {
                MessageBox.Show("مشكلة في جلب بيانات الحسابات", "79", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
                MessageBox.Show("مشكلة في عرض البيانات", "80", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
                SqlCommand cmd = new SqlCommand("select m.account_id,c.currency_name,isnull(m.amount_creditor,0)-isnull(m.amount_debit,0) as'amount'  from main_accounts m join accounts_currency c on(m.currency_id=c.currency_id) where account_name=N'" + comboBoxEx1.Text + "'", con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    textBoxX8.Text = dr["account_id"].ToString();
                    textBoxX7.Text = dr["amount"].ToString();
                    textBoxX6.Text = dr["currency_name"].ToString();
                }
            }
            catch
            {
                MessageBox.Show("مشكلة في عرض البيانات", "81", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            finally
            {
                con.Close();
            }
        }

        private void dataGridViewX1_MouseClick(object sender, MouseEventArgs e)
        {
            testclick = 1;
            try
            {
                textBoxX4.Text = dataGridViewX1.SelectedRows[0].Cells[1].Value.ToString();
                comboBoxEx.Text = dataGridViewX1.SelectedRows[0].Cells[4].Value.ToString();
                comboBoxEx1.Text = dataGridViewX1.SelectedRows[0].Cells[5].Value.ToString();
                textBoxX9.Text = dataGridViewX1.SelectedRows[0].Cells[2].Value.ToString();
                textBoxX10.Text = dataGridViewX1.SelectedRows[0].Cells[3].Value.ToString();
                textBoxX5.Text = dataGridViewX1.SelectedRows[0].Cells[6].Value.ToString();
                con.Close();
            }
            catch
            {
                MessageBox.Show("مشكلة في عرض بيانات العملية", "82", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
            textBoxX1.Text = "عملة الحساب";
            textBoxX2.Text = "رصيد الحساب";
            textBoxX3.Text = "رقم الحساب";
            textBoxX8.Text = "عملة الحساب";
            textBoxX7.Text = "رصيد الحساب";
            textBoxX6.Text = "رقم الحساب";
            textBoxX4.Clear();
            textBoxX5.Clear();
            textBoxX9.Clear();
            textBoxX10.Clear();
            comboBoxEx.Text = "";
            comboBoxEx1.Text = "";
            testclick = 0;
            timer2.Start();
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            dateTimePicker1.Value = Convert.ToDateTime(DateTime.Now.ToString());
            timer2.Start();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {

            try
            {
                con.Open();



                if (textBoxX1.Text != textBoxX6.Text)
                {
                    testclick = 0;
                    SqlCommand cmd1 = new SqlCommand("insert into exchange_currency(account_id_from,account_id_to,amount,exchange_price,exchange_rate,details,operation_date,user_id) values(" + textBoxX3.Text + "," + textBoxX8.Text + "," + textBoxX4.Text + "," + textBoxX9.Text + "," + textBoxX10.Text + ",N'" + textBoxX5.Text + "','" + dateTimePicker1.Value + "'," + user_id + ")", con);
                    SqlCommand cmd2 = new SqlCommand("select isnull(amount_debit,0) as'amountdebit' from main_accounts where account_id=" + textBoxX3.Text + "", con);
                    SqlCommand cmd3 = new SqlCommand("select isnull(amount_creditor,0) as'amountcreditor' from main_accounts where account_id=" + textBoxX8.Text + "", con);
                    SqlCommand cmd8 = new SqlCommand("select currency_id from main_accounts where account_name=N'" + comboBoxEx.Text + "'", con);
                    SqlCommand cmd9 = new SqlCommand("select currency_id from main_accounts where account_name=N'" + comboBoxEx1.Text + "'", con);
                    SqlCommand cmd6 = new SqlCommand("select isnull(amount_creditor,0) as'amountdebit' from accounts_currency where currency_id=" + Convert.ToInt16(cmd8.ExecuteScalar()) + "", con);
                    SqlCommand cmd7 = new SqlCommand("select isnull(amount_debit,0) as'amountcreditor' from accounts_currency where currency_id=" + Convert.ToInt16(cmd9.ExecuteScalar()) + "", con);
                    con.Close();
                    try
                    {
                        con.Open();
                        decimal amountdebit = Convert.ToDecimal(cmd2.ExecuteScalar()) + decimal.Parse(textBoxX4.Text);
                        decimal amountcreditor = Convert.ToDecimal(cmd3.ExecuteScalar()) + decimal.Parse(textBoxX10.Text);
                        decimal amountcreditor2 = Convert.ToDecimal(cmd6.ExecuteScalar()) + decimal.Parse(textBoxX4.Text);
                        decimal amountdebit2 = Convert.ToDecimal(cmd7.ExecuteScalar()) + decimal.Parse(textBoxX10.Text);

                        SqlCommand cmd4 = new SqlCommand("update main_accounts set amount_debit=" + amountdebit + " where account_id=" + textBoxX3.Text + "", con);
                        SqlCommand cmd5 = new SqlCommand("update main_accounts set amount_creditor=" + amountcreditor + " where account_id=" + textBoxX8.Text + "", con);
                        SqlCommand cmd10 = new SqlCommand("update accounts_currency set amount_creditor=" + amountcreditor2 + " where currency_id=" + Convert.ToInt16(cmd8.ExecuteScalar()) + "", con);
                        SqlCommand cmd11 = new SqlCommand("update accounts_currency set amount_debit=" + amountdebit2 + " where currency_id=" + Convert.ToInt16(cmd9.ExecuteScalar()) + "", con);

                        DialogResult result = MessageBox.Show("سيتم خصم مبلغ وقدره " + textBoxX4.Text + ", من حساب: " + comboBoxEx.Text + ", مقابل مصارفة عملات مقابل: " + textBoxX10.Text + ", إلى حساب: " + comboBoxEx1.Text + ", هل تريد الاستمرار؟", "83", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            cmd1.ExecuteNonQuery();
                            cmd4.ExecuteNonQuery();
                            cmd5.ExecuteNonQuery();
                            cmd10.ExecuteNonQuery();
                            cmd11.ExecuteNonQuery();
                            MessageBox.Show("تمت عملية المصارفة بنجاح", "84", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            inserttodataGridView();
                            con.Close();
                            updateamount();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("لم تتم عملية المصارفة", "85", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        con.Close();
                    }
                }
                else
                {
                    MessageBox.Show("غير مسموح المصارفة بين حسابين من نفس العملات", "86", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }



            }
            catch
            {
                MessageBox.Show("لم يتم تحديد حالة الحساب", "82b", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                con.Close();

            }


        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            if (textBoxX1.Text != textBoxX6.Text)
            {
                if (testclick == 1)
                {
                    DialogResult result = MessageBox.Show("سيتم حذف عملية رقم " + dataGridViewX1.SelectedRows[0].Cells[0].Value.ToString() + ", من حساب: " + comboBoxEx.Text + ", إلى حساب: " + comboBoxEx1.Text + ", هل تريد الاستمرار؟", "87", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            backdata();
                            SqlCommand cmd1 = new SqlCommand("delete from exchange_currency where operation_id=" + dataGridViewX1.SelectedRows[0].Cells[0].Value.ToString() + "", con);
                            con.Open();
                            cmd1.ExecuteNonQuery();
                            MessageBox.Show("تمت عملية الحذف بنجاح", "88", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            inserttodataGridView();
                            con.Close();
                            updateamount();

                        }
                        catch
                        {
                            MessageBox.Show("لم تتم عملية الحذف", "89", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            con.Close();
                        }
                    }

                }
                else
                {
                    MessageBox.Show("الرجاء تحديد العملية المراد تعديلها", "90", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("غير مسموح المصارفة بين حسابين من نفس العملات", "91", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            testclick = 0;

        }
    }
}