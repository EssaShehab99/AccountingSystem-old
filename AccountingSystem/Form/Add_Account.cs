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
    public partial class Add_Account : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-OQGE058\MSSQLSERVER,1433; Initial Catalog=accountingsystem;Integrated Security=false; User ID=sa;password=Essa771838957;");
        int user_id, testclick = 0;
        public Add_Account(int user_id)
        {
            InitializeComponent();
            this.user_id = user_id;
        }
        private void backamounts()
        {
            try
            {
                con.Open();                
                decimal amountcreditor, amount_debit;

                SqlCommand cmd0 = new SqlCommand("select currency_id from main_accounts where account_id=" + dataGridViewX1.SelectedRows[0].Cells[0].Value + "", con);
                SqlCommand cmd1 = new SqlCommand("select isnull(amount_creditor,0) from accounts_currency where currency_id=" + Convert.ToInt16(cmd0.ExecuteScalar()) + "", con);
                SqlCommand cmd2 = new SqlCommand("select isnull(amount_debit,0) from accounts_currency where currency_id=" + Convert.ToInt16(cmd0.ExecuteScalar()) + "", con);
                amountcreditor = Convert.ToDecimal(cmd1.ExecuteScalar()) - Convert.ToDecimal(dataGridViewX1.SelectedRows[0].Cells[6].Value.ToString());
                amount_debit = Convert.ToDecimal(cmd2.ExecuteScalar()) - Convert.ToDecimal(dataGridViewX1.SelectedRows[0].Cells[5].Value.ToString());
                SqlCommand cmd3 = new SqlCommand("update accounts_currency set amount_creditor=" + amountcreditor + " ,amount_debit=" + amount_debit + " where currency_id=" + cmd0.ExecuteScalar().ToString() + "", con);
                cmd3.ExecuteNonQuery();
           
            }
            catch
            {
                MessageBox.Show("مشكلة في إرجاع مبالغ الحسابات الرئيسة", "1", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                //message 1 -15
                SqlDataAdapter da = new SqlDataAdapter("select m.account_id'رقم الحساب' ,m.account_name'الاسم' ,l.level_name'النوع' ,c.currency_name'العملة' ,(isnull(m.amount_creditor,0)-isnull(m.amount_debit,0)) as'رصيد الحساب',isnull(m.amount_creditor,0)'دائن',isnull(m.amount_debit,0)'مدين' ,m.description 'التفاصيل',m.phone_number'رقم الهاتف',m.account_status'حالة الحساب',m.number_edit'مرات التعديل',m.account_date'التاريخ' from main_accounts m join accounts_currency c on(m.currency_id =c.currency_id) join accounts_levels l on(c.level_id =l.level_id)", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridViewX1.DataSource = dt;
            }
            catch
            {
                MessageBox.Show("مشكلة في عرض الحسابات ", "1", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
        private void Add_Account_Load(object sender, EventArgs e)
        {
            try
            {
                //message 1 -15
                con.Open();
                con.Close();
            }
            catch
            {
                MessageBox.Show("xxxxxxxxxxxxxxxxxxxxxxx ", "1", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            inserttodataGridView();
            comboBoxEx2.Items.Add("الخزينة");
            comboBoxEx2.Items.Add("عام");
            comboBoxEx2.Items.Add("موردين");
            comboBoxEx2.Items.Add("عملاء");

            comboBoxEx.Items.Add("ريال يمني");
            comboBoxEx.Items.Add("ريال سعودي");
            comboBoxEx.Items.Add("ريال قطري");
            comboBoxEx.Items.Add("درهم إماراتي");
            comboBoxEx.Items.Add("دولار");

            comboBoxEx1.Items.Add("فعال");
            comboBoxEx1.Items.Add("متوقف");

            comboBoxEx2.Text = "عام";
            comboBoxEx.Text = "ريال يمني";
            comboBoxEx1.Text = "فعال";
            textBoxX11.Text = "0";
            textBoxX12.Text = "0";
        }
        private void dataGridViewX1_MouseClick(object sender, MouseEventArgs e)
        {


            testclick = 1;
            try
            {
                textBoxX6.Text = dataGridViewX1.SelectedRows[0].Cells[1].Value.ToString();
                textBoxX5.Text = dataGridViewX1.SelectedRows[0].Cells[7].Value.ToString();
                textBoxX7.Text = dataGridViewX1.SelectedRows[0].Cells[8].Value.ToString();
                comboBoxEx2.Text = dataGridViewX1.SelectedRows[0].Cells[2].Value.ToString();
                comboBoxEx1.Text = dataGridViewX1.SelectedRows[0].Cells[9].Value.ToString();
                comboBoxEx.Text = dataGridViewX1.SelectedRows[0].Cells[3].Value.ToString();
                textBoxX11.Text = dataGridViewX1.SelectedRows[0].Cells[5].Value.ToString();
                textBoxX12.Text = dataGridViewX1.SelectedRows[0].Cells[6].Value.ToString();
                con.Close();
               // updateamount();
            }
            catch
            {
                MessageBox.Show("مشكلة في عرض بيانات العملية", "2", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            finally
            {
                con.Close();
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            textBoxX6.Text = "";
            textBoxX5.Text = "";
            textBoxX7.Text = "";
            textBoxX11.Text = "";
            textBoxX12.Text = "";

            comboBoxEx2.Text = "عام";
            comboBoxEx1.Text = "فعال";
            comboBoxEx.Text = "ريال يمني";

            textBoxX11.Text = "0";
            textBoxX12.Text = "0";
            timer2.Start();
            testclick = 0;
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            inserttodataGridView();
            testclick = 0;
            timer2.Start();
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxX11_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }

        }

        private void textBoxX7_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            int test = 0;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select account_name from main_accounts", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (textBoxX6.Text == (dr[0]).ToString())
                    {
                        MessageBox.Show("اسم الحساب موجود مسبقاً", "3", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        test = 1;
                    }
                }
            }
            catch
            {
                MessageBox.Show("مشكلة في جلب بيانات الحسابات", "4", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            finally
            {
                con.Close();
            }
            if (test == 0)
            {
                DialogResult result = MessageBox.Show("هل تريد الاستمرار في عملية الإضافة", "5", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int tester = 0;
                    if (comboBoxEx2.Text == "الخزينة" && comboBoxEx.Text == "ريال يمني")
                    {
                        tester = 28;    
                    }
                    else if (comboBoxEx2.Text == "الخزينة" && comboBoxEx.Text == "ريال سعودي")
                    {
                        tester = 29;
                    }
                    else if (comboBoxEx2.Text == "الخزينة" && comboBoxEx.Text == "ريال قطري")
                    {
                        tester = 30;
                    }
                    else if (comboBoxEx2.Text == "الخزينة" && comboBoxEx.Text == "درهم إماراتي")
                    {
                        tester = 31;
                    }
                    else if (comboBoxEx2.Text == "الخزينة" && comboBoxEx.Text == "دولار")
                    {
                        tester = 32;
                    }
                    else if (comboBoxEx2.Text == "عام" && comboBoxEx.Text == "ريال يمني")
                    {
                        tester = 1;
                    }
                    else if (comboBoxEx2.Text == "عام" && comboBoxEx.Text == "ريال سعودي")
                    {
                        tester = 2;
                    }
                    else if (comboBoxEx2.Text == "عام" && comboBoxEx.Text == "ريال قطري")
                    {
                        tester = 5;
                    }
                    else if (comboBoxEx2.Text == "عام" && comboBoxEx.Text == "درهم إماراتي")
                    {
                        tester = 6;
                    }
                    else if (comboBoxEx2.Text == "عام" && comboBoxEx.Text == "دولار")
                    {
                        tester = 7;
                    }
                    else if (comboBoxEx2.Text == "موردين" && comboBoxEx.Text == "ريال يمني")
                    {
                        tester = 17;
                    }
                    else if (comboBoxEx2.Text == "موردين" && comboBoxEx.Text == "ريال سعودي")
                    {
                        tester = 29;
                    }
                    else if (comboBoxEx2.Text == "موردين" && comboBoxEx.Text == "ريال قطري")
                    {
                        tester = 21;
                    }
                    else if (comboBoxEx2.Text == "موردين" && comboBoxEx.Text == "درهم إماراتي")
                    {
                        tester = 26;
                    }
                    else if (comboBoxEx2.Text == "موردين" && comboBoxEx.Text == "دولار")
                    {
                        tester = 21;
                    }
                    else if (comboBoxEx2.Text == "عملاء" && comboBoxEx.Text == "ريال يمني")
                    {
                        tester = 22;
                    }
                    else if (comboBoxEx2.Text == "عملاء" && comboBoxEx.Text == "ريال سعودي")
                    {
                        tester = 23;
                    }
                    else if (comboBoxEx2.Text == "عملاء" && comboBoxEx.Text == "ريال قطري")
                    {
                        tester = 24;
                    }
                    else if (comboBoxEx2.Text == "عملاء" && comboBoxEx.Text == "درهم إماراتي")
                    {
                        tester = 27;
                    }
                    else if (comboBoxEx2.Text == "عملاء" && comboBoxEx.Text == "دولار")
                    {
                        tester = 25;
                    }

                    SqlCommand cmd = new SqlCommand("insert into main_accounts(currency_id,account_name,amount_creditor,amount_debit,description,phone_number,account_date,account_status,user_id) values(" + tester + ",N'" + textBoxX6.Text + "'," + textBoxX11.Text + "," + textBoxX12.Text + ",N'" + textBoxX5.Text + "',N'" + textBoxX7.Text + "','" + dateTimePicker1.Value + "',N'" + comboBoxEx1.Text+ "',"+user_id+")", con);
                    try
                    {
                        con.Open();
                        decimal amountcreditor , amount_debit;
                        SqlCommand cmd2 = new SqlCommand("select isnull(amount_creditor,0) from accounts_currency where currency_id=" + tester + "", con);
                        SqlCommand cmd3 = new SqlCommand("select isnull(amount_debit,0) from accounts_currency where currency_id=" + tester + "", con);
                        amountcreditor = Convert.ToDecimal(cmd2.ExecuteScalar()) + Convert.ToDecimal(textBoxX12.Text);
                        amount_debit = Convert.ToDecimal(cmd3.ExecuteScalar()) + Convert.ToDecimal(textBoxX11.Text);
                        SqlCommand cmd4 = new SqlCommand("update accounts_currency set amount_creditor=" + amountcreditor + " ,amount_debit=" + amount_debit + " where currency_id=" + tester + "", con);
                        cmd.ExecuteNonQuery();                        
                        cmd4.ExecuteNonQuery();
                        MessageBox.Show("تم إضافة الحساب بنجاح", "6", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    catch
                    {
                        MessageBox.Show("لم تتم العملية بنجاح", "7", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        con.Close();
                        inserttodataGridView();
                    }
                }
            }
            timer2.Start();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            dateTimePicker1.Value = Convert.ToDateTime(DateTime.Now.ToString());
            timer2.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString("dd/M/yyyy,  hh:mm:ss");
            timer1.Start();

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            timer2.Stop();
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            if (testclick == 1)
            {
                DialogResult result = MessageBox.Show("هل تريد الاستمرار في عملية التعديل", "8", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    testclick = 0;
                    try
                    {
                                            int tester = 0;
                    if (comboBoxEx2.Text == "الخزينة" && comboBoxEx.Text == "ريال يمني")
                    {
                        tester = 28;    
                    }
                    else if (comboBoxEx2.Text == "الخزينة" && comboBoxEx.Text == "ريال سعودي")
                    {
                        tester = 29;
                    }
                    else if (comboBoxEx2.Text == "الخزينة" && comboBoxEx.Text == "ريال قطري")
                    {
                        tester = 30;
                    }
                    else if (comboBoxEx2.Text == "الخزينة" && comboBoxEx.Text == "درهم إماراتي")
                    {
                        tester = 31;
                    }
                    else if (comboBoxEx2.Text == "الخزينة" && comboBoxEx.Text == "دولار")
                    {
                        tester = 32;
                    }
                    else if (comboBoxEx2.Text == "عام" && comboBoxEx.Text == "ريال يمني")
                    {
                        tester = 1;
                    }
                    else if (comboBoxEx2.Text == "عام" && comboBoxEx.Text == "ريال سعودي")
                    {
                        tester = 2;
                    }
                    else if (comboBoxEx2.Text == "عام" && comboBoxEx.Text == "ريال قطري")
                    {
                        tester = 5;
                    }
                    else if (comboBoxEx2.Text == "عام" && comboBoxEx.Text == "درهم إماراتي")
                    {
                        tester = 6;
                    }
                    else if (comboBoxEx2.Text == "عام" && comboBoxEx.Text == "دولار")
                    {
                        tester = 7;
                    }
                    else if (comboBoxEx2.Text == "موردين" && comboBoxEx.Text == "ريال يمني")
                    {
                        tester = 17;
                    }
                    else if (comboBoxEx2.Text == "موردين" && comboBoxEx.Text == "ريال سعودي")
                    {
                        tester = 29;
                    }
                    else if (comboBoxEx2.Text == "موردين" && comboBoxEx.Text == "ريال قطري")
                    {
                        tester = 21;
                    }
                    else if (comboBoxEx2.Text == "موردين" && comboBoxEx.Text == "درهم إماراتي")
                    {
                        tester = 26;
                    }
                    else if (comboBoxEx2.Text == "موردين" && comboBoxEx.Text == "دولار")
                    {
                        tester = 21;
                    }
                    else if (comboBoxEx2.Text == "عملاء" && comboBoxEx.Text == "ريال يمني")
                    {
                        tester = 22;
                    }
                    else if (comboBoxEx2.Text == "عملاء" && comboBoxEx.Text == "ريال سعودي")
                    {
                        tester = 23;
                    }
                    else if (comboBoxEx2.Text == "عملاء" && comboBoxEx.Text == "ريال قطري")
                    {
                        tester = 24;
                    }
                    else if (comboBoxEx2.Text == "عملاء" && comboBoxEx.Text == "درهم إماراتي")
                    {
                        tester = 27;
                    }
                    else if (comboBoxEx2.Text == "عملاء" && comboBoxEx.Text == "دولار")
                    {
                        tester = 25;
                    }
                    backamounts();
                        con.Open();
                        decimal amountcreditor , amount_debit;
                        SqlCommand cmd1 = new SqlCommand("select isnull(number_edit,0) from main_accounts where account_id=" + dataGridViewX1.SelectedRows[0].Cells[0].Value + "", con);
                        SqlCommand cmd = new SqlCommand("Update main_accounts set currency_id=" + tester + ",account_name=N'" + textBoxX6.Text + "', amount_creditor=" + textBoxX11.Text + ",amount_debit=" + textBoxX12.Text + ",description=N'" + textBoxX5.Text + "',phone_number=" + textBoxX7.Text + ",account_date='" + dateTimePicker1.Value + "',account_status=N'" + comboBoxEx1.Text + "',number_edit="+(Convert.ToInt16(cmd1.ExecuteScalar())+1)+",user_id="+user_id+" where account_id=" + dataGridViewX1.SelectedRows[0].Cells[0].Value.ToString() + "", con);
                        SqlCommand cmd2 = new SqlCommand("select isnull(amount_creditor,0) from accounts_currency where currency_id=" + tester + "", con);
                        SqlCommand cmd3 = new SqlCommand("select isnull(amount_debit,0) from accounts_currency where currency_id=" + tester + "", con);
                        amountcreditor = Convert.ToDecimal(cmd2.ExecuteScalar()) + Convert.ToDecimal(textBoxX12.Text);
                        amount_debit = Convert.ToDecimal(cmd3.ExecuteScalar()) +Convert.ToDecimal(textBoxX11.Text);
                        SqlCommand cmd4 = new SqlCommand("update accounts_currency set amount_creditor=" + amountcreditor + " ,amount_debit=" + amount_debit + " where currency_id=" + tester + "", con);
                        cmd.ExecuteNonQuery();
                        cmd4.ExecuteNonQuery();
                        MessageBox.Show("تم تعديل بيانات الحساب بنجاح", "9", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        MessageBox.Show("لم تتم العملية بنجاح", "10", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("الرجاء تحديد العملية المراد تعديلها", "11", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            timer2.Start();
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            if (testclick == 1)
            {
                testclick = 0;
         
                    DialogResult result = MessageBox.Show("هل تريد الاستمرار في عملية الحذف", "12", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            backamounts();
                            con.Open();
                            SqlCommand cmd1 = new SqlCommand("delete main_accounts where account_id=" + dataGridViewX1.SelectedRows[0].Cells[0].Value + "", con);
                            cmd1.ExecuteNonQuery();
                            MessageBox.Show("تم حذف الحساب بنجاح", "13", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        catch
                        {
                            MessageBox.Show("لم تتم العملية بنجاح", "14", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("الرجاء تحديد العملية المراد حذفها", "15", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            timer2.Start();

        }

        private void comboBoxEx2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
