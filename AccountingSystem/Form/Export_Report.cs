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
using System.IO;

namespace AccountingSystem
{
    public partial class Export_Report : Form
    {
        //messagw 92-95
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-OQGE058\MSSQLSERVER,1433; Initial Catalog=accountingsystem;Integrated Security=false; User ID=sa;password=Essa771838957;");
        int account_id; 
        public Export_Report()
        {
            InitializeComponent();
        }

        private void Export_Report_Load(object sender, EventArgs e)
        {
            buttonX1.Enabled = false;
            buttonX2.Enabled = false;
            try
            {
                SqlCommand cmd;
                SqlDataReader dr;
                con.Open();
                cmd = new SqlCommand("select account_name from main_accounts", con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBoxEx1.Items.Add(dr[0]).ToString();
                }
            }
            catch
            {
                MessageBox.Show("مشكلة في جلب بيانات الحسابات", "92", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            finally
            {
                con.Close();
            }
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select m.account_name'الاسم' ,l.level_name'النوع' ,c.currency_name'العملة' ,(isnull(m.amount_creditor,0)-isnull(m.amount_debit,0)) as'رصيد الحساب',isnull(m.amount_creditor,0)'دائن',isnull(m.amount_debit,0)'مدين' ,m.account_status'حالة الحساب' from main_accounts m join accounts_currency c on(m.currency_id =c.currency_id) join accounts_levels l on(c.level_id =l.level_id)", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridViewX1.DataSource = dt;
            }
            catch
            {
                MessageBox.Show("مشكلة في عرض الحسابات ", "93", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {


            try
            {
                if (!checkBoxX1.Checked)
                {
                    SqlDataAdapter da = new SqlDataAdapter("(select p.amount 'مدين' ,null 'دائن',p.details'التفاصيل',p.operation_date'التاريخ' from pull_operation p where p.account_id=" + textBoxX4.Text + " union all select  null'مدين' ,d.amount 'دائن',d.details'التفاصيل',d.operation_date'التاريخ' from deposit_operation d where d.account_id=" + textBoxX4.Text + " union all select (case ex_sa1.account_id_from when " + textBoxX4.Text + " then ex_sa1.amount else null end)'مدين' ,(case ex_sa2.account_id_to when " + textBoxX4.Text + " then ex_sa2.amount else null end)'دائن',ex_sa1.details 'التفاصيل',CONVERT(VARCHAR(23), ex_sa1.operation_date, 101)'التاريخ'  from exchange_same_currency ex_sa1 join exchange_same_currency ex_sa2 on (ex_sa1.operation_id=ex_sa2.operation_id) where ex_sa1.account_id_from=" + textBoxX4.Text + " or ex_sa1.account_id_to=" + textBoxX4.Text + " union all select (case ex_cu1.account_id_from when " + textBoxX4.Text + " then ex_cu1.amount else null end)'مدين' ,(case ex_cu2.account_id_to when " + textBoxX4.Text + " then ex_cu2.exchange_rate else null end)'دائن',ex_cu1.details 'التفاصيل',CONVERT(VARCHAR(23), ex_cu1.operation_date, 101)'تاريخ العملية'  from exchange_currency ex_cu1 join exchange_currency ex_cu2 on (ex_cu1.operation_id=ex_cu2.operation_id) where ex_cu1.account_id_from=" + textBoxX4.Text + " or ex_cu1.account_id_to=" + textBoxX4.Text + ") ORDER BY التاريخ ASC", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridViewX1.DataSource = dt;

                }
                else if (checkBoxX1.Checked)
                {
                    SqlDataAdapter da = new SqlDataAdapter("(select p.amount 'مدين' ,0 'دائن',p.details'التفاصيل',p.operation_date'التاريخ' from pull_operation p where p.account_id=" + textBoxX4.Text + " and p.operation_date BETWEEN cast('" + dateTimePicker2.Value + "'  as datetime)AND cast('" + dateTimePicker1.Value + "' as datetime) union all select  null'مدين' ,d.amount 'دائن',d.details'التفاصيل',d.operation_date'التاريخ' from deposit_operation d where d.account_id=" + textBoxX4.Text + " and d.operation_date BETWEEN cast('" + dateTimePicker2.Value + "'  as datetime)AND cast('" + dateTimePicker1.Value + "' as datetime) union all select (case ex_sa1.account_id_from when " + textBoxX4.Text + " then ex_sa1.amount else null end)'مدين' ,(case ex_sa2.account_id_to when " + textBoxX4.Text + " then ex_sa2.amount else null end)'دائن',ex_sa1.details 'التفاصيل',CONVERT(VARCHAR(23), ex_sa1.operation_date, 101)'التاريخ'  from exchange_same_currency ex_sa1 join exchange_same_currency ex_sa2 on (ex_sa1.operation_id=ex_sa2.operation_id) where (ex_sa1.account_id_from=" + textBoxX4.Text + " and ex_sa1.operation_date BETWEEN cast('" + dateTimePicker2.Value + "'  as datetime)AND cast('" + dateTimePicker1.Value + "' as datetime)) or (ex_sa1.account_id_to=" + textBoxX4.Text + " and ex_sa1.operation_date BETWEEN cast('" + dateTimePicker2.Value + "'  as datetime)AND cast('" + dateTimePicker1.Value + "' as datetime)) union all select (case ex_cu1.account_id_from when " + textBoxX4.Text + " then ex_cu1.amount else null end)'مدين' ,(case ex_cu2.account_id_to when " + textBoxX4.Text + " then ex_cu2.exchange_rate else null end)'دائن',ex_cu1.details 'التفاصيل',CONVERT(VARCHAR(23), ex_cu1.operation_date, 101)'تاريخ العملية'  from exchange_currency ex_cu1 join exchange_currency ex_cu2 on (ex_cu1.operation_id=ex_cu2.operation_id) where (ex_cu1.account_id_from=" + textBoxX4.Text + " and ex_cu1.operation_date BETWEEN cast('" + dateTimePicker2.Value + "'  as datetime)AND cast('" + dateTimePicker1.Value + "' as datetime)) or (ex_cu1.account_id_to=" + textBoxX4.Text + " and ex_cu1.operation_date BETWEEN cast('" + dateTimePicker2.Value + "'  as datetime)AND cast('" + dateTimePicker1.Value + "' as datetime))) ORDER BY التاريخ ASC", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridViewX1.DataSource = dt;
                }
                buttonX2.Enabled = true;
            }
            catch
            {
                MessageBox.Show("مشكلة في عرض العمليات السابقة", "94", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }


            decimal sum1 = 0M;
            decimal sum2 = 0M;
            for (int i = 0; i < dataGridViewX1.Rows.Count; i++)
            {


                try
                {
                    sum1 += Convert.ToDecimal(dataGridViewX1.Rows[i].Cells[2].Value);
                }
                catch
                {
                }

                try
                {
                    sum2 += Convert.ToDecimal(dataGridViewX1.Rows[i].Cells[1].Value);
                }
                catch
                {
                }
            }
                textBoxX2.Text = sum1.ToString();
                textBoxX3.Text = sum2.ToString();
                textBoxX1.Text = Convert.ToString(sum1 - sum2);
                sum1 = 0;
                sum2 = 0;

        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            try
            {
                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                app.Visible = true;
                worksheet = workbook.Sheets["Sheet1"];
                worksheet = workbook.ActiveSheet;
                worksheet.Name = "Records";

                try
                {
                    for (int i = 0; i < dataGridViewX1.Columns.Count; i++)
                    {
                        worksheet.Cells[1, i + 1] = dataGridViewX1.Columns[i].HeaderText;
                    }
                    for (int i = 0; i < dataGridViewX1.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridViewX1.Columns.Count; j++)
                        {
                            if (dataGridViewX1.Rows[i].Cells[j].Value != null)
                            {
                                worksheet.Cells[i + 2, j + 1] = dataGridViewX1.Rows[i].Cells[j].Value.ToString();
                            }
                            else
                            {
                                worksheet.Cells[i + 2, j + 1] = "";
                            }
                        }
                    }

                    //Getting the location and file name of the excel to save from user. 
                    SaveFileDialog saveDialog = new SaveFileDialog();
                    saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                    saveDialog.FilterIndex = 2;

                    if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        workbook.SaveAs(saveDialog.FileName);
                        MessageBox.Show("Export Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                finally
                {
                    app.Quit();
                    workbook = null;
                    worksheet = null;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            buttonX2.Enabled = false;
        }

        private void dataGridViewX1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridViewX1.Rows[e.RowIndex].Cells["N"].Value = (e.RowIndex + 1).ToString();
        }

        private void comboBoxEx3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("select  account_id ,isnull(amount_creditor,0)-isnull(amount_debit,0) as'amount' from main_accounts where account_name=N'" + comboBoxEx1.Text + "'", con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    textBoxX4.Text = dr["account_id"].ToString();
                    textBoxX5.Text = dr["amount"].ToString();
                    buttonX1.Enabled = true;
                }
            }
            catch
            {
                MessageBox.Show("مشكلة في عرض البيانات", "37", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            finally
            {
                con.Close();
            }

        }

  

        private void checkBoxX1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBoxX1.Checked)
            {
               
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;
            }
            else
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
            }
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
            timer1.Start();
        }
    }
}
